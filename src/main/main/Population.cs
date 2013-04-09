using System;
using System.Collections.Generic;
using System.Linq;

namespace main
{
	/// <summary>
	/// Stellt eine Population dar.
	/// </summary>
	public class Population
	{				
		public List<Genome> curGeneration;
		public List<Genome> oldGeneration;
		
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name='size'>Größe der Population</param>
		/// <param name='genomeSize'>Größe des einzelnen Genomes</param>
		public Population (int size, int genomeSize)
		{
			curGeneration = new List<Genome>();
			oldGeneration = new List<Genome>();

			for(int i = 0; i < size; i++)
			{
				curGeneration.Add(new Genome(genomeSize));
			}
		}
		
		/// <summary>
		/// Setzt die neue Generation als aktuelle Generation
		/// </summary>
		public void Swap ()
		{
			oldGeneration.Clear();
			foreach (Genome genome in curGeneration) {
				oldGeneration.Add(genome.Copy());
			}
		}
		
		public Genome GetBestGenome()
		{
			Genome bestGenome = new Genome();
			bestGenome.Fitness = double.MaxValue;

			foreach (Genome genome in curGeneration)
			{
				if (genome.Fitness < bestGenome.Fitness) {
					bestGenome = genome;
				}
			}
			return bestGenome.Copy();
		}
		
		public double GetTotalFitness()
		{
			double totalFitness = 0;
			foreach (Genome genome in curGeneration)
			{
				totalFitness += genome.Fitness;
			}
			return totalFitness;
		}
		
		public double GetAverageFitness()
		{
			return GetTotalFitness() / curGeneration.Count;
		}

		public string CurrentGenerationAsString()
		{
			string tmp = string.Empty;
			foreach (Genome genome in curGeneration) 
			{
				tmp += genome.AsString() + "\r\n";
			}
			return tmp;
		}
	}
}	

