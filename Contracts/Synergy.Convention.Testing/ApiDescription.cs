using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Synergy.Convention.Testing
{
    public class ApiDescription
    {
        public static string GenerateFor(Assembly assembly)
        {
            StringBuilder description = new StringBuilder();

            string assemblyName = assembly.GetName().Name;
            description.AppendLine($"# {assemblyName}");
            description.AppendLine();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic == false)
                    continue;

                var gType = type.IsValueType ? "struct " : (type.IsEnum ? "enum " : "");
                description.AppendLine($"## {gType}{type.FullName.Replace(assemblyName + ".", "")}:");
                foreach (var property in type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    description.AppendLine($" - {GetAttributes(property)}{property.Name}: {GetTypeName(property)}");
                }
                
                foreach (var method in type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    if (method.IsSpecialName || method.DeclaringType == typeof(object) || method.DeclaringType == typeof(Exception))
                        continue;
                    
                    description.AppendLine($" - {method.Name}({GetParametersOf(method)}) : {GetTypeName(method.ReturnType)}");
                }
                
                description.AppendLine();
            }
            
            return description.ToString();
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
                description.Append($"      {GetAttributes(parameter)}{parameter.Name}: {GetTypeName(parameter.ParameterType)}");
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
        
        private static string GetTypeName(Type type)
        {
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

            return $"[{string.Join(", ", attributes)}] ";
        }
    }
}