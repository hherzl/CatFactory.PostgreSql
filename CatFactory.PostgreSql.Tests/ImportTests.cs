using System.Threading.Tasks;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class ImportTests
    {
        private readonly string ConnectionString;

        public ImportTests()
        {
            ConnectionString = "Server=localhost; Port=5432; Database=dvdrental; UserId=postgres; Password=Pass123$;";
        }

        [Fact]
        public async Task DefaultImportDvdRentalDatabaseAsync()
        {
            var database = await PostgreSqlDatabaseFactory.ImportAsync(ConnectionString);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count == 0);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 0);
        }

        [Fact]
        public void DefaultImportDvdRentalDatabase()
        {
            var database = PostgreSqlDatabaseFactory.Import(ConnectionString);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 0);
        }

        [Fact]
        public async Task ImportDvdRentalDatabaseWithViewsAsync()
        {
            var database = await PostgreSqlDatabaseFactory.ImportAsync(ConnectionString, importViews: true);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count > 0);

            Assert.True(database.FindView("public.film_list").Columns.Count == 8);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 0);
        }

        [Fact]
        public async Task ImportDvdRentalDatabaseWithSequencesAsync()
        {
            var database = await PostgreSqlDatabaseFactory.ImportAsync(ConnectionString, importSequences: true);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 13);
        }
    }
}
