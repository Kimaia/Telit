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
		public const string TR50_PARAM_OFFSET = "offset";
		public const string TR50_PARAM_LIMIT = "limit";

		public const int TR50_PARAM_OFFSET_VALUE = 0;
		public const int TR50_PARAM_LIMIT_VALUE = 50;
		#endregion

		#region DATA_MODEL 
		public const string DATA_MODEL_THING_KEY_IDENTIFIER = "key";
		#endregion
	}
}

