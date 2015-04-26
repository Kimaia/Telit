using System;

namespace Shared.Network.DataTransfer
{
	public class RemoteResponse<TResult>
	{
		public TResult Result 		{ set; get; }
		public string StatusCode 	{ set; get; }
		public string StatusMessage { set; get; }

		public bool IsOkResponse()
		{
			return StatusCode.Equals ("OK");
		}
	}

	public class RemoteResponse
	{
		public string Content	 							{ set; get; }
		public System.Net.HttpStatusCode  HttpStatusCode 	{ set; get; }
		public string StatusCode 							{ set; get; }
		public string StatusMessage 						{ set; get; }

		public RemoteResponse (string content, System.Net.HttpStatusCode  httpCode, string code, string msg)
		{
			Content = content;
			HttpStatusCode = httpCode;
			StatusCode = code;
			StatusMessage = msg;
		}

		public bool IsOkCode()
		{
			return HttpStatusCode == System.Net.HttpStatusCode.OK;
		}

		public bool IsOkResponse()
		{
			return StatusCode.Equals ("OK");
		}
	}


}