using JetBrains.Annotations;
using Synergy.NHibernate.Repositories;

namespace Synergy.NHibernate.Test.Database.Users
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class UserRepository : Repository<ISampleDatabase>, IUserRepository
    {
        
    }

    public interface IUserRepository
    {
    }
}