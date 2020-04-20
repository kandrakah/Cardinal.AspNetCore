using System;
using System.Data;
using System.Data.Common;

namespace Cardinal.AspNetCore.Extensions
{
    public static class DbDataReaderExtensions
    {
        public static DataTable ToDataTable(this DbDataReader reader)
        {
            var schema = reader.GetSchemaTable();
            var result = new DataTable(schema.TableName);

            foreach (DataRow row in schema.Rows)
            {
                var columnName = row["ColumnName"].ToString();
                if (!result.Columns.Contains(columnName))
                {
                    var column = new DataColumn();
                    column.ColumnName = columnName;
                    column.DataType = (Type)row["DataType"];
                    column.Unique = Convert.ToBoolean(row["IsUnique"]);
                    column.AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]);
                    column.ReadOnly = Convert.ToBoolean(row["IsReadOnly"]);
                    result.Columns.Add(column);
                }
            }

            while (reader.Read())
            {
                var row = result.NewRow();
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    row[i] = reader.GetValue(i);
                }
                result.Rows.Add(row);
            }

            return result;
        }
    }
}
