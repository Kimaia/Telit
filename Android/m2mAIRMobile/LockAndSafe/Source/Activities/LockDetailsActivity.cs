﻿using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using Shared.Model;
using Shared.Utils;
using Android.Views;
using System.Collections.Generic;
using Android.Graphics;
using LockAndSafe;
using System.Threading.Tasks;

namespace com.telit.lock_and_safe
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Icon = "@drawable/ic_launcher")]			
    public class LockDetailsActivity : BaseActivity
    {
        private WatchLockModel viewModel;
        private WatchedLock theLock;

        
        private TextView lockName;
        private TextView lockType;
        private TextView lockAddress;
        private TextView bateryVoltage;
        private ImageView stateImage;
        private TextView reasonText;
        private TextView lastSeenText;
        
        private LockMapFragment mapFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.lock_activity_layout);

            lockName = FindViewById<TextView>(Resource.Id.lock_name);
            lockType = FindViewById<TextView>(Resource.Id.lock_type);
            lockAddress = FindViewById<TextView>(Resource.Id.lock_address);
            bateryVoltage = FindViewById<TextView>(Resource.Id.batery_voltage);
            reasonText = FindViewById<TextView>(Resource.Id.reason_text);
            lastSeenText = FindViewById<TextView>(Resource.Id.last_seen_text);
            stateImage = FindViewById<ImageView>(Resource.Id.state_image);
            
            
            
            viewModel = new WatchLockModel();
            string tkey = Intent.GetStringExtra(Shared.Model.Constants.DATA_MODEL_THING_KEY_IDENTIFIER);
            viewModel.GetLockObject(tkey, OnDBLoadLockObject, OpenErrorDialog);
        }


        public void OnDBLoadLockObject()
        {
            try
            {
                RunOnUiThread(() =>
                    {
                        Logger.Debug("OnDBLoadLockObject()");
                        theLock = viewModel.GetLock();	
                        lockName.Text = theLock.name;
                        lockType.Text = theLock.type;
                        bateryVoltage.Text = "" + theLock.properties.voltage.value;
                        lastSeenText.Text = theLock.lastSeen ;
                        if (theLock.loc != null)
                        {
                            if (theLock.loc.addr != null)
                                lockAddress.Text = theLock.loc.addr.ToString();
                            
                            mapFragment = (LockMapFragment)FragmentManager.FindFragmentById(Resource.Id.lock_map);
                            mapFragment.SetLock(theLock);
                                
                        } 
                        if (theLock.alarms != null)
                        {
                            if (theLock.alarms.state != null)
                            {
                                Bitmap img = LocksListAdapter.GetImageForStatus(theLock.alarms.state.state);
                                if (img != null)
                                    stateImage.SetImageBitmap(img);
                            }
                            
                            if (theLock.alarms.reason != null)
                            {
                                reasonText.Text = WatchedLock.stateReason(theLock.alarms.reason.state);
                            }
                            
                            
                        } 
                    });
            }
            catch (Exception e)
            {
                OpenErrorDialog("OnDBLoadThingObject", e.Message);
            }
        }
        
    }
}

