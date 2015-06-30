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
using m2m.Android.Source.Views;

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
		private SelectorBar					selectorBar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_properties_list);

			ViewModel = new PropertyViewModel();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			ViewModel.GetThingObjectAsync (tkey, OnDBLoadThingObject, OpenErrorDialog);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			navBar.SetTitle("Properties");

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			selectorBar = FindViewById<SelectorBar> (m2m.Android.Resource.Id.SelectorBar);
			selectorBar.SetViewModel (ViewModel);

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

		#region Property
		public void OnPropertySelected(string key)
		{
			Logger.Debug ("onPropertySelected() property key: " + key);
			ViewModel.OnPropertySelected (adapter.GetPropertyObject(key), OnPropertyData, OnPropertyDataError); 
			StartLoadingSpinner ("Collecting Propertie's records.");
		}

		private void OnPropertyData()
		{
			StopLoadingSpinner ();
			Logger.Debug ("OnPropertyData()");
			chartsView.DrawChart();
		}

		private void OnPropertyDataError(string msg)
		{
			StopLoadingSpinner ();
			OpenErrorDialog (msg, null);
//			chartsView.ClearChart();
		}
		#endregion
	}
}

