// Another piece in the puzzle.
//
//  Author:
//       Doron Tohar <doron@kimaia.com>
//
using System;
using System.Linq.Expressions;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Shared.DB
{
    public class SQLiteConnectionWrapper
    {
        readonly SQLiteConnection db;
        public SQLiteConnection WrappedConnection { get { return db; } }

        public SQLiteConnectionWrapper(SQLiteConnection db)
        {
            this.db = db;
        }

        public virtual void Commit()
        {
            // do nothing
        }

        public virtual void Rollback(Exception e)
        {
            // do nothing
        }

        public string GetTableName<TEntity>() where TEntity: new()
        {
            return db.GetMapping<TEntity>().TableName;
        }

        public int DeleteAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: new()
        {
            var entities = db.Table<TEntity>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                db.Delete(entity);
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterDelete(this);
                }
            }
            return entities.Count();
        }

        public List<TEntity> LoadAll<TEntity>() where TEntity: new()
        {
            var entities = new List<TEntity>(db.Table<TEntity>());
            foreach (var entity in entities)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterLoad(this);
                }
            }
            return entities;
        }

        public void DeleteAll<TEntity>(IEnumerable<TEntity> entities) where TEntity: new()
        {
            foreach (var entity in entities)
            {
                db.Delete(entity);
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterDelete(this);
                }
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity: new()
        {
            db.Delete(entity);
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).AfterDelete(this);
            }
        }

        public void DeleteAll<TEntity>() where TEntity: new()
        {
            var entities = db.Table<TEntity>();
            foreach (var entity in entities)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterDelete(this);
                }
            }
            db.DeleteAll<TEntity>();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity: new()
        {
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).BeforeInsert(this);
            }
            db.Insert(entity);
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).AfterInsert(this);
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity: new()
        {
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).BeforeInsert(this);
            }
            db.Update(entity);
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).AfterInsert(this);
            }
        }

        public TEntity Find<TEntity>(object pk) where TEntity: class, new()
        {
            var entity = db.Find<TEntity>(pk);
            if (entity != null)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterLoad(this);
                }
            }
            return entity;
        }

        public TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: class, new()
        {
            var entity = db.Find<TEntity>(predicate);
            if (entity != null)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterLoad(this);
                }
            }
            return entity;
        }

        public IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity: new()
        {
            var entities = db.Table<TEntity>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterLoad(this);
                }
            }
            return entities;
        }

        public void InsertOrReplace<TEntity>(TEntity entity) where TEntity: Entity
        {
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).BeforeInsert(this);
            }
            if (entity.Id == 0)
            {
                db.Insert(entity);
            }
            else
            {
                db.Update(entity);
            }
            if (entity is IEntityWithAssociations)
            {
                ((IEntityWithAssociations)entity).AfterInsert(this);
            }
        }

        public void InsertOrReplace<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> predicate) where TEntity: Entity, new()
        {
            if (entity.Id == 0)
            {
                var existing = db.Find<TEntity>(predicate);
                if (existing != null)
                {
                    entity.Id = existing.Id;
                }
            }
            InsertOrReplace(entity);
        }

        public void InsertAll<TEntity>(IEnumerable<TEntity> entities) where TEntity: new()
        {
            foreach (var entity in entities)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).BeforeInsert(this);
                }
                Insert(entity);
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterInsert(this);
                }
            }
        }

        public void InsertOrReplaceAll<TEntity>(IEnumerable<TEntity> entities) where TEntity: Entity, new()
        {
            foreach (var entity in entities)
            {
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).BeforeInsert(this);
                }
                InsertOrReplace(entity);
                if (entity is IEntityWithAssociations)
                {
                    ((IEntityWithAssociations)entity).AfterInsert(this);
                }
            }
        }

    }
}

