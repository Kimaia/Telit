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

		public static string CurrentTime()
		{
			DateTime dt = DateTime.Now;
			return dt.ToString ("s") + 'Z';
		}
	}
}

