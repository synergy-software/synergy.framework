using System;

namespace Synergy.Sample.Web.API.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsConstructable(this Type t)
        {
            return !t.IsAbstract;
        }
    }
}