using System;
using System.Collections.Generic;

namespace Shared.Network.DataTransfer.TR50
{
	public static class Constants
	{
		public const string TR50_PARAM_KEY = "key";
		public const string TR50_PARAM_DEFKEY = "key";
		public const string TR50_PARAM_THINGKEY = "thingKey";
		public const string TR50_PARAM_OFFSET = "offset";
		public const string TR50_PARAM_LIMIT = "limit";
		public const string TR50_PARAM_START = "start";
		public const string TR50_PARAM_END = "end";
		public const string TR50_PARAM_LAST = "last";
		public const string TR50_PARAM_RECORDS = "records";
		public const string TR50_PARAM_TS = "ts";

		public const int 	TR50_PARAM_OFFSET_VALUE = 0;
		public const int 	TR50_PARAM_LIMIT_VALUE = 50;
		public const string TR50_PARAM_LAST_PERIOD_24h = "24h";
		public const string TR50_PARAM_LAST_PERIOD_1h = "1h";
		public const int 	TR50_PARAM_RECORDS_VALUE = 50;
	}

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

		public override string ToString()
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

	public static class TR50CommandFactory
	{
		public static TR50Command Build(M2MCommands.CommandType command, string key1, string key2 = null)
		{
			CommandParams prms = new CommandParams ();
			prms.Params = new Dictionary<string,object> ();

			switch (command) {
			case M2MCommands.CommandType.Thing_List:
				prms.Params.Add (Constants.TR50_PARAM_OFFSET, Constants.TR50_PARAM_OFFSET_VALUE);
				prms.Params.Add (Constants.TR50_PARAM_LIMIT, Constants.TR50_PARAM_LIMIT_VALUE);
				break;
			case M2MCommands.CommandType.Thing_Find:
				prms.Params.Add (Constants.TR50_PARAM_KEY, key1);
				break;
			case M2MCommands.CommandType.Location_History:
				prms.Params.Add (Constants.TR50_PARAM_THINGKEY, key1);
				prms.Params.Add (Constants.TR50_PARAM_RECORDS, 20);
				break;
			case M2MCommands.CommandType.Property_History:
				prms.Params.Add (Constants.TR50_PARAM_THINGKEY, key1);
				prms.Params.Add (Constants.TR50_PARAM_KEY, key2);
				prms.Params.Add (Constants.TR50_PARAM_RECORDS, Constants.TR50_PARAM_RECORDS_VALUE);
				break;
			case M2MCommands.CommandType.Property_Current:
				prms.Params.Add (Constants.TR50_PARAM_THINGKEY, key1);
				prms.Params.Add (Constants.TR50_PARAM_KEY, key2);
				prms.Params.Add (Constants.TR50_PARAM_TS, CurrentTime());
				break;
			case M2MCommands.CommandType.Thing_Def_Find:
				prms.Params.Add (Constants.TR50_PARAM_DEFKEY, key1);
				break;
			default:
				throw new InvalidOperationException ("Wrong M2M CommandType:" + command);
			}
			return new TR50Command (command, prms);
		}

		private static string CurrentTime()
		{
			DateTime dt = DateTime.Now;
			return dt.ToString ("s") + 'Z';
		}
	}

}

