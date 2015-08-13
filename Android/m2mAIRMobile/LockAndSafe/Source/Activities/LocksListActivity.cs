using System;

using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using Android.Content.PM;

using Shared.Model;
using Shared.Utils;
using LockAndSafe;


namespace com.telit.lock_and_safe
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Icon = "@drawable/ic_launcher")]			
    public class LocksListActivity : BaseActivity
    {
        private ListView listView;
        private NavigationBarView navBar;
        private LocksListAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_things_list);

            navBar = FindViewById<NavigationBarView>(Resource.Id.NavigationBarView); 
            listView = FindViewById<ListView>(Resource.Id.listView); 
            navBar.SetTitle("Locks");
		
            adapter = new LocksListAdapter(this);
            adapter.PopulateLocksListAsync(OnListPopulated, OpenErrorDialog);
        }

        #region Callbacks / Event handlers

        public void OnListPopulated()
        {
            try
            {
                RunOnUiThread(() =>
                    {
                        listView.Adapter = adapter;
                        listView.ItemClick += OnListItemClick;


//                        SetMapFragment();
                    });
            }
            catch (Exception e)
            {
                ShowDialog("OnListPopulated", e.Message);
            }
			
        }

        //        private void SetMapFragment()
        //        {
        //            (FragmentManager.FindFragmentById<MultipleThingsMapFragment>(m2m.Android.Resource.Id.map)).SetThingsList(adapter.GetThingsList());
        //        }

        public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Logger.Debug("OnListItemClick()");
            var watchLock = adapter.GetLockObject(e.Position);
            InitiateLockDetailsActivity(watchLock);
        }

        public void InitiateLockDetailsActivity(WatchedLock selectedLock)
        {
            var intent = new Intent(this, typeof(LockDetailsActivity));
            intent.PutExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER, selectedLock.key);
            Logger.Debug("InitiateThingActivity() Thing key: " + selectedLock.key);
            StartActivity(intent);
        }

        public void OnMapClick()
        {
            if (listView.Visibility == Android.Views.ViewStates.Visible)
                listView.Visibility = Android.Views.ViewStates.Gone;
            else
                listView.Visibility = Android.Views.ViewStates.Visible;
        }

        public override void OnBackPressed()
        {
            if (listView.Visibility == Android.Views.ViewStates.Gone)
                listView.Visibility = Android.Views.ViewStates.Visible;
            else
                base.OnBackPressed();
        }

        #endregion
    }
}

