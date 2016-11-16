using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using JetBrains.Annotations;
using NHibernate;
using NHibernate.Cfg;
using Synergy.Contracts;
using Synergy.Core;
using Synergy.Core.Libraries;
using Synergy.NHibernate.Domain;
using Synergy.NHibernate.Session;

namespace Synergy.NHibernate.Engine
{
    public abstract class Database : IDatabase
    {
        [CanBeNull] 
        private Configuration configuration;

        [CanBeNull] 
        private ISessionFactory factory;

        [NotNull] 
        private readonly object syncRoot = new object();

        /// <summary>
        /// WARN: This property is public as it is injected by Windsor container. DO NOT ASSIGN IT.
        /// </summary>
        [UsedImplicitly]
        public ILibrarian Librarian { get; set; }

        /// <summary>
        /// WARN: This property is public as it is injected by Windsor container. DO NOT ASSIGN IT.
        /// </summary>
        [UsedImplicitly]
        public IConvention[] Conventions { get; set; }

        /// <summary>
        /// WARN: This property is public as it is injected by Windsor container. DO NOT ASSIGN IT.
        /// </summary>
        [UsedImplicitly]
        public ISessionContext SessionContext { get; set; }

        [NotNull, Pure]
        protected abstract Configuration GetConfiguration();

        [NotNull, ItemNotNull, Pure]
        protected abstract IEnumerable<Type> GetEntities();

        [NotNull, ItemNotNull, Pure]
        protected virtual IConvention[] GetConventions()
        {
            return this.Conventions;
        }

        public ISessionFactory Open()
        {
            if (this.factory == null)
            {
                lock (this.syncRoot)
                {
                    if (this.factory == null)
                    {
                        this.factory = this.CreateSessionFactory();
                    }
                }
            }

            return this.factory.OrFail(nameof(this.factory));
        }

        [NotNull]
        private ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration fluentConfiguration = Fluently
                    .Configure(this.GetConfiguration())
                //.CurrentSessionContext<CurrentSessionContext>()
                //.Mappings(
                //    m => m.FluentMappings
                //          .Add<GusContractorSnapshot.Map>()
                //          .Add<GusContractorLog.Map>()
                //)
                ;

            Type[] entities = this.GetEntities()
                                  .ToArray();

            Library[] libraries = this.Librarian.OrFail(nameof(this.Librarian))
                                      .GetLibraries();

            IConvention[] conventions = this.GetConventions()
                                            .OrFail(nameof(this.Conventions));

            foreach (Library library in libraries)
            {
                Assembly assembly = library.GetAssembly();
                AutoPersistenceModel assemblyMappings = AutoMap.Assembly(assembly, new AutomappingConfiguration(entities));
                assemblyMappings.Conventions.Add(conventions);
                assemblyMappings.UseOverridesFromAssembly(assembly);
                assemblyMappings.IgnoreBase<Entity>();

                fluentConfiguration.Mappings(m => m.AutoMappings.Add(assemblyMappings));
            }

            this.configuration = fluentConfiguration.BuildConfiguration();
            return fluentConfiguration.BuildSessionFactory();
        }

        
        public virtual ISession OpenSession()
        {
            ISession session = this.Open()
                                   .OpenSession();
            
            session.FlushMode = FlushMode.Never;

            return session;
        }

        public ISession CurrentSession
        {
            get
            {
                var session = this.SessionContext.GetSession(this);
                if (session == null)
                {
                    session = this.OpenSession();
                    this.SessionContext.StoreSession(this, session);
                }

                return session;
            }
        }

        /// <inheritdoc />
        public Configuration GetNHibernateConfiguration()
        {
            this.Open();

            return this.configuration.OrFail(nameof(this.configuration));
        }

        /// <inheritdoc />
        public string GetKey()
        {
            return this.GetType()
                       .FullName;
        }

        /// <inheritdoc />
        public bool ContainsEntity(Type entityType)
        {
            Fail.IfArgumentNull(entityType, nameof(entityType));

            return this.GetEntities()
                       .Contains(entityType);
        }

        private class AutomappingConfiguration : DefaultAutomappingConfiguration
        {
            private readonly Type[] entities;

            public AutomappingConfiguration([NotNull] Type[] entities)
            {
                Fail.IfArgumentNull(entities, nameof(entities));

                this.entities = entities;
            }

            public override bool ShouldMap([NotNull] Type type)
            {
                Fail.IfArgumentNull(type, nameof(type));

                if (this.entities.Contains(type))
                    return true;

                return false;
            }

            //public override bool ShouldMap(Member member)
            //{
            //    // TODO:mace (from:mace on:25-10-2016) Add ignoring TenantId property if there is no multitentant application
            //    return base.ShouldMap(member);
            //}
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false)
                return;

            if (this.factory == null)
                return;

            this.factory.Dispose();
            this.factory = null;
        }
    }

    public interface IDatabase : IDisposable
    {
        [NotNull]
        ISessionFactory Open();

        /// <summary>
        /// Openes a new session and returns it. If the database is not opened it will also open it (see <see cref="Open"/> method).
        /// The session by default is configured to be never flushed automatically. If you want to change the behaviour ovwerwrite the method.
        /// </summary>
        /// <returns></returns>
        [NotNull, Pure]
        ISession OpenSession();

        [NotNull]
        ISession CurrentSession { get; }

        [NotNull, Pure]
        Configuration GetNHibernateConfiguration();

        [NotNull, Pure]
        string GetKey();

        [Pure]
        bool ContainsEntity([NotNull] Type entityType);
    }
}