/*
 * Created by SharpDevelop.
 * User: bwall
 * Date: 2/14/2017
 * Time: 8:56 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
namespace ExcelUnitConverter
{
    public class UnitDefinition
    {
        [PrimaryKey]
        public string fromUnit { get; set; }

        public string toUnit { get; set; }

        public double factor { get; set; }

        public double offset { get; set; }

        public double ConvertForward(double value)
        {
            return this.factor * value + this.offset;
        }

        public double ConvertBackward(double value)
        {
            return (value - this.offset) / this.factor;
        }
    }

    public class BaseUnitDef
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}

