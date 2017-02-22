using SQLite;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExcelUnitConverter
{
    public class UnitConversion
    {
        public static Dictionary<Tuple<string, string>, double> _cachedConversionFactors = new Dictionary<Tuple<string, string>, double>();
        public static Dictionary<string, UnitDefinition> allUnits = new Dictionary<string, UnitDefinition>();
        public static Dictionary<string, PreferredUnit> preferredDimensions= new Dictionary<string, PreferredUnit>();

        public static List<string> baseUnits = new List<string>();
        public static SQLiteConnection unitDatabase;
        public double factor = 1;

        public Dictionary<string, int> partsBase = new Dictionary<string, int>();
        public Dictionary<string, int> partsOrig = new Dictionary<string, int>();
        private static Dictionary<string, UnitConversion> _parsedUnitsCache = new Dictionary<string, UnitConversion>();

        public string UnitType
        {
            get
            {
                //this will return a string with the SI base units
                string strOut = "";
                List<string> colMult = new List<string>();
                List<string> colDiv = new List<string>();
                //figure out which units are numerator vs. denomenator
                foreach (var baseUnit in this.partsBase.Keys)
                {
                    if (this.partsBase[baseUnit] > 0)
                    {
                        colMult.Add(getPretty(this.partsBase, baseUnit));
                    }
                    else
                    {
                        colDiv.Add(getPretty(this.partsBase, baseUnit));
                    }
                }
                //join the ones that are in the numerator with a "*"
                strOut = string.Join("*", colMult);
                //if there were no numerators, make the numerator a "1"
                if (strOut.Length == 0)
                {
                    strOut = "1";
                }
                if (colDiv.Count > 0)
                {
                    strOut += "/" + string.Join("/", colDiv);
                }
                return strOut;
            }
        }

        public static bool CanParse(string unitName)
        {
            var unit = new UnitConversion();
            try
            {
                unit.Parse(unitName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static double ConvertValue(double value, string unitFrom, string unitTo)
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
            value *= GetConversionFactor(scaleUnitFrom, scaleUnitTo);

            //if a final conversion is needed, process that now
            if (finalToConv != null)
            {
                value = finalToConv.ConvertBackward(value);
            }

            return value;
        }

        public static UnitConversion CreateUnit(string unitLabel)
        {
            //check the cache for a parsed result
            if (_parsedUnitsCache.ContainsKey(unitLabel))
            {
                return _parsedUnitsCache[unitLabel];
            }

            var unit = new UnitConversion();
            unit.Parse(unitLabel);

            //add to the cache
            _parsedUnitsCache.Add(unitLabel, unit);

            return unit;
        }

        public static double GetConversionFactor(string unitFromStr, string unitToStr)
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

        public static void InvalidateCaches()
        {
            _parsedUnitsCache = new Dictionary<string, UnitConversion>();
            _cachedConversionFactors = new Dictionary<Tuple<string, string>, double>();
        }

        public bool AreSameUnits(UnitConversion otherUnit)
        {
            //check that all of this dict is same
            foreach (var siUnit in this.partsBase.Keys)
            {
                if (!otherUnit.partsBase.ContainsKey(siUnit))
                {
                    return false;
                }
                if (otherUnit.partsBase[siUnit] != this.partsBase[siUnit])
                {
                    return false;
                }
            }
            foreach (var siUnit in otherUnit.partsBase.Keys)
            {
                if (!this.partsBase.ContainsKey(siUnit))
                {
                    return false;
                }
                if (otherUnit.partsBase[siUnit] != this.partsBase[siUnit])
                {
                    return false;
                }
            }
            return true;
        }

        public void Parse(string vUnit)
        {
            //vUnit needs to handle expressions
            int lastStop = 0;
            int divMultFactor = 1;
            for (int chrIndex = 0; chrIndex < vUnit.Length; chrIndex++)
            {
                char charac = vUnit[chrIndex];
                if (charac == '/' || charac == '*' || chrIndex == vUnit.Length - 1)
                {
                    //process current token
                    if (chrIndex == vUnit.Length - 1)
                    {
                        chrIndex = chrIndex + 1;
                    }
                    string token = vUnit.Substring(lastStop, chrIndex - lastStop);
                    if (token != "1")
                    {
                        var tokenParsed = parseToken(token);
                        DictAddOrCreate(partsOrig, tokenParsed.Item1, tokenParsed.Item2 * divMultFactor);
                    }
                    lastStop = chrIndex + 1;
                    //flip to the bottom, set last stop
                    if (charac == '/')
                    {
                        divMultFactor = -1;
                    }
                    else
                    {
                        divMultFactor = 1;
                    }
                }
            }
            //do the conversion to SI
            updateFactorBasedOnParts();
        }

        private void DictAddOrCreate(Dictionary<string, int> dict, string key, int value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = dict[key] + value;
            }
            else
            {
                dict[key] = value;
            }
        }

        private double GetFactorToSis(string vFrom)
        {
            double factor = 1;
            if (!allUnits.ContainsKey(vFrom))
            {
                //trying to get a conversion for a unit that is not defined... exit with error
                throw new Exception("Unit is not defined");
            }
            UnitDefinition uDef = allUnits[vFrom];
            //get the new unit
            var vToUnit = uDef.toUnit;
            //get the factor for the current row
            factor = factor * uDef.factor;
            //parse that unit, down the tree
            UnitConversion vNewUnit = new UnitConversion();
            vNewUnit.Parse(vToUnit);
            //get the factor from the bottom
            factor = factor * vNewUnit.factor;
            //bring the base units up
            foreach (var baseUnit in vNewUnit.partsBase.Keys)
            {
                DictAddOrCreate(this.partsBase, baseUnit, vNewUnit.partsBase[baseUnit] * this.partsOrig[vFrom]);
            }
            //check if the to is an SI unit, if not loop
            return factor;
        }

        private string getPretty(Dictionary<string, int> dict, string Unit)
        {
            //this returns a "nice" version of the unit, including the exponent if greater than 1
            int exponent = dict[Unit];
            if (Math.Abs(exponent) != 1)
            {
                return Unit + "^" + Math.Abs(dict[Unit]);
            }
            else
            {
                return Unit;
            }
        }

        private bool isSiUnit(string vUnit)
        {
            return baseUnits.Contains(vUnit);
        }

        private Tuple<string, int> parseToken(string vToken)
        {
            //if unit has exponent, add that now
            var tokenParts = vToken.Split('^');
            string unitName;
            int unitExponent;
            if (tokenParts.Length > 1)
            {
                unitName = tokenParts[0];
                unitExponent = int.Parse(tokenParts[1]);
            }
            else
            {
                //do a check for a number
                var strPattern = "(^[a-zA-Z]+)([0-9]+$)";
                var strInput = tokenParts[0];
                Regex regEx = new Regex(strPattern);
                Match regMatch = regEx.Match(strInput);
                if (regMatch.Success)
                {
                    //have a match make the split
                    unitName = regMatch.Groups[1].Value;
                    unitExponent = int.Parse(regMatch.Groups[2].Value);
                }
                else
                {
                    //no match
                    unitName = tokenParts[0];
                    unitExponent = 1;
                }
            }
            return Tuple.Create(unitName, unitExponent);
        }

        private void updateFactorBasedOnParts()
        {
            foreach (var origUnit in partsOrig.Keys)
            {
                if (isSiUnit(origUnit))
                {
                    DictAddOrCreate(this.partsBase, origUnit, partsOrig[origUnit]);
                }
                else
                {
                    this.factor = this.factor * Math.Pow(GetFactorToSis(origUnit), partsOrig[origUnit]);
                }
            }
        }
    }
}