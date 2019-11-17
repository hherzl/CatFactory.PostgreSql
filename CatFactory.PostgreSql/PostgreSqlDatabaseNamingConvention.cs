using System.Linq;
using CatFactory.ObjectRelationalMapping;

namespace CatFactory.PostgreSql
{
    public class PostgreSqlDatabaseNamingConvention : IDatabaseNamingConvention
    {
        public virtual string GetForeignKeyConstraintName(ITable table, string[] key, ITable references)
            => string.Join("_", "FK", table.Schema, table.Name, string.Join("_", key), references.Schema, references.Name);

        public virtual string GetObjectName(params string[] names)
            => string.Join(".", names.Select(item => string.Format("{0}", item)));

        public virtual string GetParameterName(string name)
            => string.Format("@{0}", NamingConvention.GetCamelCase(name));

        public virtual string GetPrimaryKeyConstraintName(ITable table, string[] key)
           => string.Join("_", "PK", table.Schema, table.Name);

        public virtual string GetUniqueConstraintName(ITable table, string[] key)
            => string.Join("_", "U", table.Schema, table.Name, string.Join("_", key));

        public virtual string ValidName(string name)
            => string.Format("\"{0}\"", name);
    }
}
