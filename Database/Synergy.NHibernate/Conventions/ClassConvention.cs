using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ClassConvention : IClassConvention
    {
        /// <summary>
        /// Batch size of the entity. For more information visit 
        /// <a href="http://nhibernate.info/doc/nhibernate-reference/batch.html">NHibernate Chapter 13. Batch processing</a>
        /// </summary>
        protected virtual int BatchSize => 100;

        /// <inheritdoc />
        public void Apply([NotNull] IClassInstance instance)
        {
            Fail.IfArgumentNull(instance, nameof(instance));

            instance.BatchSize(this.BatchSize);
        }
    }
}