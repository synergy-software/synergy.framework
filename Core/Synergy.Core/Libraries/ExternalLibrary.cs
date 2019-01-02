using System;
using System.Reflection;

namespace Synergy.Core
{
    public class ExternalLibrary: Library
    {
        private readonly Type typeFromLibrary;

        /// <inheritdoc />
        public ExternalLibrary(Type typeFromLibrary)
        {
            this.typeFromLibrary = typeFromLibrary;
        }

        /// <inheritdoc />
        public override Assembly GetAssembly()
        {
            return this.typeFromLibrary.Assembly;
        }
    }
}
