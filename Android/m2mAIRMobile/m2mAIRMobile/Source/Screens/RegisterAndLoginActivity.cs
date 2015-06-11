using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Shared.Utils;
using Shared.ViewModel;

namespace Android.Source.Screens
{
	[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class RegisterAndLoginActivity : BaseActivity
	{

		private RegisterAndLoginViewModel viewModel;

		private EditText username;
		private EditText password;

		#region lifecycle
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			viewModel = new RegisterAndLoginViewModel ();
			viewModel.LoginSuccess += new EventHandler (LoginSuccess);

			SetContentView (m2m.Android.Resource.Layout.activity_login);

			username = FindViewById<EditText>(m2m.Android.Resource.Id.username);
			password = FindViewById<EditText>(m2m.Android.Resource.Id.password);
			Button login = FindViewById<Button> (m2m.Android.Resource.Id.login);

			login.Click += (object sender, EventArgs e) => { OnLogin(); };
		}
		#endregion


		#region Event handlers
		private void OnLogin()
		{
			StartLoadingSpinner("Authenticating user credentials.");
			viewModel.StartLogin(username.Text, password.Text, ShowDialog); 
		}

		public void LoginSuccess(object sender, EventArgs e)
		{
			var intent = new Intent(this, typeof(ThingsListActivity));
			StartActivity(intent);
			Finish ();
		}
		#endregion
	}
}

