using System.Collections.Generic;
using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using Microsoft.Extensions.Logging;

namespace CatFactory.PostgreSql
{
    public partial class PostgreSqlDatabaseFactory
    {
        public static async Task<Database> ImportAsync(ILogger<PostgreSqlDatabaseFactory> logger, string connectionString, bool importViews = false, bool importSequences = false, IEnumerable<string> exclusions = null)
        {
            var factory = new PostgreSqlDatabaseFactory(logger)
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    ConnectionString = connectionString,
                    ImportViews = importViews,
                    ImportSequences = importSequences
                }
            };

            if (exclusions != null)
                factory.DatabaseImportSettings.Exclusions.AddRange(exclusions);

            return await factory.ImportAsync();
        }

        public static async Task<Database> ImportAsync(string connectionString, bool importViews = false, bool importSequences = false, IEnumerable<string> exclusions = null)
        {
            var factory = new PostgreSqlDatabaseFactory
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    ConnectionString = connectionString,
                    ImportViews = importViews,
                    ImportSequences = importSequences
                }
            };

            if (exclusions != null)
                factory.DatabaseImportSettings.Exclusions.AddRange(exclusions);

            return await factory.ImportAsync();
        }

        public static Database Import(ILogger<PostgreSqlDatabaseFactory> logger, string connectionString, bool importViews = false, bool importSequences = false, IEnumerable<string> exclusions = null)
            => ImportAsync(logger, connectionString, importViews, importSequences, exclusions).GetAwaiter().GetResult();

        public static Database Import(string connectionString, bool importViews = false, bool importSequences = false, IEnumerable<string> exclusions = null)
            => ImportAsync(connectionString, importViews, importSequences, exclusions).GetAwaiter().GetResult();
    }
}
