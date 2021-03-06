﻿using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CatFactory.PostgreSql.Tests
{
    public class ImportTests
    {
        [Fact]
        public async Task DefaultImportDvdRentalDatabaseAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count == 0);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 0);
        }

        [Fact]
        public void DefaultImportDvdRentalDatabase()
        {
            var database = PostgreSqlDatabaseFactory
                .Import("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 0);
        }

        [Fact]
        public async Task ImportDvdRentalDatabaseWithViewsAsync()
        {
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;", importViews: true);

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
            var database = await PostgreSqlDatabaseFactory
                .ImportAsync("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;", importSequences: true);

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True((database as PostgreSqlDatabase).Sequences.Count == 13);
        }
    }
}
