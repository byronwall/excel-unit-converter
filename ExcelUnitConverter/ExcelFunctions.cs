/*
 * Created by SharpDevelop.
 * User: bwall
 * Date: 2/14/2017
 * Time: 8:56 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using ExcelDna.Integration;
using SQLite;

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
                //check the cache based on the string inputs
                var key = Tuple.Create(unitFromStr, unitToStr);
                if (UnitConversion._cachedConversionFactors.ContainsKey(key))
                {
                    return UnitConversion._cachedConversionFactors[key];
                }

                UnitConversion unitFrom = UnitConversion.CreateUnit(unitFromStr);
                UnitConversion unitTo = UnitConversion.CreateUnit(unitToStr);

                if (!unitFrom.AreSameUnits(unitTo))
                {
                    throw new Exception(string.Format("not converting same unit types {0} to {1}", unitFromStr, unitToStr));
                }

                var convFactor = unitFrom.factor / unitTo.factor;

                //add the factor to the cache
                UnitConversion._cachedConversionFactors.Add(key, convFactor);

                return convFactor;
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
                //not in the cache, check if the start is a bare unit and has an offset
                string scaleUnitFrom = unitFrom;
                if (UnitConversion.allUnits.ContainsKey(unitFrom))
                {
                    var unitFromDef = UnitConversion.allUnits[unitFrom];
                    if (unitFromDef.offset != 0)
                    {
                        //need to process the conversion
                        value = unitFromDef.ConvertForward(value);
                        scaleUnitFrom = unitFromDef.toUnit;
                    }
                }

                string scaleUnitTo = unitTo;
                UnitDefinition finalToConv = null;
                if (UnitConversion.allUnits.ContainsKey(unitTo))
                {
                    var unitToDef = UnitConversion.allUnits[unitTo];
                    if (unitToDef.offset != 0)
                    {
                        //need to process the conversion
                        scaleUnitTo = unitToDef.toUnit;
                        finalToConv = unitToDef;
                    }
                }

                //have the scale units, get the factor for those				
                value *= ConvFactor(scaleUnitFrom, scaleUnitTo);

                //if a final conversion is needed, process that now
                if (finalToConv != null)
                {
                    value = finalToConv.ConvertBackward(value);
                }

                return value;

            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }

        public static bool isInit = false;

        public static void InitData()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;

            //need to create a directory if it does not exist
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var addinPath = Path.Combine(path, "ExcelUnitConverter");

            if (!Directory.Exists(addinPath))
            {
                Directory.CreateDirectory(addinPath);
            }

            //create the database and tables if needed
            var dbPath = Path.Combine(addinPath, "config.db");

            //copy the database over if it does not exist
            if (!File.Exists(dbPath))
            {
                var embeddedDb = Assembly.GetExecutingAssembly().GetManifestResourceStream("ExcelUnitConverter.config.db");
                embeddedDb.CopyTo(new FileStream(dbPath, FileMode.CreateNew));
            }

            //database exists, load the units
            var db = new SQLiteConnection(dbPath);

            UnitConversion.allUnits = new Dictionary<string, UnitDefinition>();
            var allUnits = db.Table<UnitDefinition>();
            foreach (var unit in allUnits)
            {
                UnitConversion.allUnits.Add(unit.fromUnit, unit);
            }

            UnitConversion.baseUnits = new List<string>();
            var baseUnits = db.Table<BaseUnitDef>();
            foreach (var baseUnit in baseUnits)
            {
                UnitConversion.baseUnits.Add(baseUnit.Name);
            }

            UnitConversion.unitDatabase = db;
        }
    }
}

