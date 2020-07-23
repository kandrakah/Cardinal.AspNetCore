using System;
using System.Reflection;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="MemberInfo"/>.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Extensão que traz o tipo do <see cref="MemberInfo"/> informado.
        /// </summary>
        /// <param name="memberInfo">Objeto referenciado</param>
        /// <returns>Tipo equivalente ao MemberInfo informado</returns>
        public static Type AsType(this MemberInfo memberInfo)
        {
            return memberInfo as Type;
        }

        /// <summary>
        /// Extensão que traz o nome qualificado do <see cref="MemberInfo"/> informado.
        /// </summary>
        /// <param name="memberInfo">Objeto referenciado</param>
        /// <returns>Nome qualificado do MemberInfo</returns>
        public static string GetQualifiedName(MemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                var type = memberInfo as Type;
                return type != null ? (type.Namespace + "." + type.Name) : memberInfo.Name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Extensão que traz o tipo herdado do <see cref="MemberInfo"/> informado.
        /// </summary>
        /// <param name="memberInfo">Objeto referenciado</param>
        /// <returns>Tipo herdado pelo MemberInfo informado</returns>
        public static Type GetReflectedType(MemberInfo memberInfo)
        {
            return memberInfo.ReflectedType;
        }
    }
}
