using System.Reflection;
using JetBrains.Annotations;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Session
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SessionCoordinator : ISessionCoordinator
    {
        private readonly IDatabase[] databases;

        public SessionCoordinator(IDatabase[] databases)
        {
            this.databases = databases;
        }

        /// <inheritdoc />
        public void StartSessionsFor(MethodInfo method)
        {
            foreach (IDatabase database in this.databases)
            {
                // ReSharper disable once UnusedVariable
                var currentSession = database.CurrentSession;
            }
        }
    }

    public interface ISessionCoordinator
    {
        void StartSessionsFor(MethodInfo method);
    }
}