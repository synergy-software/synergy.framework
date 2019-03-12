using System;
using System.Linq;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.NHibernate.Engine
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class DatabaseProvider : IDatabaseProvider
    {
        // TODO:mace (from:mace on:05-04-2018) użyj component locatora
        private readonly IDatabase[] databases;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public DatabaseProvider(IDatabase[] databases)
        {
            this.databases = databases;
        }

        /// <inheritdoc />
        public IDatabase Get(Type databaseType)
        {
            Fail.IfArgumentNull(databaseType, nameof(databaseType));
            Fail.IfFalse(typeof(IDatabase).IsAssignableFrom(databaseType), Violation.Of("{0} is not " + nameof(IDatabase), databaseType));

            return this.databases.SingleOrDefault(db => databaseType.IsInstanceOfType(db));
        }

        ///// <inheritdoc />
        //public IDatabase GetDatabaseForEntity(Type entityType)
        //{
        //    Fail.IfArgumentNull(entityType, nameof(entityType));

        //    return this.databases.SingleOrDefault(db => db.ContainsEntity(entityType));
        //}
    }

    /// <summary>
    /// Component serving database for specific conditions. 
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Gets a database of provided Type or null if there is no such database.
        /// </summary>
        [CanBeNull, Pure]
        IDatabase Get([NotNull] Type databaseType);

        //[CanBeNull, Pure]
        //IDatabase GetDatabaseForEntity([NotNull] Type entityType);
    }
}