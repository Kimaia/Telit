using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Shared.Utils;
using Shared.Model;
using Newtonsoft.Json.Linq;

namespace Shared.Network.DataTransfer
{
	#region TR50 Mid Classes
	public class TR50Block
	{
		public Dictionary<string,object> block;

		public TR50Block(Dictionary<string,object> dict)
		{
			block = dict;
		}
	}

	public class TR50Request
	{
		public Dictionary<string, Dictionary<string,object>> body;
	}
	#endregion

	public class TR50Serializer
	{
		#region Serialize
		public TR50Request Serialize(List<TR50Command> commands)
		{
			var request = PrepareForSerialise (commands);
			Logger.Debug (JsonConvert.SerializeObject(request.body, Formatting.Indented));
			return request;
		}
			
		private TR50Request PrepareForSerialise(List<TR50Command> commands)
		{
			TR50Request request = new TR50Request ();
			request.body = new Dictionary<string, Dictionary<string, object>> ();

			// auth block
			request.body.Add ("auth", prepareAuthBlock().block);

			int indexer = 1;
			foreach (TR50Command command in commands) 
			{
				TR50Block b = prepareCommandBlock (command);
				request.body.Add (indexer++.ToString (), b.block);
			}
					
			return request;
		}

		private TR50Block prepareAuthBlock ()
		{
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			dict.Add ("sessionId", Settings.Instance.GetSessionId());
			return new TR50Block(dict);
		}

		private TR50Block prepareCommandBlock (TR50Command command)
		{
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			dict.Add ("command", command.Command);

			TR50Block p = prepareParamsBlock (command.Params);
			if (p != null)
				dict.Add ("params", p.block);

			return new TR50Block(dict);
		}

		private TR50Block prepareParamsBlock (TR50Params prms)
		{
			if (prms == null || prms.Params == null || prms.Params.Count == 0) {
				Logger.Debug ("prepareParamsBlock() no params.");
				return null;
			}
			return new TR50Block(prms.Params);
		}
		#endregion

		#region DeSerialize
		public List<Thing> DeSerialize(List<TR50Command> commands, string response)
		{
			List<Thing> result = null;
			try 
			{
//				var jResponse = JObject.Parse(response);
//
//				result = JsonConvert.DeserializeObject<List<Thing>>(response);
				Logger.Debug (result.ToString ());
			}
			catch (Exception e) 
			{
				Logger.Error (e.Message);
				return null;
			}
			return result;
		}
		#endregion
	}

	#region tester 
	public class SerializeTester
	{
		public void test()
		{
			var comnds = prepareCommands ();
			TR50Serializer serializer = new TR50Serializer ();
			var str = serializer.Serialize (comnds);
			Logger.Debug ("converted JSON():\n" + str);
		}

		public List<TR50Command> prepareCommands()
		{
			List<TR50Command> list = new List<TR50Command> ();
			list.Add(new TR50Command ("thing.list"));

			TR50Params prms = new TR50Params ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add("thingKey", "mything");
			prms.Params.Add("key", "myalarm");
			prms.Params.Add("last", "24h");
			list.Add(new TR50Command ("alarms", prms));
			return list;
		}
	}
	#endregion
}