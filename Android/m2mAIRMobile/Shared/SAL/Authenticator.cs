using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Network;
using Shared.Network.DataTransfer;

namespace Shared.SAL
{
	public class Authenticator
	{
		private readonly string M2MHost = "https://api.devicewise.com";
		private readonly string AuthPath = "/rest/auth";

		private readonly M2MServer server;
		private readonly CancellationTokenSource tokenSource;

		private static Authenticator instance;
		public static Authenticator Instance 
		{
			get {
				if (instance == null)
					instance = new Authenticator ();
				return instance; 
			}
		}
		private Authenticator()
		{
			server = new M2MServer (M2MHost);
			tokenSource = new CancellationTokenSource();
		}

		public M2MServer Server { get { return server; } }



	
		// Authenticate with server
		public async Task<RemoteResponse> AuthenticateAsync (string username, string password)
		{
			var token = tokenSource.Token;
			var bodyParams = new Dictionary<string, object> { { "username", "demo@devicewise.com" }, { "password", "demo123" }	};
//			var bodyParams = new Dictionary<string, object> { { "username", username }, { "password", password }	};

			var response = await server.PostAsync(AuthPath, null ,bodyParams, token);

			Logger.Debug ("AuthenticateAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}
	}
}

