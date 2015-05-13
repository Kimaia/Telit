using System;
using System.Collections.Generic;

namespace Shared.Network.DataTransfer.TR50
{
	public class TR50Request
	{
		public Dictionary<string, Dictionary<string,object>> body;
	}

	public class TR50RequestBlock
	{
		public Dictionary<string,object> block;

		public TR50RequestBlock(Dictionary<string,object> dict)
		{
			block = dict;
		}
	}

}

