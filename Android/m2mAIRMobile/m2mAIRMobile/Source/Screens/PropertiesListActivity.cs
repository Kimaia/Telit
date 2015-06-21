using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Source.Views;
using Android.Graphics;

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;
using Shared.Charts;

namespace Android.Source.Screens
{
	[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class PropertiesListActivity : BaseActivity
	{
		private PropertiesListViewModel 	viewModel;
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

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel = new PropertiesListViewModel ();
			viewModel.GetThingObjectAsync (tkey, OnDBLoadThingObject, ShowDialog);

			chartsView = FindViewById<ChartsView> (m2m.Android.Resource.Id.chartsView);
			chartsView.SetViewModel (viewModel);
			chartsView.LoadChartView ();
		}


		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");

					daThing = viewModel.GetThing ();
					thingBriefView.SetThing (daThing);

					adapter = new PropertiesListAdapter(this, daThing);
					adapter.PopulatePropertiesListAsync (OnListPopulated, ShowDialog);
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message);
			}
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
				ShowDialog ("OnListPopulated", e.Message);
			}
		}
		#endregion

		#region Property history
		public void onPropertySelected(string key)
		{
			Logger.Debug ("onPropertySelected() property key: " + key);
			viewModel.GetPropertyHistoryAsync (key, adapter.GetPropertyObject(key), OnPropertyHistory, OnPropertyHistoryError); 
			StartLoadingSpinner ("Collecting Propertie's records.");
		}

		private void OnPropertyHistory(string key)
		{
			StopLoadingSpinner ();
			Logger.Debug ("OnPropertyHistory()");

			chartsView.Update(key);
		}

		private void OnPropertyHistoryError(string key, string msg)
		{
			StopLoadingSpinner ();
			ShowDialog (msg, null);
			chartsView.RemoveAllCharts();
		}
		#endregion
	}
}

