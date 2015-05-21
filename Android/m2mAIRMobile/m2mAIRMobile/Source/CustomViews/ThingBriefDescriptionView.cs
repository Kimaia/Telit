
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

using Shared.Model;

namespace Android.Source.Screens
{
	public class ThingBriefDescriptionView : LinearLayout
	{
		private TextView thingName;
		private TextView thingStatus;
		private TextView thingLastSeen;

		public ThingBriefDescriptionView (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public ThingBriefDescriptionView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		public ThingBriefDescriptionView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.thing_brief_description, this);

			thingName = FindViewById<TextView>(m2m.Android.Resource.Id.ThingName);
			thingStatus = FindViewById<TextView>(m2m.Android.Resource.Id.Status);
			thingLastSeen = FindViewById<TextView>(m2m.Android.Resource.Id.LastSeen);
		}


		public void SetThing(Thing thing)
		{
			thingName.Text = thing.name;
			thingStatus.Text = (thing.connected) ? "connected" : "disconnected";
			thingLastSeen.Text = thing.lastSeen;
		}
	}
}

