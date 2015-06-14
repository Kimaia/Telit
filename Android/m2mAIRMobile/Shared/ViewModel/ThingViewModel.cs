﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;
using Android.Gms.Maps.Model;
using Android.Source.Screens;

namespace Shared.ViewModel
{
	public class ThingViewModel : BaseViewModel
	{
		private DALManager 					dataManager;
		private Thing 						handledThing;

		public ThingViewModel ()
		{
			dataManager = new DALManager();
		}


		public void GetThingObject (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					Logger.Debug ("GetThingObject(), Thing key:" + key);
					Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));
					handledThing = await dataManager.DBLoadItemAsync<Thing> (predicate);
					onSuccess();
				}
				catch (Exception e)
				{
					onError("Failed Get Thing Object", e.Message);
				}
			});
		}

		public Thing GetThing()
		{
			return handledThing;
		}


		private TR50Command prepareTR50Command(M2MCommands.CommandType command, string key)
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object> ();

			switch (command) {
			case M2MCommands.CommandType.Thing_Find:
				prms.Params.Add (Shared.Model.Constants.TR50_PARAM_KEY, key);
				break;
			case M2MCommands.CommandType.Location_History:
				prms.Params.Add (Shared.Model.Constants.TR50_PARAM_THINGKEY, key);
				prms.Params.Add(Shared.Model.Constants.TR50_PARAM_RECORDS, 20);
				break;
			default:
				throw new InvalidOperationException ("Wrong M2M CommandType:" + command);
			}
			return new TR50Command (command, prms);
		}
	}
}

