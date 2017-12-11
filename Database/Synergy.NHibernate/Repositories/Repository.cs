using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Repositories
{
    // TODO:mace (from:mace on:08-11-2016) add ReadoOnlyRepository with StatelesSession


    // TODO:mace (from:mace on:06-12-2017) rename Repository<T> na DatabaseRepository<T>

    public abstract class Repository<TDatabse> : IRepository
        where TDatabse : IDatabase
    {
        /// <summary>
        ///     WARN: This property is public as it is injected by Windsor container. DO NOT ASSIGN IT.
        /// </summary>
        [UsedImplicitly]
        [NotNull] 
        // ReSharper disable once NotNullMemberIsNotInitialized
        public TDatabse Database { get; set; }

        [NotNull]
        protected ISession CurrentSession => this.Database.OrFail(nameof(this.Database))
                                               .CurrentSession;
    }

    public interface IRepository
    {
    }
}