using Synergy.Sample.Web.API.Tests.Infrastructure;
using Xunit;

namespace Synergy.Sample.Web.API.Tests
{
    // TODO: Marcin Celej [from: Marcin Celej on: 08-04-2023]: Do something to prevent orphan process from this web api tests
    
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