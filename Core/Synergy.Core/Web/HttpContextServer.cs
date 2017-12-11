using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <inheritdoc cref="IHttpContextServer" />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HttpContextServer : HttpContextBase, IHttpContextServer
    {
        /// <inheritdoc />
        public string MapPath(string path)
        {
            Fail.IfArgumentNull(path, nameof(path));

            return this.GetContext()
                       .Server
                       .MapPath(path);
        }
    }

    /// <summary>
    ///     Wrapper around HttpContext.Current.Server - it is wrapped in a component that can be easilly mocked or replaced in
    ///     different environments.
    /// </summary>
    public interface IHttpContextServer
    {
        /// <summary>
        ///     Determines whether the HttpContext.Current.Server is available.
        ///     If it is not all the other methods of this component will throw exception.
        /// </summary>
        [Pure]
        bool IsAvailable();

        /// <summary>
        ///     Maps a virtual path to a physical path.
        /// </summary>
        [NotNull]
        [Pure]
        string MapPath([NotNull] string path);
    }
}