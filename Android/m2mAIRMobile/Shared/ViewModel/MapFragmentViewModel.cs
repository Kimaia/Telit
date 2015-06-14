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
					var response = await dataManager.M2MLoadListAsync<TR50LocationHistoryParams> (prepareTR50Command (M2MCommands.CommandType.Location_History, thingKey));
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

