
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

using Android.Source.Screens;
using Shared.Charts;
using Shared.ViewModel;
using Shared.Network.DataTransfer.TR50;
using System.Threading.Tasks;
using System.Drawing;
using NChart3D_Android;


namespace Android.Source.Views
{
	public class ChartsView : LinearLayout, INChartSeriesDataSource, INChartValueAxisDataSource
	{
		private NChartView			nchartView;
		private IChartDataSource 	viewModel;

		private List<Point> 		m2mPoints;
		private int 				minX, maxX;
		private long 				minY, maxY;

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
			LayoutInflater.From(Context).Inflate(m2m.Android.Resource.Layout.charts_view, this);
			nchartView = FindViewById<NChartView>(m2m.Android.Resource.Id.nchartView);
		}

		public void Cleanup ()
		{
			nchartView.Cleanup ();
		}

		public void SetViewModel (PropertyViewModel 	vm)
		{
			viewModel = vm;
		}


		public void LoadChartView ()
		{
			// chart
			nchartView.Chart.LicenseKey = "plf4E6SYNIynnmYSIuJK67UXWX3XqaMXTPO0KOFWJB+tIt0ABOgbqCrSYbhPDsYqjrEPycTNNZA8AYmh9h845udCeGSKDks5yVpv5FmhCz+B1KOKfECXhM4w202sJFMphO+HufuwET8Kxtuv+7nPBpDpydQwys1+sZ4EiUa5kAVH//DneMNjrZ+ScjwpXyiAAIy7AIsm3pdzQ1GFB018mbJeRBFTf7vrT7tL787/1L+xGCciaC2ZVqpu4+CWw2nadhgRmskoSEjutuyRmt4/C2MgNSLI9uBXfcOWUlJK3eEdyJXPnOhV1vrTDRA9eYAq+iylpeiZp9OWtDnD67mQMn02/1xo1iHs3z/qvJUKiru27EcO3XLXpfVGvbGSDr9CYpZDEB7e1hKqZi0l5QA7FKrrM7w6FVhJJI8suatTaVyB3x2e8qZ9gKNh0YrY3BTAS9jnEwdYRpIrfEho+lNP3NIPp101PC4kjgub7EAo2c1yGA2k7xovc2UG9vR6oyR0DQmcjHSszM2EK5B5Dj8wPZL2fn19AA07mC7JKsxA8yp4RbK7hU9Cr2Y21I3ceN4L22N8R8CkHdFoP1CS20Ru61YIH6hzhplCteqrcz5u62BPEGk+3UGPws2RsQqC/UOhEf5OhhmJxT+KcqjcTf6waRQe+YByaymMKi8o79p+IK8=";
			nchartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
			nchartView.Chart.ShouldAntialias = true;
			AddSeries ();
		}

		public void AddSeries()
		{
			// series
//			NChartLineSeries series = new NChartLineSeries(); //NChartAreaSeries ();
			NChartAreaSeries series = new NChartAreaSeries ();
			series.Brush = new NChartSolidColorBrush (Android.Graphics.Color.Orange);
			series.Brush.Opacity = 0.7f;
			series.BorderThickness = 2.0f;
			series.BorderBrush = new NChartSolidColorBrush(Android.Graphics.Color.Black);

			series.DataSource = this;
			nchartView.Chart.AddSeries (series);
		}

		public void DrawChart()
		{
			nchartView.Chart.Background = new NChartLinearGradientBrush(Android.Graphics.Color.Gray, Android.Graphics.Color.White);
			nchartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
			nchartView.Chart.CartesianSystem.XAxis.DataSource = this;
			nchartView.Chart.UpdateData ();
		}

		public void ClearChart()
		{
			nchartView.Chart.RemoveAllSeries ();
			AddSeries ();
			nchartView.Chart.UpdateData ();
		}

		#region INChartSeriesDataSource
		// Get points for the series.
		public NChartPoint[] Points (NChartSeries series)
		{
			m2mPoints = viewModel.Points ();

			if (m2mPoints == null || m2mPoints.Count == 0)
				return new NChartPoint[0];
			else
				return Convert2NChartPoints (m2mPoints, series);
		}

		// Get name of the series.
		public string Name (NChartSeries series)
		{
			return viewModel.Name ();
		}

		public global::Android.Graphics.Bitmap Image (NChartSeries series)
		{
			return null;
		}
		#endregion

		#region INChartValueAxisDataSource
		public string Name (NChartValueAxis axis)
		{
			if (axis.Kind == NChartValueAxisKind.X)
				return "X Axis";
			else 
				return "Y Axis";
		}

		public string DoubleToString (double value, NChartValueAxis axis)
		{
			return null;
		}

		public Java.Lang.Number Length (NChartValueAxis axis)
		{
			return null;
		}

		public Java.Lang.Number Max (NChartValueAxis axis)
		{
			if (axis.Kind == NChartValueAxisKind.X)
				return (Java.Lang.Number)maxX;
			else
				return (Java.Lang.Number)maxY;
		}

		public Java.Lang.Number Min (NChartValueAxis axis)
		{
			if (axis.Kind == NChartValueAxisKind.X)
				return (Java.Lang.Number)minX;
			else
				return (Java.Lang.Number)minY;
		}

		public Java.Lang.Number Step (NChartValueAxis axis)
		{
			return null;
		}

		public string[] Ticks (NChartValueAxis axis)
		{
			if (m2mPoints == null || m2mPoints.Count == 0)
				return null;
			
			if (axis.Kind == NChartValueAxisKind.X)
				return viewModel.Ticks ();
			else
				return null;		
		}
		#endregion

		#region NChartPoint
		private NChartPoint[] Convert2NChartPoints(List<Point> points,  NChartSeries series)
		{
			minY = maxY = points [0].Y;
			NChartPoint[] result = new NChartPoint[points.Count];
			for (int i = 0; i < points.Count; ++i) 
			{
				Point pi = points [i];

				if (minY > pi.Y)
					minY = pi.Y;
				else if (maxY < pi.Y)
					maxY = pi.Y;

				result [i] = new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (pi.X, pi.Y), series);
			}

			maxX = points [0].X;
			minX = points.Last().X;

			return result;
		}
		#endregion
	}
}

