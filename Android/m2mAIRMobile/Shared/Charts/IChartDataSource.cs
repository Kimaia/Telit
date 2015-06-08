using System;
using System.Collections.Generic;
using Android.Graphics;

namespace Shared.Charts
{
	public interface IChartDataSource
	{
		Android.Graphics.Bitmap 	Image ();
		string 						Name ();
		List<Point> 				Points ();
	}
}

