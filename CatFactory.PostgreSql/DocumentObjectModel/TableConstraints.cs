namespace CatFactory.PostgreSql.DocumentObjectModel
{
    public class TableConstraints
    {
        public string ConstraintCatalog { get; set; }

        public string ConstraintSchema { get; set; }

        public string ConstraintName { get; set; }

        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ConstraintType { get; set; }

        public string IsDeferrable { get; set; }

        public string InitiallyDeferred { get; set; }
    }
}
