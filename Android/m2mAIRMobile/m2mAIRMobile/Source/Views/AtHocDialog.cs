
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
using Android.Graphics.Drawables;
using Android.Text.Util;
using AtHoc.Android.Source.Utils;
using AtHoc.Shared.Native;

namespace AtHoc.Android.Source.Views
{
	public class AtHocDialog : Dialog
	{
		public enum DialogType
		{
			OneButton,
			TwoButtons,
			ThreeButtons,
			ErrorDialog,
			InfoDialog
		}

		TextView title;
		TextView messageTextBold;
		TextView messageText;
		Button leftButton;
		Button rightButton;
		Button centerButton;
		ImageView closeIcon;
		float dimAmount = 0.5f;

		/// <summary>
		/// Initializes a new instance of the <see cref="AtHoc.Android.Source.Views.AtHocDialog"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public AtHocDialog(Context context) : base(context, Android.Resource.Style.Theme_Transparent)
		{
		}

		/// <summary>
		/// Create the dialog
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			//set dialog state
			base.OnCreate(savedInstanceState);
			//set dialog theme
			SetDialogTheme();
			//set dialog view
			SetContentView(Resource.Layout.athoc_custom_dialog);
			InitializeViews();
		}

		void SetDialogTheme()
		{
			RequestWindowFeature((int)WindowFeatures.NoTitle);
			Window.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
			WindowManagerLayoutParams lp = Window.Attributes;
			lp.DimAmount = dimAmount;
			Window.Attributes = lp;
			Window.AddFlags(WindowManagerFlags.BlurBehind);
			Window.AddFlags(WindowManagerFlags.DimBehind);
			SetCanceledOnTouchOutside(false);//make the dialog modal
		}

		void InitializeViews()
		{
			//Initializes  views
			title = FindViewById<TextView>(Resource.Id.titleText);
			closeIcon = FindViewById<ImageView>(Resource.Id.x);
			messageTextBold = FindViewById<TextView>(Resource.Id.messageTextBold);
			messageText = FindViewById<TextView>(Resource.Id.messageText);
			leftButton = FindViewById<Button>(Resource.Id.negativeButtonView);
			rightButton = FindViewById<Button>(Resource.Id.positiveButtonView);
			centerButton = FindViewById<Button>(Resource.Id.neutralButtonView);
		}

		void InitializeIcon(ImageView image, ViewStates visibility, Action imageClick)
		{
			image.Visibility = ViewStates.Visible;
			image.Click += (s, e) =>
			{
				imageClick.Invoke();
			};	
		}

		private void InitializeMessage(TextView textView, int textId, ViewStates visibility, GravityFlags gravity)
		{
			textView.Visibility = visibility;
			textView.SetText(textId);
			textView.Gravity = gravity;
		}

		void OpenGpsSettings()
		{
			DeviceUtils.OpenGpsSettings(base.Context);
		}

		private void InitializeButton(Button button, string buttonText, ViewStates visibility, int resourceId, Action clickAction)
		{
			button.Text = buttonText;
			button.Visibility = visibility;
			button.SetBackgroundResource(resourceId);
			button.Click += (s, e) =>
			{
				//open settings dialog
				clickAction.Invoke();
			};	
		}

		public override void Show()
		{
			throw new Exception("never call this method directyly");
			//base.Show ();
			//We call show only from the concrete view
		}

		public void ShowFirstRunMessageView()
		{
			if (IsShowing)
				return;

			base.Show();
			this.title.SetCompoundDrawablesWithIntrinsicBounds(Resource.Drawable.athoc_logo_white_small, 0, Resource.Drawable.titlebar_logo, 0);

			InitializeIcon(closeIcon, ViewStates.Visible, Dismiss);

			InitializeMessage(messageTextBold, Resource.String.registration_first_time_dialog_bold, ViewStates.Visible, GravityFlags.Left);

			InitializeMessage(messageText, Resource.String.registration_first_time_dialog_regular, ViewStates.Visible, GravityFlags.Left);
			Linkify.AddLinks(messageText, MatchOptions.All);

			int padding = Context.Resources.GetDimensionPixelSize(Resource.Dimension.small_padding);

			FindViewById(Resource.Id.messageContainer).SetBackgroundResource(Resource.Drawable.background_dialog_bottom);
			FindViewById(Resource.Id.messageContainer).SetPadding(padding, padding, padding, padding);

			HideControls(Resource.Id.buttonDivider, Resource.Id.buttonContainer);
		}

		/// <summary>
		/// Locations the services disabled.
		/// </summary>
		/// <param name="dialogTitle">Dialog title.</param>
		/// <param name="message">Message.</param>
		/// <param name="errorId">Error identifier.(id = 222622)</param>
		/// <param name="leftButtonText">Left button text.</param>
		/// <param name="rightButtonText">Right button text.</param>
		public void ShowLocationServicesDisabled(string dialogTitle, string messageBody, int errorCode, string leftButtonText, string rightButtonText)
		{
			if (IsShowing)
				return;

			base.Show();
			title.Text = dialogTitle;

			messageText.Text = string.Format("{0} [{1:X}]", messageBody, errorCode);

			InitializeButton(leftButton, leftButtonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom_left, Dismiss);

			FindViewById(Resource.Id.negativeDivider).Visibility = ViewStates.Visible;

			InitializeButton(rightButton, rightButtonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom_right, OpenGpsSettings);
		}

		void HideControls(params int[]resourceIds)
		{
			foreach (int control in resourceIds)
			{
				FindViewById(control).Visibility = ViewStates.Gone;
			}
		}

		public void ShowThreeButtonDialog(string dialogTitle, string messageBody, string leftButtonText, string rightButtonText, Action rightAction, string centerButtonText, Action  centerAction)
		{
			if (IsShowing)
				return;

			base.Show();

			title.Text = dialogTitle;

			if (!string.IsNullOrWhiteSpace(messageBody))
				messageText.Text = string.Format("{0}", messageBody);
			else
				messageText.Visibility = ViewStates.Gone;

			InitializeButton(leftButton, leftButtonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom_left, Dismiss);

			FindViewById(Resource.Id.negativeDivider).Visibility = ViewStates.Visible;

			InitializeButton(rightButton, rightButtonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom_right, delegate
			{
				rightAction.Invoke();
				Dismiss();
			});

			FindViewById(Resource.Id.neutralDivider).Visibility = ViewStates.Visible;

			InitializeButton(centerButton, centerButtonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom_center, delegate
			{
				centerAction.Invoke();
				Dismiss();
			});

			//HideControls(Resource.Id.buttonDivider, Resource.Id.buttonContainer);
		}

		/// <summary>
		/// Raises the button dialog event.
		/// </summary>
		/// <param name="dialogTitle">Dialog title.</param>
		/// <param name="messageBody">Message body.</param>
		/// <param name="errorCode">Error code.</param>
		/// <param name="buttonText">Button text.</param>
		public void ShowOneButtonDialog(string dialogTitle, string messageBody, int errorCode, string buttonText)
		{
			if (IsShowing)
				return;

			base.Show();
			this.title.Text = dialogTitle;

			messageText.Text = string.Format("{0} [{1:X}]", messageBody, errorCode);

			InitializeButton(leftButton, buttonText, ViewStates.Visible, Resource.Drawable.background_dialog_bottom, Dismiss);

			HideControls(Resource.Id.positiveButtonView);


		}
	}
}

