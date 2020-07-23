using System;
using System.Data;
using System.Reflection;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DataRow"/>.
    /// </summary>
    public static class DataRowExtensions
    {
        /// <summary>
        /// Extensão para converter o um DataRow em um objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto à ser parseado</typeparam>
        /// <param name="dataRow">Objeto referenciado</param>
        /// <returns>Objeto proveniente do DataRow</returns>
        public static T GetItem<T>(this DataRow dataRow)
        {
            var tmp = typeof(T);
            var instance = Activator.CreateInstance<T>();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                foreach (PropertyInfo info in tmp.GetProperties())
                {
                    if (info.Name == column.ColumnName)
                    {
                        info.SetValue(instance, dataRow[column.ColumnName], null);
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
