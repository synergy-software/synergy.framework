using JetBrains.Annotations;
using Synergy.Core.Windsor;

namespace Synergy.Core.Test
{
    public class ApplicationServer
    {
        [NotNull]
        public static IWindsorEngine Start()
        {
            var rootLibrary = new SynergyCoreTestLibrary();
            IWindsorEngine windsorEngine = new WindsorEngine();
            windsorEngine.Start(rootLibrary);
            return windsorEngine;
        }
    }
}