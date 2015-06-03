using System;

namespace Shared.Model
{
	public static class Constants
	{
		#region LOGIN_STATE
		public enum User_Login_States
		{
			Login_State_Register,
			Login_State_LoggedIn
		}
		public const string LOGIN_STATE = "Login_State";
		public const User_Login_States VM_STATE_REGISTER = User_Login_States.Login_State_Register;
		public const User_Login_States VM_STATE_LOGIN = User_Login_States.Login_State_LoggedIn;
		#endregion

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

