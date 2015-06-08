﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;

namespace Shared.ViewModel
{
	public class PropertiesListViewModel
	{
		private ModelServicesManager 	dataManager;
		private Thing 					daThing;
//		private Dictionary<string, List<TR50PropertyHistoryParams.PropertyValue>> propertyHistory;

		public PropertiesListViewModel ()
		{
			dataManager = new ModelServicesManager();
		}


		public void GetThingObject (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {

				Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));

				daThing = await dataManager.LoadItemFromDBAsync<Thing> (predicate);
				Logger.Debug ("GetThingObject(), Thing key:" + key);

				// raise event for completion
				onSuccess();
			});
		}

		public Thing GetThing()
		{
			return daThing;
		}

		public void GetPropertyHistory (string propertyName, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					var propertyHistory = await dataManager.LoadM2MDataListAsync<TR50PropertyHistoryParams> (prepareTR50Command (propertyName));
					Logger.Debug ("GetPropertyHistory(), Property Name:" + propertyHistory.Params.values.ToString());

					onSuccess();
				}
				catch (Exception e)
				{
					onError("Failed Get Property History", e.Message, 0, null);
				}
			});
		}


		private TR50Command prepareTR50Command(string key)
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_THINGKEY, daThing.key);
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_KEY, key);
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_LAST, Shared.Model.Constants.TR50_PARAM_LAST_PERIOD_VALUE);
			return new TR50Command (M2MCommands.CommandType.Property_History, prms);
		}
	}
}

