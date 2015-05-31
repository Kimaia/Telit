using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Shared.DB;
using Shared.SAL;
using Shared.Utils;
using Shared.Model;
using Shared.Network.DataTransfer;
using Shared.Network.DataTransfer.TR50;

namespace Shared.ModelManager
{
	public class DBFetchException : Exception
	{
		public DBFetchException(string message) : base(message) {}
	}

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
			return response;
		}
		#endregion


		#region item operations
		public async Task<TEntity> GetDBDataItemAsync<TEntity>(TR50Command commands, Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			// first load from DB
			var item = await LoadItemFromDBAsync<TEntity> (predicate);
			if (item == null) {
				throw new DBFetchException("Failed fetch Item");
			}
			Logger.Debug ("GetDataItemAsync(), :" + item.ToString());
			return item;
		}

		private async Task<TEntity> LoadItemFromDBAsync<TEntity> (Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			var dao = new Dao ();
			return await dao.Find<TEntity> (predicate);
		}

		private async Task<Type> LoadItemFromServerAsync<Type> (TR50Command commands) where Type : ITR50IsPayloadEmpty
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
		public async Task<TR50Response<Type>> LoadM2MDataListAsync<Type>(TR50Command commands) where Type : ITR50IsPayloadEmpty, new() 
		{
			return await m2mService.RequestListAsync<Type> (commands);
		}

		public async Task<List<Type>> GetDBDataListAsync<Type>() where Type : new()
		{
			var dao = new Dao ();
			var list = await dao.LoadAll<Type> ();

			Logger.Debug ("GetDBDataListAsync(), List count:" + list.Count);
			return list;
		}

		public async Task InsertListIntoDBAsync<Type> (List<Type> list) where Type : new()
		{
			var dao = new Dao();
			await dao.InsertAll<Type> (list);
		}
		#endregion
	}
}

