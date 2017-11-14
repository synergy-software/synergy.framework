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

    [Transient]
    public class StatefulComponent : IStatefulComponent
    {
        private readonly State state;
        public ISingletonComponentMock Dependency { get; }
        public string Id => this.state.Id;

        public StatefulComponent(State state, ISingletonComponentMock dependency)
        {
            this.state = state;
            this.Dependency = dependency;
        }
    }

    public interface IStatefulComponent
    {
        string Id { get; }
        ISingletonComponentMock Dependency { get; }
    }

    public class State
    {
        public readonly string Id;

        public State(string id)
        {
            this.Id = id;
        }
    }
}