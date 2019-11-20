using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using CatFactory.ObjectRelationalMapping.Programmability;
using CatFactory.PostgreSql.DocumentObjectModel.Queries;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CatFactory.PostgreSql
{
    public class PostgreSqlDatabaseFactory : IDatabaseFactory
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatabaseImportSettings m_databaseImportSettings;

        protected readonly ILogger<PostgreSqlDatabaseFactory> Logger;

        public PostgreSqlDatabaseFactory()
        {
        }

        public PostgreSqlDatabaseFactory(ILogger<PostgreSqlDatabaseFactory> logger)
        {
            Logger = logger;
        }

        public DatabaseImportSettings DatabaseImportSettings
        {
            get => m_databaseImportSettings ?? (m_databaseImportSettings = new DatabaseImportSettings());
            set => m_databaseImportSettings = value;
        }

        public DbConnection GetConnection()
            => new NpgsqlConnection(DatabaseImportSettings.ConnectionString);

        public IEnumerable<DatabaseTypeMap> DatabaseTypeMaps
            => PostgreSqlDatabaseTypeMaps.DatabaseTypeMaps;

        public virtual async Task<Database> ImportAsync()
        {
            using (var connection = GetConnection())
            {
                var database = new PostgreSqlDatabase
                {
                    DataSource = connection.DataSource,
                    Name = connection.DataSource,
                    DefaultSchema = "public",
                    SupportTransactions = true,
                    DatabaseTypeMaps = PostgreSqlDatabaseTypeMaps.DatabaseTypeMaps.ToList(),
                    NamingConvention = new PostgreSqlDatabaseNamingConvention()
                };

                await connection.OpenAsync();

                // todo: add user defined datatypes

                Logger?.LogInformation("Importing Db Objects for '{0}'...", database.Name);

                foreach (var dbObject in await GetDbObjectsAsync(connection))
                {
                    if (DatabaseImportSettings.Exclusions.Contains(dbObject.FullName))
                        continue;

                    database.DbObjects.Add(dbObject);
                }

                if (DatabaseImportSettings.ImportTables)
                {
                    Logger?.LogInformation("Importing tables for '{0}'...", database.Name);

                    foreach (var table in await GetTablesAsync(connection, database))
                    {
                        if (DatabaseImportSettings.Exclusions.Contains(table.FullName))
                            continue;

                        // todo: Set primary key for table
                        // reference: http://technosophos.com/2015/10/26/querying-postgresql-to-find-the-primary-key-of-a-table.html
                        table.PrimaryKey = await GetPrimaryKeyAsync(connection, table);

                        database.Tables.Add(table);
                    }
                }

                if (DatabaseImportSettings.ImportViews)
                {
                    Logger?.LogInformation("Importing views for '{0}'...", database.Name);

                    foreach (var view in await GetViewsAsync(connection, database))
                    {
                        if (DatabaseImportSettings.Exclusions.Contains(view.FullName))
                            continue;

                        database.Views.Add(view);
                    }
                }

                if (DatabaseImportSettings.ImportSequences)
                {
                    Logger?.LogInformation("Importing sequences for '{0}'...", database.Name);

                    foreach (var sequence in await GetSequencesAsync(connection, database))
                    {
                        if (DatabaseImportSettings.Exclusions.Contains(sequence.FullName))
                            continue;

                        database.Sequences.Add(sequence);
                    }
                }

                connection.Close();

                connection.Dispose();

                return database;
            }
        }

        public Database Import()
            => ImportAsync().GetAwaiter().GetResult();

        protected virtual async Task<ICollection<DbObject>> GetDbObjectsAsync(DbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = DatabaseImportSettings.ImportCommandText;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var collection = new Collection<DbObject>();

                    while (await reader.ReadAsync())
                    {
                        collection.Add(new DbObject
                        {
                            DataSource = connection.DataSource,
                            DatabaseName = connection.Database,
                            Schema = reader.GetString(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2)
                        });
                    }

                    reader.Close();

                    return collection;
                }
            }
        }

        protected virtual async Task<ICollection<Table>> GetTablesAsync(DbConnection connection, Database database)
        {
            var collection = new Collection<Table>();

            foreach (var dbObject in database.GetTables())
            {
                var table = new Table
                {
                    DataSource = connection.DataSource,
                    DatabaseName = connection.Database,
                    Schema = dbObject.Schema,
                    Name = dbObject.Name
                };

                var columns = await connection.GetColumnsAsync(table.Schema, table.Name);

                foreach (var postgreColumn in columns)
                {
                    var column = new Column
                    {
                        Name = postgreColumn.ColumnName,
                        Type = postgreColumn.DataType,
                        Nullable = string.Compare(postgreColumn.IsNullable, "YES", true) == 0 ? true : false,
                        Scale = Convert.ToInt16(postgreColumn.NumericScale),
                        DefaultValue = postgreColumn.ColumnDefault
                    };

                    if (column.Type.Contains("char"))
                        column.Length = Convert.ToInt32(postgreColumn.CharacterMaximumLength);
                    else if (column.Type.Contains("numeric"))
                        column.Prec = Convert.ToInt16(postgreColumn.NumericPrecision);

                    table.Columns.Add(column);
                }

                collection.Add(table);
            }

            return collection;
        }

        protected async virtual Task<PrimaryKey> GetPrimaryKeyAsync(DbConnection connection, ITable table)
        {
            var keyColumnUsages = await connection.GetKeyColumnUsagesAsync(table.Schema, table.Name);

            var tableConstraints = await connection.GetTableConstraintsAsync(table.Schema, table.Name);

            var query = from
                            keyColumnUsage in keyColumnUsages
                        join
                            tableConstraint in tableConstraints
                                on keyColumnUsage.ConstraintName equals tableConstraint.ConstraintName into constraintsTemp
                        from
                            constraint in constraintsTemp.DefaultIfEmpty()
                        where
                            constraint.TableName == table.Name && constraint.ConstraintType == "PRIMARY KEY"
                        orderby
                            keyColumnUsage.OrdinalPosition
                        select new
                        {
                            keyColumnUsage.ConstraintName,
                            keyColumnUsage.ColumnName,
                            keyColumnUsage.OrdinalPosition
                        };

            var list = query.ToList();

            return list.Count == 0 ? null : new PrimaryKey(list.First().ConstraintName, list.Select(item => item.ColumnName).ToArray());
        }

        protected virtual async Task<ICollection<View>> GetViewsAsync(DbConnection connection, Database database)
        {
            var collection = new Collection<View>();

            foreach (var dbObject in database.GetViews())
            {
                var view = new View
                {
                    DataSource = connection.DataSource,
                    DatabaseName = connection.Database,
                    Schema = dbObject.Schema,
                    Name = dbObject.Name
                };

                var columns = await connection.GetColumnsAsync(view.Schema, view.Name);

                foreach (var postgreColumn in columns)
                {
                    var column = new Column
                    {
                        Name = postgreColumn.ColumnName,
                        Type = postgreColumn.DataType,
                        Nullable = string.Compare(postgreColumn.IsNullable, "YES", true) == 0 ? true : false,
                        Scale = Convert.ToInt16(postgreColumn.NumericScale),
                        DefaultValue = postgreColumn.ColumnDefault
                    };

                    if (column.Type.Contains("char"))
                        column.Length = Convert.ToInt32(postgreColumn.CharacterMaximumLength);
                    else if (column.Type.Contains("numeric"))
                        column.Prec = Convert.ToInt16(postgreColumn.NumericPrecision);

                    view.Columns.Add(column);
                }

                collection.Add(view);
            }

            return collection;
        }

        protected virtual async Task<ICollection<Sequence>> GetSequencesAsync(DbConnection connection, Database database)
        {
            var collection = new Collection<Sequence>();

            foreach (var dbObject in database.GetSequences())
            {
                var sequence = new Sequence
                {
                    DataSource = connection.DataSource,
                    DatabaseName = connection.Database,
                    Schema = dbObject.Schema,
                    Name = dbObject.Name
                };

                var postgreSequence = await connection.GetSequencesAsync(sequence.Schema, sequence.Name);

                collection.Add(sequence);
            }

            return collection;
        }
    }
}
