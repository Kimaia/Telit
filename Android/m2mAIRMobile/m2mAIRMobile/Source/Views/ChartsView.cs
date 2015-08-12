
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
using Shared.Model;
using Shared.Network.DataTransfer.TR50;
using System.Threading.Tasks;
using System.Drawing;
using NChart3D_Android;


namespace Android.Source.Views
{
    public class ChartsView : LinearLayout, INChartSeriesDataSource, INChartValueAxisDataSource
    {
        private NChartView nchartView;
        private IChartDataSource viewModel;
        private string licenseKey;

        private List<Point> m2mPoints;
        private int minX, maxX;
        private long minY, maxY;

        public ChartsView(Context context)
            : base(context)
        {
            Initialize(context);
        }

        public ChartsView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context);
        }

        public ChartsView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        void Initialize(Context context)
        {
            LayoutInflater.From(Context).Inflate(m2m.Android.Resource.Layout.charts_view, this);
            nchartView = FindViewById<NChartView>(m2m.Android.Resource.Id.nchartView);
            licenseKey = context.Resources.GetString(m2m.Android.Resource.String.NChart_Key);
        }

        public void Cleanup()
        {
            nchartView.Cleanup();
        }

        public void SetModel(PropertyModel 	vm)
        {
            viewModel = vm;
        }


        public void LoadChartView()
        {
            // chart
            nchartView.Chart.LicenseKey = licenseKey;
            nchartView.Chart.ShouldAntialias = true;
            nchartView.Chart.Background = new NChartLinearGradientBrush(Android.Graphics.Color.Gray, Android.Graphics.Color.White);
//			nchartView.Chart.CartesianSystem.Margin = new NChartMargin (10.0f, 10.0f, 10.0f, 20.0f);
            nchartView.Chart.CartesianSystem.XAxis.DataSource = this;
            nchartView.Chart.CartesianSystem.YAxis.DataSource = this;
            AddSeries();
        }

        public void AddSeries()
        {
            // series
//			NChartColumnSeries series = new NChartColumnSeries();
//			NChartLineSeries series = new NChartLineSeries();
            NChartAreaSeries series = new NChartAreaSeries();
            series.Brush = new NChartSolidColorBrush(Android.Graphics.Color.Orange);
            series.Brush.Opacity = 0.7f;
            series.BorderThickness = 2.0f;
            series.BorderBrush = new NChartSolidColorBrush(Android.Graphics.Color.Black);

            series.DataSource = this;
            nchartView.Chart.AddSeries(series);
        }

        public void DrawChart()
        {
            nchartView.Chart.UpdateData();
        }

        public void ClearChart()
        {
            nchartView.Chart.RemoveAllSeries();
            AddSeries();
            nchartView.Chart.UpdateData();
        }

        #region INChartSeriesDataSource

        // Get points for the series.
        public NChartPoint[] Points(NChartSeries series)
        {
            m2mPoints = viewModel.Points();

            if (m2mPoints == null || m2mPoints.Count == 0)
                return new NChartPoint[0];
            else
                return Convert2NChartPoints(m2mPoints, series);
        }

        // Get name of the series.
        public string Name(NChartSeries series)
        {
            return viewModel.Name();
        }

        public global::Android.Graphics.Bitmap Image(NChartSeries series)
        {
            return null;
        }

        #endregion

        #region INChartValueAxisDataSource

        public string Name(NChartValueAxis axis)
        {
            if (axis.Kind == NChartValueAxisKind.X)
                return "X Axis";
            else
                return "Y Axis";
        }

        public string DoubleToString(double value, NChartValueAxis axis)
        {
            return null;
        }

        public Java.Lang.Number Length(NChartValueAxis axis)
        {
            return null;
        }

        public Java.Lang.Number Max(NChartValueAxis axis)
        {
            if (axis.Kind == NChartValueAxisKind.X)
                return (Java.Lang.Number)maxX;
            else
                return (Java.Lang.Number)maxY;
        }

        public Java.Lang.Number Min(NChartValueAxis axis)
        {
            if (axis.Kind == NChartValueAxisKind.X)
                return (Java.Lang.Number)minX;
            else
                return (Java.Lang.Number)minY;
        }

        public Java.Lang.Number Step(NChartValueAxis axis)
        {
            return null;
        }

        public string[] Ticks(NChartValueAxis axis)
        {
            return null;
//
//			if (m2mPoints == null || m2mPoints.Count == 0)
//				return null;
//			
//			if (axis.Kind == NChartValueAxisKind.X)
//				return viewModel.Ticks ();
//			else
//				return null;		
        }

        #endregion

        #region NChartPoint

        private NChartPoint[] Convert2NChartPoints(List<Point> points, NChartSeries series)
        {
            minY = maxY = points[0].Y;
            NChartPoint[] result = new NChartPoint[points.Count];
            for (int i = 0; i < points.Count; ++i)
            {
                Point pi = points[i];

                if (minY > pi.Y)
                    minY = pi.Y;
                else if (maxY < pi.Y)
                    maxY = pi.Y;

                result[i] = new NChartPoint(NChartPointState.PointStateAlignedToXWithXY(pi.X, pi.Y), series);
            }

            minX = points[0].X;
            maxX = points.Last().X;

            return result;
        }

        #endregion
    }
}

