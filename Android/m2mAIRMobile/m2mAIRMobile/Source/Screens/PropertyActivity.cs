
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
using Android.Source.Views;
using Android.Source.Screens;
using NChart3D_Android;
using Android.Graphics;

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;
using m2m.Android.Source.Views;


namespace m2m.Android.Source.Screens
{
	[Activity]
	public class PropertyActivity : BaseActivity
	{
		private PropertyViewModel 			viewModel;
		private Thing 						daThing;

		private NavigationBarView 			navBar; 
		private ThingBriefDescriptionView 	thingBriefView;
		private ChartsView 					chartsView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_property);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(Resource.Id.NavigationBarView); 
			navBar.SetTitle("Property");

			viewModel = new PropertyViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, ShowDialog);

			chartsView = FindViewById<ChartsView> (Resource.Id.chartsView);
			chartsView.LoadChartView ();
		}


		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");
					daThing = viewModel.GetThing ();		
					thingBriefView.SetThing (daThing);

//					string pName = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_PROPERTY_NAME_IDENTIFIER);	
//					viewModel.GetPropertyHistory(pName, OnPropertyHistory, ShowDialog); 
//
//					StartLoadingSpinner("Collecting Propertie's records.");

					OnPropertyHistory();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message);
			}
		}

		private void OnPropertyHistory()
		{
			Logger.Debug ("OnPropertyHistory()");
			// update chart
			chartsView.Update();
		}
	}
}
