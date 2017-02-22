using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExcelUnitConverter
{
    public static class Helpers
    {
        private static bool _isInit = false;

        public static void InitData()
        {
            if (_isInit)
            {
                return;
            }
            _isInit = true;

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
    }
}