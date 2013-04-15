using System;
using System.Collections.Generic;

namespace main
{
	public static class Helper
	{
		/// <summary>
		/// Konvertiert den Inhalt einer Liste in einem String.
		/// </summary>
		/// <returns>Inhalt als String</returns>
		/// <param name='list'>Zu konvertierende Liste</param>
		public static string ListToString (List<int> list)
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
		
		public static int GetRandomInteger()
		{
			Random rnd =  new Random(Guid.NewGuid().GetHashCode());
			return rnd.Next();
		}
		
		public static int GetRandomInteger(int min, int max)
		{
			Random rnd =  new Random(Guid.NewGuid().GetHashCode());
			return rnd.Next(min, max + 1);
		}
		
		public static double GetRandomDouble()
		{
			Random rnd =  new Random(Guid.NewGuid().GetHashCode());
			return rnd.NextDouble();
		}
		
		public static class Fitness
		{
			/// <summary>
			/// Ermittelt das Genom mit dem besten Fitnesswert
			/// </summary>
			/// <returns>Genom</returns>
			public static Genome GetBestGenome(List<Genome> generation)
			{
				Genome bestGenome = new Genome();
				bestGenome.Fitness = double.MaxValue;
	
				foreach (Genome genome in generation)
				{
					if (genome.Fitness < bestGenome.Fitness) {
						bestGenome = genome;
					}
				}
				return bestGenome.Copy();
			}
			
			/// <summary>
			/// Berechnet Summe aller Fitnesswerte der aktuellen Generation
			/// </summary>
			/// <returns>Summe Gesamtfitness</returns>
			public static double GetTotalFitness(List<Genome> generation)
			{
				double totalFitness = 0;
				foreach (Genome genome in generation)
				{
					totalFitness += genome.Fitness;
				}
				return totalFitness;
			}
			
			/// <summary>
			/// Berechnet die durchschnittliche Fitness der aktuellen Population
			/// </summary>
			/// <returns>Fitnesswert</returns>
			public static double GetAverageFitness(List<Genome> generation)
			{
				return GetTotalFitness(generation) / generation.Count;
			}
			
		}
	}
}

