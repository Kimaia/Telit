using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
namespace DifferentCharts
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("DifferentChartsDelegate")]
	public partial class DifferentChartsDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// Set chart type these:
			SeriesType selectedType = SeriesType.Column2D;
			window.RootViewController = chooseController (selectedType);
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}

		UIViewController chooseController (SeriesType type)
		{
			bool is3D = type >= SeriesType.Column3D;
			switch (type) {
			case SeriesType.Area2D:
			case SeriesType.Area3D:
				{
					AreaViewController controller = new AreaViewController ();
					controller.drawIn3D = is3D;
					return controller;
				}

			case SeriesType.Band:
				{
					BandViewController controller = new BandViewController ();
					return controller;
				}

			case SeriesType.Bar2D:
			case SeriesType.Bar3D:
				{
					BarViewController controller = new BarViewController ();
					controller.drawIn3D = is3D;
					return controller;
				}

			case SeriesType.Bubble2D:
			case SeriesType.Bubble3D:
				{
					BarViewController controller = new BarViewController ();
					controller.drawIn3D = is3D;
					return controller;
				}
					
			}
			return null;
		}
	}
}

