using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using NHibernate.Cfg;
using Synergy.NHibernate.Engine;
using Synergy.NHibernate.Test.Database.Users;
using Synergy.NHibernate.Test.Database.Words;

namespace Synergy.NHibernate.Test.Database
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SampleDatabase : NHibernate.Engine.Database, ISampleDatabase
    {
        public const string SchemaName = "words";

        protected override Configuration GetConfiguration()
        {
            Assembly currentAssembly = this.GetType()
                                           .Assembly;
            
            Configuration configure = new Configuration().Configure(currentAssembly, "Synergy.NHibernate.Sample.NHibernate.config");
            return configure;
        }

        public override IEnumerable<Type> GetEntities()
        {
            return new[]
            {
                typeof(User),
                typeof(WordGroup),
                typeof(Word)
            };
        }
    }

    public interface ISampleDatabase : IDatabase
    {
    }
}