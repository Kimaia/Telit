using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shared.DB
{
	/// <summary>
	/// Base DAO. The connection to the sqlite db is done using an async connection. 
	/// This has 2 effects:
	/// 1. All operations are serialized (since the async connection uses a lock internally).
	/// 2. You can 'await' all operations which means you can call them from the UI thread.
	/// </summary>
	public class Dao
	{
		readonly SQLiteAsyncConnection asyncDb;

		public Dao()
		{
            asyncDb = Kimchi.Connection;
		}

		protected SQLiteAsyncConnection GetUnderlyingConnection()
		{
			return asyncDb;
		}

        protected string GetTableName<TEntity>(SQLiteConnection db) where TEntity : new()
		{
			return db.GetMapping<TEntity>().TableName;
		}

        public async Task ExecuteInTransactionAsync(Action<SQLiteConnectionWrapper> action)
        {
            await asyncDb.RunInWrappedTransactionAsync(action);
        }

        public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<SQLiteConnectionWrapper, TResult> func)
        {
            TResult result = default(TResult);
            Action<SQLiteConnectionWrapper> action = db =>
            {
                result = func(db);
            };
            await asyncDb.RunInWrappedTransactionAsync(action);
            return result;
        }

        public async Task<List<TEntity>> LoadAll<TEntity>() where TEntity : new()
		{
			List<TEntity> entities = null;

			Action<SQLiteConnectionWrapper> action = db => 
			{
                entities = db.LoadAll<TEntity>();
			};

			await asyncDb.RunInWrappedTransactionAsync(action);
			return entities;
		}

        public async Task DeleteAll<TEntity>(IEnumerable<TEntity> entities) where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
				db.DeleteAll(entities);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task Delete<TEntity>(TEntity entity) where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
				db.Delete(entity);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task DeleteAll<TEntity>() where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
                db.DeleteAll<TEntity>();
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task Insert<TEntity>(TEntity entity) where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => db.Insert(entity);
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task LoadAndUpdate<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> predicate) where TEntity : Entity, new()
        {
            Action<SQLiteConnectionWrapper> action = db => 
            {
                db.InsertOrReplace(entity, predicate);
            };
            await asyncDb.RunInWrappedTransactionAsync(action);
        }

        public async Task Update<TEntity>(TEntity entity) where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
				db.Update(entity);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, new()
		{
			TEntity entity = default(TEntity);
            Action<SQLiteConnectionWrapper> action = db => 
			{
                entity = db.Find<TEntity>(predicate);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
			return entity;
		}

        public async Task<TEntity> Find<TEntity>(object pk) where TEntity : class, new()
        {
            TEntity entity = default(TEntity);
            Action<SQLiteConnectionWrapper> action = db => 
            {
                entity = db.Find<TEntity>(pk);
            };
            await asyncDb.RunInWrappedTransactionAsync(action);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : new()
		{
            IEnumerable<TEntity> entities = null;
            Action<SQLiteConnectionWrapper> action = db => 
			{
                entities = db.FindAll<TEntity>(predicate);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
			return entities;
		}

        public async Task InsertOrReplace<TEntity>(TEntity entity) where TEntity : Entity
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
				db.InsertOrReplace(entity);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}

        public async Task InsertOrReplaceAll<TEntity>(List<TEntity> entities) where TEntity : Entity, new()
        {
            Action<SQLiteConnectionWrapper> action = db => 
            {
                db.InsertOrReplaceAll<TEntity>(entities);
            };
            await asyncDb.RunInWrappedTransactionAsync(action);
        }

        public async Task InsertAll<TEntity>(List<TEntity> entities) where TEntity : new()
		{
            Action<SQLiteConnectionWrapper> action = db => 
			{
                db.InsertAll<TEntity>(entities);
			};
            await asyncDb.RunInWrappedTransactionAsync(action);
		}
    }
}