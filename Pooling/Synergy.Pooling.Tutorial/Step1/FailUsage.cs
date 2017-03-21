using JetBrains.Annotations;

namespace Synergy.Pooling.Tutorial.Step1
{
    static class FailUsage
    {
        static void Sample([NotNull] object value, int number)
        {
            //
            // WARN: Heap Allocation Viewer underlines the method below:
            //       - object allocation: parameters array 'args' creation
            //       - boxing allocation: conversion from value 'int' to reference type 'object'
            //

            Fail.IfNull(value, "value is null but should be {0}", number);

            //
            // Below is sample memory allocation
            //
            // |XXX|XXX|YYY|YYY|   |   |...
            // |400|401|402|403|404|405|...

            //
            // After Garbage collection it looks like this:
            //
            // |   |   |YYY|YYY|   |   |...
            // |400|401|402|403|404|405|...

            //
            // Then compactation occurs:
            //
            // |YYY|YYY|   |   |   |   |...
            // |400|401|402|403|404|405|...

            //
            // And all this is expensive and lasts
            //
        }
    }
}
