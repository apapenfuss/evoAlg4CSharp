using System;
using Gtk;
using System.Collections.Generic;
using main;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnBtnStartClicked (object sender, EventArgs e)
	{
		List<int> a = new List<int>() {1,2,3,4,8,5,6,7};
		List<int> b = new List<int>() {1,4,8,6,5,7,2,3};
		
		a = Evolution.Mutate(a);
		List<int> c = Evolution.Recombine(a,b);
	}
}
