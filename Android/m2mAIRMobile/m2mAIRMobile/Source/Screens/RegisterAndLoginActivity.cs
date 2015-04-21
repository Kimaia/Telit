using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Shared.Utils;
using Shared.ViewModel;

namespace Android.Source.Screens
{
	[Activity (Label = "LoginActivity")]			
	public class RegisterAndLoginActivity : BaseActivity
	{

		private readonly RegisterAndLoginViewModel viewModel;

		private EditText username;
		private EditText password;

		public RegisterAndLoginActivity()
		{
			viewModel = new RegisterAndLoginViewModel ();

			viewModel.RegisterationSuccess += new EventHandler (RegisterationSuccess);
			viewModel.LoginSuccess += new EventHandler (LoginSuccess);
		}

		#region lifecycle
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.Main);

			username = FindViewById<EditText>(m2m.Android.Resource.Id.username);
			password = FindViewById<EditText>(m2m.Android.Resource.Id.password);
			Button login = FindViewById<Button> (m2m.Android.Resource.Id.login);

			// When the user clicks login, send a REST authentication request to the m2m server
			login.Click += (object sender, EventArgs e) => { OnLogin(); };
		}
		#endregion

		private void OnLogin()
		{
			viewModel.StartLogin(username.Text, password.Text, ShowDialog); 
		}

		public void RegisterationSuccess(object sender, EventArgs e)
		{
			Logger.Debug ("RegistrationSuccess()");
			var intent = new Intent(this, typeof(ThingsActivity));
			StartActivity(intent);
			Finish ();
		}

		public void LoginSuccess(object sender, EventArgs e)
		{
			Logger.Debug ("LoginSuccess()");
			var intent = new Intent(this, typeof(ThingsActivity));
			StartActivity(intent);
			Finish ();
		}

		public void ShowDialog(string title, string message, int errorCode, string dismiss)
		{
			Logger.Debug ("OnEror() Dialog: " + message);
		}
	}
}

