using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqlite;

namespace Azure.SqlConnector
{
    public class SqlConnector
    {
        public String ConnectionString { get; set; }
    
        public SqlConnector(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool ConnectAndDumpIntoLocalDb(SqliteDb db)
        {
            try
            {
                if (ConnectionString is null)
                    return false;

                var builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = ConnectionString;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM WattAwareAgent", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //AgentMessageParser parser = AgentMessageParser.Instance;
                            //while (reader.Read())
                            //{
                            //    dbToDumpInto.InsertIntoAgentMsgTable(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4));

                            //    IAgentMsgPayload data = parser.BuildMsgPayload(reader.GetString(4));

                            //    if (data.MsgType == AgentMessaging.AgentMessageType.Event && data.HasBeenBuiltSuccessfully())
                            //    {
                            //        EventPayload dataPayload = (EventPayload)data;
                            //        dataPayload.ParseDataIntoDatabase(db);
                            //    }
                            //}
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return false;
        }
    }
}
