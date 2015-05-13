using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network
{
	public static class M2MCommands
	{
		
		public enum CommandType
		{
			UnDefined_Type,
			Thing_List, 
			Thing_Find, 
			Alarms
		}

		public static readonly Dictionary<CommandType, string> Names = new Dictionary<CommandType, string> {
			{ CommandType.UnDefined_Type, null },
			{ CommandType.Thing_List, "thing.list" },
			{ CommandType.Thing_Find, "thing.find" },
			{ CommandType.Alarms, "Alarms" }
		};

	}
}

