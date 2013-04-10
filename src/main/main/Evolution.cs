using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	public static class Evolution
	{
		/// <summary>
		/// Mutiert ein Genom
		/// </summary>
		/// <param name="genome">Das Genome, was mutiert werden soll</param>
		/// <param name="invert">Optinal, null = Zufällige Methode, true = invertieren, false = tauschen</param>
		public static void Mutate (Genome genome, bool? invert ) 
		{
			
//			Console.WriteLine("Mutate:");
			/*
			Console.WriteLine(string.Format("\tGenom: {0}", ListToString(genome)));
			Console.WriteLine();
			 */
			Random rnd = new Random();
			if (!invert.HasValue)
				invert = Convert.ToBoolean(rnd.Next(2));
			
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
			//Console.WriteLine(string.Format("\t\tZufallsindices: {0} und {1}",z1,z2));

			// Wenn true, vertausche, sonst, invertiere
			if (!invert.Value) 
			{
				//Console.WriteLine("\t\tVertausche...");
				tmp = genome[z1];
				genome[z1] = genome[z2];
				genome[z2] = tmp;
			}
			else
			{
				//Console.WriteLine("\t\tInvertiere...");
				tmp = z1 < z2 ? z1 : z2;
				genome.Reverse(tmp, Math.Abs(z1-z2)+1);
			}
			/*
			Console.WriteLine();
			Console.WriteLine(string.Format("Mutiertes Genom: {0}", ListToString(genome)));
			*/
		}
		
		/// <summary>
		/// Rekombiniert die Genome A und B
		/// </summary>
		/// <returns>Das Kind-Genom</returns>
		/// <param name="genomeA">Genome A</param>
		/// <param name="genomeB">Genome B</param>
		public static Genome Recombine (Genome genomeA, Genome genomeB)
		{
			//Fehler, wenn Genome null
			if (genomeA == null || genomeB == null) {
				throw new ArgumentNullException();
			}
			//Fehler, wenn Längen der Genome unterschiedlich
			if (genomeA.Count != genomeB.Count) {
				throw new ArgumentException("Cant recombine genomes with a different count of values.");
			}

//			Console.WriteLine(string.Format("Recombine:"));
//			Console.WriteLine(string.Format("\tGenomeA: {0}", genomeA.AsString()));
//			Console.WriteLine(string.Format("\tGenomeB: {0}", genomeB.AsString()));
//			Console.WriteLine();

			//Kinder-List mit Startallel initialisieren
			Genome childs = new Genome();
			childs.Add(genomeA[0]);
			
			//Nachbarn für alle Allele ermitteln

//			Console.WriteLine(string.Format("\tErmittle alle Nachbarn..."));
//			Console.WriteLine();

			Dictionary<int, Genome> neighbours = new Dictionary<int, Genome>();
			foreach (int gene in genomeA)
			{
				neighbours.Add(gene, GetNeighboursOfValue(gene, genomeA,genomeB));
//				Console.WriteLine(string.Format("\t\tAllel: {0}, Nachbarn: {1}", gene, Helper.ListToString(neighbours[gene])));
			}
			//Console.WriteLine();
			
			//Kind ermitteln
//			Console.WriteLine(string.Format("\tErmittle Kind-Genom..."));
//			Console.WriteLine();
			int tempAllel = 0;
			//Alle Allele durchgehen
			int j = 0;
			int g = 1;
			while (j < genomeA.Count() - 1)
			{
				//Falls letztes Allel -> nicht behandeln da Endpunkt
//				if (genomeA.IndexOf(g) == genomeA.Count -1)
//					break;
				
				int minLength = int.MaxValue;
//				Console.WriteLine(string.Format("\t\tErmittle eine der kleinsten Nachbarmengen für Allel: {0}", g));
				foreach (int i in neighbours[g])
				{
					Genome subNeighbours = neighbours[i].Copy();
					subNeighbours.RemoveAll(s => childs.Contains(s));
					//Immer wenn eine kürzere Nachbarmenge gefunden wurde -> das zugehörige Allel merken, insofern noch nicht im Kind vorhanden
					//todo: Hier evtl Fehler, Ergebniss Allele doppelt
					if (subNeighbours.Count() < minLength && !childs.Contains(i)) {
						tempAllel = i;
						//Neue kürzeste Länge merken die es zu unterbieten gilt
						minLength = subNeighbours.Count();
					}
				}
//				Console.WriteLine(string.Format("\t\tErgebnis: {0}", tempAllel));
				//Ermitteltes Allel zum Kind hinzufügen
				childs.Add(tempAllel);
				g = tempAllel;
				j++;
			}

//			Console.WriteLine();
//			Console.WriteLine(string.Format("Kindgenom: {0}", childs.AsString()));

			return childs;
		}
		
		/// <summary>
		/// Ermittelt alle Nachbarn aus zwei Genomen für ein bestimmtes Allel
		/// </summary>
		/// <returns>Liste mit Nachbarn</returns>
		/// <param name="value">Allel</param>
		/// <param name="genomeA">Genome A</param>
		/// <param name="genomeB">Genome B</param>
		private static Genome GetNeighboursOfValue (int value, Genome genomeA, Genome genomeB)
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
			Genome neighbours = new Genome();
			neighbours.Add(a[genomeA.IndexOf(value)]);
			neighbours.Add(a[genomeA.IndexOf(value)+2]);
			neighbours.Add(b[genomeB.IndexOf(value)]);
			neighbours.Add(b[genomeB.IndexOf(value)+2]);

			//Doppelte Einträge entfernen und ab damit
			return new Genome(neighbours.Distinct().ToArray());
		}
		
