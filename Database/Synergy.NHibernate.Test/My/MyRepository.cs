using System.Linq;
using JetBrains.Annotations;
using NHibernate.Linq;
using Synergy.NHibernate.Repositories;

namespace Synergy.NHibernate.Test.My
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class MyRepository : Repository<IMyDatabase>, IMyRepository
    {
        public MyEntity[] GetAll()
        {
            return this.CurreSession
                       .Query<MyEntity>()
                       .ToArray();
        }
    }

    public interface IMyRepository : IRepository
    {
        [NotNull]
        [ItemNotNull]
        MyEntity[] GetAll();
    }
}