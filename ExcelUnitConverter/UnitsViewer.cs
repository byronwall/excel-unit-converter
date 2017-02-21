/*
 * Created by SharpDevelop.
 * User: bwall
 * Date: 2/20/2017
 * Time: 11:33 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using ExcelDna.Integration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExcelUnitConverter
{
    /// <summary>
    /// Description of UnitsViewer.
    /// </summary>
    public partial class UnitsViewer : Form
    {
        public UnitsViewer()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            RefreshDataGrid();
            AreInputsValid();
        }

        private void RefreshDataGrid()
        {
            dataGridView1.DataSource = UnitConversion.allUnits.Values.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //verify that the unit is valid
            var isValid = AreInputsValid();

            if (!isValid)
            {
                return;
            }

            //create the unit def
            var uDef = new UnitDefinition();
            uDef.fromUnit = txtFromUnit.Text;
            uDef.toUnit = txtToUnit.Text;
            uDef.factor = double.Parse(txtFactor.Text);
            uDef.offset = double.Parse(txtOffset.Text);

            //looks good, add the unit to the database
            //add the unit to the working lists

            UnitConversion.allUnits.Add(uDef.fromUnit, uDef);
            UnitConversion.unitDatabase.Insert(uDef);

            //refresh the datagridview down below
            RefreshDataGrid();
        }

        private bool AreInputsValid()
        {
            return isFromValid() && isToValid() && isFactorValid() && isOffsetValid();
        }

        private bool isFromValid()
        {
            var fromUnit = txtFromUnit.Text;
            var inList = UnitConversion.baseUnits.Contains(fromUnit) || UnitConversion.allUnits.ContainsKey(fromUnit);

            var isValid = !inList;
            lblFromValid.Text = (isValid) ? "valid" : "from not unique";

            return isValid;
        }

        private bool isToValid()
        {
            var toUnit = txtToUnit.Text;
            var isValid = UnitConversion.CanParse(toUnit);
            lblToValid.Text = (isValid) ? "valid" : "to unit not valid";

            return isValid;
        }

        private bool isFactorValid()
        {
            var factor = txtFactor.Text;
            double result;

            var isValid = double.TryParse(factor, out result);
            lblFactorValid.Text = (isValid) ? "valid" : "enter a number";
            return isValid;
        }

        private bool isOffsetValid()
        {
            var offset = txtOffset.Text;
            double result;

            var isValid = double.TryParse(offset, out result);
            lblOffsetValid.Text = (isValid) ? "valid" : "enter a number";
            return isValid;
        }

        private void txtFromUnit_TextChanged(object sender, EventArgs e)
        {
            isFromValid();
        }

        private void txtToUnit_TextChanged(object sender, EventArgs e)
        {
            isToValid();
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            isFactorValid();
        }

        private void txtOffset_TextChanged(object sender, EventArgs e)
        {
            isOffsetValid();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //TODO need to do some sanity checks here

            //TODO need to push the change back into the database

            //TODO need to support deleting a unit def
            UnitConversion.InvalidateCaches();

            Microsoft.Office.Interop.Excel.Application app = (Microsoft.Office.Interop.Excel.Application)ExcelDnaUtil.Application;
            app.CalculateFullRebuild();
        }
    }
}
