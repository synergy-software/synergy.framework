using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Session
{
    public class SessionsContainer : IDisposable
    {
        private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>(1);
        private readonly Dictionary<string, IStatelessSession> statelesSessions = new Dictionary<string, IStatelessSession>(1);

        public void StoreSession([NotNull] IDatabase database, [NotNull] ISession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            string key = database.GetKey();
            this.sessions.Add(key, session);
        }

        public void StoreSession([NotNull] IDatabase database, [NotNull] IStatelessSession session)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));

            string key = database.GetKey();
            this.statelesSessions.Add(key, session);
        }

        [CanBeNull]
        public ISession GetSession([NotNull] IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            string key = database.GetKey();
            ISession session;
            this.sessions.TryGetValue(key, out session);
            if (session != null && session.IsOpen == false)
            {
                // Session is closed (disposed probably) so lets remove it and return null
                this.RemoveSession(session);
                session = null;
            }

            return session;
        }

        [CanBeNull]
        public IStatelessSession GetStatelessSession([NotNull] IDatabase database)
        {
            Fail.IfArgumentNull(database, nameof(database));

            string key = database.GetKey();
            IStatelessSession session;
            this.statelesSessions.TryGetValue(key, out session);
            if (session != null && session.IsOpen == false)
            {
                // Session is closed (disposed probably) so lets remove it and return null
                this.RemoveSession(session);
                session = null;
            }

            return session;
        }

        private void RemoveSession(ISession session)
        {
            foreach (var pair in this.sessions.ToList())
            {
                if (pair.Value == session)
                    this.sessions.Remove(pair.Key);
            }
        }

        private void RemoveSession(IStatelessSession session)
        {
            foreach (var pair in this.statelesSessions.ToList())
            {
                if (pair.Value == session)
                    this.sessions.Remove(pair.Key);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeDictionary(this.sessions);
            DisposeDictionary(this.statelesSessions);
        }

        private static void DisposeDictionary<T>([NotNull] Dictionary<string, T> dictionary) where T:IDisposable
        {
            foreach (var session in dictionary.Values)
                session.Dispose();

            dictionary.Clear();
        }
    }
}