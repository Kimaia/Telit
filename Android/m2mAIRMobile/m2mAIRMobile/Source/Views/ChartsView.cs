
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
using Shared.Charts;
using Shared.ViewModel;
using Shared.Network.DataTransfer.TR50;
using System.Threading.Tasks;


namespace Android.Source.Views
{
	public class ChartsView : LinearLayout, INChartSeriesDataSource, INChartValueAxisDataSource
	{
		private NChartView					chartView;
		private Context 					context;
		private PropertiesListViewModel 	viewModel;

		private string 						currentKey;
		private List<Point> 				daPoints;
		private List<TR50PropertyValue> 	m2mPoints;
		private int minX, maxX;
		private long minY, maxY;

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


		public void SetViewModel (PropertiesListViewModel 	vm)
		{
			viewModel = vm;
		}


		public void LoadChartView ()
		{
			// chart
			chartView.Chart.LicenseKey = "plf4E6SYNIynnmYSIuJK67UXWX3XqaMXTPO0KOFWJB+tIt0ABOgbqCrSYbhPDsYqjrEPycTNNZA8AYmh9h845udCeGSKDks5yVpv5FmhCz+B1KOKfECXhM4w202sJFMphO+HufuwET8Kxtuv+7nPBpDpydQwys1+sZ4EiUa5kAVH//DneMNjrZ+ScjwpXyiAAIy7AIsm3pdzQ1GFB018mbJeRBFTf7vrT7tL787/1L+xGCciaC2ZVqpu4+CWw2nadhgRmskoSEjutuyRmt4/C2MgNSLI9uBXfcOWUlJK3eEdyJXPnOhV1vrTDRA9eYAq+iylpeiZp9OWtDnD67mQMn02/1xo1iHs3z/qvJUKiru27EcO3XLXpfVGvbGSDr9CYpZDEB7e1hKqZi0l5QA7FKrrM7w6FVhJJI8suatTaVyB3x2e8qZ9gKNh0YrY3BTAS9jnEwdYRpIrfEho+lNP3NIPp101PC4kjgub7EAo2c1yGA2k7xovc2UG9vR6oyR0DQmcjHSszM2EK5B5Dj8wPZL2fn19AA07mC7JKsxA8yp4RbK7hU9Cr2Y21I3ceN4L22N8R8CkHdFoP1CS20Ru61YIH6hzhplCteqrcz5u62BPEGk+3UGPws2RsQqC/UOhEf5OhhmJxT+KcqjcTf6waRQe+YByaymMKi8o79p+IK8=";
			chartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
			chartView.Chart.ShouldAntialias = true;
			AddChart ();
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

		public void Update(string key)
		{
			currentKey = key;
	
			chartView.Chart.CartesianSystem.XAxis.DataSource = this;
			chartView.Chart.UpdateData ();
		}

		public void RemoveAllCharts()
		{
			chartView.Chart.RemoveAllSeries ();
		}

		#region INChartSeriesDataSource
		// Get points for the series.
		public NChartPoint[] Points (NChartSeries series)
		{
			m2mPoints = viewModel.Points (currentKey);
			return ConvertScaleAndAnalysePoints (series);
		}
		// Get name of the series.
		public string Name (NChartSeries series)
		{
			return viewModel.Name (currentKey);
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
			if (axis.Kind == NChartValueAxisKind.X) 
			{
				string[] ticks = new string[3];
				ticks [0] = m2mPoints [0].ts;
				ticks [1] = m2mPoints[m2mPoints.Count/2].ts;
				ticks [2] = m2mPoints.Last ().ts;
				return ticks;
			}
			else
				return null;		
		}
		#endregion

		#region NChartPoint
		private NChartPoint[] ConvertScaleAndAnalysePoints(NChartSeries series)
		{
			Convert2Points ();

			return Convert2NChartPoints (series);
		}

		private void Convert2Points()
		{
			daPoints = new List<Point> ();
			long firstX = TS2Seconds (m2mPoints [0].ts);
			foreach (TR50PropertyValue pv in m2mPoints) 
			{
				PointF pf = new PointF (TS2Seconds (pv.ts) - firstX, pv.value);
				daPoints.Add (new Point((int)pf.X, (int)pf.Y));
			}
		}

		private NChartPoint[] Convert2NChartPoints(NChartSeries series)
		{
			minY = maxY = daPoints [0].Y;
			NChartPoint[] result = new NChartPoint[daPoints.Count];
			for (int i = 0; i < daPoints.Count; ++i) 
			{
				Point pi = daPoints [i];

				if (minY > pi.Y)
					minY = pi.Y;
				else if (maxY < pi.Y)
					maxY = pi.Y;

				result [i] = new NChartPoint (NChartPointState.PointStateAlignedToXWithXY (pi.X, pi.Y), series);
			}

			minX = daPoints [0].X;
			maxX = daPoints.Last().X;

			return result;
		}

		private long TS2Seconds(string ts)
		{
			return (DateTime.Parse (ts)).Ticks / 10000000;
		}

		#endregion
	}
}

