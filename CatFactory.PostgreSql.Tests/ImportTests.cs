using System.Threading.Tasks;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class ImportTests
    {
        [Fact]
        public async Task ImportDvdRentalDatabaseAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);
        }

        [Fact]
        public void ImportDvdRentalDatabase()
        {
            var database = PostgreSqlDatabaseFactory
                .Import("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);
        }
    }
}
