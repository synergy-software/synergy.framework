using System.Web;
using System.Web.SessionState;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HttpContextSession : HttpContextBase, IHttpContextSession
    {
        /// <inheritdoc />
        public override bool IsAvailable()
        {
            return base.IsAvailable() && (HttpContext.Current.Session != null);
        }

        /// <inheritdoc />
        public T Get<T>(string key)
        {
            Fail.IfArgumentNull(key, nameof(key));

            return this.GetSession()[key].AsOrFail<T>();
        }

        /// <inheritdoc />
        public void Set<T>(string key, T value)
        {
            Fail.IfArgumentNull(key, nameof(key));

            this.GetSession()[key] = value;
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            Fail.IfArgumentNull(key, nameof(key));

            this.GetSession()
                .Remove(key);
        }

        /// <inheritdoc />
        public void Abandon()
        {
            this.GetSession()
                .Abandon();
        }

        [NotNull]
        [Pure]
        private HttpSessionState GetSession()
        {
            Fail.IfFalse(this.IsAvailable(), Violation.Of("HttpContext.Current.Session is not available"));

            return this.GetContext()
                       .Session;
        }
    }

    /// <summary>
    ///     Wrapper around HttpContext.Current.Session - it is wrapped in a component that can be easilly mocked or replaced in
    ///     different environments.
    /// </summary>
    public interface IHttpContextSession
    {
        /// <summary>
        ///     Determines whether the HttpContext.Current.Session is available.
        ///     If it is not all the other methods of this component will throw exception.
        /// </summary>
        [Pure]
        bool IsAvailable();

        /// <summary>
        ///     Gets a value stored under the specified key or null (default(T)) if there is nothing stored there.
        /// </summary>
        [CanBeNull]
        [Pure]
        T Get<T>([NotNull] string key);

        /// <summary>
        ///     Sets a value under specified key.
        /// </summary>
        void Set<T>([NotNull] string key, [CanBeNull] T value);

        /// <summary>
        ///     Removes a value stored under the specified key.
        /// </summary>
        void Remove([NotNull] string key);

        /// <summary>
        ///     Cancels the current session.
        /// </summary>
        void Abandon();
    }
}