using System.Collections;
using System.Reflection;
using Synergy.Catalogue;

namespace Synergy.Documentation.Api;

/// <summary>
/// Explores types reachable through public instance properties for documentation purposes.
/// </summary>
/// <remarks>
/// Traversal unwraps nullable value types, arrays and the first generic argument of enumerable types.
/// Common scalar types and types in System subnamespaces are not explored recursively.
/// </remarks>
public class Dependencies
{
    private readonly bool includeNested;

    /// <summary>Collects a root type and the types discovered through its public properties.</summary>
    /// <param name="root">The starting type.</param>
    /// <param name="includeNested">Whether to also traverse public types nested directly in the root.</param>
    /// <returns>The types yielded by the traversal, in discovery order.</returns>
    public static List<Type> Of(Type root, bool includeNested = false)
    {
        return new Dependencies(includeNested)
               .DependenciesOf(root)
               .ToList();
    }

    private readonly List<Type> visited = new(100);

    private Dependencies(bool includeNested)
    {
        this.includeNested = includeNested;
    }

    /// <summary>Enumerates a type and its property dependencies using this explorer's traversal state.</summary>
    /// <param name="type">The starting type.</param>
    /// <returns>The types yielded by the traversal, including configured public nested roots.</returns>
    /// <remarks>Enumeration updates the explorer's collection of visited types.</remarks>
    public IEnumerable<Type> DependenciesOf(Type type)
    {
        var roots = new List<Type>();
        roots.Add(type);
        
        if (this.includeNested)
        {
            roots.AddRange(type.GetNestedTypes(BindingFlags.Public));
        }

        foreach (Type root in roots)
        {
            foreach (var dependency in this.DependenciesOf(root, 0))
            {
                yield return dependency;
            }
        }
    }

    private IEnumerable<Type> DependenciesOf(Type type, int level)
    {
        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
            type = underlyingType;

        if (type.IsPrimitive)
            yield break;

        if (type.In(typeof(string), typeof(Guid), typeof(DateTime), typeof(decimal)))
            yield break;

        if (level == 0)
        {
            this.visited.Add(type);
            yield return type;
        }

        if (type.IsArray)
        {
            type = type.GetElementType()
                   ?? throw new InvalidOperationException($"Array type {type} has no element type.");
        }
        else if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            if (type.GetGenericArguments()
                    .Length == 0)
                yield break;

            type = type.GetGenericArguments()
                       .First();
        }

        if (type.Namespace?.StartsWith("System.") == true)
            yield break;

        if (this.visited.Contains(type) && level > 0)
            yield break;

        if (this.visited.Contains(type) == false)
        {
            this.visited.Add(type);
            yield return type;
        }

        foreach (var property in this.GetPropertiesOf(type))
        {
            foreach (var dependency in this.DependenciesOf(property.PropertyType, level + 1))
            {
                yield return dependency;
            }
        }
    }

    private PropertyInfo[] GetPropertiesOf(Type type)
    {
        return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
    }
}