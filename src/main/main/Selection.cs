using System;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	public static class Selection
	{
		public static class Tsp
		{
			public static void TopOfNewGenAndOldGen (Population population, int TopCount)
			{
				List<Genome> newGen = new List<Genome> ();
				newGen.AddRange (population.curGeneration);
				newGen.AddRange (population.oldGeneration);
				//list.Sort((a,b) => a.date.CompareTo(b.date));
				newGen.Sort((a,b) => a.Fitness.CompareTo(b.Fitness));

				newGen.RemoveRange(TopCount - 1, newGen.Count - TopCount);

//				string tmp = string.Empty;
//				foreach (Genome genome in newGen) 
//				{
//					tmp += genome.AsString() + "\r\n";
//				}
//				Console.WriteLine( tmp);

				population.curGeneration.Clear();
				foreach (Genome genome in newGen) {
					population.curGeneration.Add(genome.Copy());
				}
			}

		}
	}
}

