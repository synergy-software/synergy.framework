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
        private readonly IWebContextSorage<SessionsContainer> webContextSorage;
        private readonly IWcfContextSorage<SessionsContainer> wcfContextSorage;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionContext(
            IWebContextSorage<SessionsContainer> webContextSorage,
            IWcfContextSorage<SessionsContainer> wcfContextSorage,
            IThreadStaticContextSorage<SessionsContainer> threadStaticContextSorage,
            IStaticContextStorage<SessionsContainer> staticContextStorage)
        {
            this.webContextSorage = webContextSorage;
            this.wcfContextSorage = wcfContextSorage;
            this.threadStaticContextSorage = threadStaticContextSorage;
            this.staticContextStorage = staticContextStorage;
        }

        /// <inheritdoc />
        public ISession GetSession(IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            SessionsContainer container = this.GetSessionsContainer();
            return container.GetSession(database);
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
        public ISession[] RemoveSessions()
        {
            SessionsContainer container = this.GetContextStorage().Clear();
            if (container == null)
                return new ISession[0];

            return container.RemoveSessions();
        }

        [NotNull]
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
        [CanBeNull]
        [Pure]
        ISession GetSession([NotNull] IDatabase database);

        void StoreSession([NotNull] IDatabase database, [NotNull] ISession session);

        [NotNull]
        [ItemNotNull]
        ISession[] RemoveSessions();
    }
}