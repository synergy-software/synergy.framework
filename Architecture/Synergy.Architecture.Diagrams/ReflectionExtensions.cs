using System.Reflection;

namespace Synergy.Architecture.Diagrams;

internal static class ReflectionExtensions
{
    public static bool IsRecord(this Type type)
        => type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Any(m => m.Name == "<Clone>$");

    public static bool IsAssignableTo(this Type type, string assignableType)
    {
        var checkedType = type;
        while (checkedType != null)
        {
            if (checkedType.FullName == assignableType)
                return true;
                
            checkedType = checkedType.BaseType;
        }

        return false;
    }
    
    public static Attribute? GetCustomAttribute(this Type type, string attributeType)
        => type.GetCustomAttributes().FirstOrDefault(a => a.GetType().IsAssignableTo(attributeType));
    
    public static Attribute? GetCustomAttribute(this MethodInfo method, string attributeType)
        => method.GetCustomAttributes().FirstOrDefault(a => a.GetType().IsAssignableTo(attributeType));
    
    public static T GetPropertyValue<T>(this object obj, string propertyName)
        => (T)obj.GetType().GetProperty(propertyName)?.GetValue(obj);
}