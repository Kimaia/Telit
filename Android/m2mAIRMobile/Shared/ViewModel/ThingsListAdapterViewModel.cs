using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.DB;
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


		public async Task PopulateThingsListAsync ()
		{
			// first load from DB
			var dao = new Dao();
			var list = await dao.LoadAll<Thing>();
			if (list.Count == 0) 
			{
				// if DB empty - load from Server
				list = await LoadFromServerAsync ();

				// and insert into DB
				await InsertIntoDBAsync(list);
			}
			thingsList = list;
			Logger.Debug ("PopulateThingsList(), Things count:" + thingsList.Count);
		}

		private async Task<List<Thing>> LoadFromServerAsync ()
		{
			return await stub.getThingsList();
		}

		private async Task InsertIntoDBAsync (List<Thing> list)
		{
			var dao = new Dao();
			await dao.InsertAll<Thing> (list);
		}
	}
}

