using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DatabaseGateway.SQL
{
    public class SQLConnection : ISQLConnection
    {
        private readonly SqlConnection SqlConnection;
        private string ConnectionString;

        public SQLConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        private void OpenConnection()
        {
            if (SqlConnection != null)
            {
                SqlConnection.Open();
            }
        }

        private void CloseConnection()
        {
            if (SqlConnection != null)
            {
                SqlConnection.Close();
            }
        }

        public int ExecuteStoredProcedure(string storedProcedure, Dictionary<string, string> parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("ProcedureName", SqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters != null)
                    {
                        foreach (var item in parameters)
                        {
                            command.Parameters.Add($"@{item.Key}", SqlDbType.VarChar);
                            command.Parameters[$"@{item.Key}"].Value = item.Value;
                        }
                    }

                    OpenConnection();

                    int result = command.ExecuteNonQuery();

                    CloseConnection();

                    return result;
                }
            }
        }
    }
}
