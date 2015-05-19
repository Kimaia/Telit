
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
	public class PropertiesListAdapter : BaseAdapter
	{
		private PropertiesListAdapterViewModel viewModel;
		private Context context;

		public PropertiesListAdapter(Context context, Thing daThing)
		{
			this.context = context;
			this.viewModel = new PropertiesListAdapterViewModel(daThing);
		}



		public async Task PopulatePropertiesListAsync(BaseViewModel.OnSuccess onSuccess, BaseViewModel.OnError onError)
		{
			await viewModel.PopulatePropertiesList(onSuccess, onError);
		}

		public Property GetPropertyObject(int position)
		{
			return viewModel.propertiesList.ElementAt(position).Value;
		}


		#region implemented abstract members of BaseAdapter
		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
			get { return viewModel.propertiesList.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			throw new NotImplementedException();
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = viewModel.propertiesList.ElementAt(position).Value;
			View view = convertView;
			if (view == null) 
				view = LayoutInflater.From (context).Inflate (m2m.Android.Resource.Layout.list_cell_Property, null);

			view.FindViewById<TextView> (m2m.Android.Resource.Id.propertyName).Text = item.name;
			view.FindViewById<TextView> (m2m.Android.Resource.Id.unit).Text = item.unit;
			return view;
		}

		#endregion
	}
}

