using System.Dynamic;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para dynamic.
    /// </summary>
    public static class DynamicExtensions
    {
        /// <summary>
        /// Extensão que faz a conversão de um dinâmico em um objeto.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto à ser convertido.</typeparam>
        /// <param name="dynamicObject">Este objeto.</param>
        /// <returns>Objeto convertido.</returns>
        public static T Cast<T>(this ExpandoObject dynamicObject) where T : class
        {
            var dymObject = (dynamic)dynamicObject;
            return (T)dymObject;
        }
    }
}
