using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;

namespace Shared.ViewModel
{
	public class LauncherViewModel
	{

		// events
		public event EventHandler UserNotRegistered;
		public event EventHandler UserLoggedOut;
		public event EventHandler UserLoggedIn;

		#if DEBUG
		private static bool firstLaunch = true;
		#endif

		public LauncherViewModel()
		{
			#if DEBUG
			if (firstLaunch)
			{
				Settings.Instance.SetRegistered (false);
				Settings.Instance.SetUserName (null);
				Settings.Instance.SetPassword ("1");
				Settings.Instance.SetSessionId (null);
				firstLaunch = false;
			}
			#endif
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

