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

namespace Android.Source.Screens
{
	[Activity]			
	public class ThingActivity : BaseActivity, IOnMapReadyCallback
	{
		private ThingViewModel 	viewModel;
		private Thing 			daThing;

		private NavigationBarView navBar; 
		private ThingBriefDescriptionView thingBriefView;
		private LocationTextView locationView;

		private GoogleMap gMap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_thing);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			locationView = FindViewById<LocationTextView>(m2m.Android.Resource.Id.LocationTextView);
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
					locationView.SetLocation (daThing);

					SetUpMap();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}

		private void SetUpMap()
		{
			if (gMap == null) 
			{
				(FragmentManager.FindFragmentById<MapFragment>(m2m.Android.Resource.Id.map)).GetMapAsync(this);
			}
		}

		public void OnMapReady(GoogleMap gmap)
		{
			gMap = gmap;	
		}

		#region Event handlers
		private void OnProperties()
		{
			var intent = new Intent(this, typeof(PropertiesListActivity));
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, daThing.key);
			Logger.Debug ("OnListItemClick() Thing key: " + daThing.key);

			StartActivity(intent);
			Finish ();
		}
		#endregion
	}
}

