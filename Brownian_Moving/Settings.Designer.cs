namespace Brownian_Moving
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.Mass_value = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.Speed_Value = new System.Windows.Forms.TrackBar();
            this.R_Value = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Mass_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.R_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Масса Шара";
            // 
            // Mass_value
            // 
            this.Mass_value.Location = new System.Drawing.Point(63, 66);
            this.Mass_value.Maximum = 300;
            this.Mass_value.Minimum = 10;
            this.Mass_value.Name = "Mass_value";
            this.Mass_value.Size = new System.Drawing.Size(255, 45);
            this.Mass_value.TabIndex = 1;
            this.Mass_value.TabStop = false;
            this.Mass_value.Value = 20;
            this.Mass_value.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Скорость Шара";
            // 
            // Speed_Value
            // 
            this.Speed_Value.Location = new System.Drawing.Point(63, 134);
            this.Speed_Value.Name = "Speed_Value";
            this.Speed_Value.Size = new System.Drawing.Size(246, 45);
            this.Speed_Value.TabIndex = 3;
            // 
            // R_Value
            // 
            this.R_Value.AllowDrop = true;
            this.R_Value.BackColor = System.Drawing.SystemColors.Control;
            this.R_Value.Location = new System.Drawing.Point(63, 185);
            this.R_Value.Maximum = 100;
            this.R_Value.Minimum = 5;
            this.R_Value.Name = "R_Value";
            this.R_Value.Size = new System.Drawing.Size(246, 45);
            this.R_Value.TabIndex = 4;
            this.R_Value.Value = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Радиус Шара";
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(13, 272);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(93, 40);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(223, 272);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(86, 40);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 341);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.R_Value);
            this.Controls.Add(this.Speed_Value);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Mass_value);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.Mass_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Speed_Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.R_Value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar Mass_value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar Speed_Value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TrackBar R_Value;
    }
}