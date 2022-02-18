using System;
using System.Collections.Generic;

namespace Synergy.Convention.Testing.Rules
{
    public static class SealedRule
    {
        public static IEnumerable<Deficit> MustBeSealed(this Type type)
        {
            if (type.IsSealed == false)
                yield return new Deficit(type, "sealed");
        }

        public static IEnumerable<Deficit> MustBeSealedOrAbstract(this Type type)
        {
            if (type.IsAbstract)
                yield break;

            foreach (var deficit in type.MustBeSealed())
                yield return deficit;
        }
    }
}