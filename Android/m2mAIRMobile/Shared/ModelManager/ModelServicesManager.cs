using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.DB;
using Shared.SAL;
using Shared.Utils;
using Shared.Network.DataTransfer;
using Shared.Network.DataTransfer.TR50;

namespace Shared.ModelManager
{
	public class ModelServicesManager
	{
		private M2MAuthenticator m2mAuthenticator;
		private M2MServiceManager m2mService;

		public ModelServicesManager ()
		{
			m2mAuthenticator = M2MAuthenticator.Instance;
			m2mService = new M2MServiceManager();
		}



		#region Authenticate
		public async Task<RemoteResponse> AuthenticateAsync (string username, string password)
		{
			var response = await m2mAuthenticator.AuthenticateAsync(username,  password);
			Logger.Debug ("AuthenticateAsync(), ResponseCode: " + response.StatusCode + ", StatusMessage: " + response.StatusMessage);
			return response;
		}
		#endregion


		public async Task<List<Type>> GetDataListAsync<Type>(TR50Command commands) where Type : new()
		{
			// first load from DB
			var list = await LoadListFromDBAsync<Type> ();
			if (list.Count == 0) {
				// if DB empty - load from Server
				list = await LoadListFromServerAsync<Type> (commands);
				if (list.Count > 0) {
					// and insert into DB
					await InsertListIntoDBAsync (list);
				}
			}
			Logger.Debug ("GetDataListAsync(), List count:" + list.Count);
			return list;
		}
	

		private async Task<List<Type>> LoadListFromDBAsync<Type> () where Type : new()
		{
			var dao = new Dao ();
			return await dao.LoadAll<Type> ();
		}

		private async Task<List<Type>> LoadListFromServerAsync<Type> (TR50Command commands)
		{
			return await m2mService.RequestListAsync<Type> (commands);
		}

		private async Task InsertListIntoDBAsync<Type> (List<Type> list) where Type : new()
		{
			var dao = new Dao();
			await dao.InsertAll<Type> (list);
		}
	}
}

