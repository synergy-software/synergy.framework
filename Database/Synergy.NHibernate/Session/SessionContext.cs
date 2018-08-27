using System;
using System.Linq;
using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Contexts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Session
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SessionContext : ISessionContext
    {
        private readonly IContextStorage<SessionsContainer>[] sessionStorages;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionContext(IContextStorage<SessionsContainer>[] sessionStorages)
        {
            this.sessionStorages = sessionStorages;
        }

        /// <inheritdoc />
        public void StoreSession(IDatabase database, ISession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            SessionsContainer container = this.GetSessionsContainer();
            container.StoreSession(database, session);
        }

        /// <inheritdoc />
        public void StoreSession(IDatabase database, IStatelessSession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            SessionsContainer container = this.GetSessionsContainer();
            container.StoreSession(database, session);
        }

        /// <inheritdoc />
        public ISession GetSession(IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            SessionsContainer container = this.GetSessionsContainer();
            ISession session = container.GetSession(database);
            return session;
        }

        /// <inheritdoc />
        public IStatelessSession GetStatelessSession(IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            SessionsContainer container = this.GetSessionsContainer();
            var session = container.GetStatelessSession(database);
            return session;
        }

        [NotNull, MustUseReturnValue]
        private SessionsContainer GetSessionsContainer()
        {
            IContextStorage<SessionsContainer> storage = this.GetContextStorage();
            SessionsContainer container = storage.Get();
            if (container == null)
            {
                container = new SessionsContainer();
                storage.Store(container);
            }

            return container;
        }

        private static Type[] preferredContextStorages = new []
        {
            typeof(ICustomSessionStorage),
            typeof(IWebContextStorage<SessionsContainer>),
            typeof(IWcfContextStorage<SessionsContainer>),
            typeof(IThreadStaticContextStorage<SessionsContainer>)
        };

        public static void ConfigureContextStorages([NotNull] Action<ContextStorageConfigurator> configure)
        {
            Fail.IfArgumentNull(configure, nameof(configure));
            var configurator = new ContextStorageConfigurator();
            configure(configurator);
            preferredContextStorages = configurator.GetStorageTypes();
        }
        

        [NotNull, Pure]
        private IContextStorage<SessionsContainer> GetContextStorage()
        {

            foreach (var storageType in preferredContextStorages)
            {
                var storageCandidates = this.sessionStorages.Where(s => storageType.IsInstanceOfType(s));
                var firstAvailable = storageCandidates.FirstOrDefault(x => x.IsAvailable());
                if (firstAvailable != null)
                {
                    return firstAvailable;
                }
            }
            throw Fail.Because("There is no context storage available - are you missing declaration: using(new " + nameof(SessionThreadStaticScope) +
                               "()) {{ DATABASE ACCESS CODE; }}");
        }
    }

    /// <summary>
    /// Component responsible for storing and retrieval of NHibernate sessions. It can store sessions in:
    /// web context, wcf context or thread static field - the strategy (context storage) is chosen
    /// depending on the application that uses it.
    /// </summary>
    public interface ISessionContext
    {
        void StoreSession([NotNull] IDatabase database, [NotNull] ISession session);
        void StoreSession([NotNull] IDatabase database, [NotNull] IStatelessSession session);

        [CanBeNull, MustUseReturnValue]
        ISession GetSession([NotNull] IDatabase database);

        [CanBeNull, MustUseReturnValue]
        IStatelessSession GetStatelessSession([NotNull] IDatabase database);
    }
}