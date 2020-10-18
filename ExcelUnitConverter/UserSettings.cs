using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace ExcelUnitConverter
{
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        private void btnUpdatePath_Click(object sender, EventArgs e)
        {
            if (txtDbNewPath.Text != string.Empty)
            {
                AddinSettings.DbPath = txtDbNewPath.Text;
                AddIn.SaveSettings();

                UpdateFormFields();

                //TODO confirm that switching works
            }
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            UpdateFormFields();
        }

        private void UpdateFormFields()
        {
            txtDbCurrentPath.Text = AddinSettings.DbPath;
        }
    }
}