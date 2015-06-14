using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Shared.Model;
using Shared.Utils;
using Android.Source.Screens;
using Shared.Network.DataTransfer.TR50;
using Shared.ViewModel;

namespace m2m.Android.Source.Views
{
	public class ThingInvalidGeoCoordinates : Exception
	{
		public ThingInvalidGeoCoordinates(string message) : base(message) {}
	}

	public class ThingMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener 
	{
		public delegate void OnLocationHistorytSuccess(List<LatLng> locationHistory);

		private Activity 				activity;
		private MapFragmentViewModel 	viewModel;

		private GoogleMap		 		gMap;
		private Thing 					daThing;
		private LatLng 					thingLatLng;
		private MarkerOptions 			locationMarker; 
		private List<MarkerOptions> 	historyMarkers;
		private LatLngBounds			historyBounds;
		private bool 					historyVisible;

		public ThingMapFragment() : base()
		{
			viewModel = new MapFragmentViewModel ();
			daThing = null;
		}

		#region lifecycle
		public override void OnAttach (Activity activity)
		{
			base.OnAttach (activity);
			this.activity = activity;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			GetMapAsync (this);
		}
		#endregion 

		public void OnMapReady(GoogleMap gmap)
		{
			gMap = gmap;
			if (daThing != null)
				SetCameraAndMarker ();
		}

		public void SetThing(Thing thing)
		{
			daThing = thing;
			if (gMap != null)
				SetCameraAndMarker();
		}

		private void SetCameraAndMarker()
		{
			Location loc = daThing.loc;
			if (loc == null)
				SetLocationUnAvailable ();
			else 
			{
				if (IsValidCoordinates (loc)) {
					SetGeo ();
				} else if (loc.addr != null) {
					SetAddress ();
				}

				gMap.SetInfoWindowAdapter (this);
				gMap.SetOnInfoWindowClickListener (this);
			}
		}

		private bool IsValidCoordinates(Location loc)
		{
			if (loc == null)
				return false;

			if (loc.lat > 90 || loc.lat < -90 || loc.lng > 180 || loc.lng < -180) {
				Logger.Error ("InvalidGeoCoordinates: Thing Id: " + daThing.id + ", Lat: " + loc.lat + ", Lng: " + loc.lng);
				return false;
			} else
				return true;
		}

		private void SetGeo()
		{
			thingLatLng = new LatLng (daThing.loc.lat, daThing.loc.lng);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom (thingLatLng, 10);
			gMap.MoveCamera (camera);

			locationMarker = new MarkerOptions ().SetPosition (thingLatLng).SetTitle (daThing.name).Draggable (true);
			gMap.AddMarker (locationMarker);
		}

		private void SetAddress()
		{
			Logger.Debug ("SetAddress()");
			SetLocationUnAvailable ();
		}

		private void SetLocationUnAvailable()
		{
			Logger.Info ("SetLocationUnAvailable()");

			CameraUpdate camera = CameraUpdateFactory.NewLatLng (new LatLng (0, 0));
			gMap.MoveCamera (camera);

			((BaseActivity)activity).OpenErrorDialog("Location UnAvailable", null);
		}

		public View GetInfoWindow (Marker marker)
		{
			
			View view = activity.LayoutInflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
			view.FindViewById<TextView> (m2m.Android.Resource.Id.ThingName).Text = daThing.name;
			view.FindViewById<TextView>(m2m.Android.Resource.Id.Status).Text = (daThing.connected) ? "connected" : "disconnected";
			view.FindViewById<TextView> (m2m.Android.Resource.Id.LastSeen).Text = daThing.lastSeen;
			return view;
		}

		public View GetInfoContents (Marker marker)
		{
			return null;
		}

		void GoogleMap.IOnInfoWindowClickListener.OnInfoWindowClick (Marker marker)
		{
			return;
		}

		public void OnMapClick (LatLng point)
		{
			return;
		}

		#region Location History
		public void OnLocationHistoryClicked()
		{
			Logger.Debug ("OnLocationHistoryClicked()");
			if (historyMarkers == null) 
			{
				viewModel.GetLocationHistoryAsync (daThing.key, OnLocationHistoryRecords, ((BaseActivity)this.activity).ShowDialog);
				((BaseActivity)this.activity).StartLoadingSpinner ("Collecting Location history records.");
				return;
			}
			else
				ToggleLocationHistorys ();
		}

		public void OnLocationHistoryRecords(List<LatLng> history)
		{
			Logger.Debug ("OnLocationHistoryRecords()");
			((BaseActivity)this.activity).StopLoadingSpinner ();

			SetHistoryMarkers (history);

			historyVisible = false;
			ToggleLocationHistorys ();
		}

		private void SetHistoryMarkers(List<LatLng> history)
		{
			historyMarkers = new List<MarkerOptions> ();
			var builder = new LatLngBounds.Builder();
			foreach (LatLng point in history) {
				builder.Include (point);
				MarkerOptions mrk = new MarkerOptions ().SetPosition (point).InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.mm_20_yellow));
				historyMarkers.Add (mrk);
			}
			historyBounds = builder.Build ();
		}

		private void ToggleLocationHistorys()
		{
			try{
				((BaseActivity)this.activity).RunOnUiThread(()=>{

					if (this.historyVisible)
					{
						historyVisible = false;
						gMap.Clear();
						gMap.AddMarker (locationMarker);

						CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom (thingLatLng, 10);
						gMap.MoveCamera (camera);
					}
					else
					{
						historyVisible = true;
						foreach (MarkerOptions mrk in historyMarkers) 
							gMap.AddMarker (mrk);

						CameraUpdate camera = CameraUpdateFactory.NewLatLngBounds (historyBounds, 300);
						gMap.MoveCamera (camera);
					}
				});
			}
			catch(Exception e){
				((BaseActivity)this.activity).ShowDialog ("Location History Unavailable", e.Message);
			}
		}
		#endregion
	}
}

