
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
using Android.Source.Views;
using Android.Source.Screens;
using Shared.ViewModel;
using NChart3D_Android;
using Android.Graphics;

namespace m2m.Android.Source.Screens
{
	[Activity]
	public class PropertyActivity : BaseActivity, INChartSeriesDataSource
	{
		private PropertyViewModel 			viewModel;
		private Thing 						daThing;

		private NavigationBarView 			navBar; 
		private ThingBriefDescriptionView 	thingBriefView;
		private NChartView 					chartView;

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

			chartView = FindViewById<NChartView> (Resource.Id.nchartView);
			LoadChartView ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			chartView.OnResume ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			chartView.OnPause ();
		}


		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");
					daThing = viewModel.GetThing ();		
					thingBriefView.SetThing (daThing);

					string pName = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_PROPERTY_NAME_IDENTIFIER);	
					viewModel.GetPropertyHistory(pName, OnPropertyHistory, ShowDialog); 

					StartLoadingSpinner("Collecting Propertie's records.");

					OnPropertyHistory();
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
		}

		private void OnPropertyHistory()
		{
			Logger.Debug ("OnPropertyHistory()");
			// update chart
			chartView.Chart.UpdateData ();
		}

		#region NCharts
		private void LoadChartView ()
		{
			// chart
			chartView.Chart.LicenseKey = "";
			chartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
			chartView.Chart.ShouldAntialias = true;

			// series
			NChartColumnSeries series = new NChartColumnSeries ();
			series.Brush = new NChartSolidColorBrush (Color.Red);
			series.DataSource = this;
			chartView.Chart.AddSeries (series);
		}

		// Get points for the series.
		public NChartPoint[] Points (NChartSeries series)
		{
			Random random = new Random ();
			NChartPoint[] result = new NChartPoint[11];
			for (int i = 0; i <= 10; ++i)
				result [i] = new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (i, random.Next (30) + 1), series);
			return result;
		}
		// Get name of the series.
		public string Name (NChartSeries series)
		{
			return "My series";
		}

		public global::Android.Graphics.Bitmap Image (NChartSeries series)
		{
			return null;
		}
		#endregion
	}
}
