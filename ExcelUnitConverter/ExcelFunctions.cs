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
using ExcelDna.IntelliSense;
namespace ExcelUnitConverter
{
	public static class ExcelFunctions
	{
		[ExcelFunction("Provides the conversion factor from one unit to another via multiplication")]
		public static double ConvFactor(
			[ExcelArgument(Name = "UnitFrom", Description = "is the base, current units")] string unitFromStr, 
			[ExcelArgument(Name = "UnitTo", Description = "is the desired unit, obtained by multiplying the base by the factor")] string unitToStr)
		{			
			try {
				Debug.Print("incoming unit from {0}", unitFromStr);
				Debug.Print("incoming unit to {0}", unitToStr);
				UnitConversion unitFrom = UnitConversion.CreateUnit(unitFromStr);
				UnitConversion unitTo = UnitConversion.CreateUnit(unitToStr);
				return unitFrom.factor / unitTo.factor;
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
				InitData();
				UnitConversion unitFrom = UnitConversion.CreateUnit(unitFromStr);
				return unitFrom.UnitType;
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
				InitData();
				//check if the unit is defined in base units -> done
				while (UnitConversion.allUnits.ContainsKey(unitFrom)) {
					var unitFromDef = UnitConversion.allUnits[unitFrom];
					if (unitFromDef.offset != 0) {
						//need to process the conversion
						value = unitFromDef.ConvertForward(value);
						unitFrom = unitFromDef.toUnit;
					} else {
						break;
					}
				}
				//at this point, unitFrom is in a unit that does not have an offset... convert to SI using factors
				var fromUnit = UnitConversion.CreateUnit(unitFrom);
				value = value * fromUnit.factor;
				var finalUnitTo = unitTo;
				//determine last output that is non offset    
				var backChain = new List<UnitDefinition>();
				while (UnitConversion.allUnits.ContainsKey(unitTo)) {
					var unitToDef = UnitConversion.allUnits[unitTo];
					if (unitToDef.offset != 0) {
						//need to process the conversion
						unitTo = unitToDef.toUnit;
						backChain.Add(unitToDef);
					} else {
						break;
					}
				}
				//unitTo now contains the final unit to get to that is not offset... get the factor to there
				var toUnit = UnitConversion.CreateUnit(unitTo);
				if (!toUnit.AreSameUnits(fromUnit)) {
					throw new Exception("not the same unit");
				}
				value = value / toUnit.factor;
				//process the back chain to get to the final answer
				for (int unitIndex = backChain.Count - 1; unitIndex >= 0; unitIndex--) {
					var convFrom = backChain[unitIndex];
					value = convFrom.ConvertBackward(value);
				}
				//on the back end, check do the same checks in reverse
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
				using (StreamReader sr = new StreamReader(stream,Encoding.UTF8)) {
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
				using (StreamReader sr = new StreamReader(stream,Encoding.UTF8)) {
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

