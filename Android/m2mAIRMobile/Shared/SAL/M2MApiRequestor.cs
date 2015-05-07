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
		private readonly TR50ObjectConverter Tr50Converter;

		public M2MApiRequestor()
		{
			Tr50Converter = new TR50ObjectConverter();
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
		}


		// preform request to server
		public async Task<List<Type>> RequestListAsync<Type> (TR50Command command)
		{
			var request = ConvertRequest(command);

			var response = await PostAsync (request);

			var converted = ConvertResponse<Type> (response);

			var thingsList = BuildResult<Type> (converted);

			return thingsList;
		}



		private TR50Request ConvertRequest(TR50Command command)
		{
			return Tr50Converter.ConvertRequest (command);
		}

		private async Task<RemoteResponse> PostAsync(TR50Request request)
		{
			var token = tokenSource.Token;
			var response = await server.PostAsync(ApiPath, request.body, token);
			Logger.Debug ("PostAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}

		private TR50Response<Type> ConvertResponse<Type>(RemoteResponse response)
		{
			return Tr50Converter.ConvertResponse<Type> (response.Content);
		}

		private List<Type> BuildResult<Type>(TR50Response<Type> m2mResponse)
		{
			return m2mResponse.Params.result;
		}
	}
}