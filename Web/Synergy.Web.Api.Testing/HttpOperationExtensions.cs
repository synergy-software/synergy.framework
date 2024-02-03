using System.Collections.Generic;
using Synergy.Contracts;
using Synergy.Web.Api.Testing.Assertions;

namespace Synergy.Web.Api.Testing
{
    public static class HttpOperationExtensions
    {
        public static TOperation Details<TOperation>(this TOperation operation, string details)
            where TOperation : HttpOperation
        {
            operation.SetDescription(details.OrFailIfWhiteSpace());
            return operation;
        }

        public static TOperation ShouldBe<TOperation>(this TOperation operation, IAssertion assertion)
            where TOperation : HttpOperation
        {
            operation.Assert(new[] {assertion});
            return operation;
        }

        public static TOperation ShouldBe<TOperation>(this TOperation operation, IEnumerable<IAssertion> assertions)
            where TOperation : HttpOperation
        {
            operation.Assert(assertions);
            return operation;
        }
    }
}