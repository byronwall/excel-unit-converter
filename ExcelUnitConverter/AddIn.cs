using ExcelDna.Integration;

namespace ExcelUnitConverter
{
    public class AddIn : IExcelAddIn
    {
        public void AutoOpen()
        {
            //This is disbaled until resolved in Excel 2016
            //IntelliSenseServer.Register();
            Helpers.InitData();
        }

        public void AutoClose()
        {
            UnitConversion.unitDatabase.Close();
        }
    }
}