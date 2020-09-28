using Synergy.Sample.Web.API.Tests.Infrastructure;
using Xunit;

namespace Synergy.Sample.Web.API.Tests
{
    public class Repair
    {
        public const bool Mode = false;

        [Fact]
        public void you_cannot_leave_repair_mode()
        {
            using var testServer = new SampleTestServer {Repair = Repair.Mode};
            testServer.FailIfLeftInRepairMode();
        }
    }
}