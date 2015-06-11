using System;
using SQLite;

using Shared.DB;
using Shared.Model;
using Shared.Utils;


namespace Shared.ViewModel
{
	public class LauncherViewModel : BaseViewModel
	{

		public LauncherViewModel()
		{
			InitDB ();
		}


		private void InitDB()
		{
			var db = Kimchi.Connection;
			try
			{
				db.CreateTableAsync<Thing>().Wait();

				#if DEBUG
				var dao = new Dao();
				dao.DeleteAll<Thing>();
				#endif
			}
			catch(Exception e)
			{
				Logger.Error("Failed Initialize db" + e);
				throw;
			}
			finally
			{
				// do nothing
			}
		}

		public bool IsLoggedIn ()
		{
			return (Settings.Instance.GetSessionId () != null); 
		}
	}
}

