using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace CodeSnippets
{
	public partial class Form1 : Form
	{
		readonly TimeSpan checkForegroundInterval = TimeSpan.FromMilliseconds(500);
		readonly string cCodeSnippetDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).TrimEnd('\\') + "\\FJH\\CodeSnippets";
		const string cSnippetExtension = ".fsnip";

		CodePreview codePreview;
		bool hasTwoOrMoreScreens = false;
		List<CodeSnippet> allSnippets = new List<CodeSnippet>();

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll", SetLastError = true)]
		static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		public Form1()
		{
			InitializeComponent();

			PopulateSnippets();

			codePreview = new CodePreview();

			if (Screen.AllScreens.Length > 1)
				hasTwoOrMoreScreens = true;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			StylingInterop.SetTreeviewVistaStyle(treeView1);
			this.Location = new Point(WorkingArea.Right - this.Width, 0);
			this.Height = WorkingArea.Height;
			StartForegroundCheckTimer();
		}

		private void PopulateSnippets()//ApplicationTypes apptype = ApplicationTypes.CSharp)
		{
			allSnippets.Clear();
			treeView1.Nodes.Clear();
			treeView1.SelectedNode = null;

			if (!Directory.Exists(cCodeSnippetDir))
				Directory.CreateDirectory(cCodeSnippetDir);
			foreach (string snipFile in Directory.GetFiles(cCodeSnippetDir, "*" + cSnippetExtension, SearchOption.TopDirectoryOnly))
			{
				string fileContents = File.ReadAllText(snipFile);
				if (string.IsNullOrWhiteSpace(fileContents))
				{
					UserMessages.ShowErrorMessage("File content is incorrect format, file " + snipFile + ", content: " + Environment.NewLine + fileContents);
					continue;
				}

				int newLinePos = -1;
				int newLineLength = 0;

				if (newLinePos == -1)
				{
					newLinePos = fileContents.IndexOf("\r\n");
					newLineLength = 2;
				}
				if (newLinePos == -1)
				{
					newLinePos = fileContents.IndexOf("\n\r");
					newLineLength = 2;
				}
				if (newLinePos == -1)
				{
					newLinePos = fileContents.IndexOf("\n");
					newLineLength = 1;
				}

				if (newLinePos == -1)
				{
					UserMessages.ShowErrorMessage("File content is incorrect format, file " + snipFile + ", content: " + Environment.NewLine + fileContents);
					continue;
				}

				string firstLine = fileContents.Substring(0, newLinePos);
				string[] ApptypeAndDisplayname = firstLine.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				if (ApptypeAndDisplayname.Length != 2)
				{
					UserMessages.ShowErrorMessage("File first line is incorrect format, file " + snipFile + ", first line: \"" + firstLine + "\"");
					continue;
				}

				string snippetContent = fileContents.Substring(newLinePos + newLineLength);
				if (string.IsNullOrWhiteSpace(snippetContent))
					UserMessages.ShowWarningMessage("Snippet content is empty, full file contents for file " + snipFile + ": " + Environment.NewLine + fileContents);

				ApplicationTypes newapptype;
				if (!Enum.TryParse(ApptypeAndDisplayname[0], true, out newapptype))
				{
					UserMessages.ShowErrorMessage("Application type on first line cannot be parsed, file " + snipFile + ", application type string: \"" + ApptypeAndDisplayname[0] + "\"");
					continue;
				}
				allSnippets.Add(new CodeSnippet(newapptype, ApptypeAndDisplayname[1], snippetContent));
			}

			//            allSnippets.Add(new CodeSnippet(ApplicationTypes.CSharp, "Test1csharp",
			//@"using System.Text;
			//using System.Windows.Forms;"));
			currentApplicationType = ApplicationTypes.None;//apptype;
			SetFilterOnApplicationList(currentApplicationType);
		}

		Timer timerForegroundCheck;
		private void StartForegroundCheckTimer()
		{
			if (timerForegroundCheck == null)
				timerForegroundCheck = new Timer();
			timerForegroundCheck.Interval = (int)checkForegroundInterval.TotalMilliseconds;
			timerForegroundCheck.Tick += delegate
			{
				IntPtr h = GetForegroundWindow();
				uint processId;
				uint threadId = GetWindowThreadProcessId(h, out processId);
				try
				{
					Process proc = Process.GetProcessById((int)processId);
					if (proc == null) return;
					if (proc.Id == Process.GetCurrentProcess().Id) return;

					if (proc.MainModule.FileName.EndsWith("bds.exe", StringComparison.InvariantCultureIgnoreCase))
						SetFilterOnApplicationList(ApplicationTypes.Delphi);
					else if (proc.MainModule.FileName.EndsWith("vcsexpress.exe", StringComparison.InvariantCultureIgnoreCase)
						|| proc.MainModule.FileName.EndsWith("devenv.exe", StringComparison.InvariantCultureIgnoreCase))
						SetFilterOnApplicationList(ApplicationTypes.CSharp);
					else
						SetFilterOnApplicationList(ApplicationTypes.None);
				}
				catch { }//Must maybe show exception message??
			};
			timerForegroundCheck.Start();
		}

		private ApplicationTypes currentApplicationType = ApplicationTypes.None;
		private void SetFilterOnApplicationList(ApplicationTypes applicationType)
		{
			if (applicationType == currentApplicationType) return;

			if (BusyDragging) return;
			if (MouseButtons == System.Windows.Forms.MouseButtons.Right || isContextMenuOpen) return;

			treeView1.Nodes.Clear();
			var filteredList = allSnippets.Where(snip => snip.ApplicationType == applicationType);
			foreach (CodeSnippet snippet in filteredList)
				AddTreeNode(snippet);
			labelActiveApplication.Text = "Active application: " + applicationType.ToString();
			currentApplicationType = applicationType;
		}

		Rectangle workingArea = new Rectangle(-1, -1, -1, -1);
		private Rectangle WorkingArea
		{
			get
			{
				if (workingArea.Height != -1)
					return workingArea;
				workingArea = Screen.PrimaryScreen.WorkingArea;
				if (hasTwoOrMoreScreens)
					workingArea = Screen.AllScreens[1].WorkingArea;
				return workingArea;
			}
		}

		private void AddTreeNode(CodeSnippet snippet)
		{
			TreeNode node = treeView1.Nodes.Add(snippet.DisplayName, snippet.DisplayName);
			node.ContextMenuStrip = contextMenuStripNodes;
			node.Tag = snippet;
		}

		private void treeView1_MouseHover(object sender, EventArgs e)
		{
			Point hoverLocation = treeView1.PointToClient(MousePosition);
			TreeNode node = treeView1.HitTest(hoverLocation).Node;//e.Location).Node;
			if (node == null)
			{
				if (codePreview.Visible)
				{
					//if (BusyDragging)
					//    MarkToHideOnMouseUp = true;
					//else
					HideCodepreview();
				}
				return;
			}

			if (treeView1.SelectedNode != node)
				treeView1.SelectedNode = node;
			CodeSnippet snippet = node.Tag as CodeSnippet;
			if (snippet == null) return;
			Point pointToClient = this.PointToClient(new Point(this.Left, this.Top));
			//codePreview.Location = new Point(this.Left - codePreview.Width, e.Location.Y - pointToClient.Y);
			codePreview.Location = new Point(this.Left - codePreview.Width, hoverLocation.Y - pointToClient.Y);
			if (!codePreview.Visible)
				AttemptCodepreviewShow();
		}

		//private DateTime timeFirstShowRequest = DateTime.MaxValue;
		//private bool MarkToHideOnMouseUp = false;
		private void treeView1_MouseMove(object sender, MouseEventArgs e)
		{
			
		}

		private void AttemptCodepreviewShow()
		{
			//if (timeFirstShowRequest == DateTime.MaxValue)
			//{
			//    timeFirstShowRequest = DateTime.Now;
			//    return;
			//}

			//if (DateTime.Now.Subtract(timeFirstShowRequest).TotalMilliseconds > 500)
			//{
				codePreview.Show();
			//}
		}

		private void treeView1_MouseLeave(object sender, EventArgs e)
		{
			treeView1.SelectedNode = null;
			if (codePreview.Visible)
				HideCodepreview();
		}

		private void HideCodepreview()
		{
			//timeFirstShowRequest = DateTime.MaxValue;
			codePreview.Hide();
		}

		private bool BusyDragging = false;
		private void treeView1_MouseDown(object sender, MouseEventArgs e)
		{
			// Get the tree.
			TreeView tree = (TreeView)sender;

			// Get the node underneath the mouse.
			TreeNode node = tree.GetNodeAt(e.X, e.Y);
			tree.SelectedNode = node;

			// Start the drag-and-drop operation with a cloned copy of the node.
			if (node == null) return;

			CodeSnippet snippet = node.Tag as CodeSnippet;
			if (snippet == null) return;

			contextMenuStripNodes.Tag = snippet;

			if (e.Button != System.Windows.Forms.MouseButtons.Left)
				return;

			BusyDragging = true;
			DataObject data = new DataObject(DataFormats.Text, snippet.Code);
			tree.DoDragDrop(data, DragDropEffects.Copy);
		}

		private void treeView1_MouseUp(object sender, MouseEventArgs e)
		{
			//This code in drag leave timer
			//BusyDragging = false;
			//MarkToHideOnMouseUp = false;
		}

		private void treeView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Text))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private string newLine = Environment.NewLine;
		private void treeView1_DragDrop(object sender, DragEventArgs e)
		{
			ApplicationTypes appType = currentApplicationType;
			string droppedText = e.Data.GetData(DataFormats.Text, false).ToString();
			string inputName = SharedClasses.DialogBoxStuff.InputDialog(
				"Give a name for this snippet for " + currentApplicationType.ToString() + ", snippet text:" + newLine + newLine + droppedText,
				"Snippet name",
				this,
				300,
				180);
			if (inputName != null && inputName != "")
				allSnippets.Add(new CodeSnippet(appType, inputName, droppedText));
			SetFilterOnApplicationList(appType);
		}

		private void treeView1_DragLeave(object sender, EventArgs e)
		{
			//ThreadingInterop.PerformVoidFunctionSeperateThread(() =>
			//{
			Timer t = new Timer();
			t.Interval = 500;
			t.Tick += (s, ev) =>
			{
				if (!MouseButtons.HasFlag(MouseButtons.Left))
				{
					Timer thisTimer = s as Timer;
					thisTimer.Stop();
					thisTimer.Dispose(); thisTimer = null;
					//this.Invoke((Action)delegate
					//{
					BusyDragging = false;
					SetFilterOnApplicationList(currentApplicationType);
					//});
				}
			};
			t.Start();
			//while (MouseButtons.HasFlag(MouseButtons.Left))
			//{ }// Application.DoEvents();
			//this.Invoke((Action)delegate { SetFilterOnApplicationList(currentApplicationType); });
			//});
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//ToolStripMenuItem tsi = sender as ToolStripMenuItem;
			//if (tsi == null) return;
			//CodeSnippet snip = tsi.Tag as CodeSnippet;
			CodeSnippet snip = contextMenuStripNodes.Tag as CodeSnippet;
			if (snip == null) return;
			UserMessages.ShowInfoMessage("Removing item " + snip.DisplayName);
		}

		private bool isContextMenuOpen = false;
		private void contextMenuStripNodes_Opened(object sender, EventArgs e)
		{
			isContextMenuOpen = true;
		}

		private void contextMenuStripNodes_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			isContextMenuOpen = false;
		}

		private void linkLabelAddSnippet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ApplicationTypes appType = currentApplicationType;
			//string droppedText = e.Data.GetData(DataFormats.Text, false).ToString();
			string droppedText = SharedClasses.DialogBoxStuff.InputDialog(
				"Paste the text here to store as snippet for " + currentApplicationType.ToString(),
				"New snippet content",
				this);
			string inputName = SharedClasses.DialogBoxStuff.InputDialog(
				"Give a name for this snippet for " + currentApplicationType.ToString() + ", snippet text:" + newLine + newLine + droppedText,
				"Snippet name",
				this,
				300,
				180);
			if (inputName != null && inputName != "")
				allSnippets.Add(new CodeSnippet(appType, inputName, droppedText));
			SetFilterOnApplicationList(appType);
		}
	}

	public enum ApplicationTypes { None, CSharp, Delphi }
	public class CodeSnippet
	{
		public ApplicationTypes ApplicationType;
		public string DisplayName;
		public string Code;
		public CodeSnippet(ApplicationTypes ApplicationType, string DisplayName, string Code)
		{
			this.ApplicationType = ApplicationType;
			this.DisplayName = DisplayName;
			this.Code = Code;
		}
		public override string ToString()
		{
			return DisplayName;
		}
	}
}
