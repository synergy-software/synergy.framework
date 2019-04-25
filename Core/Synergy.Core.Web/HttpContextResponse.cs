using System;
using System.Web;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HttpContextResponse : HttpContextBase, IHttpContextResponse
    {
        /// <inheritdoc />
        public override bool IsAvailable()
        {
            return base.IsAvailable() && this.IsResponseAvailable();
        }

        /// <inheritdoc />
        public void ClearCookie(string cookieName)
        {
            Fail.IfArgumentNull(cookieName, nameof(cookieName));

            var aCookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddYears(-30)
            };
            this.GetResponse()
                .Cookies
                .Add(aCookie);
        }

        /// <inheritdoc />
        public void End()
        {
            this.GetResponse()
                .End();
        }

        [NotNull]
        [Pure]
        private HttpResponse GetResponse()
        {
            Fail.IfFalse(this.IsAvailable(), Violation.Of("HttpContext.Current.Response is not available."));

            return HttpContext.Current.Response;
        }

        private bool IsResponseAvailable()
        {
            //
            // WARN: Let's read flag 'internal bool HideRequestResponse;' from HttpContext 
            //       - if we don't, reading HttpContext.Current.Response throws exception
            //
            return this.ReadHttpContextInternalFlag("HideRequestResponse");
        }
    }

    /// <summary>
    ///     Wrapper around HttpContext.Current.Response - it is wrapped in a component that can be easilly mocked or replaced
    ///     in
    ///     different environments.
    /// </summary>
    public interface IHttpContextResponse
    {
        /// <summary>
        ///     Determines whether the HttpContext.Current.Response is available.
        ///     If it is not all the other methods of this component will throw exception.
        /// </summary>
        [Pure]
        bool IsAvailable();

        /// <summary>
        ///     Clears a cooki - it actually issues a cookie to a client with the current response but the cookie expiration date
        ///     is set to 'long time ago...'.
        /// </summary>
        void ClearCookie([NotNull] string cookieName);

        /// <summary>
        ///     Sends all currently buffered output to the client then closes the socket connection.
        /// </summary>
        void End();
    }
}