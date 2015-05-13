
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
using Shared.ViewModel;

namespace Android.Source.Screens
{
    public class ThingsListAdapter : BaseAdapter
    {
		private ThingsListAdapterViewModel viewModel;
        private Context context;

		// events
		public event EventHandler OnListPopulated;

		public ThingsListAdapter(Context context)
		{
			this.context = context;
			this.viewModel = new ThingsListAdapterViewModel();
		}

		public async void PopulateThingsListAsync(string vm_state)
        {
			Logger.Info ("PopulateThingsListAsync(), VM_State:" + vm_state);
			await viewModel.PopulateThingsListAsync(vm_state);

			// raise event for completion
			this.OnListPopulated (this, new EventArgs ());
        }

		public Thing GetThingObject(int position)
		{
			return viewModel.thingsList[position];
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
				view = LayoutInflater.From (context).Inflate (m2m.Android.Resource.Layout.listcell_thing, null);

			view.FindViewById<TextView> (m2m.Android.Resource.Id.Text1).Text = item.id;
			view.FindViewById<TextView> (m2m.Android.Resource.Id.Text2).Text = item.key;
			return view;
		}

        #endregion
    }
}

