using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using Shared.Network;

namespace Shared.SAL
{
	public class Authenticator
	{
		public delegate void OnAuthenticationError(string title, string message, int code, string dismissCaption);

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
		public async Task AuthenticateAsync (string username, string password, OnAuthenticationError OnError, Action<UserDetails> onSuccess)
		{
			var token = tokenSource.Token;
//			var bodyParams = new Dictionary<string, object> { { "username", "demo@devicewise.com" }, { "password", "demo123" }	};
			var bodyParams = new Dictionary<string, object> { { "username", username }, { "password", password }	};

			var response = await server.PostAsync<string>(AuthPath, null ,bodyParams, token);

			Logger.Debug ("AuthenticateAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			if (response.IsOkResponse())
			{
				onSuccess(new UserDetails (username, password, response.StatusMessage));
			}
			else
			{
				OnError("AuthenticationFailed", response.StatusMessage, 0x222D2A, "Ok");
			}
		}
	}
}

