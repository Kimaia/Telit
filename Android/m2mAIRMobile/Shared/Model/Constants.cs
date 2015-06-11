using System;

namespace Shared.Model
{
	public static class Constants
	{
		#region TR50
		public const string TR50_PARAM_KEY = "key";
		public const string TR50_PARAM_THINGKEY = "thingKey";
		public const string TR50_PARAM_OFFSET = "offset";
		public const string TR50_PARAM_LIMIT = "limit";
		public const string TR50_PARAM_START = "start";
		public const string TR50_PARAM_END = "end";
		public const string TR50_PARAM_LAST = "last";
		public const string TR50_PARAM_RECORDS = "records";

		public const int 	TR50_PARAM_OFFSET_VALUE = 0;
		public const int 	TR50_PARAM_LIMIT_VALUE = 50;
		public const string TR50_PARAM_LAST_PERIOD_VALUE = "24h";
		public const int 	TR50_PARAM_RECORDS_VALUE = 50;
		#endregion

		#region DATA_MODEL 
		public const string DATA_MODEL_THING_KEY_IDENTIFIER = "thing_key";
		public const string DATA_MODEL_PROPERTY_NAME_IDENTIFIER = "property_name";
		#endregion
	}
}

