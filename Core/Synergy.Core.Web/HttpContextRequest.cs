using System;
using System.Web;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HttpContextRequest : HttpContextBase, IHttpContextRequest
    {
        /// <inheritdoc />
        public override bool IsAvailable()
        {
            return base.IsAvailable() && this.IsRequestAvailable();
        }

        /// <inheritdoc />
        public string GetRawUrl()
        {
            return this.GetRequest()
                       .RawUrl;
        }

        /// <inheritdoc />
        public HttpCookie GetCookie(string cookieName)
        {
            return this.GetRequest()
                       .Cookies[cookieName];
        }

        /// <inheritdoc />
        public string GetRequestId()
        {
            if (this.IsAvailable())
                return null;

            const string currentRequestIdentifierContextItemId = "requestId-FC7406C5-91CB-420E-A819-B1694F40522A";
            var items = this.GetContext().Items;

            if (items.Contains(currentRequestIdentifierContextItemId) == false)
            {
                items.Add(currentRequestIdentifierContextItemId, this.GenerateRequestId());
            }

            return items[currentRequestIdentifierContextItemId].AsOrFail<string>();
        }

        [NotNull, Pure]
        private string GenerateRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        [NotNull]
        [Pure]
        private HttpRequest GetRequest()
        {
            Fail.IfFalse(this.IsAvailable(), Violation.Of("HttpContext.Current.Request is not available."));

            return HttpContext.Current.Request;
        }

        private bool IsRequestAvailable()
        {
            //
            // WARN: Let's read flag 'internal bool HideRequestResponse;' from HttpContext 
            //       - if we don't, reading HttpContext.Current.Request throws exception
            //
            return this.ReadHttpContextInternalFlag("HideRequestResponse");
        }
    }

    /// <summary>
    ///     Wrapper around HttpContext.Current.Request - it is wrapped in a component that can be easilly mocked or replaced in
    ///     different environments.
    /// </summary>
    public interface IHttpContextRequest
    {
        /// <summary>
        ///     Determines whether the HttpContext.Current.Request is available.
        ///     If it is not all the other methods of this component will throw exception.
        /// </summary>
        [Pure]
        bool IsAvailable();

        /// <summary>
        ///     Gets the URI requsted by the client, which may include PathInfo and QueryString if it exists.
        ///     This value is unaffected by any URL rewriting or routing that may occur on the server.
        /// </summary>
        [NotNull]
        [Pure]
        string GetRawUrl();

        /// <summary>
        ///     Returns an <see cref='System.Web.HttpCookie' /> item from the request cookies collection.
        /// </summary>
        [CanBeNull]
        [Pure]
        HttpCookie GetCookie([NotNull] string cookieName);

        /// <summary>
        /// Returns id of current request or null if there is no web request now.
        /// The id is a guid. This method checks if there is already id assigned to the request and creates new one if there is no such id.
        /// </summary>
        [CanBeNull, Pure]
        string GetRequestId();
    }
}