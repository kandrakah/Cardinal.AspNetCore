using System;
using System.Data;
using System.Data.Common;

namespace Cardinal.Utils.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DbDataReader"/>.
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        /// Extensão que transforma um <see cref="DbDataReader"/> em um <see cref="DataTable"/>.
        /// </summary>
        /// <param name="reader">Objeto referenciado</param>
        /// <returns>DataTable proveniente do DataReader</returns>
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
                    column.DataType = row["DataType"] as Type;
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
