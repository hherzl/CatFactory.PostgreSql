using System;
using System.Collections.Generic;
using System.Data;
using CatFactory.ObjectRelationalMapping;

namespace CatFactory.PostgreSql
{
    // reference: https://www.npgsql.org/doc/types/basic.html
    public static class PostgreSqlDatabaseTypeMaps
    {
        public static IEnumerable<DatabaseTypeMap> DatabaseTypeMaps
            => new List<DatabaseTypeMap>
            {
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "bool",
                    ClrFullNameType = typeof(bool).FullName,
                    DatabaseType = "boolean",
                    DbTypeEnum = DbType.Boolean
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "short",
                    ClrFullNameType = typeof(short).FullName,
                    DatabaseType = "smallint",
                    DbTypeEnum = DbType.Int16
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "int",
                    ClrFullNameType = typeof(int).FullName,
                    DatabaseType = "integer",
                    DbTypeEnum = DbType.Int32
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "decimal",
                    ClrFullNameType = typeof(decimal).FullName,
                    DatabaseType = "numeric",
                    DbTypeEnum = DbType.Decimal
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "decimal",
                    ClrFullNameType = typeof(decimal).FullName,
                    DatabaseType = "money",
                    DbTypeEnum = DbType.Decimal
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "decimal",
                    ClrFullNameType = typeof(float).FullName,
                    DatabaseType = "real",
                    DbTypeEnum = DbType.Single
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "long",
                    ClrFullNameType = typeof(long).FullName,
                    DatabaseType = "bigint",
                    DbTypeEnum = DbType.Int64
                },
                new DatabaseTypeMap
                {
                    ClrAliasType = "string",
                    ClrFullNameType = typeof(string).FullName,
                    DatabaseType = "text",
                    DbTypeEnum = DbType.String
                },
                new DatabaseTypeMap
                {
                    AllowsLengthInDeclaration = true,
                    ClrAliasType = "string",
                    ClrFullNameType = typeof(string).FullName,
                    DatabaseType = "character varying",
                    DbTypeEnum = DbType.String
                },
                new DatabaseTypeMap
                {
                    AllowsLengthInDeclaration = true,
                    ClrAliasType = "string",
                    ClrFullNameType = typeof(string).FullName,
                    DatabaseType = "character",
                    DbTypeEnum = DbType.String
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrFullNameType = typeof(DateTime).FullName,
                    DatabaseType = "date",
                    DbTypeEnum = DbType.DateTime
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrFullNameType = typeof(Guid).FullName,
                    DatabaseType = "uuid",
                    DbTypeEnum = DbType.DateTime
                },
                new DatabaseTypeMap
                {
                    AllowClrNullable = true,
                    ClrAliasType = "byte[]",
                    ClrFullNameType = typeof(byte[]).FullName,
                    DatabaseType = "bytea",
                    DbTypeEnum = DbType.Binary
                }
            };
    }
}
