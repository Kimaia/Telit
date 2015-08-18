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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Shared.Model;
using Shared.Utils;
using Shared.Network.DataTransfer.TR50;
using LockAndSafe;

namespace com.telit.lock_and_safe
{

    public class LockMapFragment : MapFragment, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter, GoogleMap.IOnInfoWindowClickListener
    {
        public delegate void OnLocationHistorytSuccess(List<LatLng> locationHistory);

        private Activity activity;
        
        private GoogleMap gMap;
        private WatchedLock theLock;
        private LatLng lockLatLng;
        private MarkerOptions locationMarker;

        
        public LockMapFragment()
            : base()
        {
            theLock = null;
        }

        #region lifecycle

        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);
            this.activity = activity;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            GetMapAsync(this);
        }

        #endregion

        public void OnMapReady(GoogleMap gmap)
        {
            gMap = gmap;
            if (theLock != null)
                SetCameraAndMarker();
        }

        public void SetLock(WatchedLock lck)
        {
            theLock = lck;
            if (gMap != null)
                SetCameraAndMarker();
        }

        private void SetCameraAndMarker()
        {
            Location loc = theLock.loc;
            if (loc == null)
                SetLocationUnAvailable();
            else
            {
                if (IsValidCoordinates(loc))
                {
                    SetGeo();
                }
                else if (loc.addr != null)
                {
                    SetAddress();
                }

                gMap.SetInfoWindowAdapter(this);
                gMap.SetOnInfoWindowClickListener(this);
            }
        }

        private bool IsValidCoordinates(Location loc)
        {
            if (loc == null)
                return false;

            if (loc.lat > 90 || loc.lat < -90) //|| loc.lng > 180 || loc.lng < -180)
            {
                Logger.Error("InvalidGeoCoordinates: Thing Id: " + theLock.id + ", Lat: " + loc.lat + ", Lng: " + loc.lng);
                return false;
            }
            else
                return true;
        }

        private void SetGeo()
        {
            lockLatLng = new LatLng(theLock.loc.lat, theLock.loc.lng);
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(lockLatLng, 10);
            gMap.MoveCamera(camera);
            var markerDescriptor = BitmapDescriptorFactory.FromResource(GetImageResourceForStatus(theLock.alarms.state.state));
            locationMarker = new MarkerOptions().SetPosition(lockLatLng).SetTitle(theLock.name).Draggable(true).InvokeIcon(markerDescriptor);
            gMap.AddMarker(locationMarker);
        }

        private void SetAddress()
        {
            Logger.Debug("SetAddress()");
            SetLocationUnAvailable();
        }

        private void SetLocationUnAvailable()
        {
            Logger.Info("SetLocationUnAvailable()");

            CameraUpdate camera = CameraUpdateFactory.NewLatLng(new LatLng(0, 0));
            gMap.MoveCamera(camera);

            ((BaseActivity)activity).ShowDialog("Location UnAvailable", null);
        }

        public View GetInfoWindow(Marker marker)
        {
            View view = activity.LayoutInflater.Inflate(Resource.Layout.map_thing_info_geoLocation, null, false);
            view.FindViewById<TextView>(Resource.Id.latitude).Text = theLock.loc.lat.ToString();
            view.FindViewById<TextView>(Resource.Id.longitude).Text = theLock.loc.lng.ToString();
            return view;
        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        void GoogleMap.IOnInfoWindowClickListener.OnInfoWindowClick(Marker marker)
        {
            return;
        }

        public void OnMapClick(LatLng point)
        {
            return;
        }

        
        public static int GetImageResourceForStatus(int state)
        {
            switch (state)
            {
                case (int)LockState.Unknown:
                    return Resource.Drawable.unknown_small;
                case (int)LockState.Locked:
                    return Resource.Drawable.locked_small;
                case (int)LockState.Unlocked:
                    return Resource.Drawable.unlocked_small;
                case (int)LockState.Maintenence:
                    return Resource.Drawable.maintenence_small;
                case (int)LockState.BreakIn:
                    return Resource.Drawable.broke_in_lock_small;
                default:
                    return Resource.Drawable.unknown_small;
            }
        }
        
        
    }
}

