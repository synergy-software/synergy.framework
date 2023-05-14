using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace Synergy.Architecture.Diagrams.Api
{
    // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: remove this class from here - check what it is used for
    
    public static class ApiDescription
    {
        static readonly BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        public static string GenerateFor(Assembly assembly)
        {
            var description = new StringBuilder();

            var assemblyName = assembly.GetName().Name;
            description.AppendLine($"# {assemblyName}");
            description.AppendLine();

            return ApiDescription.GenerateFor(assembly.GetTypes(), description, assemblyName);
        }

        public static string GenerateFor(IEnumerable<Type> types, StringBuilder? description = null, string? assemblyName = null)
        {
            description ??= new StringBuilder();

            foreach (var type in types)
            {
                if (type.IsPublic == false && type.IsNestedPublic == false)
                    continue;

                var shortNamespace = ApiDescription.GetShortenNamespace(type, assemblyName);
                var typeName = ApiDescription.GetShortTypeName(type);
                var baseType = ApiDescription.GetParents(type);

                description.AppendLine($"## {shortNamespace}{ApiDescription.GetTypeName(type)}{typeName}{baseType}");

                foreach (var property in type.GetProperties(ApiDescription.bindingFlags).OrderBy(p => p.Name))
                {
                    if (property.DeclaringType == typeof(Exception) || property.DeclaringType == typeof(Attribute))
                        continue;

                    description.AppendLine($" - {ApiDescription.GetPropertyName(property)}: {ApiDescription.GetTypeName(property)}{ApiDescription.GetAttributes(property)} {ApiDescription.GetAccessors(property)}");
                }

                foreach (var field in type.GetFields(ApiDescription.bindingFlags))
                {
                    if (field.Name == "value__" && type.IsEnum)
                        continue;

                    if (type.IsEnum)
                    {
                        description.AppendLine($" - {field.Name} = {(int)Enum.Parse(type, field.Name)}");
                    }
                    else
                    {
                        description.AppendLine($" - {ApiDescription.GetFieldName(field)}: {ApiDescription.GetTypeName(field)}{ApiDescription.GetAttributes(field)} (field)");
                    }
                }

                if (type.IsEnum == false)
                {
                    foreach (var constructor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                    {
                        description.AppendLine($" - ctor({ApiDescription.GetParametersOf(constructor)})");
                    }

                    foreach (var method in type.GetMethods(ApiDescription.bindingFlags).OrderBy(m => m.Name))
                    {
                        if (method.IsSpecialName || method.DeclaringType.In(typeof(object), typeof(Exception), typeof(Attribute)))
                            continue;

                        if (type.IsValueType && method.Name.In(nameof(Equals), nameof(object.GetHashCode), nameof(object.ToString)))
                            continue;

                        if (method.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                            continue;

                        if (type.IsRecord() && method.Name.In(nameof(object.ToString), nameof(object.GetHashCode), nameof(Equals), "<Clone>$", "Deconstruct"))
                            continue;

                        var methodDescription = ApiDescription.For(method);
                        description.AppendLine($" - {methodDescription}");
                    }
                }

                description.AppendLine();
            }

            return description.ToString();
        }

        public static string For(MethodInfo method, bool withAttributes = true)
        {
            var generics = method.GetGenericArguments();
            var gD = generics.Length == 0 ? "" : "<" + String.Join(", ", generics.Select(g => ApiDescription.GetTypeName(g))) + ">";
            var attributes = withAttributes ? ApiDescription.GetAttributes(method) : "";
            var methodDescription = $"{ApiDescription.GetMethodName(method)}{gD}({ApiDescription.GetParametersOf(method, withAttributes)}) : {ApiDescription.GetTypeName(method)}{attributes}";
            return methodDescription;
        }

        private static string GetShortenNamespace(Type type, string? assemblyName)
        {
            if (assemblyName == null)
                return type.Namespace + ".";
            
            var shortNamespace = type.Namespace.Replace(assemblyName, "").TrimStart('.');
            if (String.IsNullOrEmpty(shortNamespace) == false)
                shortNamespace += ".";
            return shortNamespace;
        }

        private static string GetShortTypeName(Type type)
        {
            var typeName = type.IsEnum ? " (enum)" : (type.IsValueType ? " (struct)" : " (class)");
            if (type.IsRecord())
                typeName = " (record)";
            if (typeof(Exception).IsAssignableFrom(type))
                typeName = " (exception)";
            if (typeof(Attribute).IsAssignableFrom(type))
                typeName = " (attribute)";
            if (type.IsAbstract)
                typeName = " (abstract class)";
            if (type.IsInterface)
                typeName = " (interface)";
            return typeName;
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

        private static bool IsStatic(this PropertyInfo source, bool nonPublic = false)
            => source.GetAccessors(nonPublic).Any(x => x.IsStatic);

        [Pure]
        private static bool In<T>(this T value, params T[] values)
            => values.Contains(value);

        [Pure]
        private static bool NotIn<T>(this T value, params T[] values)
            => value.In(values) == false;

        private static string GetMethodName(MethodInfo method)
        {
            if (method.IsStatic)
                return $"{method.DeclaringType.Name}.{method.Name}";

            return method.Name;
        }

        private static string GetParents(Type type)
        {
            List<string> parents = new List<string>();

            if (type.BaseType != null && type.BaseType != typeof(object) && type.IsValueType == false)
                parents.Add(ApiDescription.GetTypeName(type.BaseType));

            parents.AddRange(type.GetInterfaces().Select(i => ApiDescription.GetTypeName(i)));

            if (parents.Count == 0)
                return "";

            return " : " + String.Join(", ", parents);
        }

        private static string GetParametersOf(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();
            if (parameters.Any() == false)
                return "";

            var last = parameters.Last();
            StringBuilder description = new StringBuilder();
            foreach (ParameterInfo parameter in parameters)
            {
                description.AppendLine();
                description.Append($"     {parameter.Name}: {ApiDescription.GetTypeName(parameter)}{ApiDescription.GetAttributes(parameter)}");
                if (parameter != last)
                    description.Append(",");
            }

            description.AppendLine();
            description.Append("   ");
            return description.ToString();
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

        private static string GetTypeName(PropertyInfo property)
        {
            var type = ApiDescription.GetTypeName(property.PropertyType);
            var nullable = property.GetCustomAttributes()
                .Any(a => a.GetType()
                    .FullName == "System.Runtime.CompilerServices.NullableAttribute");

            if (nullable)
                return type + "?";

            return type;
        }

        private static string GetTypeName(FieldInfo field)
        {
            var type = ApiDescription.GetTypeName(field.FieldType);
            return type;
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
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return $"{ApiDescription.GetTypeName(Nullable.GetUnderlyingType(type))}?";
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