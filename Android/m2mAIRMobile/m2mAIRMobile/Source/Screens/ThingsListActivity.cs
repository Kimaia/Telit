﻿using System;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Android.Content.PM;

using Shared.Model;
using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity (Label = "ThingsListActivity")]			
	public class ThingsListActivity : BaseActivity
	{
		private ListView listView;
		private ThingsListAdapter adapter;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(m2m.Android.Resource.Layout.activity_things_list);

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			adapter = new ThingsListAdapter(this);
			adapter.PopulateThingsListAsync (Intent.GetStringExtra(Shared.Model.Constants.VM_STATE), OnListPopulated, ShowDialog);
		}

		public void OnListPopulated()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnListPopulated()");
					listView.Adapter = adapter;
					Logger.Debug ("OnListPopulated()2");
					listView.ItemClick += OnListItemClick;
				});
			}
			catch(Exception e){
				Logger.Debug ("OnListPopulated(): " + e.Message);
			}
			
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(ThingActivity));

			var thing = adapter.GetThingObject (e.Position);
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, thing.key);
			Logger.Debug ("OnListItemClick() Thing key: " + thing.key);

			StartActivity(intent);
			Finish ();
		}	
	}
}

