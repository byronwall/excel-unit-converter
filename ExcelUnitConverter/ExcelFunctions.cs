using ExcelDna.Integration;
using System;
using System.Diagnostics;

namespace ExcelUnitConverter
{
    public static class ExcelFunctions
    {
        [ExcelFunction("Provides the conversion factor from one unit to another via multiplication")]
        public static double ConvFactor(
            [ExcelArgument(Name = "UnitFrom", Description = "is the base, current units")] string unitFromStr,
            [ExcelArgument(Name = "UnitTo", Description = "is the desired unit, obtained by multiplying the base by the factor")] string unitToStr)
        {
            try
            {
                return UnitConversion.GetConversionFactor(unitFromStr, unitToStr);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }

        [ExcelFunction("Provides the SI unit type")]
        public static string UnitType(
            [ExcelArgument(Name = "Unit", Description = "is the current unit")]string unitFromStr)
        {
            try
            {
                //gets the unit and returns the SI type
                return UnitConversion.CreateUnit(unitFromStr).UnitType;
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }

        [ExcelFunction("Converts a value from a given unit to a new unit")]
        public static double Conv(
            [ExcelArgument(Name = "Value", Description = "is the current value")] double value,
            [ExcelArgument(Name = "UnitFrom", Description = "is the current unit")] string unitFrom,
            [ExcelArgument(Name = "UnitTo", Description = "is the desired unit")] string unitTo)
        {
            try
            {
                return UnitConversion.ConvertValue(ref value, unitFrom, unitTo);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }

        [ExcelFunction("Converts a value from a given unit to the corresponding SI version.  Will return unit as an array formula")]
        public static object ConvToSI(
            [ExcelArgument(Name = "Value", Description = "is the current value")] double value,
            [ExcelArgument(Name = "UnitFrom", Description = "is the current unit")] string unitFrom)
        {
            try
            {
                var unitToSi = UnitConversion.CreateUnit(unitFrom).UnitType;
                var newValue = UnitConversion.ConvertValue(ref value, unitFrom, unitToSi);

                //the object array allows for the output to fill an array formula
                object[] result = new object[] { newValue, unitToSi };

                return result;
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }
    }
}