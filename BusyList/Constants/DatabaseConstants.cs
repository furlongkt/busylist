using System;
using System.IO;

namespace BusyList.Constants
{
    /// <summary>
    /// This class holds constants used to interact with the local database.
    /// </summary>
    public static class DatabaseConstants
    {
        /// <summary>
        /// Name of the SQLite database file with the extension
        /// </summary>
        public const string DatabaseFilename = "BusyList.db3";

        /// <summary>
        /// Flags used to open the local database connection
        /// Currently these flags are:
        /// <c>SQLite.SQLiteOpenFlags.ReadWrite</c> - Open the database in read/write mode
        /// <c>SQLite.SQLiteOpenFlags.Create</c> - Create the database if it doesn't exist
        /// <c>SQLite.SQLiteOpenFlags.SharedCache</c> - Enable multi-threaded database access
        /// </summary>
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// Path to the local database's file including the filename and extension
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
