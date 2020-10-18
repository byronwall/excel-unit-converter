namespace ExcelUnitConverter
{
    partial class UserSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpdatePath = new System.Windows.Forms.Button();
            this.txtDbNewPath = new System.Windows.Forms.TextBox();
            this.txtDbCurrentPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdatePath
            // 
            this.btnUpdatePath.Location = new System.Drawing.Point(97, 74);
            this.btnUpdatePath.Name = "btnUpdatePath";
            this.btnUpdatePath.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePath.TabIndex = 0;
            this.btnUpdatePath.Text = "update path";
            this.btnUpdatePath.UseVisualStyleBackColor = true;
            this.btnUpdatePath.Click += new System.EventHandler(this.btnUpdatePath_Click);
            // 
            // txtDbNewPath
            // 
            this.txtDbNewPath.Location = new System.Drawing.Point(97, 48);
            this.txtDbNewPath.Name = "txtDbNewPath";
            this.txtDbNewPath.Size = new System.Drawing.Size(305, 20);
            this.txtDbNewPath.TabIndex = 1;
            // 
            // txtDbCurrentPath
            // 
            this.txtDbCurrentPath.Location = new System.Drawing.Point(97, 22);
            this.txtDbCurrentPath.Name = "txtDbCurrentPath";
            this.txtDbCurrentPath.ReadOnly = true;
            this.txtDbCurrentPath.Size = new System.Drawing.Size(305, 20);
            this.txtDbCurrentPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "current db path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "new db path";
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 125);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDbCurrentPath);
            this.Controls.Add(this.txtDbNewPath);
            this.Controls.Add(this.btnUpdatePath);
            this.Name = "UserSettings";
            this.Text = "UserSettings";
            this.Load += new System.EventHandler(this.UserSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdatePath;
        private System.Windows.Forms.TextBox txtDbNewPath;
        private System.Windows.Forms.TextBox txtDbCurrentPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}