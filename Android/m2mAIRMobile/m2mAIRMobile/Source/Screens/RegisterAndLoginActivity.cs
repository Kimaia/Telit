using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Shared.Utils;
using Shared.ViewModel;

namespace Android.Source.Screens
{
	[Activity]			
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
			viewModel.RegisterationSuccess += new EventHandler (RegisterationSuccess);
			viewModel.LoginSuccess += new EventHandler (LoginSuccess);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.Main);

			username = FindViewById<EditText>(m2m.Android.Resource.Id.username);
			password = FindViewById<EditText>(m2m.Android.Resource.Id.password);
			Button login = FindViewById<Button> (m2m.Android.Resource.Id.login);

			// When the user clicks login, send a REST authentication request to the m2m server
			login.Click += (object sender, EventArgs e) => { OnLogin(); };
		}
		#endregion


		#region Callbacks
		private void OnError(string title, string message, int errorCode, string dismiss)
		{
			StopLoadingSpinner ();
			Logger.Error ("OnEror() Dialog: " + message + ", error dode: " + errorCode);
			OpenErrorDialog (message, errorCode);
		}
		#endregion

		#region Event handlers
		private void OnLogin()
		{
			StartLoadingSpinner("Authenticating user credentials.");
			viewModel.StartLogin(username.Text, password.Text, OnError); 
		}

		public void RegisterationSuccess(object sender, EventArgs e)
		{
			Logger.Debug ("RegistrationSuccess()");
			var intent = new Intent(this, typeof(ThingsListActivity));
			intent.PutExtra (Shared.Model.Constants.VM_STATE, 
								Shared.Model.Constants.VM_States.VM_State_Register.ToString());
			StartActivity(intent);
			Finish ();
		}

		public void LoginSuccess(object sender, EventArgs e)
		{
			Logger.Debug ("LoginSuccess()");
			var intent = new Intent(this, typeof(ThingsListActivity));
			intent.PutExtra (Shared.Model.Constants.VM_STATE, 
								Shared.Model.Constants.VM_States.VM_State_Login.ToString());
			StartActivity(intent);
			Finish ();
		}
		#endregion
	}
}

