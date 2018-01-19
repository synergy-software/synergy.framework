using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Contexts;

namespace Synergy.NHibernate.Session
{
    public class SessionThreadStaticScope : ThreadStaticContextScope<SessionsContainer>
    {
        public override void Dispose()
        {
            this.DisposeSessions();
            base.Dispose();
        }

        private void DisposeSessions()
        {
            Fail.IfNull(Sack, nameof(Sack) + " is null");

            // ReSharper disable once ArrangeStaticMemberQualifier
            SessionsContainer sessionsContainer = Sack.Value;

            // Sack may be empty - when no session was started
            if (sessionsContainer == null)
                return;
            
            ISession[] sessions = sessionsContainer.RemoveSessions();
            foreach (ISession session in sessions)
            {
                session.Dispose();
            }
        }
    }
}