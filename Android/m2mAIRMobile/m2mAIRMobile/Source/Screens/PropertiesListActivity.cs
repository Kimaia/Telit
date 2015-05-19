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

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;

namespace Android.Source.Screens
{
	[Activity]			
	public class PropertiesListActivity : BaseActivity
	{
		private PropertiesListViewModel 	viewModel;

		private Thing	 daThing;

		private TextView thingName;
		private TextView thingStatus;
		private TextView thingLastSeen;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_properties_list);

			base.SetNavigationTitle ("Properties");

			thingName = FindViewById<TextView>(m2m.Android.Resource.Id.ThingName);
			thingStatus = FindViewById<TextView>(m2m.Android.Resource.Id.Status);
			thingLastSeen = FindViewById<TextView>(m2m.Android.Resource.Id.LastSeen);

			viewModel = new PropertiesListViewModel ();
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
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}
	}
}

