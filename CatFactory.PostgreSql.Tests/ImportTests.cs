﻿using System.Threading.Tasks;
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

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count > 0);

            Assert.True(database.FindView("public.film_list").Columns.Count == 8);
        }

        [Fact]
        public void ImportDvdRentalDatabase()
        {
            var database = PostgreSqlDatabaseFactory
                .Import("Server=localhost; Port=5432; Database=dvdrental; User Id=postgres; Password=Pass123$;");

            Assert.True(database.Tables.Count > 0);

            Assert.True(database.FindTable("public.film").PrimaryKey != null);
            Assert.True(database.FindTable("public.film").Columns.Count == 13);

            Assert.True(database.Views.Count > 0);

            Assert.True(database.FindView("public.film_list").Columns.Count == 8);
        }
    }
}
