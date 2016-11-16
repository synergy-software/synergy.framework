using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Context;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Session
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SessionContext : ISessionContext
    {
        private readonly IThreadStaticContextSorage<SessionsContainer> threadStaticContextSorage;
        private readonly IWebContextSorage<SessionsContainer> webContextSorage;
        private readonly IWcfContextSorage<SessionsContainer> wcfContextSorage;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionContext(
            IWebContextSorage<SessionsContainer> webContextSorage,
            IWcfContextSorage<SessionsContainer> wcfContextSorage,
            IThreadStaticContextSorage<SessionsContainer> threadStaticContextSorage)
        {
            this.webContextSorage = webContextSorage;
            this.wcfContextSorage = wcfContextSorage;
            this.threadStaticContextSorage = threadStaticContextSorage;
        }

        /// <inheritdoc />
        public ISession GetSession(IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            SessionsContainer container = this.GetSessionsContainer();
            return container.GetSession(database);
        }

        public void StoreSession(IDatabase database, ISession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            SessionsContainer container = this.GetSessionsContainer();
            container.Store(database, session);
        }

        public ISession[] RemoveSessions()
        {
            SessionsContainer container = this.GetSessionsContainer();
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

        private IContextSorage<SessionsContainer> GetContextStorage()
        {
            // TODO:mace (from:mace on:31-10-2016) check if this context works in asynchronous Task started from mvc action

            if (this.webContextSorage.IsAvailable())
                return this.webContextSorage;

            if (this.wcfContextSorage.IsAvailable())
                return this.wcfContextSorage;

            if (this.threadStaticContextSorage.IsAvailable())
                return this.threadStaticContextSorage;

            throw Fail.Because("There is no context storage available");
        }
    }

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