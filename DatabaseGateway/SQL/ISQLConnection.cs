using System.Collections.Generic;

namespace DatabaseGateway.SQL
{
    public interface ISQLConnection
    {
        int ExecuteStoredProcedure(string storedProcedure, Dictionary<string, string> parameters);
    }
}