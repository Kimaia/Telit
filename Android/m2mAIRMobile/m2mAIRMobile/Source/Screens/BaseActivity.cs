using System;
using System.Reflection;
using System.Threading;

using Android.App;
using Android.OS;
using Android.Widget;

using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity (Label = "BaseActivity")]			
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
		#endregion



		public void OpenErrorDialog(string msg, int errno)
		{
			Toast.MakeText(this, msg + " error: " + errno, ToastLength.Long).Show();
		}

		protected void ShowDialog(string title, string message, int errorCode, string dismiss)
		{
			Logger.Error ("OnEror() Dialog: " + message + ", error dode: " + errorCode);
		}

		public void PerformOnMainThread(Action action)
		{
			RunOnUiThread(action);
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

