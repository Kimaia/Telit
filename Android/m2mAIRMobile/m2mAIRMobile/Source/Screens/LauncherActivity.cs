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

		private LauncherViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			viewModel = new LauncherViewModel ();

			viewModel.UserNotRegistered += new EventHandler (PreRegister);
			viewModel.UserLoggedOut += new EventHandler (LoggedOut);
			viewModel.UserLoggedIn += new EventHandler (LoggedIn);

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
			var intent = new Intent(this, typeof(ThingsListActivity));
			intent.PutExtra (Shared.Model.Constants.LOGIN_STATE, 
				Shared.Model.Constants.User_Login_States.Login_State_LoggedIn.ToString());
			StartActivity(intent);
			Finish ();
		}
	}
}


