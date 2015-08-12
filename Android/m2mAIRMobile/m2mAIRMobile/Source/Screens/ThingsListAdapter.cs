
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

namespace Android.Source.Screens
{
    public class ThingsListAdapter : BaseAdapter
    {
        private ThingsListAdapterModel viewModel;
        private Context context;

        public ThingsListAdapter(Context context)
        {
            this.context = context;
            this.viewModel = new ThingsListAdapterModel();
        }



        public async Task PopulateThingsListAsync(BaseModel.OnSuccess onSuccess, BaseModel.OnError onError)
        {
            await viewModel.PopulateThingsList(onSuccess, onError);
        }

        public Thing GetThingObject(int position)
        {
            return viewModel.thingsList[position];
        }

        public List<Thing> GetThingsList()
        {
            return viewModel.thingsList;
        }


        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return viewModel.thingsList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = viewModel.thingsList[position];
            View view = convertView;
            if (view == null)
                view = LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.listcell_thing, null);

            view.FindViewById<TextView>(m2m.Android.Resource.Id.ThingName).Text = item.name;
            view.FindViewById<TextView>(m2m.Android.Resource.Id.Status).Text = (item.connected) ? "Connected" : "Disconnected";
            view.FindViewById<TextView>(m2m.Android.Resource.Id.LastSeen).Text = item.lastSeen;
            return view;
        }

        #endregion
    }
}

