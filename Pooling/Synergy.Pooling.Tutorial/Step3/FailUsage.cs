using JetBrains.Annotations;

namespace Synergy.Pooling.Tutorial.Step3
{
    static class FailUsage
    {
        static void Sample([NotNull] object value, int number)
        {
            Fail.IfNull(value, "value is null but should be {0}", number);
        }
    }
}
