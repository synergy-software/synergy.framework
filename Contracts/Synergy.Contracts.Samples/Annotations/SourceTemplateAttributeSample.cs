using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Annotations
{
    public static class SourceTemplateAttributeSample
    {
        [SourceTemplate]
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable UnusedVariable
        public static void forEach<T>([NotNull] this IEnumerable<T> xs)
        {
            foreach (T x in xs)
            {
                //$ $END$
            }
        }
        // ReSharper restore UnusedVariable

        [SourceTemplate]
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        public static void newGuid(this object obj, [Macro(Expression = "guid()", Editable = -1)] string newguid)
        {
            Console.WriteLine("$newguid$");
        }

        [SourceTemplate]
        [UsedImplicitly]
        public static void Bar<T>(this T entity)
        {
            /*$ var $entity$Id = entity.GetId();;
            DoSomething($entity$Id); */
        }

        [UsedImplicitly]
        private static void Test()
        {
            //var enumerable = new List<string>();

            //enumerable.forEach
            //enumerable.newGuid

            //enumerable.Bar
        }
    }
}