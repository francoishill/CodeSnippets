namespace CodeSnippets
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.labelActiveApplication = new System.Windows.Forms.Label();
			this.contextMenuStripNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.linkLabelAddSnippet = new System.Windows.Forms.LinkLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.contextMenuStripNodes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.Indent = 5;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.ShowLines = false;
			this.treeView1.ShowPlusMinus = false;
			this.treeView1.ShowRootLines = false;
			this.treeView1.Size = new System.Drawing.Size(93, 288);
			this.treeView1.TabIndex = 0;
			this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
			this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
			this.treeView1.DragLeave += new System.EventHandler(this.treeView1_DragLeave);
			this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
			this.treeView1.MouseLeave += new System.EventHandler(this.treeView1_MouseLeave);
			this.treeView1.MouseHover += new System.EventHandler(this.treeView1_MouseHover);
			this.treeView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseMove);
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
			// 
			// labelActiveApplication
			// 
			this.labelActiveApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelActiveApplication.AutoSize = true;
			this.labelActiveApplication.Location = new System.Drawing.Point(2, 312);
			this.labelActiveApplication.Name = "labelActiveApplication";
			this.labelActiveApplication.Size = new System.Drawing.Size(91, 13);
			this.labelActiveApplication.TabIndex = 1;
			this.labelActiveApplication.Text = "Active application";
			// 
			// contextMenuStripNodes
			// 
			this.contextMenuStripNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
			this.contextMenuStripNodes.Name = "contextMenuStrip1";
			this.contextMenuStripNodes.Size = new System.Drawing.Size(118, 26);
			this.contextMenuStripNodes.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripNodes_Closed);
			this.contextMenuStripNodes.Opened += new System.EventHandler(this.contextMenuStripNodes_Opened);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.removeToolStripMenuItem.Text = "Remo&ve";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// linkLabelAddSnippet
			// 
			this.linkLabelAddSnippet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelAddSnippet.AutoSize = true;
			this.linkLabelAddSnippet.Location = new System.Drawing.Point(265, 312);
			this.linkLabelAddSnippet.Name = "linkLabelAddSnippet";
			this.linkLabelAddSnippet.Size = new System.Drawing.Size(26, 13);
			this.linkLabelAddSnippet.TabIndex = 2;
			this.linkLabelAddSnippet.TabStop = true;
			this.linkLabelAddSnippet.Text = "Add";
			this.linkLabelAddSnippet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAddSnippet_LinkClicked);
			// 
			// tabControl1
			// 
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(183, 288);
			this.tabControl1.TabIndex = 3;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(8, 9);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(280, 288);
			this.splitContainer1.SplitterDistance = 93;
			this.splitContainer1.TabIndex = 4;
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 330);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.linkLabelAddSnippet);
			this.Controls.Add(this.labelActiveApplication);
			this.Name = "Form1";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Code snippets";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.contextMenuStripNodes.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label labelActiveApplication;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripNodes;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.LinkLabel linkLabelAddSnippet;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.SplitContainer splitContainer1;


	}
}

