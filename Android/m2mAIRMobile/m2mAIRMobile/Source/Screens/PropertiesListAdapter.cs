﻿
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
    public class PropertiesListAdapter : BaseAdapter
    {
        private PropertiesListAdapterModel viewModel;
        private Context context;
        private RadioButton activeButton;

        public PropertiesListAdapter(Context context, Thing daThing)
        {
            this.context = context;
            this.viewModel = new PropertiesListAdapterModel(daThing);
        }



        public async Task PopulatePropertiesListAsync(BaseModel.OnSuccess onSuccess, BaseModel.OnError onError)
        {
            await viewModel.PopulatePropertiesList(onSuccess, onError);
        }

        public Property GetPropertyObject(int position)
        {
            return viewModel.propertiesList.ElementAt(position).Value;
        }

        public Property GetPropertyObject(string key)
        {
            Property prop = null;
            viewModel.propertiesList.TryGetValue(key, out prop);
            return prop;
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
            public RadioButton radioButton;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            string propertyKey = viewModel.propertiesList.ElementAt(position).Key; 
            Property property = viewModel.propertiesList.ElementAt(position).Value;

            ViewHolder holder = null;
            View view = convertView;

            // TODO no resycling of Views
            view = LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.list_cell_Property, null);
            holder = new ViewHolder();
            holder.radioButton = view.FindViewById<RadioButton>(m2m.Android.Resource.Id.propertyRadioButton);
            holder.radioButton.Checked = false;
            holder.radioButton.Text = property.name;
            view.Tag = holder;
            holder.radioButton.Click += delegate
            {
                if (activeButton != null)
                    activeButton.Checked = false;
                activeButton = holder.radioButton;
                holder.radioButton.Checked = true;
                ((PropertiesListActivity)context).OnPropertySelected(propertyKey);
            };

            return view;
        }

        #endregion
    }
}

