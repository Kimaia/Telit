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

namespace m2m.Android.Source.CustomViews
{
	public class ThingMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener 
	{
		private GoogleMap 	gMap;
		private Thing 		daThing;

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
			return inflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
		}

		public void SetThing(Thing thing)
		{
			daThing = thing;
		}

		public void OnMapReady(GoogleMap gmap)
		{
			gMap = gmap;

			LatLng latlng = new LatLng (daThing.loc.lat, daThing.loc.lng);
			CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom (latlng, 10);
			gMap.MoveCamera (camera);

			MarkerOptions options = new MarkerOptions ().SetPosition (latlng).SetTitle (daThing.name).Draggable (true);
			gMap.AddMarker (options);

			gMap.SetInfoWindowAdapter (this);
			gMap.SetOnInfoWindowClickListener (this);
		}

		public void OnMapClick (LatLng point)
		{
			throw new NotImplementedException ();
		}

		public View GetInfoWindow (Marker marker)
		{
//			View view = LayoutInflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
//			view.FindViewById<TextView> (m2m.Android.Resource.Id.ThingName).Text = daThing.name;
//			view.FindViewById<TextView>(m2m.Android.Resource.Id.Status).Text = (daThing.connected) ? "connected" : "disconnected";
//			view.FindViewById<TextView> (m2m.Android.Resource.Id.LastSeen).Text = daThing.lastSeen;
//			return view;
			return null;
		}

		public View GetInfoContents (Marker marker)
		{
			return null;
		}

		void GoogleMap.IOnInfoWindowClickListener.OnInfoWindowClick (Marker marker)
		{
			return;
		}

	}
}

