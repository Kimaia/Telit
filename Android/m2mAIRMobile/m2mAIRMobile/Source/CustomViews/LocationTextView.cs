
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
using Shared.Utils;

namespace Android.Source.Screens
{
	public class LocationTextView : LinearLayout
	{
		private TextView thingLat;
		private TextView thingLong;
		private TextView thingStreet;
		private TextView thingCity;
		private TextView thingState;

		public LocationTextView (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public LocationTextView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		public LocationTextView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.location_details, this);

			thingLat = FindViewById<TextView>(m2m.Android.Resource.Id.latitude);
			thingLong = FindViewById<TextView>(m2m.Android.Resource.Id.longiude);
			thingStreet = FindViewById<TextView>(m2m.Android.Resource.Id.Street);
			thingCity = FindViewById<TextView>(m2m.Android.Resource.Id.City);
			thingState = FindViewById<TextView>(m2m.Android.Resource.Id.State);
		}


		public void SetLocation(Thing thing)
		{
			if (thing.loc != null) {
				thingLat.Text = thing.loc.lat.ToString ();
				thingLong.Text = thing.loc.lng.ToString ();
				thingStreet.Text = thing.loc.addr.street;
				thingCity.Text = thing.loc.addr.city;
				thingState.Text = thing.loc.addr.state;
			} else {
				Logger.Info ("Thing Key: " + thing.key + "No Location data.");
			}
		}
	}
}

