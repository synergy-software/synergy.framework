using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Synergy.NHibernate.Session;

namespace Synergy.NHibernate.Test
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class DatabaseTestAttribute : TestActionAttribute
    {
        private SessionThreadStaticScope scope;

        /// <inheritdoc />
        public override void BeforeTest(ITest test)
        {
            this.scope = new SessionThreadStaticScope();
        }

        /// <inheritdoc />
        public override void AfterTest(ITest test)
        {
            this.scope.Dispose();
            this.scope = null;
        }
    }
}