using System;
using System.Reflection;
using System.Threading;

using Android.App;
using Android.OS;
using Android.Widget;

using Shared.Utils;
using Android.Graphics;

using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using LockAndSafe;
using Android.Views;


namespace com.telit.lock_and_safe
{
    [Activity]			
    public abstract class BaseActivity : Activity
    {
        private Dialog dialog;
        private Action onPause;
        private Action onResume;
        private static Bitmap sprite;

        #region lifecycle

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (sprite == null)
                sprite = BitmapFactory.DecodeResource(Resources, Resource.Drawable.sprite);
            Logger.Debug("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);
        }

        protected override void OnPause()
        {
            Logger.Debug("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

            if (onPause != null)
            {
                onPause();
            }

            if (dialog != null)
            {
                StopLoadingSpinner();
            }
            base.OnPause();
        }

        protected override void OnResume()
        {
            Logger.Debug("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

            base.OnResume();
            if (onResume != null)
            {
                onResume();
            }
        }

        protected override void OnStop()
        {
            Logger.Debug("ThreadId-" + Thread.CurrentThread.ManagedThreadId + "," + this.GetType().Name + "," + MethodBase.GetCurrentMethod().Name);

            base.OnStop();
        }

        #endregion



        public void StartLoadingSpinner(string msg, Action onCancel = null)
        {
            try
            {
                RunOnUiThread(() =>
                    {
                        if (dialog != null)
                        {
                            StopLoadingSpinner();
                        }

                        
                        dialog = new Dialog(this);
                        dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                        dialog.SetContentView(Resource.Layout.lock_progress_dialog);
                        
                        // set the custom dialog components - text, image and button
                        TextView text = (TextView)dialog.FindViewById(Resource.Id.text);
                        text.Text = msg;
                        RelativeLayout rl = (RelativeLayout)dialog.FindViewById(Resource.Id.lock_sprite_layout);
                        var spriteView = new SpriteView(this, sprite, 4, 4, 5);
                        rl.AddView(spriteView);
                        
                        dialog.SetCancelable(onCancel != null);
                        dialog.CancelEvent += (sender, e) =>
                        {
                            onCancel();
                        };
                        dialog.Show();
                        
                    });
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
        }

        public void StopLoadingSpinner()
        {
            PerformOnMainThread(() =>
                {
                    if (dialog != null)
                    {
                        dialog.Hide();
                        dialog.Dismiss();
                        dialog = null;
                    }
                });
        }

        public void OpenErrorDialog(string title, string message)
        {
            Logger.Error("Error cought: \n" + title + ",  " + message);
            RunOnUiThread(() =>
                {
                    StopLoadingSpinner();
                    Toast.MakeText(this, title, ToastLength.Long).Show();
                });
        }

        public void ShowDialog(string title, string message)
        {
            RunOnUiThread(() =>
                {
                    StopLoadingSpinner();
                    Toast.MakeText(this, title, ToastLength.Long).Show();
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
                Logger.Error("PerformOnMainThread - action(): " + action.Method.Name + ", Error message:" + e.Message);
            }
        }

        public bool IsActive
        { 
            get { return dialog.IsShowing; }
        }

        protected void SetOnPause(Action action)
        {
            onPause = action;
        }

        protected void SetOnResume(Action action)
        {
            onResume = action;
        }
    }
}

