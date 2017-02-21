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
using System.Linq;

namespace ExcelUnitConverter
{
    public static class ExcelCommands
    {
        [ExcelCommand(MenuName = "Units", MenuText = "Show Units")]
        public static void ShowForm()
        {
            try
            {
                //gets the unit and returns the SI type
                var frmInstance = new UnitsViewer();
                frmInstance.Show();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                throw;
            }
        }
    }
}



