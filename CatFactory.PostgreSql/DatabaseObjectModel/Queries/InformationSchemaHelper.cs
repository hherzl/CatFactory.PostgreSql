using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace CatFactory.PostgreSql.DatabaseObjectModel.Queries
{
    public static class InformationSchemaHelper
    {
        public static async Task<ICollection<Tables>> GetTablesAsync(this DbConnection connection)
        {
            var command = connection.CreateCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  table_catalog, ");
            cmdText.Append("  table_schema, ");
            cmdText.Append("  table_name, ");
            cmdText.Append("  table_type, ");
            cmdText.Append("  self_referencing_column_name, ");
            cmdText.Append("  reference_generation, ");
            cmdText.Append("  user_defined_type_catalog, ");
            cmdText.Append("  user_defined_type_schema, ");
            cmdText.Append("  user_defined_type_name, ");
            cmdText.Append("  is_insertable_into, ");
            cmdText.Append("  is_typed, ");
            cmdText.Append("  commit_action ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.tables ");
            cmdText.Append(" ; ");

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            var collection = new Collection<Tables>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new Tables
                {
                    TableCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    TableSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    TableName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    TableType = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    SelfReferencingColumnName = reader[4] is DBNull ? string.Empty : reader.GetString(4),
                    ReferenceGeneration = reader[5] is DBNull ? string.Empty : reader.GetString(5),
                    UserDefinedTypeCatalog = reader[6] is DBNull ? string.Empty : reader.GetString(6),
                    UserDefinedTypeSchema = reader[7] is DBNull ? string.Empty : reader.GetString(7),
                    UserDefinedTypeName = reader[8] is DBNull ? string.Empty : reader.GetString(8),
                    IsInsertableInto = reader[9] is DBNull ? string.Empty : reader.GetString(9),
                    IsTyped = reader[10] is DBNull ? string.Empty : reader.GetString(10),
                    CommitAction = reader[11] is DBNull ? string.Empty : reader.GetString(11)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<Tables> GetTables(this DbConnection connection)
            => connection.GetTablesAsync().GetAwaiter().GetResult();

        public static async Task<ICollection<TableConstraints>> GetTableConstraintsAsync(this DbConnection connection, string tableSchema = null, string tableName = null)
        {
            using var command = new NpgsqlCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  constraint_catalog, ");
            cmdText.Append("  constraint_schema, ");
            cmdText.Append("  constraint_name, ");
            cmdText.Append("  table_catalog, ");
            cmdText.Append("  table_schema, ");
            cmdText.Append("  table_name, ");
            cmdText.Append("  constraint_type, ");
            cmdText.Append("  is_deferrable, ");
            cmdText.Append("  initially_deferred ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.table_constraints ");
            cmdText.Append(" WHERE ");
            cmdText.Append("  (@tableSchema IS null OR table_schema = @tableSchema) ");
            cmdText.Append("  AND (@tableName IS null OR table_name = @tableName) ");
            cmdText.Append(" ; ");

            command.Connection = (NpgsqlConnection)connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            command.Parameters.AddWithValue("@tableSchema", NpgsqlDbType.Varchar, (object)tableSchema ?? DBNull.Value);
            command.Parameters.AddWithValue("@tableName", NpgsqlDbType.Varchar, (object)tableName ?? DBNull.Value);

            var collection = new Collection<TableConstraints>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new TableConstraints
                {
                    ConstraintCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    ConstraintSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    ConstraintName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    TableCatalog = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    TableSchema = reader[4] is DBNull ? string.Empty : reader.GetString(4),
                    TableName = reader[5] is DBNull ? string.Empty : reader.GetString(5),
                    ConstraintType = reader[6] is DBNull ? string.Empty : reader.GetString(6),
                    IsDeferrable = reader[7] is DBNull ? string.Empty : reader.GetString(7),
                    InitiallyDeferred = reader[8] is DBNull ? string.Empty : reader.GetString(8)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<TableConstraints> GetTableConstraints(this DbConnection connection)
            => connection.GetTableConstraintsAsync().GetAwaiter().GetResult();

        public static async Task<ICollection<Views>> GetViewsAsync(this DbConnection connection)
        {
            using var command = connection.CreateCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  table_catalog, ");
            cmdText.Append("  table_schema, ");
            cmdText.Append("  table_name, ");
            cmdText.Append("  view_definition, ");
            cmdText.Append("  check_option, ");
            cmdText.Append("  is_updatable, ");
            cmdText.Append("  is_insertable_into, ");
            cmdText.Append("  is_trigger_updatable, ");
            cmdText.Append("  is_trigger_deletable, ");
            cmdText.Append("  is_trigger_insertable_into ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.views ");
            cmdText.Append(" ; ");

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            var collection = new Collection<Views>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new Views
                {
                    TableCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    TableSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    TableName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    ViewDefinition = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    CheckOption = reader[4] is DBNull ? string.Empty : reader.GetString(4),
                    IsUpdatable = reader[5] is DBNull ? string.Empty : reader.GetString(5),
                    IsInsertableInto = reader[6] is DBNull ? string.Empty : reader.GetString(6),
                    IsTriggerUpdatable = reader[7] is DBNull ? string.Empty : reader.GetString(7),
                    IsTriggerDeletable = reader[8] is DBNull ? string.Empty : reader.GetString(8),
                    IsTriggerInsertableInto = reader[9] is DBNull ? string.Empty : reader.GetString(9)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<Views> GetViews(this DbConnection connection)
            => connection.GetViewsAsync().GetAwaiter().GetResult();

        public static async Task<ICollection<Columns>> GetColumnsAsync(this DbConnection connection, string tableSchema = null, string tableName = null)
        {
            using var command = new NpgsqlCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  table_catalog, ");
            cmdText.Append("  table_schema, ");
            cmdText.Append("  table_name, ");
            cmdText.Append("  column_name, ");
            cmdText.Append("  ordinal_position, ");
            cmdText.Append("  column_default, ");
            cmdText.Append("  is_nullable, ");
            cmdText.Append("  data_type, ");
            cmdText.Append("  character_maximum_length, ");
            cmdText.Append("  character_octet_length, ");
            cmdText.Append("  numeric_precision, ");
            cmdText.Append("  numeric_precision_radix, ");
            cmdText.Append("  numeric_scale, ");
            cmdText.Append("  datetime_precision, ");
            cmdText.Append("  interval_type, ");
            cmdText.Append("  interval_precision, ");
            cmdText.Append("  character_set_catalog, ");
            cmdText.Append("  character_set_schema, ");
            cmdText.Append("  character_set_name, ");
            cmdText.Append("  collation_catalog, ");
            cmdText.Append("  collation_schema, ");
            cmdText.Append("  collation_name, ");
            cmdText.Append("  domain_catalog, ");
            cmdText.Append("  domain_schema, ");
            cmdText.Append("  domain_name, ");
            cmdText.Append("  udt_catalog, ");
            cmdText.Append("  udt_schema, ");
            cmdText.Append("  udt_name, ");
            cmdText.Append("  scope_catalog, ");
            cmdText.Append("  scope_schema, ");
            cmdText.Append("  scope_name, ");
            cmdText.Append("  maximum_cardinality, ");
            cmdText.Append("  dtd_identifier, ");
            cmdText.Append("  is_self_referencing, ");
            cmdText.Append("  is_identity, ");
            cmdText.Append("  identity_generation, ");
            cmdText.Append("  identity_start, ");
            cmdText.Append("  identity_increment, ");
            cmdText.Append("  identity_maximum, ");
            cmdText.Append("  identity_minimum, ");
            cmdText.Append("  identity_cycle, ");
            cmdText.Append("  is_generated, ");
            cmdText.Append("  generation_expression, ");
            cmdText.Append("  is_updatable ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.columns ");
            cmdText.Append(" WHERE ");
            cmdText.Append("  (@tableSchema IS null OR table_schema = @tableSchema) ");
            cmdText.Append("  AND (@tableName IS null OR table_name = @tableName) ");
            cmdText.Append(" ; ");

            command.Connection = (NpgsqlConnection)connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            command.Parameters.AddWithValue("@tableSchema", NpgsqlDbType.Varchar, (object)tableSchema ?? DBNull.Value);
            command.Parameters.AddWithValue("@tableName", NpgsqlDbType.Varchar, (object)tableName ?? DBNull.Value);

            var collection = new Collection<Columns>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new Columns
                {
                    TableCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    TableSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    TableName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    ColumnName = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    OrdinalPosition = reader[4] is DBNull ? 0 : reader.GetInt32(4),
                    ColumnDefault = reader[5] is DBNull ? string.Empty : reader.GetString(5),
                    IsNullable = reader[6] is DBNull ? string.Empty : reader.GetString(6),
                    DataType = reader[7] is DBNull ? string.Empty : reader.GetString(7),
                    CharacterMaximumLength = reader[8] is DBNull ? 0 : reader.GetInt32(8),
                    CharacterOctetLength = reader[9] is DBNull ? 0 : reader.GetInt32(9),
                    NumericPrecision = reader[10] is DBNull ? 0 : reader.GetInt32(10),
                    NumericPrecisionRadix = reader[11] is DBNull ? 0 : reader.GetInt32(11),
                    NumericScale = reader[12] is DBNull ? 0 : reader.GetInt32(12),
                    DatetimePrecision = reader[13] is DBNull ? 0 : reader.GetInt32(13),
                    IntervalType = reader[14] is DBNull ? string.Empty : reader.GetString(14),
                    IntervalPrecision = reader[15] is DBNull ? string.Empty : reader.GetString(15),
                    CharacterSetCatalog = reader[16] is DBNull ? string.Empty : reader.GetString(16),
                    CharacterSetSchema = reader[17] is DBNull ? string.Empty : reader.GetString(17),
                    CharacterSetName = reader[18] is DBNull ? string.Empty : reader.GetString(18),
                    CollationCatalog = reader[19] is DBNull ? string.Empty : reader.GetString(19),
                    CollationSchema = reader[20] is DBNull ? string.Empty : reader.GetString(20),
                    CollationName = reader[21] is DBNull ? string.Empty : reader.GetString(21),
                    DomainCatalog = reader[22] is DBNull ? string.Empty : reader.GetString(22),
                    DomainSchema = reader[23] is DBNull ? string.Empty : reader.GetString(23),
                    DomainName = reader[24] is DBNull ? string.Empty : reader.GetString(24),
                    UdtCatalog = reader[25] is DBNull ? string.Empty : reader.GetString(25),
                    UdtSchema = reader[26] is DBNull ? string.Empty : reader.GetString(26),
                    UdtName = reader[27] is DBNull ? string.Empty : reader.GetString(27),
                    ScopeCatalog = reader[28] is DBNull ? string.Empty : reader.GetString(28),
                    ScopeSchema = reader[29] is DBNull ? string.Empty : reader.GetString(29),
                    ScopeName = reader[30] is DBNull ? string.Empty : reader.GetString(30),
                    MaximumCardinality = reader[31] is DBNull ? 0 : reader.GetInt32(31),
                    DtdIdentifier = reader[32] is DBNull ? string.Empty : reader.GetString(32),
                    IsSelfReferencing = reader[33] is DBNull ? string.Empty : reader.GetString(33),
                    IsIdentity = reader[34] is DBNull ? string.Empty : reader.GetString(34),
                    IdentityGeneration = reader[35] is DBNull ? string.Empty : reader.GetString(35),
                    IdentityStart = reader[36] is DBNull ? string.Empty : reader.GetString(36),
                    IdentityIncrement = reader[37] is DBNull ? string.Empty : reader.GetString(37),
                    IdentityMaximum = reader[38] is DBNull ? string.Empty : reader.GetString(38),
                    IdentityMinimum = reader[39] is DBNull ? string.Empty : reader.GetString(39),
                    IdentityCycle = reader[40] is DBNull ? string.Empty : reader.GetString(40),
                    IsGenerated = reader[41] is DBNull ? string.Empty : reader.GetString(41),
                    GenerationExpression = reader[42] is DBNull ? string.Empty : reader.GetString(42),
                    IsUpdatable = reader[43] is DBNull ? string.Empty : reader.GetString(43)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<Columns> GetColumns(this DbConnection connection)
            => connection.GetColumnsAsync().GetAwaiter().GetResult();

        public static async Task<ICollection<KeyColumnUsage>> GetKeyColumnUsagesAsync(this DbConnection connection, string tableSchema = null, string tableName = null)
        {
            using var command = new NpgsqlCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  constraint_catalog, ");
            cmdText.Append("  constraint_schema, ");
            cmdText.Append("  constraint_name, ");
            cmdText.Append("  table_catalog, ");
            cmdText.Append("  table_schema, ");
            cmdText.Append("  table_name, ");
            cmdText.Append("  column_name, ");
            cmdText.Append("  ordinal_position, ");
            cmdText.Append("  position_in_unique_constraint ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.key_column_usage ");
            cmdText.Append(" WHERE ");
            cmdText.Append("  (@tableSchema IS null OR table_schema = @tableSchema) ");
            cmdText.Append("  AND (@tableName IS null OR table_name = @tableName) ");
            cmdText.Append(" ; ");

            command.Connection = (NpgsqlConnection)connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            command.Parameters.AddWithValue("@tableSchema", NpgsqlDbType.Varchar, (object)tableSchema ?? DBNull.Value);
            command.Parameters.AddWithValue("@tableName", NpgsqlDbType.Varchar, (object)tableName ?? DBNull.Value);

            var collection = new Collection<KeyColumnUsage>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new KeyColumnUsage
                {
                    ConstraintCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    ConstraintSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    ConstraintName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    TableCatalog = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    TableSchema = reader[4] is DBNull ? string.Empty : reader.GetString(4),
                    TableName = reader[5] is DBNull ? string.Empty : reader.GetString(5),
                    ColumnName = reader[6] is DBNull ? string.Empty : reader.GetString(6),
                    OrdinalPosition = reader[7] is DBNull ? 0 : reader.GetInt32(7),
                    PositionInUniqueConstraint = reader[8] is DBNull ? 0 : reader.GetInt32(8)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<KeyColumnUsage> GetKeyColumnUsages(this DbConnection connection)
            => connection.GetKeyColumnUsagesAsync().GetAwaiter().GetResult();

        public static async Task<ICollection<Sequences>> GetSequencesAsync(this DbConnection connection, string sequenceSchema = null, string sequenceName = null)
        {
            using var command = new NpgsqlCommand();

            var cmdText = new StringBuilder();

            cmdText.Append(" SELECT ");
            cmdText.Append("  sequence_catalog, ");
            cmdText.Append("  sequence_schema, ");
            cmdText.Append("  sequence_name, ");
            cmdText.Append("  data_type, ");
            cmdText.Append("  numeric_precision, ");
            cmdText.Append("  numeric_precision_radix, ");
            cmdText.Append("  numeric_scale, ");
            cmdText.Append("  start_value, ");
            cmdText.Append("  minimum_value, ");
            cmdText.Append("  maximum_value, ");
            cmdText.Append("  increment, ");
            cmdText.Append("  cycle_option ");
            cmdText.Append(" FROM ");
            cmdText.Append("  information_schema.sequences ");
            cmdText.Append(" WHERE ");
            cmdText.Append("  (@sequenceSchema IS null OR sequence_schema = @sequenceSchema) ");
            cmdText.Append("  AND (@sequenceName IS null OR sequence_name = @sequenceName) ");
            cmdText.Append(" ; ");

            command.Connection = (NpgsqlConnection)connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            command.Parameters.AddWithValue("@sequenceSchema", NpgsqlDbType.Varchar, (object)sequenceSchema ?? DBNull.Value);
            command.Parameters.AddWithValue("@sequenceName", NpgsqlDbType.Varchar, (object)sequenceName ?? DBNull.Value);

            var collection = new Collection<Sequences>();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                collection.Add(new Sequences
                {
                    SequenceCatalog = reader[0] is DBNull ? string.Empty : reader.GetString(0),
                    SequenceSchema = reader[1] is DBNull ? string.Empty : reader.GetString(1),
                    SequenceName = reader[2] is DBNull ? string.Empty : reader.GetString(2),
                    DataType = reader[3] is DBNull ? string.Empty : reader.GetString(3),
                    NumericPrecision = reader[4] is DBNull ? 0 : reader.GetInt32(4),
                    NumericPrecisionRadix = reader[5] is DBNull ? 0 : reader.GetInt32(5),
                    NumericScale = reader[6] is DBNull ? 0 : reader.GetInt32(6),
                    StartValue = reader[7] is DBNull ? string.Empty : reader.GetString(7),
                    MinimumValue = reader[8] is DBNull ? string.Empty : reader.GetString(8),
                    MaximumValue = reader[9] is DBNull ? string.Empty : reader.GetString(9),
                    Increment = reader[10] is DBNull ? string.Empty : reader.GetString(10),
                    CycleOption = reader[11] is DBNull ? string.Empty : reader.GetString(11)
                });
            }

            reader.Close();

            return collection;
        }

        public static ICollection<Sequences> GetSequences(this DbConnection connection)
            => connection.GetSequencesAsync().GetAwaiter().GetResult();

        public static async Task<object> GetCurrValAsync(this DbConnection connection, string sequenceSchema, string sequenceName)
        {
            using var command = new NpgsqlCommand();

            var cmdText = new StringBuilder();

            cmdText.AppendFormat(" SELECT last_value FROM {0}.{1}; ", sequenceSchema, sequenceName);

            command.Connection = (NpgsqlConnection)connection;
            command.CommandType = CommandType.Text;
            command.CommandText = cmdText.ToString();

            return await command.ExecuteScalarAsync();
        }
    }
}
