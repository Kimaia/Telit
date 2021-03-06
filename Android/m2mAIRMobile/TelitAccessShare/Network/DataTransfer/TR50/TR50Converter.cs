﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Shared.Utils;


namespace Shared.Network.DataTransfer.TR50
{
	public class TR50ConversionException : Exception
	{
		public TR50ConversionException(string message) : base(message) {}
	}

	public class TR50ResponseFailureException : Exception
	{
		public TR50ResponseFailureException(string message) : base(message) {}
	}

	public class TR50Converter
	{
		private const string TR50FailureHeader = "\"success\": false";

		#region ConvertRequest
		public TR50Request ConvertRequest(TR50Command command)
		{
			var request = PrepareForSerialise (command);

			if (request == null)
				throw new TR50ConversionException ("Failed TR50 request convertion :" + command.ToString());

			Logger.Debug (JsonConvert.SerializeObject(request.body, Formatting.Indented));
			return request;
		}
			
		private TR50Request PrepareForSerialise(TR50Command command)
		{
			TR50Request request = new TR50Request ();
			request.body = new Dictionary<string, Dictionary<string, object>> ();

			// auth block
			request.body.Add ("auth", prepareAuthBlock().block);

			TR50RequestBlock b = prepareCommandBlock (command);
			request.body.Add ("1", b.block);
					
			return request;
		}

		private TR50RequestBlock prepareAuthBlock ()
		{
			Dictionary<string, object> dict = new Dictionary<string, object> ();
            dict.Add ("sessionId", Settings.Instance[Settings.SessionId]);
			return new TR50RequestBlock(dict);
		}

		private TR50RequestBlock prepareCommandBlock (TR50Command command)
		{
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			dict.Add ("command", command.Command);

			TR50RequestBlock p = prepareParamsBlock (command.Params);
			if (p != null)
				dict.Add ("params", p.block);

			return new TR50RequestBlock(dict);
		}

		private TR50RequestBlock prepareParamsBlock (CommandParams prms)
		{
			if (prms == null || prms.Params == null || prms.Params.Count == 0) {
				Logger.Debug ("prepareParamsBlock() no params.");
				return null;
			}
			return new TR50RequestBlock(prms.Params);
		}
		#endregion

		#region ConvertResponse
		public TR50Response<Type> ConvertResponse<Type>(string m2mresponse) where Type : ITR50HasPayload
		{
			try
			{
				var Response = new TR50Response<Type> ();
				JToken responseToken = JToken.Parse(m2mresponse);

				// check if failed for authentication
				if (responseToken.First.ToString().Equals(TR50FailureHeader))
				{
					var errorMessages = responseToken["errorMessages"].ToObject<List<string>> ();
					throw new TR50ResponseFailureException ("TR50 Response Failed :" + errorMessages.ToString());
				}

				JObject responseObject = responseToken ["1"].Value<JObject> ();

				JToken successToken = responseObject.First;
				Boolean isSuccess = successToken.ToObject<Boolean>();
				if (isSuccess)
				{
					Response = responseObject.ToObject<TR50Response<Type>> ();
					if (Response == null || Response.Params == null)
						throw new TR50ConversionException ("Failed TR50 response convertion :" + m2mresponse);
					return Response;
				}
				else
				{
					var errorMessages = responseObject["errorMessages"].ToObject<List<string>> ();
					throw new TR50ResponseFailureException ("TR50 Response Failed :" + errorMessages.ToString());
				}
			}
			catch (Exception e) 
			{
				throw new TR50ConversionException ("Failed TR50 response convertion :" + e.Message + ", raw response: " + m2mresponse);
			}
		}
		#endregion
	}
}