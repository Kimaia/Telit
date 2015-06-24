using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;
using Shared.Charts;

namespace Shared.ViewModel
{
	public class PropertyHistoryViewModel: IChartDataSource
	{
		public delegate void OnSuccess(string key);
		public delegate void OnError(string key, string msg);

		private DALManager 					dataManager;
		private Thing						daThing;
		private Property 					daProperty;
		private List<TR50PropertyValue> 	displayedHistoryRecords;

		// singleton
		private static PropertyHistoryViewModel instance;
		public static PropertyHistoryViewModel Instance 
		{
			get {
				if (instance == null)
					instance = new PropertyHistoryViewModel ();
				return instance; 
			}
		}
		private PropertyHistoryViewModel ()
		{
			dataManager = new DALManager();
		}


		public void GetThingObjectAsync (string key, BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					Logger.Debug ("GetThingObject(), Thing key:" + key);
					Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));
					daThing = await dataManager.DBLoadItemAsync<Thing> (predicate);
					onSuccess();
				}
				catch (Exception e)
				{
					onError("Thing Object Unavailable", e.Message);
				}
			});
		}

		public Thing GetThing()
		{
			return daThing;
		}


		public void GetPropertyHistoryAsync (string propertyKey, Property property, OnSuccess onSuccess, OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					daProperty = property;
					var command = TR50CommandFactory.Build (M2MCommands.CommandType.Property_History, daThing.key, propertyKey);
					var historyRecords = await dataManager.M2MLoadListAsync<TR50PropertyHistoryParams> (command);
					if (historyRecords.Params.HasPayload())
					{
						displayedHistoryRecords = new List<TR50PropertyValue>();
						foreach (TR50PropertyValue pv in historyRecords.Params.values)
							if (pv.HasPayload())
								this.displayedHistoryRecords.Add(pv);

						onSuccess(propertyKey);
					}
					else
					{
						// display error dialog and cleaer the graph display
						onError(propertyKey, "Property has no history records");
					}
				}
				catch (Exception e)
				{
					onError(propertyKey, "Property's records Unavailable: " + e.Message);
				}
			});
		}
			

		#region IChartDataSource
		public List<TR50PropertyValue> Points (string propertyKey)
		{
			return displayedHistoryRecords;
		}

		public string Name (string key)
		{
			return daProperty.name;
		}
		#endregion

	}
}

