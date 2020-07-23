using System.Collections.Generic;
using System.Data;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DataTable"/>.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Extensão para converter o Datatable para lista.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto à ser parseado</typeparam>
        /// <param name="table">Objeto referenciado</param>
        /// <returns>Lista proveniente do DataTable atual</returns>
        public static IEnumerable<T> Enumerate<T>(this DataTable table) where T : new()
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
