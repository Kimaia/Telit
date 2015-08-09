using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using NChart3D_iOS;

namespace DataSmoothing
{
	public class DataSmoothingViewController : UIViewController, INChartSeriesDataSource, INChartValueAxisDataSource
	{
		NChartView m_view;
		Random m_rand;

		public DataSmoothingViewController () : base ()
		{
			m_rand = new Random ();
		}

		public override void LoadView ()
		{
			// Create a chart view that will display the chart.
			m_view = new NChartView ();

			// Paste your license key here.
			m_view.Chart.LicenseKey = "";

			// Switch on antialiasing.
			m_view.Chart.ShouldAntialias = true;

			// Margin to ensure some free space for the iOS status bar.
			m_view.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);

			// Set data source for X-Axis.
			m_view.Chart.CartesianSystem.XAxis.DataSource = this;

			// Create series that will be displayed on the chart.
			NChartAreaSeries series = new NChartAreaSeries ();

			// Set brush that will fill that series with color.
			series.Brush = new NChartSolidColorBrush (UIColor.FromRGBA (0.38f, 0.8f, 0.91f, 0.9f));

			// Set data source for the series.
			series.DataSource = this;

			// Set data smoother.
			series.DataSmoother = new NChartDataSmoother2D ();

			// Add series to the chart.
			m_view.Chart.AddSeries (series);

			// Update data in the chart.
			m_view.Chart.UpdateData ();

			// Set chart view to the controller.
			this.View = m_view;

			// Uncomment this line to get the animated transition.
			//m_view.Chart.PlayTransition(1.0f, false);
		}

		#region INChartSeriesDataSource

		public NChartPoint [] Points (NChartSeries series)
		{
			// Create points with some data for the series.
			List<NChartPoint> result = new List<NChartPoint> ();
			for (int i = 0; i < 5; ++i)
				result.Add (new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (i, m_rand.Next () % 30 + 1), series));
			return result.ToArray ();
		}

		public string Name (NChartSeries series)
		{
			// Get name of the series.
			return "My series";
		}

		// If you don't want to implement method, return null.
		public UIImage Image (NChartSeries series) { return null; }

		#endregion

		#region INChartValueAxisDataSource

		public string [] Ticks (NChartValueAxis axis)
		{
			// Get names for the X-Axis ticks.
			return new string[] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };
		}

		// If you don't want to implement method, return null.
		public string DoubleToString (double value, NChartValueAxis axis) { return null; }
		public NSNumber Length (NChartValueAxis axis) { return null; }
		public NSNumber Max (NChartValueAxis axis) { return null; }
		public NSNumber Min (NChartValueAxis axis) { return null; }
		public string Name (NChartValueAxis axis) { return null; }
		public NSNumber Step (NChartValueAxis axis) { return null; }

		#endregion
	}
}

