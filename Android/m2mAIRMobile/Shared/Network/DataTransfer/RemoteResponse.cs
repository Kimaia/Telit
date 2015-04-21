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
}