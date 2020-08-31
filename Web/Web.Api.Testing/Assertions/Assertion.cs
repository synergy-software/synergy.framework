using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Web.Api.Testing.Features;

namespace Synergy.Web.Api.Testing.Assertions
{
    public abstract class Assertion : IAssertion
    {
        public string? ExpectedResult { get; protected set; }

        public abstract Result Assert(HttpOperation operation);

        public IAssertion Expected(string expected)
        {
            ExpectedResult = expected.OrFailIfWhiteSpace(nameof(expected));
            return this;
        }

        public static Result Ok => Result.Ok;
        public static Result Failure(string message) => new Result(message);

        public struct Result
        {
            private const string OkMessage = "OK";
            public string Message { get; }
            public bool IsOk => Message == OkMessage;

            public Result([NotNull] string message)
            {
                Message = message.OrFailIfWhiteSpace(nameof(message)).Trim();
            }

            public static Result Ok => new Result(OkMessage);
        }
    }

    public interface IAssertion : IExpectation
    {
        Assertion.Result Assert(HttpOperation operation);
    }
}