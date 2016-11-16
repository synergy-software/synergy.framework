using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class EmptyStringLengthConvention : IPropertyConvention
    {
        /// <inheritdoc />
        public void Apply([NotNull]IPropertyInstance instance)
        {
            Fail.IfArgumentNull(instance, nameof(instance));

            if (instance.Type.GetUnderlyingSystemType() != typeof(string))
                return;

            int length = ((IPropertyInspector)instance).Length;
            Fail.IfEqual(0, length, "{0}.{1} length is 0", instance.EntityType.Name, instance.Name);
        }
    }
}