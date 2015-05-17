using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Shared.ViewModel;
using Shared.Model;
using Shared.Utils;


namespace Android.Source.Screens
{
	[Activity]			
	public class ThingActivity : BaseActivity
	{
		private ThingViewModel viewModel;

		private TextView thingId;
		private TextView thingKey;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			viewModel = new ThingViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey);
		}


		public void OnDBLoadThingObject(object sender, EventArgs e)
		{
			Logger.Debug ("OnDBLoadThingObject()");

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_thing);

			thingId = FindViewById<TextView>(m2m.Android.Resource.Id.Text1);
			thingKey = FindViewById<TextView>(m2m.Android.Resource.Id.Text2);
		}
	}
}

