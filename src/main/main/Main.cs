using System;
using Gtk;
using System.Collections.Generic;

namespace main
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Title = "Evolutionary Algorithms for C#";
			win.Show ();
			Application.Run ();
		}
	}
}
