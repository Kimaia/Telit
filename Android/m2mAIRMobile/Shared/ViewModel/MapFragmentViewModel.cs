using System;
using m2m.Android.Source.Views;
using Shared.Network.DataTransfer.TR50;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.ModelManager;
using Shared.Network;

namespace Shared.ViewModel
{
	public class MapFragmentViewModel
	{
		private DALManager 		dataManager;

		public MapFragmentViewModel ()
		{
			dataManager = new DALManager();
		}


		public void GetLocationHistoryAsync (string thingKey, ThingMapFragment.OnLocationHistorytSuccess onSuccess, BaseViewModel.OnError onError)
		{
			Task.Run (async () => {
				try 
				{
					var command = TR50CommandFactory.Build (M2MCommands.CommandType.Location_History, thingKey);
					var response = await dataManager.M2MLoadListAsync<TR50LocationHistoryParams> (command);
					onSuccess(ConvertToLatLng(response.Params));
				}
				catch (Exception e)
				{
					onError("Location History Unavailable", e.Message);
				}
			});
		}

		public List<LatLng> ConvertToLatLng(TR50LocationHistoryParams locationHistory)
		{
			List<LatLng> history = new List<LatLng> ();
			foreach (TR50LocationHistoryValue point in locationHistory.values) 
				history.Add(new LatLng(point.lat, point.lng));

			return history;
		}
	}
}

