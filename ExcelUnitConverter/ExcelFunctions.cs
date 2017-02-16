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
namespace ExcelUnitConverter
{
	public static class ExcelFunctions
	{
		
		private static Dictionary<Tuple<string, string>, double> _cachedConversionFactors = new Dictionary<Tuple<string, string>, double>();
		
		[ExcelFunction("Provides the conversion factor from one unit to another via multiplication")]
		public static double ConvFactor(
			[ExcelArgument(Name = "UnitFrom", Description = "is the base, current units")] string unitFromStr, 
			[ExcelArgument(Name = "UnitTo", Description = "is the desired unit, obtained by multiplying the base by the factor")] string unitToStr)
		{			
			try {
				//check the cache based on the string inputs
				var key = Tuple.Create(unitFromStr, unitToStr);
				if (_cachedConversionFactors.ContainsKey(key)) {
					return _cachedConversionFactors[key];
				}
				
				UnitConversion unitFrom = UnitConversion.CreateUnit(unitFromStr);
				UnitConversion unitTo = UnitConversion.CreateUnit(unitToStr);
				
				if (!unitFrom.AreSameUnits(unitTo)) {
					throw new Exception(string.Format("not converting same unit types {0} to {1}", unitFromStr, unitToStr));
				}
				
				var convFactor = unitFrom.factor / unitTo.factor;
				
				//add the factor to the cache
				_cachedConversionFactors.Add(key, convFactor);
				
				return convFactor;
			} catch (Exception e) {
				Debug.Print(e.ToString());
				throw;
			}
		}

		[ExcelFunction("Provides the SI unit type")]
		public static string UnitType(
			[ExcelArgument(Name = "Unit", Description = "is the current unit")]string unitFromStr)
		{
			try {
				//gets the unit and returns the SI type
				return UnitConversion.CreateUnit(unitFromStr).UnitType;
				
			} catch (Exception e) {
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
			try {				
				//not in the cache, check if the start is a bare unit and has an offset
				string scaleUnitFrom = unitFrom;
				if (UnitConversion.allUnits.ContainsKey(unitFrom)) {
					var unitFromDef = UnitConversion.allUnits[unitFrom];
					if (unitFromDef.offset != 0) {
						//need to process the conversion
						value = unitFromDef.ConvertForward(value);
						scaleUnitFrom = unitFromDef.toUnit;
					}
				}
				
				string scaleUnitTo = unitTo;
				UnitDefinition finalToConv = null;
				if (UnitConversion.allUnits.ContainsKey(unitTo)) {
					var unitToDef = UnitConversion.allUnits[unitTo];
					if (unitToDef.offset != 0) {
						//need to process the conversion
						scaleUnitTo = unitToDef.toUnit;
						finalToConv = unitToDef;
					}
				}
				
				//have the scale units, get the factor for those				
				value *= ConvFactor(scaleUnitFrom, scaleUnitTo);
				
				//if a final conversion is needed, process that now
				if (finalToConv != null) {
					value = finalToConv.ConvertBackward(value);
				}
				
				return value;
				
			} catch (Exception e) {
				Debug.Print(e.ToString());
				throw;
			}
		}

		public static bool isInit = false;

		public static void InitData()
		{
			if (isInit) {
				return;
			}
			isInit = true;
			
			var baseUnits = new List<string>();
			//load from a file
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("baseUnits")) {
				// read from stream to read the resource file
				using (StreamReader sr = new StreamReader(stream, Encoding.UTF8)) {
					while (!sr.EndOfStream) {
						var line = sr.ReadLine();
						if (line == "") {
							continue;
						}
						baseUnits.Add(line);
					}
					UnitConversion.baseUnits = baseUnits;
				}			
			}
			var allUnits = new Dictionary<string, UnitDefinition>();
			//load from a file
			
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("allUnits")) {
				// read from stream to read the resource file
				using (StreamReader sr = new StreamReader(stream, Encoding.UTF8)) {
					while (!sr.EndOfStream) {
						var line = sr.ReadLine();
						if (line == "") {
							continue;
						}			
			
						var parts = line.Split('\t');
						UnitDefinition uDef = new UnitDefinition();
						uDef.fromUnit = parts[0];
						var factorString = parts[1];
						uDef.factor = (factorString == "") ? 0 : double.Parse(factorString);
						var offsetString = parts[2];
						uDef.offset = (offsetString == "") ? 0 : double.Parse(offsetString);
						uDef.toUnit = parts[3];
						allUnits.Add(uDef.fromUnit, uDef);
						UnitConversion.allUnits = allUnits;
					}
				}
			}
		}
	}
}

