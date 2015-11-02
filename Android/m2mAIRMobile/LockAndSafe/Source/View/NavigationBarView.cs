
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.Utils;
using LockAndSafe;


namespace com.telit.lock_and_safe
{
    public class NavigationBarView : LinearLayout
    {
        private TextView navigationTitle;
        private Button logout;

        public NavigationBarView(Context context)
            : base(context)
        {
            Initialize(context);
        }

        public NavigationBarView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context);
        }

        public NavigationBarView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        void Initialize(Context context)
        {
            LayoutInflater.From(context).Inflate(Resource.Layout.nav_bar, this);

            navigationTitle = FindViewById<TextView>(Resource.Id.Navigation_Title);
            logout = FindViewById<Button>(Resource.Id.logout);
            logout.Click += (object sender, EventArgs e) =>
            {
                OnLogoutClicked();
            };
        }

        public void SetTitle(string title)
        {
            navigationTitle.Text = title;
        }

        public void OnLogoutClicked()
        {
            Logger.Debug("OnLogoutClicked()");
//            Settings.Instance[Settings.SessionId] = null;
            var intent = new Intent(this.Context, typeof(RegisterAndLoginActivity));
            this.Context.StartActivity(intent);
//			Finish ();
        }
    }
}

