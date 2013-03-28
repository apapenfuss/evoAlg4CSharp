using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	public static class Evolution
	{
		/// <summary>
		/// Läst ein Genom zufällig mutieren
		/// </summary>
		/// <remarks>Vertauscht Zwei zufällige Einträge oder Invertiert einen zufälligen Bereich</remarks>
		/// <param name="genome">Das Genome, was invertiert werden soll</param>
		public static List<int> Mutate(List<int> genome) 
		{
			Console.WriteLine("Mutate:");
			Console.WriteLine(string.Format("\tGenom: {0}", ListToString(genome)));
			Console.WriteLine();

			Random rnd = new Random();
			bool swap = Convert.ToBoolean(rnd.Next(2));
			
			int z1 = 0;
			int z2 = 0;
			int tmp = 0;
			int length = genome.Count;
			bool equal = true;

			//Zwei verschiedene Zufallsindices ermitteln
			while (equal)
			{
				//Von Index 1 an da sich der erste Wert (index 0) nicht ändern soll
				z1 = rnd.Next(1, length);
				z2 = rnd.Next(1, length);
				if (z1 != z2 )
					equal = false;
			}
			Console.WriteLine(string.Format("\t\tZufallsindices: {0} und {1}",z1,z2));

			// Wenn true, vertausche, sonst, invertiere
			if (swap) 
			{
				Console.WriteLine("\t\tVertausche...");
				tmp = genome[z1];
				genome[z1] = genome[z2];
				genome[z2] = tmp;
			}
			else
			{
				Console.WriteLine("\t\tInvertiere...");
				tmp = z1 < z2 ? z1 : z2;
				genome.Reverse(tmp, Math.Abs(z1-z2)+1);
			}
			Console.WriteLine();
			Console.WriteLine(string.Format("Mutiertes Genom: {0}", ListToString(genome)));
			return genome;
		}
		
		/// <summary>
		/// Rekombiniert die Genome A und B
		/// </summary>
		/// <returns>Das Kind-Genom</returns>
		/// <param name="genomeA">Genome A</param>
		/// <param name="genomeB">Genome B</param>
		public static List<int> Recombine(List<int> genomeA, List<int> genomeB)
		{
			//Fehler, wenn Genome null
			if (genomeA == null || genomeB == null) {
				throw new ArgumentNullException();
			}
			//Fehler, wenn Längen der Genome unterschiedlich
			//todo: eventuell doch möglich?
			if (genomeA.Count != genomeB.Count) {
				throw new ArgumentException("Cant recombine genomes with a different count of values.");
			}
			
			Console.WriteLine(string.Format("Recombine:"));
			Console.WriteLine(string.Format("\tGenomeA: {0}", ListToString(genomeA)));
			Console.WriteLine(string.Format("\tGenomeB: {0}", ListToString(genomeB)));
			Console.WriteLine();
			
			//Kinder-List mit Startallel initialisieren
			List<int> childs = new List<int>();
			childs.Add(genomeA[0]);
			
			//Nachbarn für alle Allele ermitteln
			Console.WriteLine(string.Format("\tErmittle alle Nachbarn..."));
			Console.WriteLine();
			Dictionary<int, List<int>> neighbours = new Dictionary<int, List<int>>();
			foreach (int k in genomeA)
			{
				neighbours.Add(k, GetNeighboursOfValue(k, genomeA,genomeB));
				Console.WriteLine(string.Format("\t\tAllel: {0}, Nachbarn: {1}", k, ListToString(neighbours[k])));
			}
			Console.WriteLine();
			
			//Kind ermitteln
			Console.WriteLine(string.Format("\tErmittle Kind-Genom..."));
			Console.WriteLine();
			int tempAllel = 0;
			//Alle Allele durchgehen
			foreach (int j in genomeA)
			{
				//Falls letztes Allel -> nicht behandeln da Endpunkt
				if (genomeA.IndexOf(j) == genomeA.Count -1)
					break;
				
				int minLength = int.MaxValue;
				Console.WriteLine(string.Format("\t\tErmittle eine der kleinsten Nachbarmengen für Allel: {0}", j));
				foreach (int i in neighbours[j])
				{
					List<int> subNeighbours = neighbours[i].ToList();
					subNeighbours.RemoveAll(s => childs.Contains(s));
					//Immer wenn eine kürzere Nachbarmenge gefunden wurde -> das zugehörige Allel merken, insofern noch nicht im Kind vorhanden
					if (subNeighbours.Count < minLength && !childs.Contains(i)) {
						tempAllel = i;
						//Neue kürzeste Länge merken die es zu unterbieten gilt
						minLength = subNeighbours.Count;
					}
				}
				Console.WriteLine(string.Format("\t\tErgebnis: {0}", tempAllel));
				//Ermitteltes Allel zum Kind hinzufügen
				childs.Add(tempAllel);
			}
			Console.WriteLine();
			Console.WriteLine(string.Format("Kindgenom: {0}", ListToString(childs)));
			return childs;
		}
		
		/// <summary>
		/// Ermittelt alle Nachbarn aus zwei Genomen für ein bestimmtes Allel
		/// </summary>
		/// <returns>Liste mit Nachbarn</returns>
		/// <param name="value">Allel</param>
		/// <param name="genomeA">Genome A</param>
		/// <param name="genomeB">Genome B</param>
		private static List<int> GetNeighboursOfValue (int value, List<int> genomeA, List<int> genomeB)
		{
			//todo: evtl. einfach an den Anfang und am Ende der Liste den jeweiligen Nachbarn einfügen,
			//spart die zusätzlichen if-Anweisungen

			//Kreisgenom simulieren
			List<int> a = genomeA.ToList();
			a.Insert(0,genomeA[genomeA.Count-1]);
			a.Add(genomeA[0]);
			List<int> b = genomeB.ToList();
			b.Insert(0,genomeB[genomeB.Count-1]);
			b.Add(genomeB[0]);

			//Nachbarn
			List<int> neighbours = new List<int>();
			neighbours.Add(a[genomeA.IndexOf(value)]);
			neighbours.Add(a[genomeA.IndexOf(value)+2]);
			neighbours.Add(b[genomeB.IndexOf(value)]);
			neighbours.Add(b[genomeB.IndexOf(value)+2]);

//			if (genomeA.IndexOf(value) == 0) {
//				neighbours.Add(genomeA[genomeA.Count-1]);
//				neighbours.Add(genomeA[1]);
//			}
//			else if (genomeA.IndexOf(value) == genomeA.Count -1)
//			{
//				neighbours.Add(genomeA[genomeA.Count-2]);
//				neighbours.Add(genomeA[0]);
//			}
//			else
//			{
//				neighbours.Add(genomeA[genomeA.IndexOf(value)-1]);
//				neighbours.Add(genomeA[genomeA.IndexOf(value)+1]);
//			}
//			
//			//Nachbarn aus Liste B
//			if (genomeB.IndexOf(value) == 0) {
//				neighbours.Add(genomeB[genomeB.Count-1]);
//				neighbours.Add(genomeB[1]);
//			}
//			else if (genomeB.IndexOf(value) == genomeB.Count -1)
//			{
//				neighbours.Add(genomeB[genomeB.Count-2]);
//				neighbours.Add(genomeB[0]);
//			}
//			else
//			{
//				neighbours.Add(genomeB[genomeB.IndexOf(value)-1]);
//				neighbours.Add(genomeB[genomeB.IndexOf(value)+1]);
//			}
			//Doppelte Einträge entfernen und ab damit
			return neighbours.Distinct().ToList();
		}
		
		/// <summary>
		/// Convertiert den Inhalt einer Liste in einen String.
		/// </summary>
		/// <returns>Inhalt als String</returns>
		/// <param name="list">Zu konvertierende Liste</param>
		private static string ListToString(List<int> list)
		{
			string tmp = "{";
			string sep = string.Empty;
			foreach (int i in list) {
				tmp = string.Format("{0}{1} {2}", tmp, sep, i);
				sep = ",";
			}
			tmp = string.Format("{0} }}", tmp);
			return tmp;
		}
	}
}

