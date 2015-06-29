using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Source.Views;

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;
using Shared.Charts;

namespace Android.Source.Screens
{
	[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class PropertiesListActivity : BaseActivity
	{
		private PropertyViewModel 			ViewModel;
		private PropertiesListAdapter		adapter;
		private ListView					listView;

		private Thing	 					daThing;

		private NavigationBarView 			navBar; 
		private ThingBriefDescriptionView 	thingBriefView;
		private ChartsView 					chartsView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_properties_list);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			navBar.SetTitle("Properties");

			//TODO replace with selector
			Button history = FindViewById<Button> (m2m.Android.Resource.Id.mode_history);
			history.Click += (object sender, EventArgs e) => { OnModeHistory(); };
			Button continuous = FindViewById<Button> (m2m.Android.Resource.Id.mode_continuous);
			continuous.Click += (object sender, EventArgs e) => { OnModeContinuous(); };


			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			ViewModel = PropertyViewModel.Instance;
			ViewModel.GetThingObjectAsync (tkey, OnDBLoadThingObject, OpenErrorDialog);

			chartsView = FindViewById<ChartsView> (m2m.Android.Resource.Id.chartsView);
			chartsView.SetViewModel (ViewModel);
			chartsView.LoadChartView ();
		}

		protected override void OnStop ()
		{
			chartsView.Cleanup ();

			base.OnStop ();
		}


		public void OnDBLoadThingObject()
		{
			Logger.Debug ("OnDBLoadThingObject()");

			daThing = ViewModel.GetThing ();
			thingBriefView.SetThing (daThing);

			adapter = new PropertiesListAdapter(this, daThing);
			adapter.PopulatePropertiesListAsync (OnListPopulated, OpenErrorDialog);
		}

		#region Property list
		public void OnListPopulated()
		{
			try{
				RunOnUiThread(()=>{
					listView.Adapter = adapter;

					ShowDialog ("Select a property to view", null);
				});
			}
			catch(Exception e){
				OpenErrorDialog ("OnListPopulated", e.Message);
			}
		}
		#endregion

		#region Presentation Mode
		//TODO replace with selector
		public void OnModeHistory()
		{
			Logger.Debug ("OnModeHistory");
			ViewModel.PresentationMode = PropertyViewModel.PropertyPresentationMode.History;
		}

		public void OnModeContinuous()
		{
			Logger.Debug ("OnModeContinuous");
			ViewModel.PresentationMode = PropertyViewModel.PropertyPresentationMode.Continuous;
		}
		#endregion

		#region Property
		public void OnPropertySelected(string key)
		{
			Logger.Debug ("onPropertySelected() property key: " + key);
			ViewModel.OnPropertySelected (key, adapter.GetPropertyObject(key), OnPropertyData, OnPropertyDataError); 
			StartLoadingSpinner ("Collecting Propertie's records.");
		}

		private void OnPropertyData(string key)
		{
			StopLoadingSpinner ();
			Logger.Debug ("OnPropertyHistory()");
			chartsView.DrawChart(key);
		}

		private void OnPropertyDataError(string key, string msg)
		{
			StopLoadingSpinner ();
			OpenErrorDialog (msg, null);
//			chartsView.ClearChart();
		}
		#endregion
	}
}

