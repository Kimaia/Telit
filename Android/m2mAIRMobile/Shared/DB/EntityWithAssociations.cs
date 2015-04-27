using System;
using SQLite;
using System.Collections.Generic;

namespace Shared.DB
{
    public interface IEntityWithAssociations
    {
        int Id { get; set; }
        void BeforeInsert(SQLiteConnectionWrapper db);
        void AfterInsert(SQLiteConnectionWrapper db);
        void AfterUpdate(SQLiteConnectionWrapper db);
        void AfterDelete(SQLiteConnectionWrapper db);
        void AfterLoad(SQLiteConnectionWrapper db);
    }

    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }

    public abstract class EntityWithAssociations: Entity, IEntityWithAssociations
    {
        #region IEntity implementation
        public virtual void BeforeInsert(SQLiteConnectionWrapper db)
        {
            // do nothing
        }

        public virtual void AfterInsert(SQLiteConnectionWrapper db)
        {
            // do nothing
        }

        public virtual void AfterDelete(SQLiteConnectionWrapper db)
        {
            // do nothing
        }

        public virtual void AfterLoad(SQLiteConnectionWrapper db)
        {
            // do nothing
        }

        public virtual void AfterUpdate(SQLiteConnectionWrapper db)
        {
            // Usually we want to do the same thing on insert and on update
            AfterInsert(db);
        }
        #endregion
    }
}

