namespace CatFactory.PostgreSql.DatabaseObjectModel
{
    public class Sequences
    {
        public string SequenceCatalog { get; set; }

        public string SequenceSchema { get; set; }

        public string SequenceName { get; set; }

        public string DataType { get; set; }

        public int NumericPrecision { get; set; }

        public int NumericPrecisionRadix { get; set; }

        public int NumericScale { get; set; }

        public string StartValue { get; set; }

        public string MinimumValue { get; set; }

        public string MaximumValue { get; set; }

        public string Increment { get; set; }

        public string CycleOption { get; set; }
    }
}
