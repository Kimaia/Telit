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

namespace Shared.Model
{
    public class PropertiesListAdapterModel : BaseModel
    {
        private DALManager dataManager;
        private Thing daThing;

        public Dictionary<string, Property> 	propertiesList { get; private set; }

        public PropertiesListAdapterModel(Thing thing)
        {
            dataManager = new DALManager();
            daThing = thing;
            propertiesList = new Dictionary<string, Property>();
        }


        public async Task PopulatePropertiesList(OnSuccess onSuccess, OnError onError)
        {
            await Task.Run(async () =>
                {
                    await PopulatePropertiesListAsync(onSuccess, onError);
                    if (propertiesList.Count == 0)
                        onError("PopulatePropertiesList()", "Loaded Properties List is Empty");
                    else
                        onSuccess();
                });
        }


        public async Task PopulatePropertiesListAsync(OnSuccess onSuccess, OnError onError)
        {
            try
            {
                var command = TR50CommandFactory.Build(M2MCommands.CommandType.Thing_Def_Find, daThing.defkey);
                var response = await dataManager.M2MLoadListAsync<TR50ThingDefParams>(command);
                propertiesList = response.Params.properties;

                // add the propertyKey to the Property object
                foreach (var item in propertiesList)
                    item.Value.key = item.Key;
				
                Logger.Debug("PopulatePropertiesListAsync(), Properties count:" + propertiesList.Count);
            }
            catch (Exception e)
            {
                onError("Failed Get Properties list", e.Message);
            }
        }
    }
}

