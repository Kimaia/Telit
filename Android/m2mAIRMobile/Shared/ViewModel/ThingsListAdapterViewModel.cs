using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.DB;
using Shared.Utils;
using Shared.Model;
using Shared.SAL;
using Shared.Network.DataTransfer;

namespace Shared.ViewModel
{
	public class ThingsListAdapterViewModel
	{

		private M2MApiRequestor m2mRequestor;
		public List<Thing> thingsList { get; private set; }

		public ThingsListAdapterViewModel ()
		{
			thingsList = new List<Thing> ();
			m2mRequestor = new M2MApiRequestor();
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
			return await m2mRequestor.RequestAsync (prepareCommands());
		}

		private async Task InsertIntoDBAsync (List<Thing> list)
		{
			var dao = new Dao();
			await dao.InsertAll<Thing> (list);
		}


		#if DEBUG
		public List<TR50Command> prepareCommands()
		{
			List<TR50Command> list = new List<TR50Command> ();
			//			list.Add(new TR50Command ("things.list"));

			TR50Params prms = new TR50Params ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add("offset", 0);
			prms.Params.Add("limit", 1);
			list.Add(new TR50Command ("thing.list", prms));
			return list;
		}
		#endif
	}
}

