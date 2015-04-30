using System;
using System.Collections.Generic;

namespace Shared.Network.DataTransfer
{
	public class TR50Command
	{
		public string Command  		{ get; set; }
		public TR50Params Params;

		public TR50Command (string cmnd)
		{
			Command = cmnd;
			Params = null;
		}

		public TR50Command (string cmnd, TR50Params Prms)
		{
			Command = cmnd;
			Params = Prms;
		}
	}

	public class TR50Params
	{
		public Dictionary<string, object> Params;

		public TR50Params ()
		{
			Params = null;
		}
	}

}

