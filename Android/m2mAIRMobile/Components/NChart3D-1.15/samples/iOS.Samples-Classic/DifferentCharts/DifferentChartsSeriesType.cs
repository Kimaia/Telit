using System;


using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
namespace DifferentCharts
{
	// Select chart type.
	public enum SeriesType
	{
		// 2D types.
		Column2D,
		Bar2D,
		Area2D,
		Pie2D,
		Doughnut2D,
		Line2D,
		Step2D,
		Bubble2D,
		Candlestick2D,
		OHLC2D,
		Band,               // Only in 2D
		Sequence,           // Only in 2D
		Radar,              // Only in 2D
		Funnel2D,
		Heatmap,           	// Only in 2D
		// 3D types.
		Column3D,
		Bar3D,
		Area3D,
		Pie3D,
		Doughnut3D,
		Line3D,
		Ribbon,             // Only in 3D
		Step3D,
		Bubble3D,
		Surface,            // Only in 3D
		Candlestick3D,
		OHLC3D,
		Funnel3D
	};
}

