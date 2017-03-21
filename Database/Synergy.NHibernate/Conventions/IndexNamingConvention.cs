using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Reflection;

namespace Synergy.NHibernate.Conventions
{
    public class IndexNamingConvention
    {
        [NotNull]
        public static string GetIndexName<TEntity>([NotNull] Expression<Func<TEntity, object>> property)
        {
            Fail.IfArgumentNull(property, nameof(property));

            ClassSpecifics.Concrete<TEntity> entityType = ClassSpecifics.Of<TEntity>();
            string entityName = entityType.GetClassName();
            var propertyName = entityType.GetPropertyName(property);

            var indexName = $"IX_{entityName}_{propertyName}";
            return indexName;
        }
    }
}
