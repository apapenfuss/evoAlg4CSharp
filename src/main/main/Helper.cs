using System;
using System.Collections.Generic;

namespace main
{
	public static class Helper
	{
		public static string ListToString (List<int> list)
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

