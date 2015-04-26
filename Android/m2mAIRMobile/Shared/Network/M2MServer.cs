using System;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Threading;

using Shared.Utils;
using Shared.Network.DataTransfer;

namespace Shared.Network
{
	public class M2MServer
	{
        public const string M2MUrlKey = "M2MServerUrl";
		private readonly string M2MServerUrl;
		private	readonly RestClient client;

		public M2MServer(string url)
		{
			if (String.IsNullOrEmpty (url) && !IsolatedStorageSettings.ApplicationSettings.TryGetValue (M2MUrlKey, out url)) 
			{
				url = "https://api.devicewise.com";  
			}
			M2MServerUrl = url;
			client = new RestClient(M2MServerUrl);
		}




		public async Task<RemoteResponse> PostAsync(string path, Dictionary<string, object> urlParams, Dictionary<string, object> bodyParams, CancellationToken token = default(CancellationToken))
		{
			try
			{
				var request = CreateRequest(path, urlParams, Method.POST, bodyParams);
				var response = await client.ExecuteTaskAsync(request, token);

				if (response.ErrorException != null)
				{
					throw response.ErrorException;
				}

				return new RemoteResponse (response.Content, 
											response.StatusCode, 
											response.StatusCode.ToString(), 
											response.StatusDescription);
			}
			catch (Exception e) 
			{
				Logger.Error (e.Message);
				return default (RemoteResponse);
			}
		}


		private RestRequest CreateRequest(string path, Dictionary<string, object> urlParams, Method httpMethod, Dictionary<string, object> bodyParams)
		{
			var request = new RestRequest(path, httpMethod);
			if (urlParams != null) 
			{
				foreach (var p in urlParams) 
				{
					request.AddParameter(p.Key, p.Value, ParameterType.UrlSegment);
				}
			}
			if (bodyParams != null)
			{
				foreach (var p in bodyParams) 
				{
					request.AddParameter(p.Key, p.Value);
				}
			}
			return request;
		}

	}
}