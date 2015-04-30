using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.Network;
using Shared.Network.DataTransfer;
using Newtonsoft.Json;

namespace Shared.SAL
{
	public class M2MApiRequestor
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string ApiPath = "/api";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;

		private static M2MApiRequestor instance;
		public static M2MApiRequestor Instance 
		{
			get {
				if (instance == null)
					instance = new M2MApiRequestor ();
				return instance; 
			}
		}
		public M2MApiRequestor()
		{
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
		}


		// preform request to server
		public async Task<List<Thing>> RequestAsync (string command)
		{
			var token = tokenSource.Token;

			TR50Request request = BuildRequestBody ();
			Logger.Debug (JsonConvert.SerializeObject(request.body, Formatting.Indented));

			var response = await server.PostAsync(ApiPath, request.body, token);

			Logger.Debug ("RequestAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return null;
		}

		private TR50Request BuildRequestBody()
		{
			return TR50Serializer.Serialize (prepareCommands ());
		}

		public List<TR50Command> prepareCommands()
		{
			List<TR50Command> list = new List<TR50Command> ();
//			list.Add(new TR50Command ("things.list"));

			TR50Params prms = new TR50Params ();
			prms.Params = new Dictionary<string,object>();
			prms.Params.Add("offset", 0);
			prms.Params.Add("limit", 10);
			list.Add(new TR50Command ("thing.list", prms));
			return list;
		}
	}
}