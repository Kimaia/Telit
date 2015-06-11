using System;
using System.Threading.Tasks;

using Shared.ModelManager;
using Shared.Utils;

namespace Shared.ViewModel
{
	public class RegisterAndLoginViewModel : BaseViewModel
	{
		// events
		public event EventHandler LoginSuccess;

		private DALManager authenticator;

		public RegisterAndLoginViewModel ()
		{
			authenticator = new DALManager();
		}


		public void StartLogin (string username, string password, OnError onError)
		{
			Task.Run (async () => {
				await StartLoginAsync (username, password, onError);
			});
		}


		private async Task StartLoginAsync (string username, string password, OnError onError)
		{
			#if DEBUG
			username = "demo@devicewise.com";
			password = "demo123";
			#endif

			Logger.Debug ("StartLoginAsync(),  User: " + username + ", password: " + password);
			if (!ValidateCredentials(username, password)) {
				onError ("Invalid UserName", "Username is not a valid email");
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
						onError (response.StatusMessage, response.Content);
				}
				catch (Exception e) 
				{
					onError ("Failed Login", e.Message);
				}
			}
		}

		private void AuthenticationSuccess(string username, string password, string sessionId)
		{
			Logger.Debug ("LoginSuccess(), SessionId: " + sessionId);
			Settings.Instance.SetSessionId (sessionId);
			this.LoginSuccess (this, new EventArgs ());

		}

		private bool ValidateCredentials(string username, string password)
		{
			if (username.Length == 0)
				return false;
			else if (password.Length == 0)
				return false;
			else
				return (TextUtils.ValidateEmail (username));
		}
	}
}

