using System;
using System.Collections.Generic;

using Shared.Utils;

using Newtonsoft.Json;

namespace Shared.Network.DataTransfer
{
	public class authHeader
	{
		public string sessionId { get; set; }
	}

	public class Block
	{
		public string 					 command 	{ get; set; }
		public Dictionary<string,string> Params;	


		public Block (string cmnd, Dictionary<string,string> prms)
		{
			command = cmnd;
			Params = prms;
		}
	}

	public class TR50Request
	{

		public authHeader auth;
		public Dictionary<string,Block> commands;
	}





	public class tester
	{
		public void test()
		{
			var req = make ();
			var strConverted = convert (req);
			Logger.Debug ("converted JSON():" + strConverted);
		}

		public TR50Request make()
		{
			TR50Request req = new TR50Request ();

			req.auth = new authHeader ();
			req.auth.sessionId = "12345";

			req.commands = new Dictionary<string,Block>();
			req.commands.Add("1", new Block("things.list", null));


			Dictionary<string,string> prms = new Dictionary<string,string> ();
			prms.Add("thingKey", "mything");
			prms.Add("key", "myalarm");
			prms.Add("last", "24h");
			req.commands.Add("2", new Block("alarm.summary", prms));
			return req;
		}

		public string convert(TR50Request req)
		{
			return JsonConvert.SerializeObject(req, Formatting.Indented);
		}
	}

}
//
//
//{
//	"auth" : {
//		"sessionId" : "<insert sessionid here>"
//	},
//	"1" : {
//		"command" : "<insert command here>",
//		"params" : {
//			<command specific parameters here>
//		}
//	},
//	"2" : {
//		"command" : "<insert command here>",
//		"params" : {
//			<command specific parameters here>
//		}
//	}
//}