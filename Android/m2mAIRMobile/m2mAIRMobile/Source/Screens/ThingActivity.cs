using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using Shared.ViewModel;
using Shared.Model;
using Shared.Utils;
using Android.Views;
using Android.Source.Views;

namespace Android.Source.Screens
{
	[Activity]			
	public class ThingActivity : BaseActivity, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener 
	{
		private ThingViewModel 	viewModel;
		private Thing 			daThing;

		private NavigationBarView navBar; 
		private ThingBriefDescriptionView thingBriefView;

		private GoogleMap gMap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_thing);

			SetUpMap ();

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			navBar.SetTitle("Thing");

			Button properties = FindViewById<Button> (m2m.Android.Resource.Id.properties);
			properties.Click += (object sender, EventArgs e) => { OnProperties(); };

			viewModel = new ThingViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, ShowDialog);
		}


		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");
					daThing = viewModel.GetThing ();		
					thingBriefView.SetThing (daThing);

					SetMapThing();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}

		#region GoogleMap
		private void SetUpMap()
		{
			if (gMap == null)
				(FragmentManager.FindFragmentById<MapFragment> (m2m.Android.Resource.Id.map)).GetMapAsync (this);
		}

		public void OnMapReady(GoogleMap gmap)
		{
			gMap = gmap;
		}

		private void SetMapThing()
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
			View view = LayoutInflater.Inflate (m2m.Android.Resource.Layout.map_thing_info_window, null, false);
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
			OnProperties ();
		}

		public void OnMapClick (LatLng point)
		{
			OnProperties ();
		}
		#endregion

		#region Event handlers
		private void OnProperties()
		{
			var intent = new Intent(this, typeof(PropertiesListActivity));
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, daThing.key);
			Logger.Debug ("OnProperties() Thing key: " + daThing.key);

			StartActivity(intent);
			Finish ();
		}
		#endregion
	}
}

