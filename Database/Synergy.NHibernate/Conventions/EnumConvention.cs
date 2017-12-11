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
            criteria.Expect(e => e.Property.PropertyType.IsEnum);
        }

        public void Apply([NotNull] IPropertyInstance instance)
        {
            instance.CustomType(instance.Property.PropertyType);
        }
    }
}