using System;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Android.Content.PM;

using Shared.Model;
using Shared.Utils;

namespace Android.Source.Screens
{
	[Activity]			
	public class ThingsListActivity : BaseActivity
	{
		private ListView listView;
		private ThingsListAdapter adapter;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(m2m.Android.Resource.Layout.activity_things_list);

			base.SetNavigationTitle ("Things");

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 

			adapter = new ThingsListAdapter(this);
			adapter.PopulateThingsListAsync (Intent.GetStringExtra(Shared.Model.Constants.LOGIN_STATE), OnListPopulated, ShowDialog);
		}

		public void OnListPopulated()
		{
			try{
				RunOnUiThread(()=>{
					listView.Adapter = adapter;
					listView.ItemClick += OnListItemClick;
				});
			}
			catch(Exception e){
				ShowDialog ("OnListPopulated", e.Message, -1, "dismiss");
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

