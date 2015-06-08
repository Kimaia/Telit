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
	public class PropertiesListActivity : BaseActivity, IChartDataSource
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

			chartsView = FindViewById<ChartsView> (m2m.Android.Resource.Id.chartsView);
			chartsView.LoadChartView ();

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel = new PropertiesListViewModel ();
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, ShowDialog);
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

		public void OnListPopulated()
		{
			try{
				RunOnUiThread(()=>{
					listView.Adapter = adapter;
				});
			}
			catch(Exception e){
				ShowDialog ("OnListPopulated", e.Message);
			}
		}

		public void onCheckBoxClick(string key, bool isChecked)
		{
			Logger.Debug ("onCheckBoxClick() property key: " + key + ", Checked: " + isChecked);
			if (isChecked) {
				viewModel.GetPropertyHistory (key, OnPropertyHistory, ShowDialog); 
				StartLoadingSpinner ("Collecting Propertie's records.");
			}
			else 
			{
				UncheckPropertyChart (key);
			}
		}

		private void OnPropertyHistory()
		{
			StopLoadingSpinner ();
			Logger.Debug ("OnPropertyHistory()");

			chartsView.Update();
		}

		private void UncheckPropertyChart (string key)
		{
			viewModel.UncheckPropertyChart (key);
		}

		#region IChartDataSource
		public Android.Graphics.Bitmap Image ()
		{
			throw new NotImplementedException ();
		}

		public string Name ()
		{
			throw new NotImplementedException ();
		}

		public List<Point> Points ()
		{
			return MakePoints ();
		}

		#if DEBUG
		private List<Point> MakePoints ()
		{
			Random random = new Random ();
			List<Point> points = new List<Point> ();
			for (int i = 0; i <= 10; ++i)
				points.Add (new Point (i, random.Next (30)));
			return points;
		}
		#endif
		#endregion
	}
}

