using Synergy.NHibernate.Contexts;

namespace Synergy.NHibernate.Session
{
    public class SessionThreadStaticScope : ThreadStaticContextScope<SessionsContainer>
    {
    }
}