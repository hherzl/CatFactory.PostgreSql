using System.Collections.Generic;
using CatFactory.ObjectRelationalMapping;
using CatFactory.ObjectRelationalMapping.Programmability;

namespace CatFactory.PostgreSql
{
    public interface IPostgreSqlDatabase : IDatabase
    {
        /// <summary>
        /// Gets or sets the sequences
        /// </summary>
        List<Sequence> Sequences { get; set; }
    }
}
