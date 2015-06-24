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
using m2m.Android.Source.Views;
using System.Collections.Generic;

namespace Android.Source.Screens
{
	[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class ThingActivity : BaseActivity
	{
		private ThingViewModel 	viewModel;
		private Thing 			daThing;

		private NavigationBarView navBar; 
		private ThingBriefDescriptionView thingBriefView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_thing);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			navBar.SetTitle("Thing");

			Button properties = FindViewById<Button> (m2m.Android.Resource.Id.properties);
			properties.Click += (object sender, EventArgs e) => { OnProperties(); };
			Button locationHistory = FindViewById<Button> (m2m.Android.Resource.Id.locationHistory);
			locationHistory.Click += (object sender, EventArgs e) => { OnLocationHistoryClicked(); };

			viewModel = new ThingViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, OpenErrorDialog);
		}


		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");
					daThing = viewModel.GetThing ();		
					thingBriefView.SetThing (daThing);

					SetMapFragment();
				});
			}
			catch(Exception e){
				OpenErrorDialog ("OnDBLoadThingObject", e.Message);
			}
		}

		private void SetMapFragment()
		{
			(FragmentManager.FindFragmentById<ThingMapFragment> (m2m.Android.Resource.Id.map)).SetThing(daThing);
		}

		#region Properties
		private void OnProperties()
		{
			var intent = new Intent(this, typeof(PropertiesListActivity));
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, daThing.key);
			Logger.Debug ("OnProperties() Thing key: " + daThing.key);

			StartActivity(intent);
		}
		#endregion

		#region Location History
		private void OnLocationHistoryClicked()
		{
			Logger.Debug ("OnLocationHistory() Thing key: " + daThing.key);
			FragmentManager.FindFragmentById<ThingMapFragment> (m2m.Android.Resource.Id.map).OnLocationHistoryClicked();
		}
		#endregion
	}
}

