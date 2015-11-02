using System;
using System.IO.IsolatedStorage;

namespace Shared.Utils
{
	public class Settings
	{
        public static readonly string SessionId = "SessioId";
        public static readonly string UserId    = "UserId";
        public static readonly string UserPw    = "UserPw";
        
        

		private static readonly Settings instance = new Settings();
		public static Settings Instance { get { return instance; } }
		private readonly IsolatedStorageSettings settings;
		private Settings()
		{
			settings = IsolatedStorageSettings.ApplicationSettings;
		}


//		public void SetSessionId(string value)
//		{
//			settings[SessionId] = value;
//			settings.Save();
//		}
//
//		public string GetSessionId()
//		{
//			string result = null;
//			settings.TryGetValue (SessionId, out result);
//			return result;
//		}

		public object this[string key]
		{
			get {
				return settings[key];
			}
			set {
				settings[key] = value;
                settings.Save();
			}
		}
	}
}

