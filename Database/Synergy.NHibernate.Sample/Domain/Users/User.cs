using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Conventions;
using Synergy.NHibernate.Domain;
using Synergy.NHibernate.Extensions;
using Synergy.NHibernate.Sample.Domain.Words;

namespace Synergy.NHibernate.Sample.Domain.Users
{
    public class User : Entity
    {
        public User()
        {
            this.Groups = new HashSet<WordGroup>();
        }

        public virtual string Email { get; set; }

        public virtual ISet<WordGroup> Groups { get; set; }

        [UsedImplicitly]
        public class Map : IAutoMappingOverride<User>
        {
            public const int EmailLength = 255;

            public void Override([NotNull] AutoMapping<User> mapping)
            {
                Fail.IfArgumentNull(mapping, nameof(mapping));

                mapping.Schema(SampleDatabase.SchemaName);

                mapping.Map(u => u.Email)
                       .Length(Map.EmailLength)
                       .Index(IndexNamingConvention.GetIndexName<User>(u => u.Email))
                       .UniqueKey("UQ_Email");

                mapping.HasManyBidirectional(user => user.Groups, group => group.Owner);
            }
        }
    }
}