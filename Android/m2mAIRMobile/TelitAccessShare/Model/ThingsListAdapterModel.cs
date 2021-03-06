﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Android.Content;

using Shared.Utils;
using Shared.Model;
using Shared.ModelManager;
using Shared.Network;
using Shared.Network.DataTransfer.TR50;

namespace Shared.Model
{
    public class ThingsListAdapterModel : BaseModel
    {
        public DALManager DataManager { get; private set; }

        public List<Thing> thingsList { get; private set; }

        public ThingsListAdapterModel()
        {
            DataManager = new DALManager();
            thingsList = new List<Thing>();
        }

        

        public async Task PopulateThingsList(OnSuccess onSuccess, OnError onError)
        {
            await Task.Run(async () =>
                {
                    await PopulateThingsListAsync(onSuccess, onError);
                    if (thingsList.Count == 0)
                        onError("Loaded Things List is Empty", null);
                    else
                    {
                        
                        await DataManager.DBInsertListAsync<Thing>(thingsList);
                        onSuccess();
                    }
                });
        }


        public async Task PopulateThingsListAsync(OnSuccess onSuccess, OnError onError)
        {
            try
            {
                var command = TR50CommandFactory.Build(M2MCommands.CommandType.Thing_List);
                var response = await DataManager.M2MLoadListAsync<TR50ThingsListParams>(command);
                thingsList = response.Params.result;
                Logger.Debug("PopulateThingsListAsync(), Things count:" + thingsList.Count);
            }
            catch (Exception e)
            {
                onError("Failed get Things list", e.Message);
            }
        }
    }
}

