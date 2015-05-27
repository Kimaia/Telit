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


		public async Task PopulateThingsList (string login_state, OnSuccess onSuccess, OnError onError)
		{
			await Task.Run (async () => {
				await PopulateThingsListAsync (login_state, onSuccess, onError);
				if (thingsList.Count == 0)
					onError ("PopulateThingsList()", "Loaded Things List is Empty", 0, "dismiss");
				else
				{
					await dataManager.InsertListIntoDBAsync<Thing>(thingsList);
					onSuccess();
				}
			});
		}
			

		public async Task PopulateThingsListAsync (string login_state, OnSuccess onSuccess, OnError onError)
		{
			Logger.Info ("PopulateThingsListAsync(), Login_State:" + login_state);
			try
			{
				switch (GetLoginState(login_state))
				{
				case Shared.Model.Constants.User_Login_States.Login_State_Register:
					var command = prepareTR50Command ();
					var response = await dataManager.LoadM2MDataListAsync<TR50ThingsListParams> (command);
					thingsList = ParseTR50Response(response.Params);
						break;
				case Shared.Model.Constants.User_Login_States.Login_State_LoggedIn:
					thingsList = await dataManager.GetDBDataListAsync<Thing> ();
						break;
				default:
					throw new InvalidOperationException("Wrong VM_State:" + login_state);
				}
				Logger.Debug ("PopulateThingsListAsync(), Things count:" + thingsList.Count);
			}
			catch (Exception e)
			{
				Logger.Error ("Failed PopulateThingsListAsync()", e);
				onError("PopulateThingsListAsync() failed", e.Message, 0, "dismiss");
			}
		}

		private Shared.Model.Constants.User_Login_States GetLoginState(string vm_state)
		{
			return (Shared.Model.Constants.User_Login_States)Enum.Parse(typeof(Shared.Model.Constants.User_Login_States), vm_state);
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

