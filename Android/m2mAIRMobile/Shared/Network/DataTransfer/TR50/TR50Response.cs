using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer.TR50
{
	public class TR50Response<ParamsType>
	{
		public bool			success;
		public ParamsType 	Params;
	}

	public class TR50ThingsListParams
	{
		public int 				count;
		public List<string> 	fields;
		public List<Thing> 		result;
	}
}

