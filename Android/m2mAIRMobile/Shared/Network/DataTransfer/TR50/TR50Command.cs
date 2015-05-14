using System;
using System.Collections.Generic;

namespace Shared.Network.DataTransfer.TR50
{
	public class TR50Command
	{
		public M2MCommands.CommandType 	CommandType;
		public string		 			Command  		{ get; set; }
		public CommandParams 			Params;

		public TR50Command (M2MCommands.CommandType type)
		{
			Init(type);
		}

		public TR50Command (M2MCommands.CommandType type, CommandParams Prms = null)
		{
			Init(type, Prms);
		}

		private void Init (M2MCommands.CommandType type, CommandParams Prms = null)
		{
			CommandType = type;
			Command = M2MCommands.Names[type];
			Params = Prms;
		}

		public string ToString()
		{
			return "Command: " + Command;
		}
	}

	public class CommandParams
	{
		public Dictionary<string, object> Params;

		public CommandParams ()
		{
			Params = null;
		}
	}

}

