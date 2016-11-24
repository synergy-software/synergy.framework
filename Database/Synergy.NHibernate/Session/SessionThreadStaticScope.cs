using Synergy.NHibernate.Context;

namespace Synergy.NHibernate.Session
{
    public class SessionThreadStaticScope : ThreadStaticContextScope<SessionsContainer>
    {
    }
}