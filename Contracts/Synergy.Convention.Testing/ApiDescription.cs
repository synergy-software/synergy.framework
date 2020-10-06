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

                var gType = type.IsValueType ? " (struct)" : (type.IsEnum ? " (enum)" : "");
                description.AppendLine($"## {type.FullName.Replace(assemblyName + ".", "")}{gType}:");
                foreach (var property in type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    description.AppendLine($" - {property.Name}: {GetTypeName(property)}{GetAttributes(property)}");
                }
                
                foreach (var method in type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                {
                    if (method.IsSpecialName || method.DeclaringType == typeof(object) || method.DeclaringType == typeof(Exception))
                        continue;

                    var generics = method.GetGenericArguments();
                    var gD = generics.Length == 0 ? "" : "<"+String.Join(", ", generics.Select(g => g.Name))+">";
                    description.AppendLine($" - {method.Name}{gD}({GetParametersOf(method)}) : {GetTypeName(method)}{GetAttributes(method)}");
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
            
            // var paramsArray = parameter.GetCustomAttribute<ParamArrayAttribute>();
            //
            // if (paramsArray != null)
            //     type = "params " + type;
            
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