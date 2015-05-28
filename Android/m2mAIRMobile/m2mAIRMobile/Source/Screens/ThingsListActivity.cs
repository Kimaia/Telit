using System;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Android.Content.PM;

using Shared.Model;
using Shared.Utils;
using Android.Source.Views;
using m2m.Android.Source.Views;

namespace Android.Source.Screens
{
	[Activity]			
	public class ThingsListActivity : BaseActivity
	{
		private ListView listView;
		private NavigationBarView navBar; 
		private ThingsListAdapter adapter;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(m2m.Android.Resource.Layout.activity_things_list);

			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 
			navBar.SetTitle("Things");
		
			adapter = new ThingsListAdapter(this);
			adapter.PopulateThingsListAsync (Intent.GetStringExtra(Shared.Model.Constants.LOGIN_STATE), OnListPopulated, ShowDialog);
		}

		#region Callbacks / Event handlers
		public void OnListPopulated()
		{
			try{
				RunOnUiThread(()=>{
					listView.Adapter = adapter;
					listView.ItemClick += OnListItemClick;


					SetMapFragment();
				});
			}
			catch(Exception e){
				ShowDialog ("OnListPopulated", e.Message, -1, "dismiss");
			}
			
		}

		private void SetMapFragment()
		{
			(FragmentManager.FindFragmentById<MultipleThingsMapFragment> (m2m.Android.Resource.Id.map)).SetThingsList (adapter.GetThingsList ());
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(ThingActivity));

			var thing = adapter.GetThingObject (e.Position);
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, thing.key);
			Logger.Debug ("OnListItemClick() Thing key: " + thing.key);

			StartActivity(intent);
		}
		#endregion
	}
}

