﻿using System;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Threading;

using Shared.Utils;
using Shared.Network.DataTransfer;

namespace Shared.Network
{
	public class RemoteReturnedNotOkException : Exception
	{
		public RemoteReturnedNotOkException(string message) : base(message) {}
		public static RemoteReturnedNotOkException FromResponse(RemoteResponse response)
		{
			return new RemoteReturnedNotOkException(response.StatusMessage);
		}
	}

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
			var request = CreateRequest(path, urlParams, Method.POST, bodyParams);
			var response = await client.ExecuteTaskAsync(request, token);

			if (response.ErrorException != null)
			{
				Logger.Error (response.ErrorException.Message);
				throw response.ErrorException;
			}

			return new RemoteResponse (response.Content, 
										response.StatusCode, 
										response.StatusCode.ToString(), 
										response.StatusDescription);
		}


		public async Task<RemoteResponse> PostAsync(string path, object body, CancellationToken token = default(CancellationToken))
		{
			var request = CreateRequest(path, Method.POST, body);
			var response = await client.ExecuteTaskAsync(request, token);

			if (response.ErrorException != null)
			{
				Logger.Error (response.ErrorException.Message);
				throw response.ErrorException;
			}

			return new RemoteResponse (response.Content, 
				response.StatusCode, 
				response.StatusCode.ToString(), 
				response.StatusDescription);
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

		private RestRequest CreateRequest(string path, Method httpMethod, object body)
		{
			var request = new RestRequest(path, httpMethod);
			if (body != null) 
			{
				request.RequestFormat = DataFormat.Json;
				request.JsonSerializer = new RestSharpJsonNetSerializer();
				request.AddBody (body);
			}

			return request;
		}
	}
}