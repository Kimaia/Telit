using System;
using System.IO.IsolatedStorage;

namespace Shared.Utils
{
	public class Settings
	{
		private const string Registered = "Registered";
		private const string UserName = "UserName";
		private const string Password = "Password";
		private const string SessionId = "SessioId";

		private static readonly Settings instance = new Settings();

		public static Settings Instance { get { return instance; } }

		private readonly IsolatedStorageSettings settings;

		private Settings()
		{
			settings = IsolatedStorageSettings.ApplicationSettings;
		}

		public void SetRegistered(bool value)
		{
			settings[Registered] = value.ToString();
			settings.Save();
		}

		public string GetRegistered()
		{
			settings.TryGetValue(Registered, out string result);
			return result;
		}


		public bool IsRegistered()
		{
			return (GetRegistered () == true.ToString());
		}	

		public bool SetPassword(string pwd)
		{
			string password = string.Empty;
			if (!string.IsNullOrEmpty(pwd))
			{
				if (settings.TryGetValue(Password, out password) && string.Equals(password, pwd))
				{	//password exist and equal						
					return false;
				}
				//update token with new value
				settings[Password] = pwd;
				settings.Save();
				return true;
			}
			throw new Exception("Password null");
		}

		public string GetPassword()
		{
			settings.TryGetValue (Password, out string result);
			return result;
		}

		public void SetUserName(string value)
		{
			settings[UserName] = value;
			settings.Save();
		}

		public string GetUserName()
		{
			settings.TryGetValue(UserName, out string result);
			return result;
		}

		public void SetSessionId(string value)
		{
			settings[SessionId] = value;
			settings.Save();
		}

		public string GetSessionId()
		{
			settings.TryGetValue (SessionId, out string result);
			return result;
		}

		public object this[string key]
		{
			get {
				return settings[key];
			}
			set {
				settings[key] = value;
			}
		}
	}
}

