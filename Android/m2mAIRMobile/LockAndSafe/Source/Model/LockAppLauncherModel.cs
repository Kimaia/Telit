using System;
using SQLite;

using Shared.DB;
using Shared.Model;
using Shared.Utils;


namespace com.telit.lock_and_safe
{
    public class LockAppLauncherModel : BaseModel
    {

        public LockAppLauncherModel()
        {
            InitDB();
        }


        private void InitDB()
        {
            var db = Kimchi.Connection;
            try
            {
                db.CreateTableAsync<WatchedLock>().Wait();

                #if DEBUG
                var dao = new Dao();
                dao.DeleteAll<WatchedLock>();
                #endif
            }
            catch (Exception e)
            {
                Logger.Error("Failed Initialize db" + e);
                throw;
            }
            finally
            {
                // do nothing
            }
        }

        public bool IsLoggedIn()
        {
            object o = Settings.Instance[Settings.UserId];
            return (Settings.Instance[Settings.UserId] != null); 
        }
    }
}

