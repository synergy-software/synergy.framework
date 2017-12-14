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
        private readonly Dictionary<string, Couple> sessions = new Dictionary<string, Couple>();

        public void StoreSession([NotNull] IDatabase database, [NotNull] ISession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            string key = database.GetKey();
            this.sessions.Add(key, new Couple(database, session));
        }

        [CanBeNull]
        public ISession GetSession([NotNull] IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            string key = database.GetKey();
            Couple couple;
            this.sessions.TryGetValue(key, out couple);
            return couple?.Session;
        }

        [NotNull]
        [ItemNotNull]
        public ISession[] RemoveSessions()
        {
            var sessionsToRemove = this.sessions
                                       .Values
                                       .Select(c => c.Session)
                                       .ToArray();
            Fail.IfCollectionContainsNull(sessionsToRemove, nameof(sessionsToRemove));

            this.sessions.Clear();
            return sessionsToRemove;
        }

        public void RemoveSession(ISession session)
        {
            foreach (KeyValuePair<string, Couple> pair in this.sessions.ToList())
            {
                if (pair.Value.Session == session)
                    this.sessions.Remove(pair.Key);
            }
        }

        private class Couple
        {
            public readonly IDatabase Database;
            public readonly ISession Session;

            public Couple(IDatabase database, ISession session)
            {
                this.Database = database;
                this.Session = session;
            }
        }


    }
}