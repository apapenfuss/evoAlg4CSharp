using System;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	/// <summary>
	/// Holds a Population.
	/// </summary>
	public class Population
	{
		List<List<int>> generation1;
		List<List<int>> generation2;
		int countGenes;
		
		public Population (int size, int genes)
		{
			countGenes = genes;
			generation1 = new List<List<int>>();
			List<int> genome;
			
			for(int i = 0; i < size; i++)
			{
				genome = Genome.GetNewGenome(countGenes);
				generation1.Add(genome);
			}
		}
		
	}
}	

