using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ReferenceConvention : IReferenceConvention
    {
        /// <inheritdoc />
        public void Apply([NotNull] IManyToOneInstance instance)
        {
            Fail.IfArgumentNull(instance, nameof(instance));
            Fail.IfArgumentNull(instance.EntityType, nameof(instance.EntityType));
            Fail.IfArgumentNull(instance.Class, nameof(instance.Class));
            Fail.IfArgumentNull(instance.Property, nameof(instance.Property));

            string fk = "FK_" + instance.EntityType.Name + "_" + instance.Class.Name + "_" + instance.Property.Name;
            string ix = "IX_" + instance.EntityType.Name + "_" + instance.Property.Name;

            instance.ForeignKey(fk);
            instance.Index(ix);
        }
    }
}