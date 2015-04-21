using System;

namespace Shared.DB
{	
	public class DBManager
	{
		// singleton
		private static DBManager manager;
		private  DBManager () {}
		public static DBManager Manager
		{
			get 
			{
				if (manager == null)
					manager = new DBManager();
				return manager; 
			}
		}


		public enum LoginStat { LoggedIn, LoggedOut };

		public LoginStat LoginStatus()
		{
			return DBManager.LoginStat.LoggedOut;
		}

	}
}

