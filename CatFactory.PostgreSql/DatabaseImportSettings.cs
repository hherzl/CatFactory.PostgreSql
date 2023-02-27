using System.Collections.Generic;
using System.Diagnostics;

namespace CatFactory.PostgreSql
{
    public class DatabaseImportSettings
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<string> m_exclusions;

        public DatabaseImportSettings()
        {
            ImportCommandText = @"
                SELECT
                    table_schema AS schema_name, table_name AS object_name, 'table' AS object_type
                FROM
                    information_schema.tables
                WHERE
                    table_schema = 'public'
            UNION
                SELECT
                    table_schema AS schema_name, table_name AS object_name, 'view' AS object_type
                FROM
                    information_schema.views
                WHERE
                    table_schema = 'public'
            UNION
                SELECT
                    sequence_schema AS schema_name, sequence_name AS object_name, 'sequence' AS object_type
                FROM
                    information_schema.sequences
                WHERE
                    sequence_schema = 'public'
            ;";

            ImportTables = true;
        }

        public string ConnectionString { get; set; }

        public string ImportCommandText { get; set; }

        public bool ImportTables { get; set; }

        public bool ImportViews { get; set; }

        public bool ImportSequences { get; set; }

        public List<string> Exclusions
        {
            get => m_exclusions ??= new List<string>();
            set => m_exclusions = value;
        }
    }
}
