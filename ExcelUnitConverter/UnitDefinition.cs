using SQLite;

namespace ExcelUnitConverter
{

    public class UnitDefinition
    {
        private string _fromUnit;

        public double factor { get; set; }

        [PrimaryKey]
        public string fromUnit
        {
            get
            {
                return _fromUnit;
            }
            set
            {
                //this will prevent a unit from being defined that shadows an existing unit
                var inList = UnitConversion.baseUnits.Contains(value) || UnitConversion.allUnits.ContainsKey(value);
                var isValid = !inList;

                if (isValid)
                {
                    _fromUnit = value;
                }
            }
        }

        public double offset { get; set; }

        public string toUnit
        {
            get; set;
        }

        public double ConvertBackward(double value)
        {
            return (value - this.offset) / this.factor;
        }

        public double ConvertForward(double value)
        {
            return this.factor * value + this.offset;
        }
    }
}