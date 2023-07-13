// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Synergy.Reflection
{
    /// <summary>
    /// Extension methods for retrieval of custom attributes.
    /// </summary>
#if INTERNALS
    internal
#else
    public
#endif
    static class CustomAttributeExtensions
    {
        /// <summary>
        /// Gets all attributes inherited from attribute type &lt;T>.
        /// This method searches not only attributes directly assigned to a member but also to all interfaces implemented by it.
        /// </summary>
        public static TAttribute[] GetCustomAttributesBasedOn<TAttribute>(this MemberInfo element) where TAttribute : Attribute
        {
            element = element ?? throw new ArgumentNullException(nameof(element));

            object[] memberAttributes = element.GetCustomAttributes(inherit: true);
            MemberInfo[] interfaceMembers = CustomAttributeExtensions.GetInterfaceMembersImplementedBy(element);
            object[] interfaceAttributes = interfaceMembers.SelectMany(m => m.GetCustomAttributes(inherit: true))
                                                           .ToArray();

            IEnumerable<object> allAttributes = memberAttributes.Union(interfaceAttributes).ToList();
            TAttribute[] attributes = CustomAttributeExtensions.GetInheritedAttributes<TAttribute>(allAttributes, element.Name);

            return attributes;
        }

        public static TAttribute[] GetCustomAttributesBasedOn<TAttribute>(this ParameterInfo parameter) where TAttribute : Attribute
        {
            parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));

            object[] memberAttributes = parameter.GetCustomAttributes(inherit: true);
            MemberInfo[] interfaceMembers = CustomAttributeExtensions.GetInterfaceMembersImplementedBy(parameter.Member);

            var paremeterMember = (MethodInfo)parameter.Member;
            ParameterInfo[] methodArguments = paremeterMember.GetParameters();
            var parameterIndex = Array.IndexOf(methodArguments, parameter);
            object[] interfaceAttributes = interfaceMembers.SelectMany(m => ((MethodInfo)m)
                                                                                         .GetParameters()[parameterIndex]
                                                                                         //.FailIfNull("Method {0}.{1}() should have argument {2}", m.DeclaringType, m.Name, parameter.Name)
                                                                                         .GetCustomAttributes(inherit: true))
                                                           .ToArray();

            IEnumerable<object> allAttributes = memberAttributes.Union(interfaceAttributes);
            TAttribute[] attributes = CustomAttributeExtensions.GetInheritedAttributes<TAttribute>(allAttributes, parameter.Name);

            return attributes;
        }

        [Pure]
        private static MemberInfo[] GetInterfaceMembersImplementedBy(MemberInfo member)
        {
            member = member ?? throw new ArgumentNullException(nameof(member));

            Type elementType = member.DeclaringType ?? throw new InvalidOperationException($"Member {member.Name} has no declaring type.");
            Type[] interfaces = elementType.GetInterfaces();

            var property = member as PropertyInfo;
            if (property != null)
            {
                return interfaces.Select(i => i.GetProperty(property.Name))
                                 .Where(p => p != null)
                                 .Cast<MemberInfo>()
                                 .ToArray();
            }

            if (elementType.IsInterface)
                return Array.Empty<MemberInfo>();

            var method = (MethodInfo)member;
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
        [Pure]
        public static TAttribute[] GetCustomAttributesBasedOn<TAttribute>(this Type type) where TAttribute : Attribute
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            object[] typeAttributes = type.GetCustomAttributes(inherit: true);
            IEnumerable<object> attributesFromInterfaces = type.GetInterfaces()
                                                               .SelectMany(i => i.GetCustomAttributes(inherit: true));

            IEnumerable<object> allAttributes = typeAttributes.Union(attributesFromInterfaces);
            TAttribute[] attributes = CustomAttributeExtensions.GetInheritedAttributes<TAttribute>(allAttributes, type.Name);
            return attributes;
        }

        [Pure]
        private static T[] GetInheritedAttributes<T>(IEnumerable<object> allAttributes, string name) where T : Attribute
        {
            allAttributes = allAttributes ?? throw new ArgumentNullException(nameof(allAttributes));

            T[] attributes = allAttributes
                             .Where(a => a is T)
                             .Cast<T>()
                             .ToArray();

            return attributes;
        }
    }
}