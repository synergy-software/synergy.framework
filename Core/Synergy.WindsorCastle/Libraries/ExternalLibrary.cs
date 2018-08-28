using System;
using System.Reflection;
using Synergy.Core;

namespace Synergy.WindsorCastle.Libraries
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
