using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Shared.Utils;

namespace Shared.ViewModel
{
	public class LauncherViewModel : BaseViewModel
	{

		// events
		public event EventHandler UserNotRegistered;
		public event EventHandler UserLoggedOut;
		public event EventHandler UserLoggedIn;

		public LauncherViewModel ()
		{
		}

		public void GetLoginStatus ()
		{
			// get last Session Id
			string sessionid = Settings.Instance.GetSessionId ();
			string username = Settings.Instance.GetUserName ();
			if (sessionid == null) {
				if (username == null) {
					// Not Registered
					Logger.Debug ("Not Registered");
					Settings.Instance.SetRegistered (false);
					this.UserNotRegistered (this, new EventArgs ());
				} else {
					// user Logged Out
					Logger.Debug ("Logged Out");
					this.UserLoggedOut (this, new EventArgs ());
				}
			} else {
				// user Logged In
				Logger.Debug ("Logged In");
				this.UserLoggedIn (this, new EventArgs ());
			}
		}

	}
}

