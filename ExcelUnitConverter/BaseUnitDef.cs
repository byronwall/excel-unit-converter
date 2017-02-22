using SQLite;

namespace ExcelUnitConverter
{
    public class BaseUnitDef
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}