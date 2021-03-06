﻿using ExcelDna.Integration;
using System;
using System.Diagnostics;

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

        [ExcelCommand(MenuName = "Units", MenuText = "Show Settings Form")]
        public static void ShowSettingsForm()
        {
            try
            {
                //gets the unit and returns the SI type
                var frmInstance = new UserSettings();
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