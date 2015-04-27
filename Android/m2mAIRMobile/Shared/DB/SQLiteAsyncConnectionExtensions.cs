// Another piece in the puzzle.
//
//  Author:
//       Doron Tohar <doron@kimaia.com>
//
using System;
using SQLite;
using System.Threading.Tasks;

namespace Shared.DB
{
    public static class SQLiteAsyncConnectionExtensions
    {
        public static async Task RunInWrappedTransactionAsync(this SQLiteAsyncConnection asyncDb, Action<SQLiteConnectionWrapper> action)
        {
            SQLiteConnectionWrapper connectionWrapper = null;
            Action<SQLiteConnection> wrappedAction = db =>
            {
                connectionWrapper = Kimchi.CreateConnectionWrapper(db);
                action(connectionWrapper);
            };
            try
            {
                await asyncDb.RunInTransactionAsync(wrappedAction);
                connectionWrapper.Commit();
            }
            catch (Exception e)
            {
                connectionWrapper.Rollback(e);
            }
        }
    }
}

