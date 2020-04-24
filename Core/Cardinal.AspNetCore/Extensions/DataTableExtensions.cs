using System.Collections.Generic;
using System.Data;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DataTable"/>.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Extensão para converter o Datatable para lista.
        /// </summary>
        /// <typeparam name="T">Objeto da lista.</typeparam>
        /// <param name="table">Este objeto.</param>
        /// <returns>Lista proveniente do DataTable atual.</returns>
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            var data = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var item = row.GetItem<T>();
                data.Add(item);
            }
            return data;
        }
    }
}
