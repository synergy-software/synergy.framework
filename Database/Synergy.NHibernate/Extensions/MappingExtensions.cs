using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Conventions;

namespace Synergy.NHibernate.Extensions
{
    public static class MappingExtensions
    {
        public static OneToManyPart<TChild> HasManyBidirectional<TParent, TChild>(
            [NotNull] this AutoMapping<TParent> mapping,
            [NotNull] Expression<Func<TParent, IEnumerable<TChild>>> childCollection,
            [NotNull] Expression<Func<TChild, TParent>> parentReference,
            bool cascadeAllDeleteOrphan = true)
        {
            Fail.IfArgumentNull(mapping, nameof(mapping));
            Fail.IfArgumentNull(childCollection, nameof(childCollection));
            Fail.IfArgumentNull(parentReference, nameof(parentReference));

            Member member = parentReference.ToMember();
            var parentReferenceColumnName = ForeignKeyColumnNameConvention.GetColumnName(member);

            OneToManyPart<TChild> bidirectionalMapping = mapping
                .HasMany(childCollection);

            if (cascadeAllDeleteOrphan)
            {
                bidirectionalMapping = bidirectionalMapping
                    .Cascade
                    .AllDeleteOrphan();
            }

            return bidirectionalMapping
                .Inverse()
                .KeyColumn(parentReferenceColumnName);
        }

        //public static PropertyPart Index<T>([NotNull] this AutoMapping<T> mapping, [NotNull] Expression<Func<T, object>> property, bool isUnique = false)
        //{
        //    Fail.IfArgumentNull(mapping, nameof(mapping));
        //    Fail.IfArgumentNull(property, nameof(property));
            
        //    var indexName = IndexNamingConvention.GetIndexName(property);

        //    PropertyPart map = mapping
        //        .Map(property)
        //        .Index(indexName);

        //    if (isUnique)
        //    {
        //        map.Unique();
        //    }

        //    return map;
        //}
    }
}
