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
                select
                    table_schema as schema_name, table_name as object_name, 'table' as object_type
                from
                    information_schema.tables
                where
                    table_schema = 'public'
            union
                select
                    table_schema as schema_name, table_name as object_name, 'view' as object_type
                from
                    information_schema.views
                where
                    table_schema = 'public'
            union
                select
                    sequence_schema as schema_name, sequence_name as object_name, 'sequence' as object_type
                from
                    information_schema.sequences
                where
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
            get => m_exclusions ?? (m_exclusions = new List<string>());
            set => m_exclusions = value;
        }
    }
}
