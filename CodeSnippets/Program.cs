using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace CodeSnippets
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static)
				.SetValue(null, new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CodeSnippets.app.ico")));
			Application.Run(new Form1());
		}
	}
}
