using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Shared.Model;

using Shared.Utils;
using LockAndSafe;
using Android.Graphics;



namespace com.telit.lock_and_safe
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Icon = "@drawable/ic_launcher")]			
    public class RegisterAndLoginActivity : BaseActivity
    {

        private RegisterAndLoginModel viewModel;

        private EditText username;
        private EditText password;

        #region lifecycle

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            viewModel = new RegisterAndLoginModel();
            viewModel.LoginSuccess += new EventHandler(LoginSuccess);

            SetContentView(Resource.Layout.LoginLayout);

            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            Button login = FindViewById<Button>(Resource.Id.login);

            login.Click += (object sender, EventArgs e) =>
            {
                OnLogin();
            };
            
            
            
            
            
        }

        #endregion


        #region Event handlers

        private void OnLogin()
        {
            string user = username.Text;
            string pw = password.Text;
            #if !DEBUG
            if (!user.Equals("") && !pw.Equals(""))
            {
            #endif
            StartLoadingSpinner("Authenticating user credentials.");
            viewModel.StartLogin(username.Text, password.Text, OpenErrorDialog); 
            #if !DEBUG
            }
            #endif
        }

        public void LoginSuccess(object sender, EventArgs e)
        {
            Logger.Debug("LoginSuccess");
            var intent = new Intent(this, typeof(LocksListActivity));
            StartActivity(intent);
            Finish();
        }

        #endregion
    }
}

