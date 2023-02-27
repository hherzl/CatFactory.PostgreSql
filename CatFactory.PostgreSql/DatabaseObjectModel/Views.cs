namespace CatFactory.PostgreSql.DatabaseObjectModel
{
    public class Views
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ViewDefinition { get; set; }
        public string CheckOption { get; set; }
        public string IsUpdatable { get; set; }
        public string IsInsertableInto { get; set; }
        public string IsTriggerUpdatable { get; set; }
        public string IsTriggerDeletable { get; set; }
        public string IsTriggerInsertableInto { get; set; }
    }
}
