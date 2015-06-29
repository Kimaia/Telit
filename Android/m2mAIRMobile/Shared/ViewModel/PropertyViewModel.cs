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
	public class PropertyViewModel: IChartDataSource
	{
		public delegate void OnSuccess(string key);
		public delegate void OnError(string key, string msg);

		public enum PropertyPresentationMode { History, Continuous }

		private DALManager 					dataManager;
		private Thing						daThing;
		private Property 					daProperty;

		public 	PropertyPresentationMode 	PresentationMode { get; set; }
		private List<TR50PropertyValue> 	displayedRecords;

		// singleton
		private static PropertyViewModel instance;
		public static PropertyViewModel Instance 
		{
			get {
				if (instance == null)
					instance = new PropertyViewModel ();
				return instance; 
			}
		}
		private PropertyViewModel ()
		{
			dataManager = new DALManager();
			displayedRecords = new List<TR50PropertyValue>();
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


		public void OnPropertySelected(string propertyKey, Property property, OnSuccess onSuccess, OnError onError)
		{
			switch (PresentationMode) 
			{
			case PropertyPresentationMode.History:
				GetPropertyHistoryAsync (propertyKey, property, onSuccess, onError);
				break;
			case PropertyPresentationMode.Continuous:
				GetPropertyContinuousAsync (propertyKey, property, onSuccess, onError);
				break;
			}
		}


		#region History
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
						displayedRecords.Clear();
						foreach (TR50PropertyValue pv in historyRecords.Params.values)
							if (pv.HasPayload())
								displayedRecords.Add(pv);

						onSuccess(propertyKey);
					}
					else
					{
						// display error dialog and cleaer the graph display
						displayedRecords.Clear();
						onError(propertyKey, "Property has no history records");
					}
				}
				catch (Exception e)
				{
					displayedRecords.Clear();
					onError(propertyKey, "Property's records Unavailable: " + e.Message);
				}
			});
		}
		#endregion
			
		#region Continuous
		public void GetPropertyContinuousAsync (string propertyKey, Property property, OnSuccess onSuccess, OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					daProperty = property;
					var command = TR50CommandFactory.Build (M2MCommands.CommandType.Property_Current, daThing.key, propertyKey);
					var currentRecord = await dataManager.M2MLoadListAsync<TR50PropertyValue> (command);
					if (currentRecord.Params.HasPayload())
					{
						TR50PropertyValue pv = currentRecord.Params;
						if (pv.HasPayload())
							displayedRecords.Insert(0, pv);

						onSuccess(propertyKey);
					}
					else
					{						
						// display error dialog and clear the graph display
						displayedRecords.Clear();
						onError(propertyKey, "Property has no history records");
					}
				}
				catch (Exception e)
				{
					displayedRecords.Clear();
					onError(propertyKey, "Property's records Unavailable: " + e.Message);
				}
			});
		}
		#endregion

		#region IChartDataSource
		public List<TR50PropertyValue> Points (string propertyKey)
		{
			return displayedRecords;
		}

		public string Name (string key)
		{
			return daProperty.name;
		}
		#endregion

	}
}

