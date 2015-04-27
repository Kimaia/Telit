using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.SAL;

namespace Shared.ViewModel
{
	public class ThingsListAdapterViewModel
	{

		private QueryStub stub;
		public List<Thing> thingsList { get; private set; }

		public ThingsListAdapterViewModel ()
		{
			thingsList = new List<Thing> ();
			stub  = new QueryStub();
		}


		public async Task PopulateThingsList ()
		{
			var list = await stub.getThingsList();
			foreach (var thing in list)
			{
				this.thingsList.Add(thing);
			}
			Logger.Debug ("PopulateThingsList(), Tings count:" + thingsList.Count);
		}
	}
}

