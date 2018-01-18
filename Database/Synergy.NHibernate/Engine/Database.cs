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
    /// <summary>
    /// Base implementation of component for accessing database.
    /// </summary>
    public abstract class Database : IDatabase
    {
        [CanBeNull] 
        private Configuration configuration;

        [CanBeNull]  
        private Lazy<ISessionFactory> factory;

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

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        protected Database()
        {
            this.factory = new Lazy<ISessionFactory>(this.CreateSessionFactory);
        }

        /// <summary>
        /// When overriden in a derived class returns the initial NHibernate configuration pointing to a database.
        /// You MUST override it to point the database.
        /// </summary>
        [NotNull, Pure]
        protected abstract Configuration GetConfiguration();

        /// <summary>
        /// When overriden in a derived class returns the entities that belong to the database.
        /// You MUST override it.
        /// </summary>
        [NotNull, ItemNotNull, Pure]
        protected abstract IEnumerable<Type> GetEntities();

        /// <summary>
        /// Returns a set of FluentNHibernate conventions used in entity to database mapping.
        /// Override this method to change the conventions.
        /// </summary>
        [NotNull, ItemNotNull, Pure]
        protected virtual IConvention[] GetConventions()
        {
            return this.Conventions;
        }

        /// <inheritdoc />
        public ISessionFactory Open()
        {
            Fail.IfNull(this.factory, "You cannot reopen a database when it was disposed");

            return this.factory.Value;
        }

        [NotNull]
        private ISessionFactory CreateSessionFactory()
        {
            Configuration initialConfiguration = this.GetConfiguration()
                                                     .OrFail(nameof(this.GetConfiguration) + "()");

            FluentConfiguration fluentConfiguration = Fluently.Configure(initialConfiguration)
                //.CurrentSessionContext<CurrentSessionContext>()
                //.Mappings(
                //    m => m.FluentMappings
                //          .Add<GusContractorSnapshot.Map>()
                //          .Add<GusContractorLog.Map>()
                //)
                ;

            Library[] libraries = this.Librarian.OrFail(nameof(this.Librarian))
                                      .GetLibraries();

            IConvention[] conventions = this.GetConventions()
                                            .OrFail(nameof(this.Conventions));

            Type[] entities = this.GetEntities()
                                  .ToArray();

            var automappingConfiguration = new AutomappingConfiguration(entities);

            foreach (Library library in libraries)
            {
                Assembly assembly = library.GetAssembly();
                AutoPersistenceModel assemblyMappings = AutoMap.Assembly(assembly, automappingConfiguration);
                assemblyMappings.Conventions.Add(conventions);
                assemblyMappings.UseOverridesFromAssembly(assembly);
                assemblyMappings.IgnoreBase<Entity>();

                fluentConfiguration.Mappings(m =>
                {
                    m.FluentMappings.Conventions.Add(conventions);
                    m.AutoMappings.Add(assemblyMappings);
                });
            }

            this.configuration = fluentConfiguration.BuildConfiguration();
            return fluentConfiguration.BuildSessionFactory();
        }

        protected virtual FlushMode DefaultSessionFlushMode => FlushMode.Never;

        /// <inheritdoc />
        public virtual ISession OpenSession()
        {
            ISession session = this.Open()
                                   .OpenSession();

            // WARN: By default the session will be never flushed automatically - developer MUST do it explicitly
            session.FlushMode = this.DefaultSessionFlushMode;
            this.SessionContext.StoreSession(this, session);

            return session;
        }

        /// <inheritdoc />
        public ISession CurrentSession
        {
            get
            {
                // TODO:mace (from:mace on:08-12-2017) Dodaj AllowOutOfTransactionConnections -> exception
                var session = this.GetSession();
                if (session == null)
                {
                    if (this.AllowAdHocConnections == false)
                        throw Fail.Because("You cannot start new session so easilly. Use " + nameof(ISessionInterceptor) + " or enable ad hoc transactions.");

                    session = this.OpenSession();
                }

                return session;
            }
        }

        protected virtual bool AllowAdHocConnections => false;

        /// <inheritdoc />
        public virtual ISession GetSession()
        {
            return this.SessionContext.GetSession(this);
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
                       .FullName.FailIfNull("FullName is null for {0}", this.GetType());
        }

        ///// <inheritdoc />
        //public bool ContainsEntity(Type entityType)
        //{
        //    Fail.IfArgumentNull(entityType, nameof(entityType));

        //    return this.GetEntities()
        //               .Contains(entityType);
        //}

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

        /// <summary>
        /// Disposes the database and its session factory.
        /// </summary>
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false)
                return;
            
            if (this.factory == null || this.factory.IsValueCreated == false)
                return;

            this.factory.Value.Dispose();
            this.factory = null;
            this.configuration = null;
        }
    }

    /// <summary>
    /// Component for accessing particular database.
    /// </summary>
    public interface IDatabase : IDisposable
    {
        /// <summary>
        /// Opens the database. Internally it creates a NHibernate factory (if not created yet) and returns it.
        /// The operation may be very expensive.
        /// </summary>
        [NotNull]
        ISessionFactory Open();

        /// <summary>
        /// Opens a new session and returns it. If the database is not opened it will also open it (see <see cref="Open"/> method).
        /// <para>WARN: The session by default is configured to be never flushed automatically. If you want to change the behaviour ovwerwrite the method.</para>
        /// </summary>
        [NotNull]
        ISession OpenSession();

        /// <summary>
        /// Returns the current session to this database - current means that, depending on context in which you request the session,
        /// it will be stored in different place. It is stored to make sure that all subsequent calls for current session will receive the same one.
        /// <para>E.g. two web requests will receive completely different current sessions both stored in requests web context.</para>
        /// <para>If there is no session in the current context it will be created and stored</para>
        /// </summary>
        [NotNull]
        ISession CurrentSession { get; }

        /// <summary>
        /// Returns the <see cref="CurrentSession"/> or null if there is none.
        /// </summary>
        [CanBeNull, Pure]
        ISession GetSession();

        /// <summary>
        /// Returns the final NHibernate configuration that was used to build a session factory.
        /// </summary>
        [NotNull, Pure]
        Configuration GetNHibernateConfiguration();

        /// <summary>
        /// Gets an unique key of the database. Each database stores its sessions separately under this key.
        /// </summary>
        [NotNull, Pure]
        string GetKey();
    }
}