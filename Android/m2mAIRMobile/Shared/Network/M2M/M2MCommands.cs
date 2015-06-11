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
			Thing_Def_Find, 
			Property_History,
			Property_Aggregate,
			Location_History
		}

		public static readonly Dictionary<CommandType, string> Names = new Dictionary<CommandType, string> {
			{ CommandType.UnDefined_Type, null },
			{ CommandType.Thing_List, "thing.list" },
			{ CommandType.Thing_Find, "thing.find" },
			{ CommandType.Thing_Def_Find, "thing_def.find" },
			{ CommandType.Property_History, "property.history" },
			{ CommandType.Property_Aggregate, "property.aggregate" },
			{ CommandType.Location_History, "location.history" }
		};

	}
}

