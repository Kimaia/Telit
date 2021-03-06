﻿using System;
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
	public class M2MApiService
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string ApiPath = "/api";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;
		private readonly TR50Converter Tr50Converter;

		public M2MApiService()
		{
			Tr50Converter = new TR50Converter();
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
		}


		// preform request to server
		public async Task<TR50Response<Type>> RequestListAsync<Type> (TR50Command command) where Type : ITR50HasPayload
		{
			var request = ConvertRequest(command);

			var response = await PostAsync (request);

			var converted = ConvertResponse<Type> (response);

			return converted ;
		}


		// preform request to server
		public async Task<Type> RequestItemAsync<Type> (TR50Command command) where Type : ITR50HasPayload
		{
			var request = ConvertRequest(command);

			var response = await PostAsync (request);

			var converted = ConvertResponse<Type> (response);

			var item = converted.Params;

			return item;
		}

		private async Task<RemoteResponse> PostAsync(TR50Request request)
		{
			var token = tokenSource.Token;
			var response = await server.PostAsync(ApiPath, request.body, token);
			if (!response.IsOkCode ())
				throw new RemoteReturnedNotOkException (response.StatusMessage);
			
			return response;
		}


		private TR50Request ConvertRequest(TR50Command command)
		{
			return Tr50Converter.ConvertRequest (command);
		}

		private TR50Response<Type> ConvertResponse<Type>(RemoteResponse response) where Type : ITR50HasPayload
		{
			Logger.Debug (response.Content);
			return Tr50Converter.ConvertResponse<Type> (response.Content);
		}
	}
}