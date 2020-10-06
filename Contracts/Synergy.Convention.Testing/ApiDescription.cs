using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace Synergy.Convention.Testing
{
    public static class ApiDescription
    {
        public static string GenerateFor(Assembly assembly)
        {
            StringBuilder description = new StringBuilder();

            string assemblyName = assembly.GetName().Name;
            description.AppendLine($"# {assemblyName}");
            description.AppendLine();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic == false && type.IsNestedPublic == false)
                    continue;

                var gType = type.IsEnum ? " (enum)" : (type.IsValueType ? " (struct)" : "");
                var baseType = ApiDescription.GetBaseTypeName(type);
                description.AppendLine($"## {type.FullName.Replace(assemblyName + ".", "")}{gType}{baseType}");
                foreach (var property in type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    description.AppendLine($" - {GetPropertyName(property)}: {GetTypeName(property)}{GetAttributes(property)} {GetAccessors(property)}");
                }

                foreach (var field in type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    if (field.Name == "value__" && type.IsEnum)
                        continue;
                    
                    description.AppendLine($" - {ApiDescription.GetFieldName(field)}: {GetTypeName(field)}{GetAttributes(field)} (field)");
                }

                if (type.IsEnum == false)
                {
                    foreach (var method in type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                    {
                        if (method.IsSpecialName || method.DeclaringType == typeof(object) || method.DeclaringType == typeof(Exception))
                            continue;
                        
                        if (type.IsValueType && method.Name.In(nameof(Equals), nameof(GetHashCode), nameof(ToString)))
                            continue;

                        var generics = method.GetGenericArguments();
                        var gD = generics.Length == 0 ? "" : "<" + String.Join(", ", generics.Select(g => g.Name)) + ">";
                        description.AppendLine($" - {GetMethodName(method)}{gD}({GetParametersOf(method)}) : {GetTypeName(method)}{GetAttributes(method)}");
                    }
                }

                description.AppendLine();
            }
            
            return description.ToString();
        }

        private static string GetAccessors(PropertyInfo property)
        {
            var canRead = property.GetGetMethod(false)?.IsPublic ?? false;
            var canWrite = property.GetSetMethod(false)?.IsPublic ?? false;
            
            if (canRead && canWrite)
                return "{ get; set; }";
            
            if (canRead)
                return "{ get; }";
            
            if (canWrite)
                return "{ set; }";

            return "{ ??? }";
        }

        private static string GetFieldName(FieldInfo field)
        {
            if (field.IsStatic)
                return $"{field.DeclaringType.Name}.{field.Name}";

            return field.Name;
        }

        private static string GetPropertyName(PropertyInfo property)
        {
            if (property.IsStatic())
                return $"{property.DeclaringType.Name}.{property.Name}";
            
            return property.Name;
        }

        public static bool IsStatic(this PropertyInfo source, bool nonPublic = false) 
            => source.GetAccessors(nonPublic).Any(x => x.IsStatic);
        
        [Pure] public static bool In<T>(this T value, params T[] values)
            => values.Contains(value);
        
        [Pure] public static bool NotIn<T>(this T value, params T[] values)
            => value.In(values) == false;
        
        private static string GetMethodName(MethodInfo method)
        {
            if (method.IsStatic)
                return $"{method.DeclaringType.Name}.{method.Name}";
            
            return method.Name;
        }

        private static string GetBaseTypeName(Type type)
        {
            if (type.BaseType == null)
                return "";
            
            if (type.BaseType == typeof(object))
                return "";
            
            if (type.IsValueType)
                return "";
            
            return " : " + type.BaseType.Name;
        }

        private static string GetParametersOf(MethodInfo method)
        {
            var parameters = method.GetParameters();
            if (parameters.Any() == false)
                return "";

            var last = parameters.Last();
            StringBuilder description = new StringBuilder();
            foreach (ParameterInfo parameter in parameters)
            {
                description.AppendLine();
                description.Append($"      {parameter.Name}: {GetTypeName(parameter)}{GetAttributes(parameter)}");
                if (parameter != last)
                    description.Append(",");
            }

            description.AppendLine();
            description.Append("   ");
            return description.ToString();
        }

        private static string GetTypeName(PropertyInfo property)
        {
            var type = GetTypeName(property.PropertyType);
            var nullable = property.GetCustomAttributes()
                                   .Any(a => a.GetType()
                                              .FullName == "System.Runtime.CompilerServices.NullableAttribute");
            
            if (nullable)
                return type +"?";
            
            return type;
        }
        
        private static string GetTypeName(FieldInfo field)
        {
            var type = GetTypeName(field.FieldType);
            return type;
        }
        
        private static string GetTypeName(MethodInfo method)
        {
            var type = GetTypeName(method.ReturnType);
            var nullable = method.GetCustomAttributes()
                                   .Any(a => a.GetType()
                                              .FullName == "System.Runtime.CompilerServices.NullableContextAttribute");
            
            if (nullable)
                return type +"?";
            
            return type;
        }
        
        private static string GetTypeName(ParameterInfo parameter)
        {
            var type = GetTypeName(parameter.ParameterType);
            var nullable = parameter.GetCustomAttributes()
                                    .Any(a => a.GetType()
                                               .FullName == "System.Runtime.CompilerServices.NullableAttribute");
            
            var paramsArray = parameter.GetCustomAttribute<ParamArrayAttribute>();
            
            if (paramsArray != null)
                type = "params " + type;
            
            var outAttribute = parameter.GetCustomAttribute<OutAttribute>();
            
            if (outAttribute != null)
                type = "out " + type;
            
            if (nullable)
                return type +"?";
            
            return type;
        }
        
        private static string GetTypeName(Type type)
        {
            if (type == typeof(object))
                return "object";
            
            if (type == typeof(string))
                return "string";

            if (type == typeof(int))
                return "int";

            if (type == typeof(long))
                return "long";
            
            if (type == typeof(bool))
                return "bool";
            
            return type.Name;
        }

        private static string GetAttributes(MemberInfo member)
        {
            var attributes = member.GetCustomAttributes();
            return ApiDescription.GetAttributes(attributes);
        }

        private static string GetAttributes(ParameterInfo parameter)
        {
            var attributes = parameter.GetCustomAttributes();
            return ApiDescription.GetAttributes(attributes);
        }

        private static string GetAttributes(IEnumerable<Attribute> enumerable)
        {
            var attributes = enumerable.Where(a => a.GetType().Name.StartsWith("__") == false)
                                       .Select(a => a.GetType().Name.Replace("Attribute", ""))
                                       .ToList();
            if (attributes.Any() == false)
                return "";

            return $" [{string.Join(", ", attributes)}]";
        }
    }
}