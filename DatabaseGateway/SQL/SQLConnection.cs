using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseGateway.SQL
{
    public class SQLConnection : ISQLConnection
    {
        private readonly string ConnectionString;

        public SQLConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        public int ExecuteStoredProcedure(string storedProcedure, Dictionary<string, string> parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("ProcedureName", connection) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters != null)
                    {
                        foreach (var item in parameters)
                        {
                            command.Parameters.Add($"@{item.Key}", SqlDbType.VarChar);
                            command.Parameters[$"@{item.Key}"].Value = item.Value;
                        }
                    }

                    connection.Open();

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    return result;
                }
            }
        }
    }
}
