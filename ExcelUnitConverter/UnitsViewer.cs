using ExcelDna.Integration;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ExcelUnitConverter
{
    public partial class UnitsViewer : Form
    {
        public UnitsViewer()
        {
            InitializeComponent();

            RefreshDataGridUnits();
            RefreshDataGridDimensions();
            AreInputsValid();
        }

        private bool AreInputsValid()
        {
            return isFromValid() && isToValid() && isFactorValid() && isOffsetValid();
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
            RefreshDataGridUnits();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //TODO need to do some sanity checks here... do these in property setters

            UnitConversion.unitDatabase.Update(dataGridView1.Rows[e.RowIndex].DataBoundItem);

            UnitConversion.InvalidateCaches();

            //force the spreadsheet to recalc since a change happened
            Microsoft.Office.Interop.Excel.Application app = (Microsoft.Office.Interop.Excel.Application)ExcelDnaUtil.Application;
            app.CalculateFullRebuild();
        }

        private bool isFactorValid()
        {
            var factor = txtFactor.Text;
            double result;

            var isValid = double.TryParse(factor, out result);
            lblFactorValid.Text = (isValid) ? "valid" : "enter a number";
            return isValid;
        }

        private bool isFromValid()
        {
            var fromUnit = txtFromUnit.Text;
            var inList = UnitConversion.baseUnits.Contains(fromUnit) || UnitConversion.allUnits.ContainsKey(fromUnit);

            var isValid = !inList;
            lblFromValid.Text = (isValid) ? "valid" : "from not unique";

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

        private bool isToValid()
        {
            var toUnit = txtToUnit.Text;
            var isValid = UnitConversion.CanParse(toUnit);
            lblToValid.Text = (isValid) ? "valid" : "to unit not valid";

            return isValid;
        }

        private void RefreshDataGridUnits()
        {
            dataGridView1.DataSource = UnitConversion.allUnits.Values.ToList();
        }

        private void RefreshDataGridDimensions()
        {
            dataGridView1.DataSource = UnitConversion.preferredDimensions.Values.ToList();
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            isFactorValid();
        }

        private void txtFromUnit_TextChanged(object sender, EventArgs e)
        {
            isFromValid();
        }

        private void txtOffset_TextChanged(object sender, EventArgs e)
        {
            isOffsetValid();
        }

        private void txtToUnit_TextChanged(object sender, EventArgs e)
        {
            isToValid();
        }

        private void btnDimAdd_Click(object sender, EventArgs e)
        {
            //TODO verify that the unit is valid
            var isValid = true;

            if (!isValid)
            {
                return;
            }

            //create the unit def
            var uPref = new PreferredUnit();
            uPref.Dimension = txtDimension.Text;
            uPref.Unit = txtDesiredUnit.Text;

            //looks good, add the unit to the database
            //add the unit to the working lists

            UnitConversion.preferredDimensions.Add(uPref.Dimension, uPref);
            UnitConversion.unitDatabase.Insert(uPref);

            //refresh the datagridview down below
            RefreshDataGridUnits();
        }
    }
}