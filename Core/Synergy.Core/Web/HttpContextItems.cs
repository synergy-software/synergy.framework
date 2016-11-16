using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HttpContextItems : HttpContextBase, IHttpContextItems
    {
        /// <inheritdoc />
        public T Get<T>(string key)
        {
            Fail.IfArgumentNull(key, nameof(key));

            return this.GetContext()
                       .Items[key]
                       .AsOrFail<T>();
        }

        /// <inheritdoc />
        public void Set<T>(string key, T value)
        {
            this.GetContext()
                .Items[key] = value;
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            this.GetContext()
                .Items
                .Remove(key);
        }
    }

    /// <summary>
    ///     Wrapper around HttpContext.Current.Items - it is wrapped in a component that can be easilly mocked or replaced in
    ///     different environments.
    /// </summary>
    public interface IHttpContextItems
    {
        /// <summary>
        ///     Determines whether the HttpContext.Current.Items is (are) available.
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
    }
}