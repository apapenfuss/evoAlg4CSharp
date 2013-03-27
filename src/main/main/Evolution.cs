using System;
using System.Collections;
using System.Collections.Generic;

namespace main
{
	public class Evolution
	{
		public Evolution ()
		{
		}
		
		public List<int> Mutate(List<int> genome) 
		{
			//ArrayList resultList = new ArrayList();
			Random rnd = new Random(); //todo : prüfen
			int bla = rnd.Next(2);
			
			int z1 = 0;
			int z2 = 0;
			int tmp = 0;
			int length = genome.Count;
			bool equal = true;
			while (equal) {
					z1 = rnd.Next(1, length);
					z2 = rnd.Next(1, length);
					if (z1 != z2 ) {
						equal = false;
					}
				}
			// Wenn 0, vertausche, sonst, invertiere
			if (bla != 1) {
				tmp = genome[z1-1];
				genome[z1-1] = genome[z2-1];
				genome[z2-1] = tmp;
			}
			else
			{
				tmp = z1 < z2 ? z1 : z2;
				genome.Reverse(tmp-1, Math.Abs(z1-z2)+1);
			}
			
			return genome;
		}
		
		public void recombine(List<int> genomeA, List<int> genomeB)
		{
			
		}
	}
}

