using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;

namespace Shared.ViewModel
{
	public class ThingsListAdapterViewModel
	{
		private readonly string Param_Offset = "offset";
		private readonly string Param_Limit = "limit";
		private readonly int Default_Offset = 0;
		private readonly int Default_Limit = 50;

		private ModelServicesManager dataManager;
		public List<Thing> thingsList { get; private set; }

		public ThingsListAdapterViewModel ()
		{
			thingsList = new List<Thing> ();
			dataManager = new ModelServicesManager();
		}



		public async Task PopulateThingsListAsync ()
		{
			thingsList = await dataManager.GetDataListAsync<Thing> (prepareCommand ());
			Logger.Debug ("PopulateThingsList(), Things count:" + thingsList.Count);
		}



		private TR50Command prepareCommand()
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add(Param_Offset, Default_Offset);
			prms.Params.Add(Param_Limit, Default_Limit);
			return new TR50Command (M2MCommands.CommandType.Thing_List, prms);
		}
	}
}

