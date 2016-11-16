using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using JetBrains.Annotations;
using NHibernate.Cfg;
using Synergy.Contracts;
using Synergy.NHibernate.Configurations;
using Synergy.NHibernate.Engine;
using Synergy.NHibernate.Sample.Domain.Users;
using Synergy.NHibernate.Sample.Domain.Words;

namespace Synergy.NHibernate.Sample.Domain
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SampleDatabase : Database, ISampleDatabase
    {
        protected override Configuration GetConfiguration()
        {
            Assembly currentAssembly = this.GetType()
                                           .Assembly;
            
            // TODO:mace (from:mace on:27-10-2016) add here some components to manipulate NH properties / connection string
            Configuration configure = new Configuration().Configure(currentAssembly, "Synergy.NHibernate.Sample.NHibernate.config");
            string sampleDbPath = HttpContext.Current.Server.MapPath("~/App_Data/SampleDatabase.sqlite");
            string connectionString = configure.GetProperty(NHibernateConfigurationParameter.ConnectionString)
                                               .OrFail(NHibernateConfigurationParameter.ConnectionString);
            string fixedConnectionString = connectionString.Replace("SampleDatabase.sqlite", sampleDbPath);

            configure.SetProperty(NHibernateConfigurationParameter.ConnectionString, fixedConnectionString);
            return configure;
        }

        protected override IEnumerable<Type> GetEntities()
        {
            return new[]
            {
                typeof(User),
                typeof(WordGroup)
            };
        }
    }

    public interface ISampleDatabase : IDatabase
    {
    }
}