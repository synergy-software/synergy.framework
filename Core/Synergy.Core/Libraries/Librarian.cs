using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Extensions;

namespace Synergy.Core.Libraries
{
    /// <inheritdoc />
    public class Librarian : ILibrarian
    {
        [NotNull]
        private readonly Library[] libraries;

        [NotNull]
        private readonly Library mainLibrary;

        /// <summary>
        ///     Allows inherited library to define dependencies - other libraries that are used by the calling one.
        /// </summary>
        public Librarian([NotNull] Library rootLibrary)
        {
            Fail.IfArgumentNull(rootLibrary, nameof(rootLibrary));

            this.mainLibrary = rootLibrary;
            this.libraries = this.GetAllDependencies(rootLibrary);
        }

        /// <inheritdoc />
        public Library GetRootLibrary()
        {
            return this.mainLibrary;
        }

        /// <inheritdoc />
        public Library[] GetLibraries()
        {
            return this.libraries;
        }

        [NotNull]
        [Pure]
        private Library[] GetAllDependencies(Library rootLibrary)
        {
            List<Library> allLibraries = this.GetSortedLibraries(rootLibrary);
            var coreLibrary = new SynergyCoreLibrary();

            if (allLibraries.Any(l => l.Equals(coreLibrary)) == false)
                allLibraries.Add(coreLibrary);

            return allLibraries.ToArray();
        }

        [NotNull]
        [Pure]
        private List<Library> GetSortedLibraries(Library rootLibrary)
        {
            var traversed = new List<Library>();
            var topologicalSort = new TopologicalSort<Library>();

            var toTraverse = new Queue<Library>();
            toTraverse.Enqueue(rootLibrary);
            while (toTraverse.Count > 0)
            {
                Library predecessor = toTraverse.Dequeue();
                if (traversed.Any(l => l.Equals(predecessor)) == false)
                    traversed.Add(predecessor);
                topologicalSort.Node(predecessor);
                foreach (Library successor in predecessor.Dependencies)
                {
                    Library realSuccessor = traversed.Find(t => t.Equals(successor));

                    if (realSuccessor == null)
                    {
                        if (toTraverse.Any(l => l.Equals(successor)) == false)
                            toTraverse.Enqueue(successor);

                        realSuccessor = successor;

                        if (traversed.Any(l => l.Equals(realSuccessor)) == false)
                            traversed.Add(realSuccessor);
                    }

                    topologicalSort.Edge(predecessor, realSuccessor);
                }
            }

            Queue<Library> sorted = topologicalSort.Sort();
            List<Library> allLibraries = sorted.ToList();
            return allLibraries;
        }
    }

    /// <summary>
    ///     Component responsible for providing Libraries that the current application consists of.
    /// </summary>
    public interface ILibrarian
    {
        /// <summary>
        ///     Gets the main <see cref="Library" /> of the application.
        /// </summary>
        [NotNull]
        [Pure]
        Library GetRootLibrary();

        /// <summary>
        ///     Gets all Libraries that the application consists of.
        ///     The returned list is sorted in order of usage and does not contain duplicates.
        /// </summary>
        [NotNull]
        [Pure]
        Library[] GetLibraries();
    }
}