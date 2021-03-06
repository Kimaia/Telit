﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Network;
using Shared.Network.DataTransfer;

namespace Shared.SAL
{
	public class M2MAuthService
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string AuthPath = "/rest/auth";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;

		private static M2MAuthService instance;
		public static M2MAuthService Instance 
		{
			get {
				if (instance == null)
					instance = new M2MAuthService ();
				return instance; 
			}
		}
		private M2MAuthService()
		{
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
		}

	
		// Authenticate with server
		public async Task<RemoteResponse> AuthenticateAsync (string username, string password)
		{
			var token = tokenSource.Token;

			var bodyParams = new Dictionary<string, object> { { "username", username }, { "password", password }	};

			var response = await server.PostAsync(AuthPath, null ,bodyParams, token);

			Logger.Debug ("AuthenticateAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}
	}
}

