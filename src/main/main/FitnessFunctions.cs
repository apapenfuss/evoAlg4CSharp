using System;
using System.Linq;

namespace main
{
	public static class FitnessFunctions
	{
		public static void CalcTspFitness(Genome genome)
		{
			genome.Fitness = 0;
			Genome tempGenome = genome.Copy();
			tempGenome.Add(1);
			
			int[,] matrix = new int[,]
							 {
								{ 0,  5,  8, 11,  4,  7 },
								{ 5,  0, 10,  4,  9, 12 },
								{ 8, 10,  0,  6, 17,  8 },
								{11,  4,  6,  0,  6,  5 },
								{ 4,  9, 17,  6,  0, 11 },
								{ 7, 12,  8,  5, 11,  0 }
							 };
			
			foreach (int gene in genome) {
				genome.Fitness += matrix[gene - 1,tempGenome[tempGenome.IndexOf(gene)+1]-1];
			}
		}
	}
}

