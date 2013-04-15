using System;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	public static class Selection
	{
		public enum SelPropType { Fitness, Ranking };
		public enum SelType { Roulette, SingleTournament, MultiTournament }; 
		
		public static void Plus (Population population, int TopCount)
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
		
		public static void Comma (Population population, int TopCount)
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
		
		public static void RandomParents (List<Genome> parents, ref int indexA, ref int indexB)
		{
			indexA = Helper.GetRandomInteger(1, parents.Count)-1;
			indexB = Helper.GetRandomInteger(1, parents.Count)-1;
		}
		
		
		
		public static Genome Roulette (List<Genome> generation, SelPropType type)
		{			
			switch(type)
			{
				case SelPropType.Fitness : 
					CalcSelPropByFitness(generation);
					break;
				case SelPropType.Ranking : 
					CalcSelPropByRanking(generation);
					break;
			}
			
			double rnd = Helper.GetRandomDouble();
			double cur = 0;
			
			for (int i = 0; i < generation.Count; i++) {
				cur += generation[i].SelectionProbability;
				if ( rnd <= cur)
					return generation[i].Copy();
			}
			return null;
		}
		
		public static Genome SingleTournament(List<Genome> generation, int memberCount, SelPropType type)
		{
			switch(type)
			{
				case SelPropType.Fitness : 
					CalcSelPropByFitness(generation);
					break;
				case SelPropType.Ranking : 
					CalcSelPropByRanking(generation);
					break;
			}
			
			List<Genome> member = new List<Genome>();
			for (int i = 0; i < memberCount; i++) {
				member.Add(generation[Helper.GetRandomInteger(0,generation.Count-1)]);
			}
			
			member.Sort((a,b) => a.Fitness.CompareTo(b.Fitness));
			
			return member[0].Copy();
		}
		
		public static Genome MultiTournament(List<Genome> generation, int memberCount, SelPropType type)
		{
			//todo
			return null;
		}
		
		private static void CalcSelPropByFitness(List<Genome> generation)
		{
			
			foreach (Genome genome in generation)
			{
				genome.SelectionProbability = genome.Fitness / Helper.Fitness.GetTotalFitness(generation);
			}
		}
		
		private static void CalcSelPropByRanking(List<Genome> generation)
		{
			generation.Sort((a,b) => a.Fitness.CompareTo(b.Fitness));
			foreach (Genome genome in generation)
			{
				// 2 / r * ( 1 - (i-1) / (r-1))
				genome.SelectionProbability = (double)(2 / (double)generation.Count) * ( 1 - ((double)(generation.IndexOf(genome))/(double)(generation.Count-1)));
			}
		}
	}
}

