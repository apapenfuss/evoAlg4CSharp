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
		/// Setzt die aktuelle Generation als alte Generation
		/// </summary>
		public void SaveAsOldGen ()
		{
			oldGeneration.Clear();
			foreach (Genome genome in curGeneration) {
				oldGeneration.Add(genome.Copy());
			}
			curGeneration.Clear();
		}
		
		
		
		/// <summary>
		/// Konvertiert die aktuelle Generation in einen String.
		/// </summary>
		/// <returns>Inhalt als String</returns>
		public string CurrentGenerationAsString()
		{
			string tmp = string.Empty;
			foreach (Genome genome in curGeneration) 
			{
				tmp += "\t" + genome.AsString() + "\r\n";
			}
			return tmp;
		}
	}
}	

