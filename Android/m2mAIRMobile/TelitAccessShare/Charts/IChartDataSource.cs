using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shared.Charts
{
	public enum Axis { X, Y };

	public interface IChartDataSource
	{
		List<Point> 	Points ();
		string 			Name ();
		string[] 		Ticks ();
	}
}

