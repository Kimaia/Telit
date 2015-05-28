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

namespace m2m.Android.Source.Views
{
	public class ThingMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener 
	{
		private GoogleMap 	gMap;
		private Thing 		daThing;

		private LayoutInflater inflater;

		public ThingMapFragment() : base()
		{
			daThing = null;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			GetMapAsync (this);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			this.inflater = inflater;
			return base.OnCreateView (inflater, container, savedInstanceState);
		}

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
			LatLng latlng = new LatLng (daThing.loc.lat, daThing.loc.lng);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom (latlng, 10);
			gMap.MoveCamera (camera);

			MarkerOptions options = new MarkerOptions ().SetPosition (latlng).SetTitle (daThing.name).Draggable (true);
			gMap.AddMarker (options);

			gMap.SetInfoWindowAdapter (this);
			gMap.SetOnInfoWindowClickListener (this);
		}

		public View GetInfoWindow (Marker marker)
		{
			View view = inflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
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

