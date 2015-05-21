﻿using System;
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
	public class PropertiesListAdapterViewModel : BaseViewModel
	{
		private ModelServicesManager 	dataManager;
		private Thing 					daThing;
		public Dictionary<string, Property> 			propertiesList { get; private set; }

		public PropertiesListAdapterViewModel (Thing thing)
		{
			dataManager = new ModelServicesManager();
			daThing = thing;
			propertiesList = new Dictionary<string, Property> ();
		}


		public async Task PopulatePropertiesList (OnSuccess onSuccess, OnError onError)
		{
			await Task.Run (async () => {
				await PopulatePropertiesListAsync (onSuccess, onError);
				if (propertiesList.Count == 0)
					onError ("PopulatePropertiesList()", "Loaded Properties List is Empty", 0, "dismiss");
				else
				{
//					await dataManager.InsertListIntoDBAsync<Property>(propertiesList);
					onSuccess();
				}
			});
		}


		public async Task PopulatePropertiesListAsync (OnSuccess onSuccess, OnError onError)
		{
			try
			{
				var command = prepareTR50Command ();
				var response = await dataManager.LoadM2MDataListAsync<TR50ThingDefParams> (command);
				propertiesList = ParseTR50Response(response.Params);
				Logger.Debug ("PopulatePropertiesListAsync(), Properties count:" + propertiesList.Count);
			}
			catch (Exception e)
			{
				Logger.Error ("Failed PopulatePropertiesListAsync()", e);
				onError("PopulatePropertiesListAsync() failed", e.Message, 0, "dismiss");
			}
		}



		private TR50Command prepareTR50Command()
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add(Shared.Model.Constants.TR50_PARAM_KEY, daThing.defkey);
			return new TR50Command (M2MCommands.CommandType.Thing_Def_Find, prms);
		}

		private Dictionary<string, Property> ParseTR50Response(TR50ThingDefParams response)
		{
			return response.properties;
		}
	}
}
