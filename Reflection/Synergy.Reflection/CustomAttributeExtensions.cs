using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Reflection
{
    /// <summary>
    /// Extension methods for retrieval of custom attributes.
    /// </summary>
    public static class CustomAttributeExtensions
    {
        /// <summary>
        /// Gets all attributes inherited from attribute type &lt;T>.
        /// This method searches not only attributes directly assigned to a member but also to all interfaces implemented by it.
        /// </summary>
        [NotNull]
        [Pure]
        [ItemNotNull]
        public static TAttribute[] GetCustomAttributesBasedOn<TAttribute>([NotNull] this MemberInfo element) where TAttribute : Attribute
        {
            Fail.IfArgumentNull(element, nameof(element));
            Fail.IfArgumentNull(element.DeclaringType, nameof(element.DeclaringType));

            object[] memberAttributes = element.GetCustomAttributes(inherit: true);
            MemberInfo[] interfaceMembers = CustomAttributeExtensions.GetInterfaceMembersImplementedBy(element);
            object[] interfaceAttributes = interfaceMembers.SelectMany(m => m.GetCustomAttributes(inherit: true))
                                                           .ToArray();

            IEnumerable<object> allAttributes = memberAttributes.Union(interfaceAttributes);
            TAttribute[] attributes = CustomAttributeExtensions.GetInheritedAttributes<TAttribute>(allAttributes, element.Name);

            return attributes;
        }

        [NotNull, Pure]
        private static MemberInfo[] GetInterfaceMembersImplementedBy([NotNull] MemberInfo member)
        {
            Fail.IfArgumentNull(member, nameof(member));
            Fail.IfArgumentNull(member.DeclaringType, nameof(member.DeclaringType));

            Type elementType = member.DeclaringType;
            Type[] interfaces = elementType.GetInterfaces();

            var property = member as PropertyInfo;
            if (property != null)
            {
                return interfaces.Select(i => i.GetProperty(property.Name))
                                 .Where(p => p != null)
                                 .Cast<MemberInfo>()
                                 .ToArray();
            }

            var method = member.CastOrFail<MethodInfo>();
            var interfaceMethods = new List<MethodInfo>();
            foreach (Type @interface in interfaces)
            {
                InterfaceMapping map = elementType.GetInterfaceMap(@interface);
                int elementIndex = Array.IndexOf(map.TargetMethods, method);
                if (elementIndex == -1)
                    continue;

                MethodInfo interfaceMember = map.InterfaceMethods[elementIndex];
                interfaceMethods.Add(interfaceMember);
            }

            return interfaceMethods.Cast<MemberInfo>()
                                   .ToArray();
        }

        /// <summary>
        /// Gets all attributes inherited from attribute type &lt;T>.
        /// This method searches not only attributes directly assigned to a member but also to all interfaces implemented by it.
        /// </summary>
        [NotNull]
        [Pure]
        [ItemNotNull]
        public static TAttribute[] GetCustomAttributesBasedOn<TAttribute>([NotNull] this Type type) where TAttribute : Attribute
        {
            Fail.IfArgumentNull(type, nameof(type));

            object[] typeAttributes = type.GetCustomAttributes(inherit: true);
            IEnumerable<object> attributesFromInterfaces = type.GetInterfaces()
                                                               .SelectMany(i => i.GetCustomAttributes(inherit: true));

            IEnumerable<object> allAttributes = typeAttributes.Union(attributesFromInterfaces);
            TAttribute[] attributes = CustomAttributeExtensions.GetInheritedAttributes<TAttribute>(allAttributes, type.Name);
            return attributes;
        }

        [NotNull]
        [Pure]
        [ItemNotNull]
        private static T[] GetInheritedAttributes<T>([NotNull] IEnumerable<object> allAttributes, string name) where T : Attribute
        {
            Fail.IfArgumentNull(allAttributes, nameof(allAttributes));

            T[] attributes = allAttributes
                .Where(a => a is T)
                .Cast<T>()
                .ToArray();

            Fail.IfCollectionContainsNull(attributes, $"Attributes collection on {name}");

            return attributes;
        }
    }
}