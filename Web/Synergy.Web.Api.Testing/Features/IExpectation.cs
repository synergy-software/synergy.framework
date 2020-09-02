using System.Net.Http;

namespace Synergy.Web.Api.Testing.Features
{
    public interface IExpectation
    {
        string? ExpectedResult { get; }
    }

    public interface IHttpRequestStorage
    {
        HttpRequestMessage GetSavedRequest();
    }

    public interface IHttpResponseStorage
    {
        HttpResponseMessage GetSavedResponse();
    }
}