
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
            view.FindViewById<TextView>(Resource.Id.lock_type).Text = item.key.Split("Thing".ToArray())[0];
            view.FindViewById<TextView>(Resource.Id.lock_address).Text = (item.loc == null || item.loc.addr == null) ? "unknown address" : item.loc.addr.ToString();
            
            ImageView statusImage = view.FindViewById<ImageView>(Resource.Id.status_image_view);
            
            if (item.alarms != null && item.alarms.state != null)
            {
                switch (item.alarms.state.state)
                {
                    case (int)LockState.Unknown:
                        statusImage.SetImageBitmap(unknown);
                        break;
                    case (int)LockState.Locked:
                        statusImage.SetImageBitmap(locked);
                        break;
                    case (int)LockState.Unlocked:
                        statusImage.SetImageBitmap(unlocked);
                        break;
                    case (int)LockState.Maintenence:
                        statusImage.SetImageBitmap(maintenence);
                        break;
                    case (int)LockState.BreakIn:
                        statusImage.SetImageBitmap(broken);
                        break;
                    default:
                        break;
                }
            }
            return view;
        }

        #endregion
        
    }
}

