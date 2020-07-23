using Cardinal.Utils;
using System.Reflection;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Extensão que traz a versão do assembly.
        /// </summary>
        /// <param name="assembly">Objeto referenciado</param>
        /// <returns>Instância de <see cref="CardinalVersion"/> contendo a versão do assembly</returns>
        public static CardinalVersion GetVersion(this Assembly assembly)
        {
            var assemblyVersion = assembly.GetName().Version;
            var version = CardinalVersion.Parse(assemblyVersion);
            return version;
        }
    }
}
