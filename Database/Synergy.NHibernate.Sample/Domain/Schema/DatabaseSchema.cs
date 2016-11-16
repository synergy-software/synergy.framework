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
if exists (select * from dbo.sysobjects where id = object_id(N'[WordGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
  DROP TABLE WordGroup
if exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
  DROP TABLE [User]")
                        .ExecuteUpdate();
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