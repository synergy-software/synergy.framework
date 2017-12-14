using NHibernate;
using Synergy.NHibernate.Contexts;

namespace Synergy.NHibernate.Session
{
    public class SessionThreadStaticScope : ThreadStaticContextScope<SessionsContainer>
    {
        public override void Dispose()
        {
            // ReSharper disable once ArrangeStaticMemberQualifier
            ISession[] sessions = Sack.Value.RemoveSessions();
            foreach (ISession session in sessions)
            {
                session.Dispose();
            }

            base.Dispose();
        }
    }
}