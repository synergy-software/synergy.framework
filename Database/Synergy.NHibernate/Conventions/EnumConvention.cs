using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class EnumConvention : IUserTypeConvention
    {
        public void Accept([NotNull] IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(e => this.IsItEnum(e.Property.PropertyType));
        }

        private bool IsItEnum([NotNull] Type type)
        {
            if (type.IsEnum)
                return true;

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null && underlyingType.IsEnum)
                return true;

            return false;
        }

        public void Apply([NotNull] IPropertyInstance instance)
        {
            instance.CustomType(instance.Property.PropertyType);
        }
    }
}