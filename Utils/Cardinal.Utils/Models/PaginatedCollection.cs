using System.Collections.Generic;

namespace System.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedCollection<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="total"></param>
        public PaginatedCollection(IEnumerable<T> collection, int total)
        {
            Collection = collection;
            Total = total;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Collection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
    }
}
