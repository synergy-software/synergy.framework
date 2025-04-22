using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Synergy.Architecture.Diagrams.Api
{
    // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: remove this class from here - check what it is used for
    
    internal static class ApiDescription
    {
        public static string For(MethodInfo method, bool withAttributes = true)
        {
            var generics = method.GetGenericArguments();
            var gD = generics.Length == 0 ? "" : "<" + String.Join(", ", generics.Select(g => ApiDescription.GetTypeName(g))) + ">";
            var attributes = withAttributes ? ApiDescription.GetAttributes(method) : "";
            var methodDescription = $"{ApiDescription.GetMethodName(method)}{gD}({ApiDescription.GetParametersOf(method, withAttributes)}) : {ApiDescription.GetTypeName(method)}{attributes}";
            return methodDescription;
        }

        private static string GetMethodName(MethodInfo method)
        {
            if (method.IsStatic)
                return $"{method.DeclaringType.Name}.{method.Name}";

            return method.Name;
        }

        private static string GetParametersOf(MethodInfo method, bool withAttributes = true)
        {
            var parameters = method.GetParameters();
            if (parameters.Any() == false)
                return "";

            var last = parameters.Last();
            StringBuilder description = new StringBuilder();
            foreach (ParameterInfo parameter in parameters)
            {
                description.AppendLine();
                var attributes = withAttributes ? ApiDescription.GetAttributes(parameter) : "";
                description.Append($"     {parameter.Name}: {ApiDescription.GetTypeName(parameter)}{attributes}");
                if (parameter != last)
                    description.Append(",");
            }

            description.AppendLine();
            description.Append("   ");
            return description.ToString();
        }

        public static string GetTypeName(MethodInfo method)
        {
            var type = ApiDescription.GetTypeName(method.ReturnType);
            var nullable = method.GetCustomAttributes()
                .Any(a => a.GetType()
                    .FullName == "System.Runtime.CompilerServices.NullableContextAttribute");

            if (nullable)
                return type + "?";

            return type;
        }

        public static string GetTypeName(ParameterInfo parameter)
        {
            var type = ApiDescription.GetTypeName(parameter.ParameterType);
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
                return type + "?";

            return type;
        }

        public static string GetTypeName(Type type)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return $"{ApiDescription.GetTypeName(underlyingType)}?";
            }

            if (type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();
                return
                    $"{type.Name.Substring(0, type.Name.IndexOf("`", StringComparison.Ordinal))}<{String.Join(", ", arguments.Select(a => ApiDescription.GetTypeName(a)))}>";
            }

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

            if (type == typeof(void))
                return "void";

            if (type == typeof(decimal))
                return "decimal";

            if (type == typeof(byte))
                return "byte";
            
            if (type.FullName == null)
                return type.Name;

            return type.FullName.Substring(type.FullName.LastIndexOf('.') + 1);
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
                .Where(a => a is not DebuggerStepThroughAttribute)
                .Select(a => a.GetType().Name.Replace("Attribute", ""))
                .ToList();

            if (attributes.Any() == false)
                return "";

            return $" [{string.Join(", ", attributes)}]";
        }
    }
}