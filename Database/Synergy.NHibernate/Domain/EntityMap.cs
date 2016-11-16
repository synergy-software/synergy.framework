using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using JetBrains.Annotations;

namespace Synergy.Database.Entities
{
    public abstract class EntityMap<T> : IAutoMappingOverride<T>
    {
        public abstract void Override([NotNull] AutoMapping<T> mapping);
    }
}