//		/// <summary>
//		/// Convertiert den Inhalt einer Liste in einen String.
//		/// </summary>
//		/// <returns>Inhalt als String</returns>
//		/// <param name="list">Zu konvertierende Liste</param>
//		private static string ListToString (List<int> list)
//		{
//			string tmp = "{";
//			string sep = string.Empty;
//			foreach (int i in list) {
//				tmp = string.Format("{0}{1} {2}", tmp, sep, i);
//				sep = ",";
//			}
//			tmp = string.Format("{0} }}", tmp);
//			return tmp;
//		}
		
		/// <summary>
		/// Der eigentliche evolutionäre Algorithmus - entspricht doc/EvoAlgTSP.pdf.
		/// </summary>
		public static void Compute()
		{
			
			int countGene = 6;						// Anzahl der Gene
			
			int maxGenerations = 10;				// Maximale Anzahl der zu erzeugenden Generationen
			int countGeneration;					// Anzahl der bereits erzeugten Generationen
			
			int countIndividuals = 10;				// Größe der Population (Anzahl der Individuen)
			int countChilds = 10;					// Anzahl der zu erzeugenden Kinder
			
			double recombinationProbability = 0.3;	// Ein Wert zwischen 0 und 1. Gibt die Wahrscheinlichkeit der Rekombination zweier Elterngenome an
			
			double averageFitness;					// Durchschnittlicher Fitnesswert der zuletzt erzeugten Generation
			
			Console.WriteLine("Compute:");
			
			// 1. Initialisiere Population P(0) mit zufälligen Genomen
			Population p = new Population(countIndividuals, countGene);
			
			for (countGeneration = 0; countGeneration < maxGenerations; countGeneration++)
			{	
				// 2. Berechne die Fitnesswerte von P(0)
				foreach (Genome genome in p.curGeneration) {
					FitnessFunctions.CalcTspFitness(genome);
				}

				Console.WriteLine(string.Format("Durchlauf: {0}", countGeneration + 1));
				Console.WriteLine(string.Format("\tAktuelle Population (Count: {1}): \r\n{0}", p.CurrentGenerationAsString(), p.curGeneration.Count));

				Genome bestGenome = p.GetBestGenome();
				
				Console.WriteLine(string.Format("\tBeste Fitness {0}", bestGenome.Fitness));
				Console.WriteLine(string.Format("\tBestes Genom {0}", bestGenome.AsString()));
				
				// Berechne Durchschnittsfitnesswert der aktuellen Generation
				//averageFitness = sumFit / countIndividuals; // p.Size();
				Console.WriteLine(string.Format("\tDurchschnittliche Fitness {0}", p.GetAverageFitness()));
			
				//alte Generation merken
				p.Swap();

				// 3. Erzeuge Kinder und füge sie P' hinzu
				
				Random rnd =  new Random(Guid.NewGuid().GetHashCode());
				double rndDouble;
				int rndIndexA, rndIndexB;
				
				int c = 0;
				while (c < countChilds) 
				{
					// Zufallszahl zwischen 0 und 1					
					rndDouble = rnd.NextDouble();
					
					if (rndDouble <= recombinationProbability)
					{
						// I.	Rekombination zweier Individuen A und B aus Population P(0)
						rndIndexA = rnd.Next(0, p.curGeneration.Count);
						rndIndexB = rnd.Next(0, p.curGeneration.Count);
//						Console.WriteLine(string.Format("rndIndexA: {0}\nrndIndexB: {1}", rndIndexA, rndIndexB));
						
						Genome child = Recombine(p.curGeneration[rndIndexA], p.curGeneration[rndIndexB]);

						// II.	Mutiere Kind c
						Mutate(child, null);
						
						// III.	Füge Kinder C zu P' hinzu
						p.curGeneration.Add(child);

//						Console.WriteLine(string.Format("\tKind: {0}", child.AsString()));

						c++;
					}
				}
				
				// 4. Berechne die Fitness von P'
				foreach (Genome genome in p.curGeneration) {
					FitnessFunctions.CalcTspFitness(genome);
				}
				
				
			// 5. Erzeuge Kind-Population -> die besten Individuen aus P' + P(0)
				//p.NewGeneration();
				Selection.Tsp.TopOfNewGenAndOldGen(p, 10);
			}

			//Ausgabe der besten Genome
			//todo: Distinct funzt nich
			List<Genome> bestGenomes = p.curGeneration.Distinct().ToList();
			foreach (Genome genome in bestGenomes) {
				Console.WriteLine(genome.AsString());
			}
		}
	}
}

