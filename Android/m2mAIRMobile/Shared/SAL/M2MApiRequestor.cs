using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.Network;
using Shared.Network.DataTransfer;
using Shared.Network.DataTransfer.TR50;

namespace Shared.SAL
{
	public class M2MApiRequestor
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string ApiPath = "/api";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;
		private readonly TR50Serializer serializer;

		private TR50Command command;

		public M2MApiRequestor()
		{
			serializer = new TR50Serializer();
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
			command = null;
		}


		// preform request to server
		public async Task<List<Type>> RequestListAsync<Type> (TR50Command command)
		{
			this.command = command;

			var request = Serialize();

			var response = await PostAsync (request);

			var serialized = DeSerialize<Type> (response);

			var thingsList = BuildResult<Type> (serialized);

			return thingsList;
		}

		private async Task<RemoteResponse> PostAsync(TR50Request request)
		{
			var token = tokenSource.Token;
			var response = await server.PostAsync(ApiPath, request.body, token);
			Logger.Debug ("RequestAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}

		private TR50Request Serialize()
		{
			return serializer.Serialize (this.command);
		}

		private TR50Response<Type> DeSerialize<Type>(RemoteResponse response)
		{
			var m2mResponse = serializer.DeSerialize<Type> (response.Content);
			Logger.Debug ("DeSerialize() /n" + m2mResponse.Params.result.ToString ());
			return m2mResponse;
		}

		private List<Type> BuildResult<Type>(TR50Response<Type> m2mResponse)
		{
			return m2mResponse.Params.result;
		}
	}
}