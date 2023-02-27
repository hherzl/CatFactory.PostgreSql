namespace CatFactory.PostgreSql.DatabaseObjectModel
{
    public class KeyColumnUsage
    {
        public string ConstraintCatalog { get; set; }
        public string ConstraintSchema { get; set; }
        public string ConstraintName { get; set; }
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int OrdinalPosition { get; set; }
        public int PositionInUniqueConstraint { get; set; }
    }
}
