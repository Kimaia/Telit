using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;
using Android.Graphics;

namespace Shared.ViewModel
{
	public class PropertiesListViewModel
	{
		public delegate void OnSuccess(string key);

		private ModelServicesManager 							dataManager;
		private Thing											daThing;
		private string	 										displayedPropertyKey;
		private List<TR50PropertyHistoryParams.PropertyValue> 	displayedHistoryRecords;

		public PropertiesListViewModel ()
		{
			dataManager = new ModelServicesManager();
		}


		public void GetThingObjectAsync (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					Logger.Debug ("GetThingObject(), Thing key:" + key);
					Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));
					daThing = await dataManager.LoadItemFromDBAsync<Thing> (predicate);
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
			return daThing;
		}

		public void GetPropertyHistoryAsync (string propertyKey, OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					this.displayedPropertyKey = propertyKey;
					var historyRecords = await dataManager.LoadM2MDataListAsync<TR50PropertyHistoryParams> (prepareTR50Command (propertyKey));
					this.displayedHistoryRecords = historyRecords.Params.values;
					Logger.Debug ("StorePropertyRecords, Property Key: " + propertyKey);

					onSuccess(propertyKey);
				}
				catch (Exception e)
				{
					onError("Failed Get Property History records", e.Message);
				}
			});
		}
			
		//TODO make async
		public List<Point> GetScaledHistoryPoints (string propertyKey)
		{
			if (displayedHistoryRecords != null)
				return ScaleAndConvert ();
			else
				return GetStupPoints ();
		}

		private List<Point> ScaleAndConvert()
		{
			List<Point> points = new List<Point> ();
			foreach (TR50PropertyHistoryParams.PropertyValue pv in displayedHistoryRecords)
				points.Add (pv.ToPoint ());

			return points;
		}

		#if DEBUG
		private List<Point> GetStupPoints ()
		{
			Random random = new Random ();
			List<Point> points = new List<Point> ();
			for (int i = 0; i <= 10; ++i)
				points.Add (new Point (i, random.Next (30)));
			return points;
		}
		#endif


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

