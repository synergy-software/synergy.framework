using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using JetBrains.Annotations;
using Synergy.NHibernate.Domain;
using Synergy.NHibernate.Extensions;
using Synergy.NHibernate.Sample.Domain.Users;

namespace Synergy.NHibernate.Sample.Domain.Words
{
    public class WordGroup : Entity
    {
        public WordGroup()
        {
            this.Words = new HashSet<Word>();
        }

        [NotNull] 
        public virtual string Name { get; set; }

        [NotNull] 
        public virtual User Owner { get; set; }

        [NotNull, ItemNotNull] 
        public virtual ISet<Word> Words { get; }

        [UsedImplicitly]
        public class Map : IAutoMappingOverride<WordGroup>
        {
            public const int NameLength = 100;

            public void Override([NotNull] AutoMapping<WordGroup> mapping)
            {
                mapping.Map(u => u.Name)
                       .Length(Map.NameLength);

                mapping.HasManyBidirectional(g => g.Words, w => w.Group);
            }
        }
    }
}