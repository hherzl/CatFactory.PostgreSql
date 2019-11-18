using System.Threading.Tasks;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class ImportTests
    {
        [Fact]
        public async Task TestDefaultImportDvdRentalDatabaseAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);
        }

        [Fact]
        public async Task TestImportDvdRentalDatabaseWithViewsAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;", importViews: true);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count > 0);

            Assert.True(database.FindView("public.film_list").Columns.Count == 8);
        }

        [Fact]
        public async Task TestImportDvdRentalDatabaseWithSequencesAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;", importSequences: true);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Sequences.Count > 0);
        }

        [Fact]
        public void TestImportDvdRentalDatabase()
        {
            var database = PostgreSqlDatabaseFactory
                .Import("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);
        }
    }
}
