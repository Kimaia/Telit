using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.Network;
using Shared.Network.DataTransfer;

namespace Shared.SAL
{
	public class M2MApiRequestor
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string ApiPath = "/api";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;
		private readonly TR50Serializer serializer;

		private List<TR50Command> commands;

		public M2MApiRequestor()
		{
			serializer = new TR50Serializer();
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
			commands = null;
		}


		// preform request to server
		public async Task<List<Thing>> RequestAsync (List<TR50Command> commands)
		{
			this.commands = commands;

			var request = Serialize();

			var response = await PostAsync (request);

			var thingsList = DeSerialize (response);

			return thingsList;
		}

		private TR50Request Serialize()
		{
			return serializer.Serialize (this.commands);
		}

		private async Task<RemoteResponse> PostAsync(TR50Request request)
		{
			var token = tokenSource.Token;
			var response = await server.PostAsync(ApiPath, request.body, token);
			Logger.Debug ("RequestAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}

		private List<Thing> DeSerialize(RemoteResponse response)
		{
			var result = serializer.DeSerialize (this.commands, response.Content);
			return result;
		}
	}
}