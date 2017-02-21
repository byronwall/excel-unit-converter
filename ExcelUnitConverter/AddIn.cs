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
using ExcelDna.Integration;
using ExcelDna.IntelliSense;
namespace ExcelUnitConverter
{
    public class AddIn : IExcelAddIn
    {
        public void AutoOpen()
        {
            //This is disbaled until resolved in Excel 2016
            //IntelliSenseServer.Register();
            ExcelFunctions.InitData();
        }

        public void AutoClose()
        {
        }
    }
}



