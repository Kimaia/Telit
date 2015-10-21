
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

using Shared.Utils;
using Shared.Model;
using LockAndSafe;
using Android.Graphics;
using Shared.Network.DataTransfer.TR50;
using Shared.ModelManager;
using Shared.Network;

namespace com.telit.lock_and_safe
{
    
    public enum LockState
    {
        Unknown,
        Locked,
        Unlocked,
        Maintenence,
        BreakIn
    }

    public class LocksListAdapter : BaseAdapter
    {
        private LocksListAdapterModel locksListAdapterModel;
        public DALManager dalManager;
        private Context context;
        private static Bitmap unknown, locked, unlocked, broken, maintenence;

        public LocksListAdapter(Context context)
        {
            this.context = context;
            this.locksListAdapterModel = new LocksListAdapterModel();
            dalManager = locksListAdapterModel.DataManager;
            if (unknown == null)
            {
                unknown = BitmapFactory.DecodeResource(this.context.Resources, Resource.Drawable.unknown);
                locked = BitmapFactory.DecodeResource(this.context.Resources, Resource.Drawable.locked);
                unlocked = BitmapFactory.DecodeResource(this.context.Resources, Resource.Drawable.unlocked);
                broken = BitmapFactory.DecodeResource(this.context.Resources, Resource.Drawable.broke_in_lock);
                maintenence = BitmapFactory.DecodeResource(this.context.Resources, Resource.Drawable.maintenence);
            }
        }



        public void PopulateLocksListAsync(BaseModel.OnSuccess onSuccess, BaseModel.OnError onError)
        {
            locksListAdapterModel.PopulateLocksList(onSuccess, onError);
        }

        public WatchedLock GetLockObject(int position)
        {
            return locksListAdapterModel.locksList[position];
        }

        public List<WatchedLock> GetLocksList()
        {
            return locksListAdapterModel.locksList;
        }


        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return locksListAdapterModel.locksList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            WatchedLock item = locksListAdapterModel.locksList[position];
            View view = convertView;
            if (view == null)
                view = LayoutInflater.From(context).Inflate(Resource.Layout.listcell_lock, null);

            view.FindViewById<TextView>(Resource.Id.lock_name).Text = item.name;
            
            if (item.alarms != null && item.alarms.reason != null)
                view.FindViewById<TextView>(Resource.Id.lock_reason).Text = WatchedLock.stateReason(item.alarms.reason.state);
            else
                view.FindViewById<TextView>(Resource.Id.lock_reason).Text = WatchedLock.stateReason(0);
            
            view.FindViewById<TextView>(Resource.Id.lock_address).Text = (item.loc == null || item.loc.addr == null) ? "unknown address" : item.loc.addr.ToString();
            
            ImageView statusImage = view.FindViewById<ImageView>(Resource.Id.status_image_view);
            
            
            TextView lastSeen = view.FindViewById<TextView>(Resource.Id.lock_last_seen);
            if (lastSeen != null)
                lastSeen.Text = "Last Seen: " + item.lastSeen;
            
            if (item.alarms != null && item.alarms.state != null)
            {
                Bitmap img = GetImageForStatus(item.alarms.state.state);
                if (img != null)
                    statusImage.SetImageBitmap(img);
                    
                
            }
            return view;
        }

        #endregion

        public static Bitmap GetImageForStatus(int state)
        {
            switch (state)
            {
                case (int)LockState.Unknown:
                    return unknown;
                case (int)LockState.Locked:
                    return locked;
                case (int)LockState.Unlocked:
                    return unlocked;
                case (int)LockState.Maintenence:
                    return maintenence;
                case (int)LockState.BreakIn:
                    return broken;
                default:
                    return null;
            }
        }

        
        
    }
}

