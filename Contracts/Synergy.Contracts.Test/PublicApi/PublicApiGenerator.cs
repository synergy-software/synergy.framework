using System.Reflection;
using ApprovalTests;
using ApprovalTests.Namers;
using PublicApiGenerator;
using Xunit;

namespace Synergy.Contracts.Test.PublicApi
{
    public class PublicApiGenerator
    {
        [Fact]
        public void GeneratePublicApiDescription()
        {
            Assembly assembly = typeof(Fail).Assembly;
            var publicApi = assembly.GeneratePublicApi();
            var writer = new ApprovalTextWriter(publicApi, "txt");
            var approvalNamer = new AssemblyPathNamer(assembly);
            Approvals.Verify(writer, approvalNamer, Approvals.GetReporter());
        }

        public class AssemblyPathNamer : UnitTestFrameworkNamer
        {
            private readonly string name;

            public AssemblyPathNamer(Assembly assembly)
            {
                name = assembly.GetName().Name;
            }

            public override string Name
            {
                get { return name; }
            }
        }
    }
}