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
//	public class RemoteReturnedNotOkException : Exception
//	{
//		public RemoteReturnedNotOkException(string message) : base(message) {}
//		public static RemoteReturnedNotOkException FromResponse<T>(RemoteResponse<T> response)
//		{
//			return new RemoteReturnedNotOkException(response.StatusMessage);
//		}
//	}

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

		public async Task<RemoteResponse<TResult>> PostAsync<TResult>(string path, Dictionary<string, object> urlParams, Dictionary<string, object> bodyParams, CancellationToken token = default(CancellationToken))
		{
			try
			{
				var request = CreateRequest(path, urlParams, Method.POST, bodyParams);
				var response = await client.ExecuteTaskAsync(request, token);

				if (response.ErrorException != null)
				{
					throw response.ErrorException;
				}
				RemoteResponse<TResult> retVal = new RemoteResponse<TResult>();
//				retVal.Result = response.Content;
				retVal.StatusCode = response.StatusCode.ToString();
				retVal.StatusMessage = response.Content;
				return retVal;
			}
			catch (Exception e) 
			{
				Logger.Error (e.Message);
				return null;
			}
		}

		protected virtual RestRequest CreateRequest(string path, Dictionary<string, object> urlParams, Method httpMethod, Dictionary<string, object> bodyParams)
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


//		private static readonly Action<RestRequest, object> AddBody = (r, b) =>
//        {
//            r.RequestFormat = DataFormat.Json;
//            if (b != null) r.AddBody(b);
//        };
//
//		public RemoteResponse<TResult> Get<TResult>(string path, Dictionary<string, object> urlParams = null)
//		{
//            var request = CreateRequest(path, urlParams, Method.GET);
//			var watch = Stopwatch.StartNew();
//			IRestResponse<RemoteResponse<TResult>> response = client.Execute<RemoteResponse<TResult>>(request);
//			watch.Stop();
//			Logger.Debug(String.Format("GET '{0}' request took {1} ms", path, watch.ElapsedMilliseconds));
//
//			if (response.ErrorException != null) 
//			{
//				Console.WriteLine("Error on GET: " + response.ErrorException);
//				throw response.ErrorException;
//			}
//			return response.Data;
//		}
//
//        public async Task<RemoteResponse<TResult>> GetAsync<TResult>(string path, Dictionary<string, object> urlParams = null, CancellationToken token = default(CancellationToken))
//		{
//			var request = CreateRequest(path, urlParams, Method.GET);
//			var watch = Stopwatch.StartNew();
//			IRestResponse<RemoteResponse<TResult>> response = await client.ExecuteTaskAsync<RemoteResponse<TResult>>(request, token);
//			watch.Stop();
//			Logger.Debug(String.Format("GET '{0}' request took {1} ms", path, watch.ElapsedMilliseconds));
//
//			if (response.ErrorException != null)
//			{
//				throw response.ErrorException;
//			}
//			return response.Data;
//		}
//
//		public RemoteResponse<TResult> PostAsync<TResult>(string path, Dictionary<string, object> urlParams, object body)
//		{
//			var request = CreateRequest(path, urlParams, Method.POST, r => AddBody(r, body));
//			var watch = Stopwatch.StartNew();
//			IRestResponse<RemoteResponse<TResult>> response = client.Execute<RemoteResponse<TResult>>(request);
//			watch.Stop();
//			Logger.Debug(String.Format("POST '{0}' request took {1} ms", path, watch.ElapsedMilliseconds));
//
//			if (response.ErrorException != null)
//			{
//				throw response.ErrorException;
//			}
//			return response.Data;
//		}
//
//		public async Task<RemoteResponse<TResult>> PostAsync<TResult>(string path, Dictionary<string, object> urlParams, object body, CancellationToken token = default(CancellationToken))
//		{
//			return await PostAsync<TResult>(path, urlParams, r=>AddBody(r, body), token);
//		}
//
//		public async Task<RemoteResponse<TResult>> PostAsync<TResult>(string path, Dictionary<string, object> urlParams, Action<RestRequest> prepareRequest, CancellationToken token = default(CancellationToken))
//		{
//			var request = CreateRequest(path, urlParams, Method.POST, prepareRequest);
//			var watch = Stopwatch.StartNew();
//			IRestResponse<RemoteResponse<TResult>> response = await client.ExecuteTaskAsync<RemoteResponse<TResult>>(request, token);           
//			watch.Stop();
//			token.ThrowIfCancellationRequested();
//			Logger.Debug(String.Format("POST '{0}' request took {1} ms", path, watch.ElapsedMilliseconds));
//
//			if (response.ErrorException != null)
//			{
//				throw response.ErrorException;
//			}
//			return response.Data;
//		}
//
//		protected virtual RestRequest CreateRequest(string path, Dictionary<string, object> urlParams, Method httpMethod, Action<RestRequest> prepareRequest = null)
//		{
//			var request = new RestRequest(path, httpMethod);
//			if (urlParams != null) 
//			{
//				foreach (var p in urlParams) 
//				{
//					request.AddParameter(p.Key, p.Value, ParameterType.UrlSegment);
//				}
//			}
//			if (prepareRequest != null)
//			{
//				prepareRequest(request);
//			}
//			return request;
//		}
	}
}