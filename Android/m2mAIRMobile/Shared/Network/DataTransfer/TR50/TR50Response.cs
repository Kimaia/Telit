using System;
using System.Collections.Generic;

namespace Shared.Network.DataTransfer.TR50
{
	public class TR50Response<ParamsType>
	{
		public bool			success;
		public ParamsType 	Params;
	}

	public class TR50ListParams<DataType>
	{
		public int 				count;
		public List<string> 	fields;
		public List<DataType> 	result;
	}
}

