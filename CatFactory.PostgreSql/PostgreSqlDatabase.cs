using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.ObjectRelationalMapping;
using CatFactory.ObjectRelationalMapping.Programmability;

namespace CatFactory.PostgreSql
{
    public class PostgreSqlDatabase : Database, IPostgreSqlDatabase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<Sequence> m_sequences;

        public PostgreSqlDatabase()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the sequences
        /// </summary>
        public List<Sequence> Sequences
        {
            get => m_sequences ??= new List<Sequence>();
            set => m_sequences = value;
        }
    }
}
