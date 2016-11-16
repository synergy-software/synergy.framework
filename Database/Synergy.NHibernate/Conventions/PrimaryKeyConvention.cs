using System.Globalization;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Domain;

namespace Synergy.NHibernate.Conventions
{
    /// <summary>
    /// Primary key generation convention - it uses HiLo algorithm for generating the PK value - <see cref="Entity.Id"/>.
    /// Override it if you want to change the convention.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class PrimaryKeyConvention : IIdConvention
    {
        /// <summary>
        /// MaxLo for the HiLo algorithm. Override the class to change the MaxLo value.
        /// </summary>
        protected virtual int MaxLo => 113;

        /// <summary>
        /// Override the method to change the Primary Key generation convetnion.
        /// </summary>
        public virtual void Apply([NotNull] IIdentityInstance instance)
        {
            Fail.IfArgumentNull(instance, nameof(instance));

            string maxLo = this.MaxLo.ToString(CultureInfo.InvariantCulture);
            instance.GeneratedBy.HiLo(maxLo);
        }
    }
}