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
        private readonly IThreadStaticContextSorage<SessionsContainer> threadStaticContextSorage;
        private readonly IStaticContextStorage<SessionsContainer> staticContextStorage;
        private readonly ICustomSessionStorage[] customSessionStorages;
        private readonly IWebContextSorage<SessionsContainer> webContextSorage;
        private readonly IWcfContextSorage<SessionsContainer> wcfContextSorage;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionContext(
            ICustomSessionStorage[] customSessionStorages,
            IWebContextSorage<SessionsContainer> webContextSorage,
            IWcfContextSorage<SessionsContainer> wcfContextSorage,
            IThreadStaticContextSorage<SessionsContainer> threadStaticContextSorage,
            IStaticContextStorage<SessionsContainer> staticContextStorage)
        {
            this.customSessionStorages = customSessionStorages;
            this.webContextSorage = webContextSorage;
            this.wcfContextSorage = wcfContextSorage;
            this.threadStaticContextSorage = threadStaticContextSorage;
            this.staticContextStorage = staticContextStorage;
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
            IContextSorage<SessionsContainer> storage = this.GetContextStorage();
            SessionsContainer container = storage.Get();
            if (container == null)
            {
                container = new SessionsContainer();
                storage.Store(container);
            }

            return container;
        }

        [NotNull, Pure]
        private IContextSorage<SessionsContainer> GetContextStorage()
        {
            foreach (ICustomSessionStorage customSessionStorage in this.customSessionStorages)
            {
                if (customSessionStorage.IsAvailable())
                    return customSessionStorage;
            }

            if (this.webContextSorage.IsAvailable())
                return this.webContextSorage;

            if (this.wcfContextSorage.IsAvailable())
                return this.wcfContextSorage;

            if (this.threadStaticContextSorage.IsAvailable())
                return this.threadStaticContextSorage;

            //
            // WARN: The static context should not be enabled here - it will not work properly in multithreaded apps - e.g. web apps
            //
            //if (this.staticContextStorage.IsAvailable())
            //    return this.staticContextStorage;

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