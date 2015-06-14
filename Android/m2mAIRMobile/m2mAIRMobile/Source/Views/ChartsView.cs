
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
using Android.Source.Screens;


namespace Android.Source.Views
{
	public class ChartsView : LinearLayout, INChartSeriesDataSource, INChartValueAxisDataSource
	{
		private NChartView	chartView;
		private Context 	context;

		private string 		currentKey;


		public ChartsView (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public ChartsView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		public ChartsView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			this.context = context;
			LayoutInflater.From(Context).Inflate(m2m.Android.Resource.Layout.charts_view, this);
			chartView = FindViewById<NChartView>(m2m.Android.Resource.Id.nchartView);
		}


		public void LoadChartView ()
		{
			// chart
			chartView.Chart.LicenseKey = "plf4E6SYNIynnmYSIuJK67UXWX3XqaMXTPO0KOFWJB+tIt0ABOgbqCrSYbhPDsYqjrEPycTNNZA8AYmh9h845udCeGSKDks5yVpv5FmhCz+B1KOKfECXhM4w202sJFMphO+HufuwET8Kxtuv+7nPBpDpydQwys1+sZ4EiUa5kAVH//DneMNjrZ+ScjwpXyiAAIy7AIsm3pdzQ1GFB018mbJeRBFTf7vrT7tL787/1L+xGCciaC2ZVqpu4+CWw2nadhgRmskoSEjutuyRmt4/C2MgNSLI9uBXfcOWUlJK3eEdyJXPnOhV1vrTDRA9eYAq+iylpeiZp9OWtDnD67mQMn02/1xo1iHs3z/qvJUKiru27EcO3XLXpfVGvbGSDr9CYpZDEB7e1hKqZi0l5QA7FKrrM7w6FVhJJI8suatTaVyB3x2e8qZ9gKNh0YrY3BTAS9jnEwdYRpIrfEho+lNP3NIPp101PC4kjgub7EAo2c1yGA2k7xovc2UG9vR6oyR0DQmcjHSszM2EK5B5Dj8wPZL2fn19AA07mC7JKsxA8yp4RbK7hU9Cr2Y21I3ceN4L22N8R8CkHdFoP1CS20Ru61YIH6hzhplCteqrcz5u62BPEGk+3UGPws2RsQqC/UOhEf5OhhmJxT+KcqjcTf6waRQe+YByaymMKi8o79p+IK8=";
			chartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
			chartView.Chart.ShouldAntialias = true;
			AddChart ();
		}

		public void Update(string key)
		{
			currentKey = key;
			chartView.Chart.UpdateData ();
		}

		public void AddChart()
		{
			// series
			NChartAreaSeries series = new NChartAreaSeries ();	// new NChartColumnSeries ();, new NChartLineSeries ();
			series.Brush = new NChartSolidColorBrush (Color.Orange);
			series.Brush.Opacity = 0.7f;
			series.DataSource = this;
			chartView.Chart.AddSeries (series);
		}

		#region INChartSeriesDataSource
		// Get points for the series.
		public NChartPoint[] Points (NChartSeries series)
		{
			List<Point> points = ((PropertiesListActivity)context).Points (currentKey);
			return ConvertPoints (series, points);
		}
		// Get name of the series.
		public string Name (NChartSeries series)
		{
			return ((PropertiesListActivity)context).Name (currentKey);
		}

		public global::Android.Graphics.Bitmap Image (NChartSeries series)
		{
			return null;
		}
		#endregion

		#region INChartValueAxisDataSource
		public string Name (NChartValueAxis nChartValueAxis)
		{
			Shared.Charts.Axis axis;
			if (nChartValueAxis.Kind == NChartValueAxisKind.X)
				axis = Shared.Charts.Axis.X;
			else if (nChartValueAxis.Kind == NChartValueAxisKind.Y)
				axis = Shared.Charts.Axis.Y;
			else
				return null;
			
			return ((PropertiesListActivity)context).AxisName (currentKey, axis);
		}

		public string DoubleToString (double value, NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}

		public Java.Lang.Number Length (NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}

		public Java.Lang.Number Max (NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}

		public Java.Lang.Number Min (NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}

		public Java.Lang.Number Step (NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}

		public string[] Ticks (NChartValueAxis axis)
		{
			throw new NotImplementedException ();
		}
		#endregion


		private NChartPoint[] ConvertPoints(NChartSeries series, List<Point> points)
		{
			NChartPoint[] result = new NChartPoint[points.Count];
			for (int i = 0; i < points.Count; ++i)
				result [i] = new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (points[i].X, points[i].Y), series);
			return result;
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

