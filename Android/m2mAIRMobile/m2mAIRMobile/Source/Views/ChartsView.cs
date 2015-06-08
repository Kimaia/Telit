
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Graphics;
using NChart3D_Android;


namespace Android.Source.Views
{
	public class ChartsView : LinearLayout, INChartSeriesDataSource
	{
		private NChartView		chartView;

		public ChartsView (Context context) :
			base (context)
		{
			Initialize ();
		}

		public ChartsView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public ChartsView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
			LayoutInflater.From(Context).Inflate(m2m.Android.Resource.Layout.charts_view, this);
			chartView = FindViewById<NChartView>(m2m.Android.Resource.Id.nchartView);
		}


		public void LoadChartView ()
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

		public void Update()
		{
			chartView.Chart.UpdateData ();
		}

		// Get points for the series.
		public NChartPoint[] Points (NChartSeries series)
		{
			return PointsStub (series);
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

		#if DEBUG
		private NChartPoint[] PointsStub (NChartSeries series)
		{
			Random random = new Random ();
			NChartPoint[] result = new NChartPoint[11];
			for (int i = 0; i <= 10; ++i)
				result [i] = new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (i, random.Next (30) + 1), series);
			return result;
		}
		#endif
	}
}

