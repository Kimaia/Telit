using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

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


		#region item operations
		public async Task<TEntity> GetDataItemAsync<TEntity>(TR50Command commands, Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			// first load from DB
			var item = await LoadItemFromDBAsync<TEntity> (predicate);
			if (item == null) {
				// if DB empty - load from Server
				item = await LoadItemFromServerAsync<TEntity> (commands);
				if (item != null) {
					// and insert into DB
					await InsertItemIntoDBAsync (item);
				}
			}
			Logger.Debug ("GetDataItemAsync(), :" + item.ToString());
			return item;
		}

		private async Task<TEntity> LoadItemFromDBAsync<TEntity> (Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			var dao = new Dao ();
			return await dao.Find<TEntity> (predicate);
		}

		private async Task<Type> LoadItemFromServerAsync<Type> (TR50Command commands)
		{
			return await m2mService.RequestItemAsync<Type> (commands);
		}

		private async Task InsertItemIntoDBAsync<Type> (Type item) where Type : new()
		{
			var dao = new Dao();
			await dao.Insert<Type> (item);
		}
		#endregion

		#region list operations
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
		#endregion
	}
}

