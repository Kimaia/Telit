using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer
{
	public static class M2MCommands
	{
		
		public enum CommandType
		{
			UnDefined_Type,
			Thing_List, 
			Properties, 
			Alarms
		}

		public static readonly Dictionary<CommandType, string> Names = new Dictionary<CommandType, string> {
			{ CommandType.UnDefined_Type, null },
			{ CommandType.Thing_List, "thing.list" },
			{ CommandType.Properties, "Properties" },
			{ CommandType.Alarms, "Alarms" }
		};

	}
}

