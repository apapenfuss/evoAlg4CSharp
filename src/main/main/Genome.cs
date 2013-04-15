using System;
using System.Collections.Generic;

namespace main
{
	public class Genome : List<int>
	{		
		private double _Fitness;
		private double _SelectionProbability;
		
		public double Fitness {
			get {
				return _Fitness;
			}
			set { _Fitness = value; }
		}
		
		public double SelectionProbability {
			get {
				return _SelectionProbability;
			}
			set {
				_SelectionProbability = value;
			}
		}			
		
		/// <summary>
		/// Leerer Konstruktor
		/// </summary>
		public Genome()
		{

		}
		
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name='arr'>Integer Array, mit dem Genom initialisiert wird</param>
		public Genome (int[] arr)
		{
			this.Clear();
			this.AddRange(arr);
		}
	
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name='size'>Anzahl der Gene</param>
		public Genome(int size) 
		{
			//List<int> genome = new List<int>() {1};
			this.Add(1);
			
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			
			int rndInt;
			
			for(int i = 0; i < size-1; i++)
			{
				rndInt=rnd.Next(1, size+1);
				while (this.Contains(rndInt))
				{
					rndInt=rnd.Next(1,size+1);
				}
				this.Add(rndInt);
			}
			
			//Console.WriteLine("Create new Genome:");
//			Console.WriteLine(string.Format("\tGenom: {0}", this.AsString()));
			//return genome;
		}
				
		/// <summary>
		/// Convertiert den Inhalt eines Genoms in einen String.
		/// </summary>
		/// <returns>Inhalt als String</returns>
		/// <param name="list">Zu konvertierendes Genom</param>
		public string AsString()
		{
			string tmp = "{";
			string sep = string.Empty;
			foreach (int i in this) {
				tmp = string.Format("{0}{1} {2}", tmp, sep, i);
				sep = ",";
			}
			tmp = string.Format("{0} }} Fitness: {1}", tmp, _Fitness);
			return tmp;
		}
		
		/// <summary>
		/// Kopiert ein Genom
		/// </summary>
		public Genome Copy()
		{
			Genome result = new Genome (this.ToArray());
			result.Fitness = this.Fitness;
			return result;
		}
	}
}

