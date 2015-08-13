using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;
using Android.Gms.Maps.Model;

//using Android.Source.Screens;

namespace com.telit.lock_and_safe
{
    public class WatchLockModel : BaseModel
    {
        private DALManager dataManager;
        private WatchedLock handledLock;

        public WatchLockModel()
        {
            dataManager = new DALManager();
        }


        public void GetLockObject(string key, BaseModel.OnSuccess onSuccess, BaseModel.OnError onError)
        {
            Task.Run(async () =>
                {
                    try
                    {
                        Logger.Debug("GetThingObject(), Lock key:" + key);
                        Expression<Func<WatchedLock, bool>> predicate = t => (t.key.Equals(key));
                        handledLock = await dataManager.DBLoadItemAsync<WatchedLock>(predicate);
                        onSuccess();
                    }
                    catch (Exception e)
                    {
                        onError("Failed Get Thing Object", e.Message);
                    }
                });
        }

        public WatchedLock GetLock()
        {
            return handledLock;
        }
    }
}

