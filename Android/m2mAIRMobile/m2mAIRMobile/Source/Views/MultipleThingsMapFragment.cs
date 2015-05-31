
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
using Shared.Model;
using Shared.Utils;
using Android.Gms.Maps.Model;
using Android.Source.Screens;

namespace m2m.Android.Source.Views
{
	public class MultipleThingsMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener
	{
		private Activity 		activity;

		private GoogleMap 		gMap;
		private List<Thing>		allThings;
		private List<Thing>		markedThings;

		public MultipleThingsMapFragment() : base()
		{
			allThings = null;
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
			if (allThings != null)
				SetCameraAndMarkers ();
		}

		public void SetThingsList(List<Thing> list)
		{
			allThings = list;
			if (gMap != null)
				SetCameraAndMarkers();
		}

		private void SetCameraAndMarkers()
		{
			var bounds = SetBounds ();
			CameraUpdate camera = CameraUpdateFactory.NewLatLngBounds (bounds, 0);
			gMap.MoveCamera (camera);

			SetMarkers ();

			gMap.MapClick += GMap_MapClick;
			gMap.SetInfoWindowAdapter (this);
			gMap.SetOnInfoWindowClickListener (this);
		}

		private void SetMarkers()
		{
			Thing T;
			for (int i=0 ; i<markedThings.Count ; i++) 
			{
				T = markedThings [i];
				MarkerOptions options = new MarkerOptions ().SetPosition (new LatLng(T.loc.lat, T.loc.lng)).SetTitle (i.ToString()).Draggable (false).Flat(false);
				gMap.AddMarker (options);
			}
		}

		private LatLngBounds SetBounds()
		{
			markedThings = new List<Thing> ();
			var builder = new LatLngBounds.Builder();
			foreach (Thing T in allThings) 
			{
				if (IsValidCoordinates (T.loc)) 
				{
					builder.Include (new LatLng (T.loc.lat, T.loc.lng));
					markedThings.Add (T);
				}
			}
			var bounds = builder.Build ();
			Logger.Debug("LatLngBounds: " + bounds.ToString());
			return bounds;
		}

		private bool IsValidCoordinates(Location loc)
		{
			if (loc == null)
				return false;
			
			if (loc.lat > 90 || loc.lat < -90 || loc.lng > 180 || loc.lng < -180) {
				Logger.Error ("InvalidGeoCoordinates: Lat: " + loc.lat + ", Lng: " + loc.lng);
				return false;
			} else
				return true;
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
			Thing T = markedThings [Convert.ToInt32 (marker.Title)];
			View view = activity.LayoutInflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
			view.FindViewById<TextView> (m2m.Android.Resource.Id.ThingName).Text = T.name;
			view.FindViewById<TextView>(m2m.Android.Resource.Id.Status).Text = (T.connected) ? "connected" : "disconnected";
			view.FindViewById<TextView> (m2m.Android.Resource.Id.LastSeen).Text = T.lastSeen;
			return view;
		}

		public View GetInfoContents (Marker marker)
		{
			return null;
		}

		void GoogleMap.IOnInfoWindowClickListener.OnInfoWindowClick (Marker marker)
		{
			Logger.Debug ("OnInfoWindowClick()");
			var thing = markedThings[Convert.ToInt32 (marker.Title)];
			((ThingsListActivity)activity).InitiateThingActivity (thing);
		}

		void GMap_MapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			((ThingsListActivity)activity).OnMapClick ();
		}

	}
}