using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Domain;

namespace Synergy.NHibernate.Test.My
{
    public class MyEntity : Entity
    {
        public virtual string SomeString { get; set; }

        [UsedImplicitly]
        public class Map : IAutoMappingOverride<MyEntity>
        {
            public const int SomeStringLength = 10;

            /// <inheritdoc />
            public void Override([NotNull] AutoMapping<MyEntity> mapping)
            {
                Fail.IfArgumentNull(mapping, nameof(mapping));

                mapping.Map(e => e.SomeString)
                       .Length(Map.SomeStringLength);
            }
        }
    }
}