using System;
using System.Collections.Generic;
using Shared.Network.DataTransfer.TR50;

namespace Shared.Charts
{
	public enum Axis { X, Y };

	public interface IChartDataSource
	{
		List<TR50PropertyValue> 	Points ();
		string 						Name ();
	}
}

