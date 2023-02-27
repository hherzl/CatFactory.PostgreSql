namespace CatFactory.PostgreSql.DatabaseObjectModel
{
    public class Columns
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int OrdinalPosition { get; set; }
        public string ColumnDefault { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public int CharacterMaximumLength { get; set; }
        public int CharacterOctetLength { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericPrecisionRadix { get; set; }
        public int NumericScale { get; set; }
        public int DatetimePrecision { get; set; }
        public string IntervalType { get; set; }
        public string IntervalPrecision { get; set; }
        public string CharacterSetCatalog { get; set; }
        public string CharacterSetSchema { get; set; }
        public string CharacterSetName { get; set; }
        public string CollationCatalog { get; set; }
        public string CollationSchema { get; set; }
        public string CollationName { get; set; }
        public string DomainCatalog { get; set; }
        public string DomainSchema { get; set; }
        public string DomainName { get; set; }
        public string UdtCatalog { get; set; }
        public string UdtSchema { get; set; }
        public string UdtName { get; set; }
        public string ScopeCatalog { get; set; }
        public string ScopeSchema { get; set; }
        public string ScopeName { get; set; }
        public int MaximumCardinality { get; set; }
        public string DtdIdentifier { get; set; }
        public string IsSelfReferencing { get; set; }
        public string IsIdentity { get; set; }
        public string IdentityGeneration { get; set; }
        public string IdentityStart { get; set; }
        public string IdentityIncrement { get; set; }
        public string IdentityMaximum { get; set; }
        public string IdentityMinimum { get; set; }
        public string IdentityCycle { get; set; }
        public string IsGenerated { get; set; }
        public string GenerationExpression { get; set; }
        public string IsUpdatable { get; set; }
    }
}
