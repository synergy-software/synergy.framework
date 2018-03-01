using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using NHibernate.Cfg;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Test.My
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class MyDatabase : Database, IMyDatabase
    {
        protected override Configuration GetConfiguration()
        {
            Assembly currentAssembly = this.GetType()
                                           .Assembly;

            return new Configuration().Configure(currentAssembly, "Synergy.NHibernate.Test.NHibernate.config");
        }

        public override IEnumerable<Type> GetEntities()
        {
            return new[] {typeof(MyEntity)};
        }
    }

    public interface IMyDatabase : IDatabase
    {
    }
}