
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
using Android.Source.Screens;

namespace Android.Source.Views
{
	public class NavigationBarView : LinearLayout
	{
		private TextView 	navigationTitle;
		private Button		logout;

		public NavigationBarView (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public NavigationBarView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		public NavigationBarView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.nav_bar, this);

			navigationTitle = FindViewById<TextView>(m2m.Android.Resource.Id.Navigation_Title);
			logout = FindViewById<Button> (m2m.Android.Resource.Id.logout);
			logout.Click += (object sender, EventArgs e) => { OnLogoutClicked(); };
		}

		public void SetTitle(string title)
		{
			navigationTitle.Text = title;
		}

		public void OnLogoutClicked()
		{
			Logger.Debug ("OnLogoutClicked()");
            Settings.Instance[Settings.UserId] = null;
            Settings.Instance[Settings.UserPw] = null;
            var intent = new Intent(this.Context, typeof(RegisterAndLoginActivity));
			this.Context.StartActivity(intent);
//			Finish ();
		}
	}
}

