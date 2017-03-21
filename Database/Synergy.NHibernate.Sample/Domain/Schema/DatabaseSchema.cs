using System;
using System.Text;
using JetBrains.Annotations;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Sample.Domain.Schema
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class DatabaseSchema : IDatabaseSchema
    {
        public void CreateFor(IDatabase database)
        {
            Configuration configuration = database.GetNHibernateConfiguration();

            var schemaExport = new SchemaExport(configuration);
            schemaExport.SetDelimiter(";");

            var scriptLines = new StringBuilder();
            schemaExport.Create(scriptLine => scriptLines.AppendLine(scriptLine), false);

            string script = scriptLines.ToString();

            Console.WriteLine(script);

            if (database is ISampleDatabase)
            {
                database.CurrentSession
                        .CreateSQLQuery(
                            @"
DROP TABLE [words].[Word]
DROP TABLE [words].[WordGroup]
DROP TABLE [words].[User]
")
                        .ExecuteUpdate();

                //database.CurrentSession
                //        .CreateSQLQuery($"DROP SCHEMA {SampleDatabase.SchemaName}")
                //        .ExecuteUpdate();

                //database.CurrentSession
                //        .CreateSQLQuery($"CREATE SCHEMA {SampleDatabase.SchemaName}")
                //        .ExecuteUpdate();
            }

            database.CurrentSession
                    .CreateSQLQuery(script)
                    .ExecuteUpdate();
        }
    }

    public interface IDatabaseSchema
    {
        void CreateFor([NotNull] IDatabase database);
    }
}