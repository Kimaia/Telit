using System;
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
	public class ThingViewModel : BaseViewModel
	{
		private ModelServicesManager 	dataManager;
		private Thing 					handledThing;

		public ThingViewModel ()
		{
			dataManager = new ModelServicesManager();
		}


		public void GetThingObject (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {

				Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));

				handledThing = await dataManager.GetDBDataItemAsync<Thing> (prepareTR50Command (key), predicate);
				Logger.Debug ("GetThingObject(), Thing key:" + key);

				// raise event for completion
				onSuccess();
			});
		}

		public Thing GetThing()
		{
			return handledThing;
		}


		private TR50Command prepareTR50Command(string key)
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_KEY, key);
			return new TR50Command (M2MCommands.CommandType.Thing_Find, prms);
		}
	}
}

