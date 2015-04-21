using System;
using System.Text.RegularExpressions;

namespace Shared.Utils
{
	public static class TextUtils
	{
		public static bool ValidateEmail(string email) 
		{
			return !email.StartsWith("@") && email.Contains("@");
		}

	}
}

