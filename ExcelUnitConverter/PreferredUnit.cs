using SQLite;

namespace ExcelUnitConverter
{
    public class PreferredUnit
    {
        [PrimaryKey]
        public string Dimension { get; set; }
        public string Unit { get; set; }

    }
}