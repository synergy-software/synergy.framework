using System.Collections;
using System.Reflection;
using Synergy.Catalogue;

namespace Synergy.Documentation.Api;

public class Dependencies
{
    public static List<Type> Of(Type root)
    {
        return new Dependencies().DependenciesOf(root)
                                 .ToList();
    }

    private readonly List<Type> visited = new(100);
    
    private IEnumerable<Type> DependenciesOf(Type type)
    {
        return this.DependenciesOf(type, 0);
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
            if (type.GetGenericArguments().Length == 0)
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
            foreach (var dependency in this.DependenciesOf(property.PropertyType))
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