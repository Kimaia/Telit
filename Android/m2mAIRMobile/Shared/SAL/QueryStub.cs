using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.Model;

namespace Shared.SAL
{
	public class QueryStub
	{
		public QueryStub ()
		{
		}

		public async Task<List<Thing>> getThingsList()
		{
			List<Thing> list = new List<Thing>();

			list.Add(new Thing("1", "11", "111"));
			list.Add(new Thing("2", "22", "222"));
			return list;
		}
	}
}

