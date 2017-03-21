using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Reflection
{
    public class ClassSpecifics
    {
        private readonly Type type;

        private ClassSpecifics(Type type)
        {
            this.type = type;
        }

        [NotNull]
        public static ClassSpecifics.Concrete<T> Of<T>()
        {
            return new Concrete<T>();
        }

        [NotNull]
        public static ClassSpecifics Of([NotNull] Type type)
        {
            Fail.IfArgumentNull(type, nameof(type));

            return new ClassSpecifics(type);
        }

        [CanBeNull, Pure]
        public string GetClassName()
        {
            return this.type.Name;
        }

        [CanBeNull, Pure]
        public FieldInfo GetFieldInfo([NotNull] string name)
        {
            FieldInfo field = type.GetField(name,
                BindingFlags.FlattenHierarchy | 
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic | 
                BindingFlags.Static);

            return field;
        }

        public class Concrete<TClass> : ClassSpecifics
        {
            internal Concrete() : base(typeof(TClass))
            {
            }

            /// <summary>
            /// Zwraca nazwę techniczną pola - np. GetPropertyName(p => p.LastName) zwróci "LastName".
            /// This method can be replaced by <c>nameof(p.LastName)</c> or <c>nameof(Contractor.LastName)</c>
            /// </summary>
            [NotNull, Pure]
            public string GetPropertyName<TProperty>([NotNull] Expression<Func<TClass, TProperty>> propertyExpression)
            {
                Fail.IfArgumentNull(propertyExpression, "propertyExpression");

                PropertyInfo propertyInfo = this.GetPropertyInfo(propertyExpression);
                return propertyInfo.Name;
            }

            [CanBeNull, Pure]
            public string GetPropertyDisplayName<TProperty>([NotNull] Expression<Func<TClass, TProperty>> propertyExpression)
            {
                Fail.IfArgumentNull(propertyExpression, nameof(propertyExpression));

                PropertyInfo propertyInfo = this.GetPropertyInfo(propertyExpression);
                var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                string propertyDisplayName = displayAttribute?.GetName();
                return propertyDisplayName;
            }

            [NotNull, Pure]
            public PropertyInfo GetPropertyInfo<TProperty>([NotNull] Expression<Func<TClass, TProperty>> propertyExpression)
            {
                Fail.IfArgumentNull(propertyExpression, nameof(propertyExpression));

                Expression body = propertyExpression.Body;
                if (body is UnaryExpression)
                    body = body.CastOrFail<UnaryExpression>().Operand;
                var expression = body.CastOrFail<MemberExpression>();
                var propertyInfo = expression.Member.CastOrFail<PropertyInfo>();
                return propertyInfo;
            }

            [NotNull, Pure]
            public string GetPropertyPath<TProperty>([NotNull] Expression<Func<TClass, TProperty>> propertyExpression)
            {
                Fail.IfArgumentNull(propertyExpression, nameof(propertyExpression));

                return ExpressionHelper.GetExpressionText(propertyExpression);
            }
        }
    }
}
