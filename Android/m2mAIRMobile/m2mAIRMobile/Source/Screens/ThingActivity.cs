using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Shared.ViewModel;
using Shared.Model;
using Shared.Utils;


namespace Android.Source.Screens
{
	[Activity]			
	public class ThingActivity : BaseActivity
	{
		private ThingViewModel 	viewModel;
		private Thing 			daThing;

		private TextView thingName;
		private TextView thingStatus;
		private TextView thingLastSeen;

		private TextView thingLat;
		private TextView thingLong;
		private TextView thingStreet;
		private TextView thingCity;
		private TextView thingState;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_thing);

			base.SetNavigationTitle ("Thing");

			thingName = FindViewById<TextView>(m2m.Android.Resource.Id.ThingName);
			thingStatus = FindViewById<TextView>(m2m.Android.Resource.Id.Status);
			thingLastSeen = FindViewById<TextView>(m2m.Android.Resource.Id.LastSeen);

			thingLat = FindViewById<TextView>(m2m.Android.Resource.Id.latitude);
			thingLong = FindViewById<TextView>(m2m.Android.Resource.Id.longiude);
			thingStreet = FindViewById<TextView>(m2m.Android.Resource.Id.Street);
			thingCity = FindViewById<TextView>(m2m.Android.Resource.Id.City);
			thingState = FindViewById<TextView>(m2m.Android.Resource.Id.State);


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
				thingName.Text = daThing.name;
				thingStatus.Text = (daThing.connected) ? "connected" : "disconnected";
				thingLastSeen.Text = daThing.lastSeen;

				SetLocation ();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}

		private void SetLocation()
		{
			if (daThing.loc != null) {
				thingLat.Text = daThing.loc.lat.ToString ();
				thingLong.Text = daThing.loc.lng.ToString ();
				thingStreet.Text = daThing.loc.addr.street;
				thingCity.Text = daThing.loc.addr.city;
				thingState.Text = daThing.loc.addr.state;
			} else {
				Logger.Info ("Thing Key: " + daThing.key + "No Location data.");
			}
		}
	}
}

