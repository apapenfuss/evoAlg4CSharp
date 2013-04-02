using System;
using System.Collections.Generic;

namespace main
{
	public static class Genome
	{
		/// <summary>
		/// Gets the new genome.
		/// </summary>
		/// <returns>A new genome</returns>
		/// <param name='countGenes'>Number of genes</param>
		public static List<int> GetNewGenome(int countGenes)
		{	
			List<int> genome = new List<int>();
			for(int i = 0; i < countGenes; i++)
			{
				genome.Add(0);
			}
			
			Console.WriteLine("Create new Genome:");
			Console.WriteLine(string.Format("\tGenom: {0}\n", ListToString(genome)));
			
			return genome;
		}
		
		/// <summary>
		/// Convertiert den Inhalt einer Liste in einen String.
		/// </summary>
		/// <returns>Inhalt als String</returns>
		/// <param name="list">Zu konvertierende Liste</param>
		private static string ListToString(List<int> list)
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
	}
}

