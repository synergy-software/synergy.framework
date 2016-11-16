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

        [NotNull]
        [Pure]
        private HttpRequest GetRequest()
        {
            Fail.IfFalse(this.IsAvailable(), "HttpContext.Current.Request is not available.");

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
    }
}