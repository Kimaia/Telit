using System;
using System.Reflection;
using System.Threading;

using Android.App;
using Android.OS;
using Android.Widget;

using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity]			
	public abstract class BaseActivity : Activity
	{
		private ProgressDialog progressSpinner;
		private Action onPause;
		private Action onResume;

		#region lifecycle
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Logger.Debug ("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);
		}

		protected override void OnPause ()
		{
			Logger.Debug ("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

			if (onPause != null)
			{
				onPause();
			}

			if (progressSpinner != null)
			{
				StopLoadingSpinner();
			}
			base.OnPause ();
		}

		protected override void OnResume ()
		{
			Logger.Debug ("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

			base.OnResume ();
			if (onResume != null)
			{
				onResume();
			}
		}

		protected override void OnStop ()
		{
			Logger.Debug ("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

			base.OnStop ();
		}

		#endregion



		public void StartLoadingSpinner(string msg, Action onCancel = null)
		{
			try
			{
				RunOnUiThread (() => {
					if (progressSpinner != null)
					{
						StopLoadingSpinner();
					}

					progressSpinner = new ProgressDialog(this);
					progressSpinner.SetCancelable(onCancel != null);
					progressSpinner.CancelEvent +=  (sender, e) =>
					{
						onCancel();
					};
					progressSpinner.SetMessage(msg);
					progressSpinner.Show();
				});
			}
			catch (Exception e) 
			{
				Logger.Debug (e.Message);
			}
		}

		public void StopLoadingSpinner()
		{
			PerformOnMainThread(() => {
					if (progressSpinner != null) 
					{
						progressSpinner.Hide();
						progressSpinner.Dismiss();
						progressSpinner = null;
					}
				});
		}

		public void OpenErrorDialog(string title, string message)
		{
			Logger.Error("Error cought: \n" + title + ",  " + message);
			RunOnUiThread (() => {
				StopLoadingSpinner();
				Toast.MakeText (this, title, ToastLength.Long).Show ();
			});
		}

		public void ShowDialog(string title, string message)
		{
			RunOnUiThread (() => {
				StopLoadingSpinner();
				Toast.MakeText (this, title, ToastLength.Long).Show ();
			});
		}

		public void PerformOnMainThread(Action action)
		{
			try
			{
				RunOnUiThread(action);
			}
			catch (Exception e)
			{
				Logger.Error ("PerformOnMainThread - action(): " + action.Method.Name + ", Error message:" + e.Message);
			}
		}

		public bool IsActive 
		{ 
			get { return progressSpinner.IsShowing;}
		}

		protected void SetOnPause (Action action)
		{
			onPause = action;
		}
		protected void SetOnResume (Action action)
		{
			onResume = action;
		}
	}
}

