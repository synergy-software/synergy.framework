using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HasManyToManyConvention : IHasManyToManyConvention
    {

        public void Apply([NotNull] IManyToManyCollectionInstance instance)
        {
            Fail.IfArgumentNull(instance, "instance");

            instance.Cascade.SaveUpdate();

            string fk = "Fk_" + instance.EntityType.Name + "_" + instance.Member.Name;

            instance.Key.ForeignKey(fk + "_1");
            instance.Relationship.ForeignKey(fk + "_2");
        }
    }
}