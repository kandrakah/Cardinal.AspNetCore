using System.Data.Common;

namespace Cardinal.AspNetCore.Extensions
{
    public static class DbCommandExtensions
    {
        public static void AddParameter(this DbCommand cmd, string key, object value)
        {
            var dbParameter = cmd.CreateParameter();
            dbParameter.ParameterName = key;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
    }
}
