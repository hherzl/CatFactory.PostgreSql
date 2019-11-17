using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatFactory.ObjectRelationalMapping;
using CatFactory.PostgreSql.DocumentObjectModel.Queries;
using Npgsql;

namespace CatFactory.PostgreSql
{
    public class PostgreSqlDatabaseFactory : IDatabaseFactory
    {
        public static async Task<Database> ImportAsync(string connectionString, IEnumerable<string> exclusions = null)
        {
            var factory = new PostgreSqlDatabaseFactory
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    ConnectionString = connectionString
                }
            };

            if (exclusions != null)
                factory.DatabaseImportSettings.Exclusions.AddRange(exclusions);

            return await factory.ImportAsync();
        }

        public static Database Import(string connectionString, IEnumerable<string> exclusions = null)
        {
            var factory = new PostgreSqlDatabaseFactory
            {
                DatabaseImportSettings = new DatabaseImportSettings
                {
                    ConnectionString = connectionString
                }
            };

            if (exclusions != null)
                factory.DatabaseImportSettings.Exclusions.AddRange(exclusions);

            return factory.Import();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatabaseImportSettings m_databaseImportSettings;

        public PostgreSqlDatabaseFactory()
        {
        }

        public DatabaseImportSettings DatabaseImportSettings
        {
            get => m_databaseImportSettings ?? (m_databaseImportSettings = new DatabaseImportSettings());
            set => m_databaseImportSettings = value;
        }

        public DbConnection GetConnection()
            => new NpgsqlConnection(DatabaseImportSettings.ConnectionString);

        public IEnumerable<DatabaseTypeMap> DatabaseTypeMaps => throw new NotImplementedException();

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

                foreach (var dbObject in await GetDbObjectsAsync(connection))
                {
                    if (DatabaseImportSettings.Exclusions.Contains(dbObject.FullName))
                        continue;

                    database.DbObjects.Add(dbObject);
                }

                foreach (var table in await GetTablesAsync(connection, database))
                {
                    if (DatabaseImportSettings.Exclusions.Contains(table.FullName))
                        continue;

                    database.Tables.Add(table);
                }

                foreach (var table in database.Tables)
                {
                    // todo: Set primary key for table
                    // reference: http://technosophos.com/2015/10/26/querying-postgresql-to-find-the-primary-key-of-a-table.html

                    table.PrimaryKey = await GetPrimaryKeyAsync(connection, table);
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
                            Catalog = connection.Database,
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

        protected virtual IEnumerable<DbObject> GetDbObjects(DbConnection connection)
            => GetDbObjectsAsync(connection).GetAwaiter().GetResult();

        protected virtual async Task<ICollection<Table>> GetTablesAsync(DbConnection connection, Database database)
        {
            var collection = new Collection<Table>();

            foreach (var dbObject in database.GetTables())
            {
                var table = new Table
                {
                    DataSource = connection.DataSource,
                    Catalog = connection.Database,
                    Schema = dbObject.Schema,
                    Name = dbObject.Name
                };

                var documentObjectModel = await connection.GetColumnsAsync(table.Schema, table.Name);

                var postgreColumns = documentObjectModel
                    .Where(item => item.TableSchema == dbObject.Schema && item.TableName == dbObject.Name)
                    .ToList();

                foreach (var postgreColumn in postgreColumns)
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
                    {
                        column.Length = Convert.ToInt32(postgreColumn.CharacterMaximumLength);
                    }
                    else if (column.Type.Contains("numeric"))
                    {
                        column.Prec = Convert.ToInt16(postgreColumn.NumericPrecision);
                    }

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
    }
}
