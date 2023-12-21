using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;
using Synergy.Catalogue;
using Synergy.Catalogue.Reflection;

namespace Synergy.Documentation.Api
{
    public static class ApiDescription
    {
        static readonly BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        public static string GenerateFor(Assembly assembly)
        {
            var description = new StringBuilder();

            var assemblyName = assembly.GetName().Name;
            description.AppendLine($"# {assemblyName}");
            description.AppendLine();
            
            var publicTypes = assembly
                .GetTypes()
                .Where(type => type.IsPublic || type.IsNestedPublic)
                .OrderBy(t => t.FullName);
            
            return GenerateFor(publicTypes, description, assemblyName);
        }

        public static string GenerateFor(
            IEnumerable<Type> types, 
            StringBuilder? description = null, 
            string? assemblyName = null
            )
        {
            description ??= new StringBuilder();

            foreach (var type in types)
            {
                var shortNamespace = GetShortenNamespace(type, assemblyName);
                var typeName = GetShortTypeName(type);
                var baseType = GetParents(type);

                description.AppendLine($"## {shortNamespace}{GetTypeName(type)}{typeName}{baseType}");

                foreach (var property in type.GetProperties(bindingFlags).OrderBy(p => p.Name))
                {
                    if (property.DeclaringType == typeof(Exception) || property.DeclaringType == typeof(Attribute))
                        continue;

                    description.AppendLine($" - {GetPropertyName(property)}: {GetTypeName(property)}{GetAttributes(property)} {GetAccessors(property)}");
                }

                foreach (var field in type.GetFields(bindingFlags))
                {
                    if (field.Name == "value__" && type.IsEnum)
                        continue;

                    if (type.IsEnum)
                    {
                        description.AppendLine($" - {field.Name} = {(int)Enum.Parse(type, field.Name)}");
                    }
                    else
                    {
                        description.AppendLine($" - {GetFieldName(field)}: {GetTypeName(field)}{GetAttributes(field)} (field)");
                    }
                }

                if (type.IsEnum == false)
                {
                    foreach (var constructor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
                    {
                        description.AppendLine($" - ctor({GetParametersOf(constructor)})");
                    }

                    foreach (var method in GetMethods(type).OrderBy(m => m))
                    {
                        description.AppendLine($" - {method}");
                    }
                }

                description.AppendLine();
            }

            return description.ToString();
        }

        private static IEnumerable<string> GetMethods(Type type)
        {
            foreach (var method in type.GetMethods(bindingFlags).OrderBy(m => m.Name))
            {
                if (method.IsSpecialName || method.DeclaringType.In(typeof(object), typeof(Exception), typeof(Attribute)))
                    continue;

                if (type.IsValueType && method.Name.In(nameof(Equals), nameof(GetHashCode), nameof(ToString)))
                    continue;

                if (method.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
                    continue;

                if (type.IsRecord() && method.Name.In(nameof(ToString), nameof(GetHashCode), nameof(Equals), "<Clone>$", "Deconstruct"))
                    continue;

                yield return For(method);
            }
        }
        
        public static string For(MethodInfo method, bool withAttributes = true)
        {
            var generics = method.GetGenericArguments();
            var gD = generics.Length == 0 ? "" : "<" + String.Join(", ", generics.Select(g => GetTypeName(g))) + ">";
            var attributes = withAttributes ? GetAttributes(method) : "";
            var methodDescription = $"{GetMethodName(method)}{gD}({GetParametersOf(method, withAttributes)}) : {GetTypeName(method)}{attributes}";
            return methodDescription;
        }

        private static string GetShortenNamespace(Type type, string? assemblyName)
        {
            if (assemblyName == null)
                return type.Namespace + ".";
            
            var shortNamespace = type.Namespace.Replace(assemblyName, "").TrimStart('.');
            if (shortNamespace.IsNullOrEmpty() == false)
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
                parents.Add(GetTypeName(type.BaseType));

            parents.AddRange(type.GetInterfaces().Select(i => GetTypeName(i)));

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
                description.Append($"     {parameter.Name}: {GetTypeName(parameter)}{GetAttributes(parameter)}");
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
            var description = new StringBuilder();
            foreach (var parameter in parameters)
            {
                description.AppendLine();
                var attributes = withAttributes ? GetAttributes(parameter) : "";
                description.Append($"     {parameter.Name}: {GetTypeName(parameter)}{attributes}");
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
            var nullable = IsMarkedAsNullable(property);
            if (nullable)
                return type.TrimEnd('?') + "?";

            return type;
        }
        
        private static bool IsMarkedAsNullable(PropertyInfo p)
            => new NullabilityInfoContext().Create(p).WriteState is NullabilityState.Nullable;

        private static string GetTypeName(FieldInfo field)
        {
            var type = GetTypeName(field.FieldType);
            var nullable = IsMarkedAsNullable(field);
            if (nullable)
                return type.TrimEnd('?') + "?";
            
            return type;
        }
        
        private static bool IsMarkedAsNullable(FieldInfo p)
            => new NullabilityInfoContext().Create(p).WriteState is NullabilityState.Nullable;

        public static string GetTypeName(MethodInfo method)
        {
            var type = GetTypeName(method.ReturnType);
            var nullable = IsMarkedAsNullable(method.ReturnParameter);
            if (nullable)
                return type.TrimEnd('?') + "?";

            return type;
        }

        private static bool IsMarkedAsNullable(ParameterInfo p)
            => new NullabilityInfoContext().Create(p).WriteState is NullabilityState.Nullable;

        public static string GetTypeName(ParameterInfo parameter)
        {
            var type = GetTypeName(parameter.ParameterType);
            var nullable = IsMarkedAsNullable(parameter);
            var paramsArray = parameter.GetCustomAttribute<ParamArrayAttribute>();

            if (paramsArray != null)
                type = "params " + type;

            var outAttribute = parameter.GetCustomAttribute<OutAttribute>();

            if (outAttribute != null)
                type = "out " + type;

            if (nullable)
                return type.TrimEnd('?') + "?";

            return type;
        }

        public static string GetTypeName(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return $"{GetTypeName(underlyingType)}?";
            }

            if (type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();
                return
                    $"{type.Name.Substring(0, type.Name.IndexOf("`", StringComparison.Ordinal))}<{String.Join(", ", arguments.Select(a => GetTypeName(a)))}>";
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
            var attributes = enumerable
                .Where(a => a.GetType().Name.StartsWith("__") == false)
                .Where(a => a is not DebuggerStepThroughAttribute)
                .Select(a => a.GetType().Name.Replace("Attribute", ""))
                .ToList();

            if (attributes.Any() == false)
                return "";

            return $" [{string.Join(", ", attributes)}]";
        }
    }
}