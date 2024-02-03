using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Synergy.Contracts;

namespace Synergy.Web.Api.Testing
{
    internal static class QueryBuilder
    {
        private static readonly Type[] primitives = {typeof(string), typeof(decimal), typeof(DateTime)};

        public static string Build([NotNull] object contract, bool commaArraySeparator = false) =>
            BuildQuery(contract, null, commaArraySeparator);

        private static string BuildQuery([NotNull] object contract, string? parentName, bool commaArraySeparator = false)
        {
            Fail.IfNull(contract);
            var objectType = contract.GetType();
            Fail.IfTrue(IsPrimitiveType(objectType), Violation.Of("You can't build query from primitive"));
            Fail.IfTrue(objectType.IsArray, Violation.Of("You can't build query from array"));

            var properties = objectType.GetProperties();
            var queryBuilder = new StringBuilder();
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var parameterName = property.Name;
                var parameterValue = property.GetValue(contract);
                var propertyType = property.PropertyType;
                if (parameterValue == null)
                    continue;

                if (IsPrimitiveType(propertyType))
                {
                    AppendPrimitive(parentName, queryBuilder, parameterName, parameterValue);
                }
                else if (propertyType.IsArray)
                {
                    AppendArray(parentName, commaArraySeparator, parameterValue, queryBuilder, parameterName);
                }
                else
                {
                    queryBuilder.Append(BuildQuery(parameterValue, property.Name, commaArraySeparator));
                }

                if (i + 1 < properties.Length)
                    queryBuilder.Append("&");
            }

            return queryBuilder.ToString();
        }

        private static void AppendPrimitive(string? parentName, StringBuilder queryBuilder, string parameterName, object parameterValue)
        {
            queryBuilder.Append(GetQueryPart(parentName, parameterName, parameterValue));
        }

        private static string GetQueryPart(string? parentName, string parameterName, object parameterValue)
        {
           return $"{(string.IsNullOrEmpty(parentName) ? "" : parentName + ".")}{parameterName}={parameterValue}";
        }

        private static void AppendArray(string? parentName, bool commaArraySeparator, object parameterValue, StringBuilder queryBuilder, string parameterName)
        {
            var array = ((IEnumerable) parameterValue).Cast<object>();
            if (commaArraySeparator)
            {
                var value = string.Join(",", array);
                queryBuilder.Append(GetQueryPart(parentName, parameterName, value));
            }
            else
            {
                var values = array.Select(item => GetQueryPart(parentName, parameterName, item));
                queryBuilder.Append(string.Join("&", values));
            }
        }

        private static bool IsPrimitiveType([NotNull] Type propertyType)
        {
            return propertyType.IsPrimitive || propertyType.IsValueType || propertyType.IsEnum ||
                   primitives.Contains(propertyType) ||
                   propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}