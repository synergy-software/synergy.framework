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

            var columnName = ForeignKeyColumnNameConvention.GetColumnName(instance.Property);
            var tableWithColumnName = $"{instance.EntityType.Name}_{columnName}";

            string fk = $"FK_{tableWithColumnName}_{instance.Class.Name}";
            string ix = $"IX_{tableWithColumnName}";

            instance.ForeignKey(fk);
            instance.Index(ix);
        }
    }
}