using System;
using SQLite;
using System.Reflection;
using System.IO;

namespace Shared.DB
{
    public static class Kimchi
    {
        static Func<SQLiteAsyncConnection> createAsyncConnection = CreateDefaultSQLiteConnection;
        static Func<SQLiteConnection, SQLiteConnectionWrapper> createConnectionWrapper = CreateDefaultConnectionWrapper;

        /// <summary>
        /// Register the specified createAsyncConnection factory method. Use this if you want to specify your own parameters to the SQLiteAsyncConnection c'tor.
        /// </summary>
        /// <param name="createAsyncConnection">Create async connection.</param>
        public static void Register(Func<SQLiteAsyncConnection> createAsyncConnection)        
        {
            Kimchi.createAsyncConnection = createAsyncConnection;
        }

        /// <summary>
        /// Register the specified createConnectionWrapper. Use this if you want to create a different wrapper.
        /// </summary>
        /// <param name="createConnectionWrapper">Create connection wrapper.</param>
        public static void Register(Func<SQLiteConnection, SQLiteConnectionWrapper> createConnectionWrapper)
        {
            Kimchi.createConnectionWrapper = createConnectionWrapper;
        }

        /// <summary>
        /// Get a new async connection.
        /// </summary>
        /// <value>The connection.</value>
        public static SQLiteAsyncConnection Connection
        {
            get
            {
                return createAsyncConnection();
            }
        }

        /// <summary>
        /// Creates the connection wrapper. Don't use this method.
        /// </summary>
        /// <returns>The connection wrapper.</returns>
        /// <param name="connection">Connection.</param>
        internal static SQLiteConnectionWrapper CreateConnectionWrapper(SQLiteConnection connection)
        {
            return createConnectionWrapper(connection);
        }

        static SQLiteAsyncConnection CreateDefaultSQLiteConnection()
        {
            return new SQLiteAsyncConnection(DatabaseFullFileName);
        }

        static SQLiteConnectionWrapper CreateDefaultConnectionWrapper(SQLiteConnection connection)
        {
            return new SQLiteConnectionWrapper(connection);
        }

        static string SanitizeFileName(string fileName)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape( new string(Path.GetInvalidFileNameChars()) );
            string invalidRegStr = string.Format( @"([{0}]*\.+$)|([{0}]+)", invalidChars );

            return System.Text.RegularExpressions.Regex.Replace(fileName, invalidRegStr, "_" );        
        }

        static string DatabaseFullFileName
        {
            get { return Path.Combine(databaseFilePath, DatabaseFileName); }
        }

        /// <summary>
        /// Gets or sets the name of the database file. If you want to change the name of the file you should do this before any connection is created.
        /// </summary>
        /// <value>The name of the database file.</value>
        public static string DatabaseFileName
        {
            get;
            set;
        }

        static readonly string databaseFilePath;
        static string DatabaseFilePath 
        { 
            get 
            { 
                return databaseFilePath; 
            } 
        }

        static Kimchi()
        {
            DatabaseFileName = SanitizeFileName(Assembly.GetExecutingAssembly().GetName().Name) + ".db";
            #if __ANDROID__
            string libraryPath = 
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            #else // __IOS__
            // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
            // (they don't want non-user-generated data in Documents)
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            #endif
            databaseFilePath = libraryPath;
        }    
    }
}

