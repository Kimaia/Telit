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

	public class DALManager
	{
		private M2MAuthService 	m2mAuthenticator;
		private M2MApiService 	m2mApiService;

		public DALManager ()
		{
			m2mAuthenticator = M2MAuthService.Instance;
			m2mApiService = new M2MApiService();
		}



		#region Authenticate
		public async Task<RemoteResponse> AuthenticateAsync (string username, string password)
		{
			var response = await m2mAuthenticator.AuthenticateAsync(username,  password);
			return response;
		}
		#endregion


		#region M2M
		private async Task<Type> M2MLoadItemAsync<Type> (TR50Command commands) where Type : ITR50HasPayload
		{
			return await m2mApiService.RequestItemAsync<Type> (commands);
		}

		public async Task<TR50Response<Type>> M2MLoadListAsync<Type>(TR50Command commands) where Type : ITR50HasPayload, new() 
		{
			return await m2mApiService.RequestListAsync<Type> (commands);
		}
		#endregion

		#region DB
		public async Task<TEntity> DBLoadItemAsync<TEntity> (Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			var dao = new Dao ();
			var item = await dao.Find<TEntity> (predicate);
			if (item == null) {
				throw new DBFetchException("DB Failed fetch Item: " + predicate);
			}
			Logger.Debug ("DBLoadItemAsync(), :" + item.ToString());
			return item;
		}

		private async Task DBInsertItemAsync<Type> (Type item) where Type : new()
		{
			var dao = new Dao();
			await dao.Insert<Type> (item);
		}

		public async Task<List<Type>> DBLoadListAsync<Type>() where Type : new()
		{
			var dao = new Dao ();
			var list = await dao.LoadAll<Type> ();

			Logger.Debug ("DBLoadListAsync(), List count:" + list.Count);
			return list;
		}

		public async Task DBInsertListAsync<Type> (List<Type> list) where Type : new()
		{
			var dao = new Dao();
			await dao.InsertAll<Type> (list);
		}
		#endregion
	}
}

