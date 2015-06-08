using System;

using Android.App;
using Android.OS;
using Android.Widget;

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;
using Android.Source.Views;
using Android.Views;

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

			chartsView = FindViewById<ChartsView> (m2m.Android.Resource.Id.chartsView);
			chartsView.LoadChartView ();

			viewModel = new PropertiesListViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, ShowDialog);

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 
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
//					listView.ItemClick += onCheckBoxClick;
				});
			}
			catch(Exception e){
				ShowDialog ("OnListPopulated", e.Message);
			}
		}

		#region checkbox and chart
		public void onCheckBoxClick(object sender, EventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			string propertyName = cb.Text;
			bool checkedd = cb.Checked;
			Logger.Debug ("onCheckBoxClick() property name: " + propertyName);

			viewModel.GetPropertyHistory(propertyName, OnPropertyHistory, ShowDialog); 
			StartLoadingSpinner("Collecting Propertie's records.");
		}

		private void OnPropertyHistory()
		{
			StopLoadingSpinner ();

			Logger.Debug ("OnPropertyHistory()");

			// update chart
			chartsView.Update();
		}
		#endregion
	}
}

