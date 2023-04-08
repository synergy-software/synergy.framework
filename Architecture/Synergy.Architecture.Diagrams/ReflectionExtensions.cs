using System.Reflection;

namespace Synergy.Architecture.Diagrams;

internal static class ReflectionExtensions
{
    public static bool IsRecord(this Type type)
        => type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Any(m => m.Name == "<Clone>$");
}