using Kozubenko;
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

        public SqliteDb()
        {
            counter++;

            Mutex.WaitOne();

            if(DbPath is null)
                throw new Exception("DbPath not set on DbLock");

            if (!File.Exists(DbPath))
                CreateDatabaseAndTable();

            OpenDatabase();
        }

        public static SqliteDb LockDb()
        {
            return new SqliteDb();
        }

        public void CreateDatabaseAndTable()
        {
            //Connection.CreateFile(DbPath);
            //OpenDatabase();

            //SQLiteCommand command = new SQLiteCommand($"CREATE TABLE {AGENT_MSGS_TABLE} (meterKey INT, agentID INT, featureID INT, timeStamp INT, data TEXT)", Connection);
            //command.ExecuteNonQuery();
        }

        private void OpenDatabase()
        {
            Connection = new SqliteConnection($"Data Source={DbPath};Version=3;");
            Connection.Open();
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
