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
		private ModelServicesManager dataManager;
		public List<Thing> thingsList { get; private set; }

		public ThingsListAdapterViewModel ()
		{
			dataManager = new ModelServicesManager();
			thingsList = new List<Thing> ();
		}


		public async Task PopulateThingsList (string vm_state, OnSuccess onSuccess, OnError onError)
		{
			await Task.Run (async () => {
				await PopulateThingsListAsync (vm_state, onSuccess, onError);
				if (thingsList.Count == 0)
					onError ("PopulateThingsList()", "Loaded Things List is Empty", 0, "dismiss");
				else
					onSuccess();
			});
		}
			

		public async Task PopulateThingsListAsync (string vm_state, OnSuccess onSuccess, OnError onError)
		{
			Logger.Info ("PopulateThingsListAsync(), VM_State:" + vm_state);
			try
			{
				switch (GetVMState(vm_state))
				{
				case Shared.Model.Constants.VM_States.VM_State_Register:
					thingsList = await dataManager.LoadM2MDataListAsync<Thing> (prepareTR50Command ());
						break;
				case Shared.Model.Constants.VM_States.VM_State_Login:
					thingsList = await dataManager.GetDBDataListAsync<Thing> ();
						break;
				default:
					throw new InvalidOperationException("Wrong VM_State:" + vm_state);
				}
				Logger.Debug ("PopulateThingsListAsync(), Things count:" + thingsList.Count);
			}
			catch (Exception e)
			{
				Logger.Error ("Failed PopulateThingsListAsync()", e);
				onError("PopulateThingsListAsync() failed", e.Message, 0, "dismiss");
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
	}
}

