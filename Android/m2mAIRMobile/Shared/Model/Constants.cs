using System;

namespace Shared.Model
{
	public static class Constants
	{
		#region VM_STATE
		public enum VM_States
		{
			VM_State_Register,
			VM_State_Login
		}
		public const string VM_STATE = "VM_State";
		public const VM_States VM_STATE_REGISTER = VM_States.VM_State_Register;
		public const VM_States VM_STATE_LOGIN = VM_States.VM_State_Login;
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

