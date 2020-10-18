using ExcelDna.Integration;
using ExcelDna.IntelliSense;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExcelUnitConverter
{
    public class AddIn : IExcelAddIn
    {
        public void AutoOpen()
        {
            try
            {
                //This is disbaled until resolved in Excel 2016
                IntelliSenseServer.Register();

                InitData();
                InitSettings();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw e;
            }
        }

        public void AutoClose()
        {
            UnitConversion.unitDatabase.Close();
        }

        private bool _isInit = false;

        public void InitData()
        {
            if (_isInit)
            {
                return;
            }
            _isInit = true;

            //need to create a directory if it does not exist
            var addinPath = AddIn.getAddinPath();

            //create the database and tables if needed
            AddinSettings.DbPath = Path.Combine(addinPath, "config.db");

            //copy the database over if it does not exist
            if (!File.Exists(AddinSettings.DbPath))
            {
                var embeddedDb = Assembly.GetExecutingAssembly().GetManifestResourceStream("ExcelUnitConverter.config.db");
                embeddedDb.CopyTo(new FileStream(AddinSettings.DbPath, FileMode.CreateNew));
            }

            //database exists, load the units
            var db = new SQLiteConnection(AddinSettings.DbPath);

            UnitConversion.baseUnits = new List<string>();
            var baseUnits = db.Table<BaseUnitDef>();
            foreach (var baseUnit in baseUnits)
            {
                UnitConversion.baseUnits.Add(baseUnit.Name);
            }

            UnitConversion.allUnits = new Dictionary<string, UnitDefinition>();
            var allUnits = db.Table<UnitDefinition>();
            foreach (var unit in allUnits)
            {
                UnitConversion.allUnits.Add(unit.fromUnit, unit);
            }

            UnitConversion.preferredDimensions = new Dictionary<string, PreferredUnit>();
            var allDimensions = db.Table<PreferredUnit>();
            foreach (var dimen in allDimensions)
            {
                UnitConversion.preferredDimensions.Add(dimen.Dimension, dimen);
            }

            UnitConversion.unitDatabase = db;
        }

        private void InitSettings()
        {
            //need to create a directory if it does not exist
            string settingsPath = getSettingsPath();

            //copy the database over if it does not exist
            if (!File.Exists(settingsPath))
            {
                File.WriteAllText(settingsPath, "");
            }
            else
            {
                var settings = File.ReadLines(settingsPath);

                foreach (var line in settings)
                {
                    var parts = line.Split('=');

                    switch (parts[0])
                    {
                        case "dbname":
                            AddinSettings.DbPath = parts[1];
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static string getSettingsPath()
        {
            return Path.Combine(getAddinPath(), "settings.ini");
        }

        private static string getAddinPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var addinPath = Path.Combine(path, "ExcelUnitConverter");

            if (!Directory.Exists(addinPath))
            {
                Directory.CreateDirectory(addinPath);
            }

            return addinPath;
        }

        public static void SaveSettings()
        {
            string settingsPath = getSettingsPath();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"dbname={AddinSettings.DbPath}");

            File.WriteAllText(settingsPath, sb.ToString());
        }
    }

    public static class AddinSettings
    {
        public static string DbPath = "";
    }
}