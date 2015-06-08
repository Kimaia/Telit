
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

		private class ViewHolder : Java.Lang.Object
		{
			public CheckBox checkbox;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			string propertyKey = viewModel.propertiesList.ElementAt (position).Key; 
			Property property = viewModel.propertiesList.ElementAt(position).Value;

			ViewHolder holder = null;
			View view = convertView;
			if (view == null) 
			{
				view = LayoutInflater.From (context).Inflate (m2m.Android.Resource.Layout.list_cell_Property, null);
				holder = new ViewHolder ();
				holder.checkbox = view.FindViewById<CheckBox> (m2m.Android.Resource.Id.propertyCheckbox);
				view.Tag = holder;
				holder.checkbox.Click += delegate {
					((PropertiesListActivity)context).onCheckBoxClick(propertyKey, holder.checkbox.Checked);
				};
			}
			else
				holder = (ViewHolder)view.Tag;
			
			holder.checkbox.Text = property.name;

			return view;
		}

		#endregion
	}
}

