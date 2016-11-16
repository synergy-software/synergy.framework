using System;
using System.Reflection;
using System.Web;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Web
{
    /// <summary>
    /// Base class for components wrapping HttpContext.Current objects.
    /// </summary>
    public abstract class HttpContextBase
    {
        /// <summary>
        /// Determines whether the HttpContext.Current is available.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAvailable()
        {
            return HttpContext.Current != null;
        }

        /// <summary>
        ///     Gets the HttpContext.Current or throws an exception if it is null.
        /// </summary>
        [NotNull]
        [Pure]
        protected HttpContext GetContext()
        {
            HttpContext context = HttpContext.Current;
            Fail.IfNull(context, "HttpContext.Current is not available");
            return context;
        }

        /// <summary>
        ///     Reads an internal flag from HttpContext.Current using reflection.
        /// </summary>
        protected bool ReadHttpContextInternalFlag([NotNull] string internalFieldName)
        {
            Fail.IfArgumentNull(internalFieldName, nameof(internalFieldName));

            HttpContext context = this.GetContext();
            Type type = context.GetType();
            FieldInfo field = type.GetField(internalFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var hideRequestResponse = false;
            if (field != null)
            {
                hideRequestResponse = field.GetValue(context)
                                           .CastOrFail<bool>();
            }
            return hideRequestResponse == false;
        }
    }
}