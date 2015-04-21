using System;
using System.Reflection;
using System.Threading;

using Android.App;
using Android.OS;

using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity (Label = "BaseActivity")]			
	public abstract class BaseActivity : Activity
	{

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

		public void SetOnPause (Action action)
		{
			onPause = action;
		}
		public void SetOnResume (Action action)
		{
			onResume = action;
		}
	}
}

