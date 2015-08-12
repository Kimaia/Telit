using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Android.Content;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;

namespace com.telit.lock_and_safe
{
    public class TR50LockListParams : ITR50HasPayload
    {
        public int count;
        public List<string> fields;
        public List<WatchedLock> result;

        public bool HasPayload()
        {
            return (count > 0);
        }
    }

    public class LocksListAdapterModel : BaseModel
    {
        public DALManager DataManager { get; private set; }

        public List<WatchedLock> locksList { get; private set; }

        public LocksListAdapterModel()
        {
            DataManager = new DALManager();
            locksList = new List<WatchedLock>();
        }

        

        public void PopulateLocksList(OnSuccess onSuccess, OnError onError)
        {
            Task.Run(async () =>
                {
                    await PopulateLocksListAsync(onSuccess, onError);
                    if (locksList.Count == 0)
                        onError("Loaded Locks List is Empty", null);
                    else
                    {
                        for (int i = 0; i < locksList.Count ;)
                        {
                            if (locksList[i].defkey.ToLower().Equals("watchlock"))
                                ++i;
                            else
                                locksList.RemoveAt(i);
                        }
                        
//                        await DataManager.DBInsertListAsync<Thing>(locksList);
                        onSuccess();
                    }
                });
        }


        public async Task PopulateLocksListAsync(OnSuccess onSuccess, OnError onError)
        {
            try
            {
                var command = TR50CommandFactory.Build(M2MCommands.CommandType.Thing_List);
                
                var response = await DataManager.M2MLoadListAsync<TR50LockListParams>(command);
                locksList = response.Params.result;
                Logger.Debug("PopulateThingsListAsync(), Things count:" + locksList.Count);
            }
            catch (Exception e)
            {
                onError("Failed get Things list", e.Message);
            }
        }
    }
}

