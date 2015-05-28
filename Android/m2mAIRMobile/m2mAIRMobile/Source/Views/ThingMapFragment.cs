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

namespace m2m.Android.Source.Views
{
	public class ThingInvalidGeoCoordinates : Exception
	{
		public ThingInvalidGeoCoordinates(string message) : base(message) {}
	}

	public class ThingMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener 
	{
		private Activity 		activity;

		private GoogleMap 		gMap;
		private Thing 			daThing;

		public ThingMapFragment() : base()
		{
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

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return base.OnCreateView (inflater, container, savedInstanceState);
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
			if (loc.lat > 90 || loc.lat < -90 || loc.lng > 180 || loc.lng < -180) {
				Logger.Error ("InvalidGeoCoordinates: Thing Id: " + daThing.id + ", Lat: " + loc.lat + ", Lng: " + loc.lng);
				return false;
			} else
				return true;
		}

		private void SetGeo()
		{
			LatLng latlng = new LatLng (daThing.loc.lat, daThing.loc.lng);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom (latlng, 10);
			gMap.MoveCamera (camera);

			MarkerOptions options = new MarkerOptions ().SetPosition (latlng).SetTitle (daThing.name).Draggable (true);
			gMap.AddMarker (options);
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

			((BaseActivity)activity).OpenErrorDialog("Location UnAvailable", 0);
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

	}
}

