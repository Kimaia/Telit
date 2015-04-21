using System;

namespace m2mAIRMobile
{
	public class App
	{
		public App ()
		{
			// Singleton
			public static App Current
			{
				get { return current; }
			} private static App current;


			#region Application context

			static App ()
			{
				current = new App();
			}
			protected App () 
			{
				// starting a service like this is blocking, so we want to do it on a background thread
				new Task ( () => { 

					// start our main service
					Log.Debug (logTag, "Calling StartService");

					bool userloggedin = false;

				} ).Start ();
			}

			#endregion

		}
	}
}

