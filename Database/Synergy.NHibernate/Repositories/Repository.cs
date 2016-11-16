using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Repositories
{
    // TODO:mace (from:mace on:08-11-2016) add ReadoOnlyRepository with StatelesSession

    public class Repository<TDatabse> : IRepository
        where TDatabse : IDatabase
    {
        /// <summary>
        ///     WARN: This property is public as it is injected by Windsor container. DO NOT ASSIGN IT.
        /// </summary>
        [UsedImplicitly]
        public TDatabse Database { get; set; }

        [NotNull]
        protected ISession CurreSession => this.Database.OrFail(nameof(this.Database))
                                               .CurrentSession;
    }

    public interface IRepository
    {
    }
}