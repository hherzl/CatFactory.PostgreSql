using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using CatFactory.PostgreSql.DatabaseObjectModel.Queries;
using Npgsql;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class DatabaseObjectModelTests
    {
        private readonly string ConnectionString;

        public DatabaseObjectModelTests()
        {
            ConnectionString = "Server=localhost; Port=5432; Database=dvdrental; UserId=postgres; Password=Pass123$;";
        }

        [Fact]
        public async Task GetTablesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var tables = await connection.GetTablesAsync();

            Assert.True(tables.Count > 0);

            connection.Close();
        }

        [Fact]
        public void GetTables()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var tables = connection.GetTables();

            Assert.True(tables.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetViewsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var views = await connection.GetViewsAsync();

            Assert.True(views.Count > 0);

            connection.Close();
        }

        [Fact]
        public void GetViews()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var views = connection.GetViews();

            Assert.True(views.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetTableConstraintsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var tableConstraints = await connection.GetTableConstraintsAsync();

            Assert.True(tableConstraints.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetTableConstraintsForTableAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var tableConstraints = await connection.GetTableConstraintsAsync(tableSchema: "public", tableName: "country");

            Assert.True(tableConstraints.Count == 4);
            Assert.True(tableConstraints.Count(item => item.ConstraintType == "PRIMARY KEY") == 1);
            Assert.True(tableConstraints.Count(item => item.ConstraintType == "CHECK") == 3);

            connection.Close();
        }

        [Fact]
        public void GetTableConstraints()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var tableConstraints = connection.GetTableConstraints();

            Assert.True(tableConstraints.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetColumnsAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var columns = await connection.GetColumnsAsync();

            Assert.True(columns.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetColumnsForTableAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var columns = await connection.GetColumnsAsync(tableSchema: "public", tableName: "inventory");

            Assert.True(columns.Count == 4);

            connection.Close();
        }

        [Fact]
        public async Task GetColumnsForViewAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var columns = await connection.GetColumnsAsync(tableSchema: "public", tableName: "film_list");

            Assert.True(columns.Count == 8);

            connection.Close();
        }

        [Fact]
        public void GetColumns()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var columns = connection.GetColumns();

            Assert.True(columns.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetKeyColumnUsagesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var keyColumnUsages = await connection.GetKeyColumnUsagesAsync();

            Assert.True(keyColumnUsages.Count > 0);

            connection.Close();
        }

        [Fact]
        public void GetKeyColumnUsages()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var keyColumnUsages = connection.GetKeyColumnUsages();

            Assert.True(keyColumnUsages.Count > 0);

            connection.Close();
        }

        [Fact]
        public async Task GetSequencesAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            var sequences = await connection.GetSequencesAsync();

            Assert.True(sequences.Count > 0);

            connection.Close();
        }

        [Fact]
        public void GetSequences()
        {
            var connection = new NpgsqlConnection(ConnectionString);

            connection.Open();

            var sequences = connection.GetSequences();

            Assert.True(sequences.Count > 0);

            connection.Close();
        }
    }
}
