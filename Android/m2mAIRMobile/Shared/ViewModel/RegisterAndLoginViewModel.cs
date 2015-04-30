using System;
using System.Threading.Tasks;

using Shared.SAL;
using Shared.Utils;
using Shared.Model;

namespace Shared.ViewModel
{
	public class RegisterAndLoginViewModel
	{

		// events
		public event EventHandler RegisterationSuccess;
		public event EventHandler LoginSuccess;

		private M2MAuthenticator authenticator;

		public RegisterAndLoginViewModel ()
		{
			authenticator = M2MAuthenticator.Instance;
		}

		public delegate void OnError(string title, string message, int code, string dismissCaption);




		public void StartLogin (string username, string password, OnError onError)
		{
			Task.Run (async () => {
				await StartLoginAsync (username, password, onError);
			});
		}


		private async Task StartLoginAsync (string username, string password, OnError onError)
		{
			Logger.Debug ("StartRegistration(),  User: " + username + ", password: " + password);
			if (!TextUtils.ValidateEmail(username)) {
				onError ("Invalid UserName", "The username you entered is not a valid email", 0x222D2A, "Ok");
				return;
			}
			else 
			{
				try 
				{
					var response = await authenticator.AuthenticateAsync (username, password);
					if (response.IsOkCode())
						AuthenticationSuccess(username, password, response.Content);
					else
						onError (response.StatusMessage, response.Content, (int)response.HttpStatusCode, "dismiss");

				}
				catch (Exception e) {
					Logger.Error ("Failed to Login", e);
					onError ("StartLoginAsync failed", e.Message, 0, "dismiss");
				}
			}
		}

		private void AuthenticationSuccess(string username, string password, string sessionId)
		{
			Logger.Info ("SessionId: " + sessionId);
			if (!Settings.Instance.IsRegistered ()) {
				// first Registration
				// Set The Register status flag to true
				// and store the registered user credentials - username, password
				// store the established Session Id
				Logger.Debug ("RegistrationSuccess()");
				Settings.Instance.SetRegistered (true);
				Settings.Instance.SetUserName (username);
				Settings.Instance.SetPassword (password);
				Settings.Instance.SetSessionId (sessionId);
				this.RegisterationSuccess (this, new EventArgs ());
			} else {
				// Login autentication - just store the session Id
				Logger.Debug ("AuthenticationSuccess()");
				Settings.Instance.SetSessionId (sessionId);
				this.LoginSuccess (this, new EventArgs ());
			}

		}

	}
}

