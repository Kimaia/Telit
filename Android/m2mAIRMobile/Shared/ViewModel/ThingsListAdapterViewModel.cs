using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Android.Content;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;

namespace Shared.ViewModel
{
	public class ThingsListAdapterViewModel : BaseViewModel
	{
		private DALManager dataManager;
		public List<Thing> thingsList { get; private set; }

		public ThingsListAdapterViewModel ()
		{
			dataManager = new DALManager();
			thingsList = new List<Thing> ();
		}


		public async Task PopulateThingsList (OnSuccess onSuccess, OnError onError)
		{
			await Task.Run (async () => {
				await PopulateThingsListAsync (onSuccess, onError);
				if (thingsList.Count == 0)
					onError ("Loaded Things List is Empty", null);
				else
				{
					await dataManager.DBInsertListAsync<Thing>(thingsList);
					onSuccess();
				}
			});
		}
			

		public async Task PopulateThingsListAsync (OnSuccess onSuccess, OnError onError)
		{
			try
			{
				var command = prepareTR50Command ();
				var response = await dataManager.M2MLoadListAsync<TR50ThingsListParams> (command);
				thingsList = ParseTR50Response(response.Params);
				Logger.Debug ("PopulateThingsListAsync(), Things count:" + thingsList.Count);
			}
			catch (Exception e)
			{
				onError("Failed get Things list", e.Message);
			}
		}


		private TR50Command prepareTR50Command()
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_OFFSET, Shared.Model.Constants.TR50_PARAM_OFFSET_VALUE);
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_LIMIT, Shared.Model.Constants.TR50_PARAM_LIMIT_VALUE);
			return new TR50Command (M2MCommands.CommandType.Thing_List, prms);
		}

		private List<Thing> ParseTR50Response(TR50ThingsListParams response)
		{
			return response.result;
		}
	}
}

