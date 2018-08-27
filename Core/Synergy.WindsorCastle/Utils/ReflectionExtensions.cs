using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Synergy.Core.Extensions
{
    internal static class ReflectionExtensions
    {
        [Pure]
        public static bool HasCustomAttribute<TAttribute>([NotNull] this Type type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttribute<TAttribute>() != null;
        }
    }
}