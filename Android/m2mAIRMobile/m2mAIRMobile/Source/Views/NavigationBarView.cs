
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

namespace Android.Source.Views
{
	public class NavigationBarView : LinearLayout
	{
		private TextView navigationTitle;

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
		}

		public void SetTitle(string title)
		{
			navigationTitle.Text = title;
		}
	}
}

