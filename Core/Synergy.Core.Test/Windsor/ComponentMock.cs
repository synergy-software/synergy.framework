using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;

namespace Synergy.Core.Test.Windsor
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ComponentMock : IComponentMock
    {
        private readonly IEnumerable<IDependentComponentMock> dependencies;

        public ComponentMock(IEnumerable<IDependentComponentMock> dependencies)
        {
            this.dependencies = dependencies;
        }

        public IEnumerable<IDependentComponentMock> GetDependencies()
        {
            return this.dependencies;
        }
    }

    public interface IComponentMock
    {
        IEnumerable<IDependentComponentMock> GetDependencies();
    }

    public class DependentComponentMock : IDependentComponentMock
    {
    }

    public interface IDependentComponentMock
    {
    }

    public class InheritedDependentComponentMock : DependentComponentMock
    {
    }

    [Transient]
    public class TransientComponentMock : ITransientComponentMock
    {
    }

    public interface ITransientComponentMock
    {
    }

    [Singleton]
    public class SingletonComponentMock : ISingletonComponentMock
    {
    }

    public interface ISingletonComponentMock
    {
    }
}