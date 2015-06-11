using System;

using Android.App;
using Android.Content;
using Android.OS;

using Shared.ViewModel;
using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity (Label = "Mobile", MainLauncher = true, Icon = "@drawable/app_icon")]
	public class LauncherActivity : BaseActivity
	{

		private LauncherViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			viewModel = new LauncherViewModel ();

			if (viewModel.IsLoggedIn ())
				LoggedIn ();
			else
				LoggedOut ();
		}

		private void LoggedOut() 
		{
			Logger.Debug ("LoggedOut");
			var intent = new Intent(this, typeof(RegisterAndLoginActivity));
			StartActivity(intent);
			Finish ();
		}

		// 
		private void LoggedIn() 
		{
			Logger.Debug ("LoggedIn");
			var intent = new Intent(this, typeof(ThingsListActivity));
			StartActivity(intent);
			Finish ();
		}
	}
}


