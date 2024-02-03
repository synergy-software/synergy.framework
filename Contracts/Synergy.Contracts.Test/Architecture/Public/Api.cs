using System.Threading.Tasks;
using NUnit.Framework;
using Synergy.Documentation.Api;
using VerifyXunit;
using Xunit;

namespace Synergy.Contracts.Test.Architecture.Public
{
    [UsesVerify]
    public class Api
    {
        [Fact]
        public async Task Generate()
        {
            // ARRANGE
            var assembly = typeof(Fail).Assembly;

            // ACT
            var publicApi = ApiDescription.GenerateFor(assembly);

            // ASSERT
            await Verifier.Verify(publicApi, "md")
                          .UseMethodName("of." + assembly.GetName().Name)
                          .UniqueForTargetFrameworkAndVersion()
                          // TODO: Marcin Celej [from: Marcin Celej on: 03-02-2024]: remove that when job fiished
                          .AutoVerify();
        }
    }
}