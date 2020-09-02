using JetBrains.Annotations;

namespace Synergy.Web.Api.Testing.Features
{
    public static class FeatureExtensions
    {
        public static TOperation InStep<TOperation>([NotNull] this TOperation operation, [NotNull] Step step)
            where TOperation : HttpOperation
        {
            step.Attach(operation);
            return operation;
        }
    }
}