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
using System.Text.RegularExpressions;
namespace ExcelUnitConverter
{
	public class UnitDefinition
	{
		public string fromUnit;

		public string toUnit;

		public double factor;

		public double offset;

		public double ConvertForward(double value)
		{
			return this.factor * value + this.offset;
		}

		public double ConvertBackward(double value)
		{
			return (value - this.offset) / this.factor;
		}
	}
}

