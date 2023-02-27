namespace CatFactory.PostgreSql.DatabaseObjectModel
{
    public class Tables
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string SelfReferencingColumnName { get; set; }
        public string ReferenceGeneration { get; set; }
        public string UserDefinedTypeCatalog { get; set; }
        public string UserDefinedTypeSchema { get; set; }
        public string UserDefinedTypeName { get; set; }
        public string IsInsertableInto { get; set; }
        public string IsTyped { get; set; }
        public string CommitAction { get; set; }
    }
}
