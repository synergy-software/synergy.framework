using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Session
{
    public class SessionsContainer
    {
        private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>();

        public void Store([NotNull] IDatabase database, [NotNull] ISession session)
        {
            var key = database.GetKey();
            this.sessions.Add(key, session);
        }

        [CanBeNull]
        public ISession GetSession([NotNull] IDatabase database)
        {
            var key = database.GetKey();
            ISession session;
            this.sessions.TryGetValue(key, out session);
            return session;
        }

        [NotNull, ItemNotNull]
        public ISession[] RemoveSessions()
        {
            var toRemove = this.sessions.Values.ToArray();
            Fail.IfCollectionContainsNull(toRemove, nameof(toRemove));
            this.sessions.Clear();
            return toRemove;
        }
    }
}