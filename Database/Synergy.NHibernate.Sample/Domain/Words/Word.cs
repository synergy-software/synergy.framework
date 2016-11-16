using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Domain;

namespace Synergy.NHibernate.Sample.Domain.Words
{
    public class Word : Entity
    {
        public virtual WordGroup Group { get; set; }

        public virtual string Polish { get; set; }
        public virtual string English { get; set; }


        [UsedImplicitly]
        public class Map : IAutoMappingOverride<Word>
        {
            public const int PolishLength = 100;
            public const int EnglishLength = 100;

            /// <inheritdoc />
            public void Override([NotNull] AutoMapping<Word> mapping)
            {
                Fail.IfArgumentNull(mapping, nameof(mapping));

                mapping.Map(w => w.Polish)
                       .Length(Map.PolishLength);

                mapping.Map(w => w.English)
                   .Length(Map.EnglishLength);
            }
        }
    }
}