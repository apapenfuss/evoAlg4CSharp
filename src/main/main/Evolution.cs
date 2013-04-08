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
			
			Console.WriteLine("Mutate:");
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
			//todo: eventuell doch möglich?
			if (genomeA.Count != genomeB.Count) {
				throw new ArgumentException("Cant recombine genomes with a different count of values.");
			}
			/*
			Console.WriteLine(string.Format("Recombine:"));
			Console.WriteLine(string.Format("\tGenomeA: {0}", ListToString(genomeA)));
			Console.WriteLine(string.Format("\tGenomeB: {0}", ListToString(genomeB)));
			Console.WriteLine();
			*/
			//Kinder-List mit Startallel initialisieren
			Genome childs = new Genome();
			childs.Add(genomeA[0]);
			
			//Nachbarn für alle Allele ermitteln
			/*
			Console.WriteLine(string.Format("\tErmittle alle Nachbarn..."));
			Console.WriteLine();
			*/
			Dictionary<int, Genome> neighbours = new Dictionary<int, Genome>();
			foreach (int gene in genomeA)
			{
				neighbours.Add(gene, GetNeighboursOfValue(gene, genomeA,genomeB));
				//Console.WriteLine(string.Format("\t\tAllel: {0}, Nachbarn: {1}", k, ListToString(neighbours[k])));
			}
			//Console.WriteLine();
			
			//Kind ermitteln
			Console.WriteLine(string.Format("\tErmittle Kind-Genom..."));
			Console.WriteLine();
			int tempAllel = 0;
			//Alle Allele durchgehen
			foreach (int gene in genomeA)
			{
				//Falls letztes Allel -> nicht behandeln da Endpunkt
				if (genomeA.IndexOf(gene) == genomeA.Count -1)
					break;
				
				int minLength = int.MaxValue;
				//Console.WriteLine(string.Format("\t\tErmittle eine der kleinsten Nachbarmengen für Allel: {0}", gene));
				foreach (int i in neighbours[gene])
				{
					Genome subNeighbours = (Genome)neighbours[i].ToList();
					subNeighbours.RemoveAll(s => childs.Contains(s));
					//Immer wenn eine kürzere Nachbarmenge gefunden wurde -> das zugehörige Allel merken, insofern noch nicht im Kind vorhanden
					//todo: Hier evtl Fehler, Ergebniss Allele doppelt
					if (subNeighbours.Count < minLength && !childs.Contains(i)) {
						tempAllel = i;
						//Neue kürzeste Länge merken die es zu unterbieten gilt
						minLength = subNeighbours.Count;
					}
				}
				//Console.WriteLine(string.Format("\t\tErgebnis: {0}", tempAllel));
				//Ermitteltes Allel zum Kind hinzufügen
				childs.Add(tempAllel);
			}
			/*
			Console.WriteLine();
			Console.WriteLine(string.Format("Kindgenom: {0}", ListToString(childs)));
			*/
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
			List<int> neighbours = new List<int>();
			neighbours.Add(a[genomeA.IndexOf(value)]);
			neighbours.Add(a[genomeA.IndexOf(value)+2]);
			neighbours.Add(b[genomeB.IndexOf(value)]);
			neighbours.Add(b[genomeB.IndexOf(value)+2]);

			//Doppelte Einträge entfernen und ab damit
			return (Genome)neighbours.Distinct().ToList();
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
		/// Berechnet die Fitness des übergebenen Genoms
		/// </summary>
		/// <returns>Fitnesswert</returns>
		/// <param name='genome'>Genom</param>
		private static double ComputeFitness (Genome genome)
		{
			double fitness = 0.00;
			
			//List<int> a = genome.ToList();
			//a.Insert(0, genome[genome.Count-1]);
			//a.Add(genome[0]);
			for (int h = 0; h < genome.Count()-1; h++)
			{
				int i = genome[h];
				int j = genome[h+1];
					if (i==1 && j==2 || i==2 && j==1 || i==4 && j==6 || i==6 && j==4)
					{
						fitness += 5;
					} 
					if (i==1 && j==3 || i==3 && j==1 || i==3 && j==6 || i==6 && j==3)
					{
						fitness += 8;
					}
					if (i==1 && j==4 || i==4 && j==1 || i==5 && j==6 || i==6 && j==5)
					{
						fitness += 11;
					}
					if (i==1 && j==5 || i==5 && j==1 || i==2 && j==4 || i==4 && j==2)
					{
						fitness += 4;
					}
					if (i==1 && j==6 || i==6 && j==1)
					{
						fitness += 7;
					}
					if (i==2 && j==3 || i==3 && j==2)
					{
						fitness += 10;
					}
					if (i==2 && j==5 || i==5 && j==2)
					{
						fitness += 9;
					}
					if (i==2 && j==6 || i==6 && j==2)
					{
						fitness += 12;
					}
					if (i==3 && j==4 || i==4 && j==3 || i==5 && j==4 || i==4 && j==5)
					{
						fitness += 6;
					}
					if (i==3 && j==5 || i==5 && j==3)
					{
						fitness += 17;
					}
			}
			
			return fitness;
		}
		
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
			// 2. Berechne die Finesswerte von P(0)
				Genome bestGenome = p.GetBestGenome();

//				for(int i = 0; i < countIndividuals; i++)
//				{
//					//curFit = ComputeFitness(p.GetGenomeAt(i));
//					if (i==0)
//					{
//						bestFit = curFit;
//						bestGenome = p.GetGenomeAt(i);
//					}
//					Console.WriteLine(string.Format("\tAktuelle Fitness {0}", curFit));
//					sumFit += curFit;
//					if(curFit < bestFit)
//					{
//						bestFit = curFit;
//						bestGenome = p.GetGenomeAt(i);
//					}
//					
//				}
				
				Console.WriteLine(string.Format("\tBeste Fitness {0}", bestGenome.Fitness));
				Console.WriteLine(string.Format("\tBestes Genom {0}", bestGenome));
				
				// Berechne Durchschnittsfitnesswert der aktuellen Generation
				//averageFitness = sumFit / countIndividuals; // p.Size();
				Console.WriteLine(string.Format("\tDurchschnittliche Fitness {0}", p.GetAverageFitness()));
			
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
						rndIndexA = rnd.Next(0, p.curGeneration.Count + 1);
						rndIndexB = rnd.Next(0, p.curGeneration.Count + 1);
						Console.WriteLine(string.Format("rndIndexA: {0}\nrndIndexB: {1}", rndIndexA, rndIndexB));
						
						Genome child = Evolution.Recombine(p.curGeneration[rndIndexA], p.curGeneration[rndIndexB]);
						
						// II.	Mutiere Kind C
						Evolution.Mutate(child, null);
						
						// III.	Füge Kind C zu P' hinzu
						p.curGeneration.Add(child);
						c++;
					}
					
				}
				
			// 4. Berechne die Fitness von P'

				
				
			// 5. Erzeuge Kind-Population -> die besten Individuen aus P' + P(0)
				//p.NewGeneration();
			}
		}
	}
}

