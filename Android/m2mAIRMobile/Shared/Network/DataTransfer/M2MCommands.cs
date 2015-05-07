using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer
{
	public static class M2MCommands
	{
		
		public enum CommandType
		{
			UnHandled_Type,
			Thing_List, 
			Properties, 
			Alarms
		}

		public static readonly Dictionary<CommandType, string> Names = new Dictionary<CommandType, string> {
			{ CommandType.UnHandled_Type, null },
			{ CommandType.Thing_List, "thing.list" },
			{ CommandType.Properties, "Properties" },
			{ CommandType.Alarms, "Alarms" }
		};

		public static readonly Dictionary<CommandType, System.Type> Types = new Dictionary<CommandType, System.Type> {
			{ CommandType.UnHandled_Type, typeof(object) },
			{ CommandType.Thing_List, typeof(Thing) },
			{ CommandType.Properties, typeof(object) },
			{ CommandType.Alarms, typeof(object) }
		};

	}
}

