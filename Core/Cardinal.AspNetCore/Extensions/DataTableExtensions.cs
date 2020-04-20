using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Cardinal.AspNetCore.Extensions
{
    public static class DataTableExtensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            var data = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            var tmp = typeof(T);
            var instance = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo info in tmp.GetProperties())
                {
                    if (info.Name == column.ColumnName)
                    {
                        info.SetValue(instance, dr[column.ColumnName], null);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return instance;
        }
    }
}
