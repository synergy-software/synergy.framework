using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Synergy.Catalogue.Reflection
{
    public static class ReflectionExtensions
    {
        public static string? GetDescription(this PropertyInfo property)
        {
            var attribute = property.GetInheritedAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }

        public static TAttribute? GetInheritedAttribute<TAttribute>(this PropertyInfo property)
            where TAttribute : Attribute
        {
            var attribute = property.GetCustomAttribute<TAttribute>(true);
            if (attribute != null)
                return attribute;

            foreach (var @interface in property.DeclaringType.GetInterfaces())
            {
                var interfaceProperty = @interface.GetProperty(property.Name);
                var interfaceAttribute = interfaceProperty?.GetCustomAttribute<TAttribute>(true);
                if (interfaceAttribute != null)
                    return interfaceAttribute;
            }

            return null;
        }
   
        public static string GetFriendlyTypeName(this Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null)
            {
                return $"{nullableType.GetFriendlyTypeName()}?";
            }
            
            if (type == typeof(string))
                return "string";
            if (type == typeof(long))
                return "long";
            if (type == typeof(ulong))
                return "ulong";
            if (type == typeof(int))
                return "int";
            if (type == typeof(uint))
                return "uint";
            if (type == typeof(short))
                return "short";
            if (type == typeof(ushort))
                return "ushort";
            if (type == typeof(decimal))
                return "decimal";
            if (type == typeof(float))
                return "float";
            if (type == typeof(double))
                return "double";
            if (type == typeof(bool))
                return "bool";
            if (type == typeof(byte))
                return "byte";
            if (type == typeof(sbyte))
                return "sbyte";
            if (type == typeof(char))
                return "char";
            if (type == typeof(object))
                return "object";

            if (type.IsArray)
            {
                var sb = new StringBuilder();

                var ranks = new Queue<int>();
                do {
                    ranks.Enqueue(type.GetArrayRank() - 1);
                    type = type.GetElementType();
                } while (type.IsArray);

                sb.Append(type.GetFriendlyTypeName());

                while (ranks.Count != 0) {
                    sb.Append('[');

                    int rank = ranks.Dequeue();
                    for (int i = 0; i < rank; i++)
                        sb.Append(',');

                    sb.Append(']');
                }

                return sb.ToString();
            }
            
            var arguments = type.GetGenericArguments();
            string typeName = type.Name;
            if (arguments.Length == 0)
                return typeName;

            var inner = arguments.Select(g => g.GetFriendlyTypeName());
            var index = typeName.IndexOf("`");
            typeName = typeName.Substring(0, index);
            return $"{typeName}<{String.Join(", ", inner)}>";
        }

        public static string GetFriendlyMethodName(this MethodInfo method)
        {
            return $"{method.Name}({String.Join(", ", method.GetParameters().Select(p => p.ParameterType.GetFriendlyTypeName()))})";
        }
    }
}