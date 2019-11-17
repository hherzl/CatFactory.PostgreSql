using System.Threading.Tasks;
using CatFactory.PostgreSql.DocumentObjectModel.Queries;
using Npgsql;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class DocumentObjectModelTests
    {
        private readonly string ConnectionString;

        public DocumentObjectModelTests()
        {
            ConnectionString = "Server=localhost; Port=5432; Database=dvdrental; UserId=postgres; Password=Pass123$;";
        }

        [Fact]
        public async Task TestGetTablesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var tables = await connection.GetTablesAsync();

            Assert.True(tables.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetTables()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var tables = connection.GetTables();

            Assert.True(tables.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task TestGetViewsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var views = await connection.GetViewsAsync();

            Assert.True(views.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetViews()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var views = connection.GetViews();

            Assert.True(views.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task TestGetTableConstraintsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var tableConstraints = await connection.GetTableConstraintsAsync();

            Assert.True(tableConstraints.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetTableConstraints()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var tableConstraints = connection.GetTableConstraints();

            Assert.True(tableConstraints.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task TestGetColumnsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var columns = await connection.GetColumnsAsync();

            Assert.True(columns.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetColumns()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var columns = connection.GetColumns();

            Assert.True(columns.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task TestGetKeyColumnUsagesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var keyColumnUsages = await connection.GetKeyColumnUsagesAsync();

            Assert.True(keyColumnUsages.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetKeyColumnUsages()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var keyColumnUsages = connection.GetKeyColumnUsages();

            Assert.True(keyColumnUsages.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task TestGetSequencesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var sequences = await connection.GetSequencesAsync();

            Assert.True(sequences.Count > 0);

            connection.Close();
        }

        [Fact]
        public void TestGetSequences()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var sequences = connection.GetSequences();

            Assert.True(sequences.Count > 0);

            connection.Close();
        }
    }
}
