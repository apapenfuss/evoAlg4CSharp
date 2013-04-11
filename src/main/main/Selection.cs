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

				newGen.RemoveRange(TopCount, newGen.Count() - TopCount);

				population.curGeneration.Clear();
				foreach (Genome genome in newGen) {
					population.curGeneration.Add(genome.Copy());
				}
			}
			
			public static void TopOfNewGen (Population population, int TopCount)
			{
				List<Genome> newGen = new List<Genome> ();
				newGen.AddRange (population.curGeneration);
				newGen.Sort((a,b) => a.Fitness.CompareTo(b.Fitness));

				newGen.RemoveRange(TopCount, newGen.Count() - TopCount);

				population.curGeneration.Clear();
				foreach (Genome genome in newGen) {
					population.curGeneration.Add(genome.Copy());
				}
			}
			
		}
	}
}

