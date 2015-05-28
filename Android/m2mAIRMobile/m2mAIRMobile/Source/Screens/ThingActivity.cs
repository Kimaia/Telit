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

namespace Android.Source.Screens
{
	[Activity]			
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

					SetMapThingMarker();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}

		private void SetMapThingMarker()
		{
			(FragmentManager.FindFragmentById<ThingMapFragment> (m2m.Android.Resource.Id.map)).SetThing(daThing);
		}

		#region Event handlers
		private void OnProperties()
		{
			var intent = new Intent(this, typeof(PropertiesListActivity));
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, daThing.key);
			Logger.Debug ("OnProperties() Thing key: " + daThing.key);

			StartActivity(intent);
		}
		#endregion
	}
}

