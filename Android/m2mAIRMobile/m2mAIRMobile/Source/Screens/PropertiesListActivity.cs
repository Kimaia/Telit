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

using Shared.Utils;
using Shared.Model;
using Shared.ViewModel;
using Android.Source.Views;
using m2m.Android.Source.Screens;

namespace Android.Source.Screens
{
	[Activity (ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class PropertiesListActivity : BaseActivity
	{
		private PropertiesListViewModel 	viewModel;
		private PropertiesListAdapter		adapter;
		private ListView					listView;

		private Thing	 					daThing;

		private NavigationBarView 			navBar; 
		private ThingBriefDescriptionView 	thingBriefView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (m2m.Android.Resource.Layout.activity_properties_list);

			thingBriefView = FindViewById<ThingBriefDescriptionView>(m2m.Android.Resource.Id.ThingBriefDescriptionView);
			navBar = FindViewById<NavigationBarView>(m2m.Android.Resource.Id.NavigationBarView); 
			navBar.SetTitle("Properties");

			viewModel = new PropertiesListViewModel ();
			string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
			viewModel.GetThingObject (tkey, OnDBLoadThingObject, ShowDialog);

			listView = FindViewById<ListView>(m2m.Android.Resource.Id.listView); 
		}


		#region Callbacks / Event handlers
		public void OnDBLoadThingObject()
		{
			try{
				RunOnUiThread(()=>{
					Logger.Debug ("OnDBLoadThingObject()");

					daThing = viewModel.GetThing ();
					thingBriefView.SetThing (daThing);

					adapter = new PropertiesListAdapter(this, daThing);
					adapter.PopulatePropertiesListAsync (OnListPopulated, ShowDialog);
				});
			}
			catch(Exception e){
				ShowDialog ("OnDBLoadThingObject", e.Message, -1, "dismiss");
			}
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
			var intent = new Intent(this, typeof(PropertyActivity));
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, daThing.key);

			Property property = adapter.GetPropertyObject(e.Position);
			intent.PutExtra (Shared.Model.Constants.DATA_MODEL_PROPERTY_NAME_IDENTIFIER, property.name);

			Logger.Debug ("OnListItemClick() Thing key: " + daThing.key + ", property name: " + property.name);
			StartActivity(intent);
		}
		#endregion
	}
}

