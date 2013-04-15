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
		/*
		List<int> a = new List<int>() {1,2,3,4,8,5,6,7};
		List<int> b = new List<int>() {1,4,8,6,5,7,2,3};
		
		a = Evolution.Mutate(a);
		List<int> c = Evolution.Recombine(a,b);
		
		// für Fehlersuche: x sollte { 1, 3, 8, 4, 4, 2, 6, 7 } sein
		Genome z = new Genome(new int[]{1,5,8,4,3,2,6,7});
		Genome y = new Genome(new int[]{1,4,8,6,5,7,2,3});
		Genome x = Evolution.Recombine(z,y);
		
		// Beispiel aus der Vorlesung
		Genome z = new Genome(new int[]{1,2,3,4,8,5,6,7});
		Genome y = new Genome(new int[]{1,4,8,6,5,7,2,3});
		Genome x = Evolution.Recombine(z,y);
		
		//List<int> d = Genome.GetNewGenome(8);
		
		Console.WriteLine("Create new Population:");
		Population p0 = new Population(100, 8);
		*/
		bool? mutate = null;
		
		mutate = rb_Rnd.Active ? (bool?)null : (rb_Change.Active ? false : true);
		
		Evolution evol = new Evolution();
		
		evol.countGene = (int)txt_countGenes.Value;
		evol.maxGenerations = (int)txt_maxGeneration.Value;
		evol.countIndividuals = (int)txt_countIndividuals.Value;
		evol.countChilds = (int)txt_countChilds.Value;
		evol.recombinationProbability = txt_recombProb.Value;
		evol.SelPropType = rb_Fitness.Active ? main.Selection.SelPropType.Fitness : main.Selection.SelPropType.Ranking;
		evol.SelType = (main.Selection.SelType)cbo_SelType.Active;
		evol.TournamentMemberCount = (int)txt_TournamentMemberCount.Value;
		
		evol.mutate =  mutate;
		evol.output = txt_Output;
		
		
		evol.Compute();
	}

	protected void OnBtnStartActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}

	protected void OnCboSelTypeChanged (object sender, System.EventArgs e)
	{
		txt_TournamentMemberCount.Sensitive = cbo_SelType.Active > 0;
	}

	
}
