/*
 * Created by SharpDevelop.
 * User: bwall
 * Date: 2/20/2017
 * Time: 11:33 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ExcelUnitConverter
{
	partial class UnitsViewer
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtFromUnit = new System.Windows.Forms.TextBox();
            this.txtToUnit = new System.Windows.Forms.TextBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFromValid = new System.Windows.Forms.Label();
            this.lblToValid = new System.Windows.Forms.Label();
            this.lblFactorValid = new System.Windows.Forms.Label();
            this.lblOffsetValid = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabUnits = new System.Windows.Forms.TabPage();
            this.tabDimensions = new System.Windows.Forms.TabPage();
            this.dataGridDimensions = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDimension = new System.Windows.Forms.Label();
            this.txtDimension = new System.Windows.Forms.TextBox();
            this.txtDesiredUnit = new System.Windows.Forms.TextBox();
            this.lblTargetUnit = new System.Windows.Forms.Label();
            this.btnDimAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabUnits.SuspendLayout();
            this.tabDimensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDimensions)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(541, 429);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // txtFromUnit
            // 
            this.txtFromUnit.Location = new System.Drawing.Point(16, 29);
            this.txtFromUnit.Name = "txtFromUnit";
            this.txtFromUnit.Size = new System.Drawing.Size(100, 20);
            this.txtFromUnit.TabIndex = 2;
            this.txtFromUnit.TextChanged += new System.EventHandler(this.txtFromUnit_TextChanged);
            // 
            // txtToUnit
            // 
            this.txtToUnit.Location = new System.Drawing.Point(122, 29);
            this.txtToUnit.Name = "txtToUnit";
            this.txtToUnit.Size = new System.Drawing.Size(100, 20);
            this.txtToUnit.TabIndex = 3;
            this.txtToUnit.TextChanged += new System.EventHandler(this.txtToUnit_TextChanged);
            // 
            // txtFactor
            // 
            this.txtFactor.Location = new System.Drawing.Point(228, 29);
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(100, 20);
            this.txtFactor.TabIndex = 4;
            this.txtFactor.TextChanged += new System.EventHandler(this.txtFactor_TextChanged);
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(334, 29);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(100, 20);
            this.txtOffset.TabIndex = 4;
            this.txtOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(441, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "from unit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "to unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "factor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(331, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "offset";
            // 
            // lblFromValid
            // 
            this.lblFromValid.AutoSize = true;
            this.lblFromValid.Location = new System.Drawing.Point(16, 58);
            this.lblFromValid.Name = "lblFromValid";
            this.lblFromValid.Size = new System.Drawing.Size(29, 13);
            this.lblFromValid.TabIndex = 7;
            this.lblFromValid.Text = "valid";
            // 
            // lblToValid
            // 
            this.lblToValid.AutoSize = true;
            this.lblToValid.Location = new System.Drawing.Point(119, 58);
            this.lblToValid.Name = "lblToValid";
            this.lblToValid.Size = new System.Drawing.Size(29, 13);
            this.lblToValid.TabIndex = 8;
            this.lblToValid.Text = "valid";
            // 
            // lblFactorValid
            // 
            this.lblFactorValid.AutoSize = true;
            this.lblFactorValid.Location = new System.Drawing.Point(225, 58);
            this.lblFactorValid.Name = "lblFactorValid";
            this.lblFactorValid.Size = new System.Drawing.Size(29, 13);
            this.lblFactorValid.TabIndex = 8;
            this.lblFactorValid.Text = "valid";
            // 
            // lblOffsetValid
            // 
            this.lblOffsetValid.AutoSize = true;
            this.lblOffsetValid.Location = new System.Drawing.Point(331, 58);
            this.lblOffsetValid.Name = "lblOffsetValid";
            this.lblOffsetValid.Size = new System.Drawing.Size(29, 13);
            this.lblOffsetValid.TabIndex = 8;
            this.lblOffsetValid.Text = "valid";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblFromValid);
            this.panel2.Controls.Add(this.lblOffsetValid);
            this.panel2.Controls.Add(this.txtFromUnit);
            this.panel2.Controls.Add(this.lblFactorValid);
            this.panel2.Controls.Add(this.txtToUnit);
            this.panel2.Controls.Add(this.lblToValid);
            this.panel2.Controls.Add(this.txtFactor);
            this.panel2.Controls.Add(this.txtOffset);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(541, 80);
            this.panel2.TabIndex = 10;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabUnits);
            this.tabControl.Controls.Add(this.tabDimensions);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(555, 541);
            this.tabControl.TabIndex = 11;
            // 
            // tabUnits
            // 
            this.tabUnits.Controls.Add(this.dataGridView1);
            this.tabUnits.Controls.Add(this.panel2);
            this.tabUnits.Location = new System.Drawing.Point(4, 22);
            this.tabUnits.Name = "tabUnits";
            this.tabUnits.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnits.Size = new System.Drawing.Size(547, 515);
            this.tabUnits.TabIndex = 0;
            this.tabUnits.Text = "Units";
            this.tabUnits.UseVisualStyleBackColor = true;
            // 
            // tabDimensions
            // 
            this.tabDimensions.Controls.Add(this.dataGridDimensions);
            this.tabDimensions.Controls.Add(this.panel1);
            this.tabDimensions.Location = new System.Drawing.Point(4, 22);
            this.tabDimensions.Name = "tabDimensions";
            this.tabDimensions.Padding = new System.Windows.Forms.Padding(3);
            this.tabDimensions.Size = new System.Drawing.Size(547, 515);
            this.tabDimensions.TabIndex = 1;
            this.tabDimensions.Text = "Dimensions";
            this.tabDimensions.UseVisualStyleBackColor = true;
            // 
            // dataGridDimensions
            // 
            this.dataGridDimensions.AllowUserToAddRows = false;
            this.dataGridDimensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridDimensions.Location = new System.Drawing.Point(3, 83);
            this.dataGridDimensions.Name = "dataGridDimensions";
            this.dataGridDimensions.Size = new System.Drawing.Size(541, 429);
            this.dataGridDimensions.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDimension);
            this.panel1.Controls.Add(this.txtDimension);
            this.panel1.Controls.Add(this.txtDesiredUnit);
            this.panel1.Controls.Add(this.lblTargetUnit);
            this.panel1.Controls.Add(this.btnDimAdd);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 80);
            this.panel1.TabIndex = 11;
            // 
            // lblDimension
            // 
            this.lblDimension.AutoSize = true;
            this.lblDimension.Location = new System.Drawing.Point(16, 58);
            this.lblDimension.Name = "lblDimension";
            this.lblDimension.Size = new System.Drawing.Size(29, 13);
            this.lblDimension.TabIndex = 7;
            this.lblDimension.Text = "valid";
            // 
            // txtDimension
            // 
            this.txtDimension.Location = new System.Drawing.Point(16, 29);
            this.txtDimension.Name = "txtDimension";
            this.txtDimension.Size = new System.Drawing.Size(100, 20);
            this.txtDimension.TabIndex = 2;
            // 
            // txtDesiredUnit
            // 
            this.txtDesiredUnit.Location = new System.Drawing.Point(122, 29);
            this.txtDesiredUnit.Name = "txtDesiredUnit";
            this.txtDesiredUnit.Size = new System.Drawing.Size(100, 20);
            this.txtDesiredUnit.TabIndex = 3;
            // 
            // lblTargetUnit
            // 
            this.lblTargetUnit.AutoSize = true;
            this.lblTargetUnit.Location = new System.Drawing.Point(119, 58);
            this.lblTargetUnit.Name = "lblTargetUnit";
            this.lblTargetUnit.Size = new System.Drawing.Size(29, 13);
            this.lblTargetUnit.TabIndex = 8;
            this.lblTargetUnit.Text = "valid";
            // 
            // btnDimAdd
            // 
            this.btnDimAdd.Location = new System.Drawing.Point(228, 29);
            this.btnDimAdd.Name = "btnDimAdd";
            this.btnDimAdd.Size = new System.Drawing.Size(75, 23);
            this.btnDimAdd.TabIndex = 5;
            this.btnDimAdd.Text = "add";
            this.btnDimAdd.UseVisualStyleBackColor = true;
            this.btnDimAdd.Click += new System.EventHandler(this.btnDimAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "dimension";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(119, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "target unit";
            // 
            // UnitsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 541);
            this.Controls.Add(this.tabControl);
            this.Name = "UnitsViewer";
            this.Text = "UnitsViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabUnits.ResumeLayout(false);
            this.tabDimensions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDimensions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtFromUnit;
        private System.Windows.Forms.TextBox txtToUnit;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFromValid;
        private System.Windows.Forms.Label lblToValid;
        private System.Windows.Forms.Label lblFactorValid;
        private System.Windows.Forms.Label lblOffsetValid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabUnits;
        private System.Windows.Forms.TabPage tabDimensions;
        private System.Windows.Forms.DataGridView dataGridDimensions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDimension;
        private System.Windows.Forms.TextBox txtDimension;
        private System.Windows.Forms.TextBox txtDesiredUnit;
        private System.Windows.Forms.Label lblTargetUnit;
        private System.Windows.Forms.Button btnDimAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}
