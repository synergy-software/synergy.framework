using System.Collections;
using System.Reflection;
using Synergy.Catalogue;

namespace Synergy.Documentation.Api;

public class Dependencies
{
    private readonly bool includeNested;

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
            type = type.GetElementType();
        }
        else if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            if (type.GetGenericArguments()
                    .Length == 0)
                yield break;

            type = type.GetGenericArguments()
                       .First();
        }

        if (type.Namespace.StartsWith("System."))
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