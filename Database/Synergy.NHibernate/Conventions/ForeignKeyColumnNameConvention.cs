using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Conventions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ForeignKeyColumnNameConvention : ForeignKeyConvention
    {
        private const string IdIdentifier = "Id";

        /// <inheritdoc />
        [NotNull]
        protected override string GetKeyName([CanBeNull] Member property, [NotNull] Type type)
        {
            if (property == null)
                return type.Name + ForeignKeyColumnNameConvention.IdIdentifier;

            return GetColumnName(property);
        }

        [NotNull]
        public static string GetColumnName([NotNull] Member property)
        {
            Fail.IfArgumentNull(property, nameof(property));

            return property.Name + ForeignKeyColumnNameConvention.IdIdentifier;
        }
    }
}