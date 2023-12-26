using System.Collections;
using System.Reflection;
using Synergy.Catalogue;

namespace Synergy.Documentation.Api;

public class Dependencies
{
    public static List<Type> Of(Type root)
    {
        return new Dependencies().DependsOn(root).ToList();
    }

    private readonly List<Type> visited = new(100);
    public Dependencies()
    {
    }
    
    public IEnumerable<Type> DependsOn(Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
            type = underlyingType;
        
        if (type.IsPrimitive)
            yield break;

        if (type.In(typeof(string), typeof(Guid), typeof(DateTime), typeof(decimal)))
            yield break;

        if (type.IsArray)
            type = type.GetElementType();
        else if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            if ( type.GetGenericArguments().Length == 0)
                yield break;
            
            type = type.GetGenericArguments().First();
        }
        
        if (type.Namespace.StartsWith("System."))
            yield break;
        
        if (this.visited.Contains(type))
            yield break;

        this.visited.Add(type);
        
        yield return type;
        
        foreach (var property in this.GetPropertiesOf(type))
        {
            foreach (var dependency in this.DependsOn(property.PropertyType))
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