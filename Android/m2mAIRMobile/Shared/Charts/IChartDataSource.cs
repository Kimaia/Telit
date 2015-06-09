using System;
using System.Collections.Generic;
using Android.Graphics;

namespace Shared.Charts
{
	public enum Axis { X, Y };

	public interface IChartDataSource
	{
		Android.Graphics.Bitmap 	Image (string key);
		string 						Name (string key);
		List<Point> 				Points (string key);
		string 						AxisName (string key, Axis axis);
	}
}

