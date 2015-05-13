using System;
using SQLite;

using Shared.DB;
using Shared.Model;
using Shared.Utils;


namespace Shared.ViewModel
{
	public class LauncherViewModel
	{

		// events
		public event EventHandler UserNotRegistered;
		public event EventHandler UserLoggedOut;
		public event EventHandler UserLoggedIn;

		public LauncherViewModel()
		{
			#if DEBUG
			Settings.Instance.SetSessionId(null);
			#endif

			InitDB ();
		}


		private void InitDB()
		{
			var db = Kimchi.Connection;
			try
			{
				db.CreateTableAsync<Thing>().Wait();

				#if DEBUG
				var dao = new Dao();
				dao.DeleteAll<Thing>();
				#endif
			}
			catch(Exception e)
			{
				Logger.Error("Failed to initialize db: " + e);
				throw;
			}
			finally
			{
				// do nothing
			}
		}

		public void GetLoginStatus ()
		{
			// get last Session Id
			if (Settings.Instance.GetSessionId () != null) 
			{
				// user Logged In
				Logger.Debug ("Logged In");
				this.UserLoggedIn (this, new EventArgs ());
			}
			else
			{
				if (Settings.Instance.GetUserName () != null) 
				{
					// user Logged Out
					Logger.Debug ("Logged Out");
					this.UserLoggedOut (this, new EventArgs ());
				}
				else
				{
					// Not Registered
					Logger.Debug ("User Not Registered");
					Settings.Instance.SetRegistered (false);
					this.UserNotRegistered (this, new EventArgs ());
				}
			}
		}
	}
}

