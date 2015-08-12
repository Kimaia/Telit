using System;

using Android.App;
using Android.Content;
using Android.OS;

using Shared.Utils;
using Shared.Model;

namespace com.telit.lock_and_safe
{
    [Activity(Label = "Lock & Safe", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class LauncherActivity : BaseActivity
    {

        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var viewModel = new LauncherModel();

            if (viewModel.IsLoggedIn())
                LoggedIn();
            else
                LoggedOut();
        }

        private void LoggedOut()
        {
            Logger.Debug("LoggedOut");
            var intent = new Intent(this, typeof(RegisterAndLoginActivity));
            StartActivity(intent);
            Finish();
        }

        //
        private void LoggedIn()
        {
            Logger.Debug("LoggedIn");
            var intent = new Intent(this, typeof(LocksListActivity));
            StartActivity(intent);
            Finish();
        }
    }
}


