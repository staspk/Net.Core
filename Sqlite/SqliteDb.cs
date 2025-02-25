using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite
{
    public class SqliteDb: IDisposable
    {
        public static string DbPath { get; set; }

        private static Mutex Mutex = new Mutex();

        private static int counter = 0;

        public SqliteConnection Connection { get; private set; }

        public SqliteDb(string connectionString)
        {
            counter++;

            Mutex.WaitOne();
        }

        public static SqliteDb LockDb()
        {
            return new SqliteDb("fakestring");
        }


        public void Dispose()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }
    }
}
