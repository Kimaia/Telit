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

namespace Shared.Model
{
    public class ThingModel : BaseModel
    {
        private DALManager dataManager;
        private Thing handledThing;

        public ThingModel()
        {
            dataManager = new DALManager();
        }


        public void GetThingObject(string key, BaseModel.OnSuccess onSuccess, BaseModel.OnError onError)
        {
            Task.Run(async () =>
                {
                    try
                    {
                        Logger.Debug("GetThingObject(), Thing key:" + key);
                        Expression<Func<Thing, bool>> predicate = t => (t.key.Equals(key));
                        handledThing = await dataManager.DBLoadItemAsync<Thing>(predicate);
                        onSuccess();
                    }
                    catch (Exception e)
                    {
                        onError("Failed Get Thing Object", e.Message);
                    }
                });
        }

        public Thing GetThing()
        {
            return handledThing;
        }
    }
}

