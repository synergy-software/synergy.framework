using JetBrains.Annotations;
using Synergy.NHibernate.Repositories;

namespace Synergy.NHibernate.Sample.Domain.Users
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class UserRepository : Repository<ISampleDatabase>, IUserRepository
    {
        
    }

    public interface IUserRepository
    {
    }
}