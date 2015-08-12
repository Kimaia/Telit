
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
using Shared.Model;
using Shared.Utils;

namespace Android.Source.Views
{
    public class SelectorBar : LinearLayout
    {
        private PropertyModel	Model;
        private Button historyButton;
        private Button continuousButton;

        public SelectorBar(Context context)
            : base(context)
        {
            Initialize(context);
        }

        public SelectorBar(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context);
        }

        public SelectorBar(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        void Initialize(Context context)
        {
            LayoutInflater.From(context).Inflate(m2m.Android.Resource.Layout.view_selector_bar, this);

            historyButton = FindViewById<Button>(m2m.Android.Resource.Id.mode_history);
            historyButton.Click += (object sender, EventArgs e) =>
            {
                OnModeHistory();
            };
            continuousButton = FindViewById<Button>(m2m.Android.Resource.Id.mode_continuous);
            continuousButton.Click += (object sender, EventArgs e) =>
            {
                OnModeContinuous();
            };
        }

        public void SetModel(PropertyModel vm)
        {
            Model = vm;
        }

        public void OnModeHistory()
        {
            Model.SetPresentationMode(PropertyModel.PropertyPresentationMode.History);
            historyButton.Selected = true;
            historyButton.Pressed = true;
            continuousButton.Selected = false;
            continuousButton.Pressed = false;
        }

        public void OnModeContinuous()
        {
            Model.SetPresentationMode(PropertyModel.PropertyPresentationMode.Continuous);
            continuousButton.Selected = true;
            continuousButton.Pressed = true;
            historyButton.Selected = false;
            historyButton.Pressed = false;
        }
    }
}

