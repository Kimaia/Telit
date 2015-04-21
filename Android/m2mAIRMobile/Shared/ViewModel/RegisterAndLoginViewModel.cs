using System;
using System.Threading.Tasks;

using Shared.SAL;
using Shared.Utils;
using Shared.Model;

namespace Shared.ViewModel
{
	public class RegisterAndLoginViewModel : BaseViewModel
	{

		// events
		public event EventHandler RegisterationSuccess;
		public event EventHandler LoginSuccess;

		private Authenticator authenticator;

		public RegisterAndLoginViewModel ()
		{
			authenticator = Authenticator.Instance;
		}

		public delegate void OnError(string title, string message, int code, string dismissCaption);


		public void StartLogin (string username, string password, OnError onError)
		{
			Task.Run (async () => {
				await StartLoginAsync (username, password, onError);
			});
		}

		async Task StartLoginAsync (string username, string password, OnError onError)
		{
			Logger.Debug ("StartRegistration(),  User: " + username + ", password: " + password);
			if (!VerifyEmail (username)) {
				onError ("Invalid UserName", "The username you entered is not a valid email", 0x222D2A, "Ok");
				return;
			}
			else {
				try {
					await authenticator.AuthenticateAsync (username, password, AuthenticationFailed, AuthenticationSuccess);
				}
				catch (Exception e) {
					Logger.Error ("Failed to Login", e);
				}
			}
		}

		private void AuthenticationSuccess(UserDetails details)
		{
			if (!Settings.Instance.IsRegistered ()) {
				// first Registration
				// Set The Register status flag to true
				// and store the registered user credentials - username, password
				// store the established Session Id
				Logger.Debug ("RegistrationSuccess()");
				Settings.Instance.SetRegistered (true);
				Settings.Instance.SetUserName (details.username);
				Settings.Instance.SetPassword (details.password);
				Settings.Instance.SetSessionId (details.sessionId);
				this.RegisterationSuccess (this, new EventArgs ());
			} else {
				// Login autentication - just store the session Id
				Logger.Debug ("AuthenticationSuccess()");
				Settings.Instance.SetSessionId (details.sessionId);
				this.LoginSuccess (this, new EventArgs ());
			}

		}

		private void AuthenticationFailed(string title, string message, int errorCode, string dismiss)
		{
			Logger.Debug ("AuthenticationFailed()");
		}

		private bool VerifyEmail(string email)
		{
			return TextUtils.ValidateEmail(email);
		}

	}
}

