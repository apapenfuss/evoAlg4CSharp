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
			win.Title = "Evolutionary Algorithms";
			win.Show ();
			Evolution e = new Evolution();
			List<int> list = new List<int>() {1,2,3,4,5,6,7,8};
			e.Mutate(list);
			Application.Run ();
		}
	}
}
