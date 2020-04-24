using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para MemberInfo.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Return the type from a MemberInfo.
        /// </summary>
        /// <param name="memberInfo">The MemberInfo to convert.</param>
        /// <returns>The type from a MemberInfo.</returns>
        public static Type AsType(MemberInfo memberInfo)
        {
            return memberInfo as Type;
        }

        /// <summary>
        /// Return the qualified name from a member info.
        /// </summary>
        /// <param name="memberInfo">The member info to convert.</param>
        /// <returns>The qualified name from a member info.</returns>
        public static string GetQualifiedName(MemberInfo memberInfo)
        {
            Contract.Assert(memberInfo != null);
            var type = memberInfo as Type;
            return type != null ? (type.Namespace + "." + type.Name) : memberInfo.Name;
        }

        /// <summary>
        /// Return the reflected type from a member info.
        /// </summary>
        /// <param name="memberInfo">The member info to convert.</param>
        /// <returns>The reflected type from a member info.</returns>
        public static Type GetReflectedType(MemberInfo memberInfo)
        {
            return memberInfo.ReflectedType;
        }
    }
}
