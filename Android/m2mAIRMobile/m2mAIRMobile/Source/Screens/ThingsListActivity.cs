using System;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;

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

			this.adapter = new ThingsListAdapter(this);
			this.adapter.OnListPopulated += new EventHandler (OnListPopulated);
			this.adapter.PopulateThingsListAsync ();
		}

		public void OnListPopulated(object sender, EventArgs e)
		{
			Logger.Debug ("OnListPopulated()");
			listView.Adapter = adapter;
			listView.ItemClick += OnListItemClick;
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			Logger.Debug ("OnListItemClick()");
			var intent = new Intent(this, typeof(ThingActivity));
			StartActivity(intent);
			Finish ();
		}	
	}
}

