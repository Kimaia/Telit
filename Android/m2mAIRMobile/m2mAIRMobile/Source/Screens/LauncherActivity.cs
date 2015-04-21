using System;

using Android.App;
using Android.Content;
using Android.OS;

using Shared.ViewModel;
using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity (Label = "m2mAIRMobile", MainLauncher = true, Icon = "@drawable/icon")]
	public class LauncherActivity : BaseActivity
	{

		private readonly LauncherViewModel viewModel;

		public LauncherActivity()
		{
			viewModel = new LauncherViewModel ();
		}


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			viewModel.UserNotRegistered += new EventHandler (PreRegister);
			viewModel.UserLoggedOut += new EventHandler (LoggedOut);
			viewModel.UserLoggedIn += new EventHandler (LoggedIn);


			#if DEBUG
			Settings.Instance.SetRegistered (false);
			Settings.Instance.SetUserName (null);
			Settings.Instance.SetPassword ("1");
			Settings.Instance.SetSessionId (null);
			#endif

			viewModel.GetLoginStatus ();

		}

		// 
		private void PreRegister(object sender, EventArgs e) 
		{
			Logger.Debug ("PreRegister()");
			var intent = new Intent(this, typeof(RegisterAndLoginActivity));
			StartActivity(intent);
			Finish ();
		}

		// 
		private void LoggedOut(object sender, EventArgs e) 
		{
			Logger.Debug ("LoggedOut");
			var intent = new Intent(this, typeof(RegisterAndLoginActivity));
			StartActivity(intent);
			Finish ();
		}

		// 
		private void LoggedIn(object sender, EventArgs e) 
		{
			Logger.Debug ("LoggedIn");
			var intent = new Intent(this, typeof(ThingsActivity));
			StartActivity(intent);
			Finish ();
		}
	}
}


