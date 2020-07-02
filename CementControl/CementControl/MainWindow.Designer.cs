namespace CementControl
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.readWeightTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelReadWeight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.desiredCementLoad = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.stopLoadWeight = new System.Windows.Forms.Button();
            this.startLoadWeight = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.weight_loaded = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label_weight = new System.Windows.Forms.Label();
            this.label_powerSupply = new System.Windows.Forms.Label();
            this.buttonConnectSerial = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxScaleComPort = new System.Windows.Forms.TextBox();
            this.textBoxPowerSupplyComPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonDeviceManager = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // readWeightTimer
            // 
            this.readWeightTimer.Interval = 3000;
            this.readWeightTimer.Tick += new System.EventHandler(this.readWeightTimer_Tick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelReadWeight);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(16, 162);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 276);
            this.panel2.TabIndex = 4;
            // 
            // labelReadWeight
            // 
            this.labelReadWeight.AutoSize = true;
            this.labelReadWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelReadWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReadWeight.ForeColor = System.Drawing.Color.Red;
            this.labelReadWeight.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelReadWeight.Location = new System.Drawing.Point(25, 96);
            this.labelReadWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReadWeight.Name = "labelReadWeight";
            this.labelReadWeight.Size = new System.Drawing.Size(221, 91);
            this.labelReadWeight.TabIndex = 1;
            this.labelReadWeight.Text = "xxx.x";
            this.labelReadWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(67, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "Vekt silo (kg)";
            // 
            // desiredCementLoad
            // 
            this.desiredCementLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desiredCementLoad.Location = new System.Drawing.Point(12, 48);
            this.desiredCementLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.desiredCementLoad.Name = "desiredCementLoad";
            this.desiredCementLoad.Size = new System.Drawing.Size(189, 34);
            this.desiredCementLoad.TabIndex = 4;
            this.desiredCementLoad.Text = "80";
            this.desiredCementLoad.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.textBoxDescription);
            this.panel3.Controls.Add(this.stopLoadWeight);
            this.panel3.Controls.Add(this.startLoadWeight);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.desiredCementLoad);
            this.panel3.Location = new System.Drawing.Point(345, 162);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(407, 276);
            this.panel3.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "Beskrivelse";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(11, 145);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(372, 30);
            this.textBoxDescription.TabIndex = 8;
            this.textBoxDescription.Text = "Beskrivelse som skrives i loggen";
            this.textBoxDescription.WordWrap = false;
            // 
            // stopLoadWeight
            // 
            this.stopLoadWeight.BackColor = System.Drawing.Color.Red;
            this.stopLoadWeight.Enabled = false;
            this.stopLoadWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopLoadWeight.Location = new System.Drawing.Point(144, 209);
            this.stopLoadWeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stopLoadWeight.Name = "stopLoadWeight";
            this.stopLoadWeight.Size = new System.Drawing.Size(100, 47);
            this.stopLoadWeight.TabIndex = 7;
            this.stopLoadWeight.Text = "Stopp";
            this.stopLoadWeight.UseVisualStyleBackColor = false;
            this.stopLoadWeight.Click += new System.EventHandler(this.stopLoadWeight_Click);
            // 
            // startLoadWeight
            // 
            this.startLoadWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startLoadWeight.Enabled = false;
            this.startLoadWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLoadWeight.Location = new System.Drawing.Point(12, 209);
            this.startLoadWeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startLoadWeight.Name = "startLoadWeight";
            this.startLoadWeight.Size = new System.Drawing.Size(100, 47);
            this.startLoadWeight.TabIndex = 6;
            this.startLoadWeight.Text = "Start";
            this.startLoadWeight.UseVisualStyleBackColor = false;
            this.startLoadWeight.Click += new System.EventHandler(this.startLoadWeight_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(339, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "Last sement (kg), uten desimal";
            // 
            // weight_loaded
            // 
            this.weight_loaded.BackColor = System.Drawing.Color.Silver;
            this.weight_loaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weight_loaded.Location = new System.Drawing.Point(31, 57);
            this.weight_loaded.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weight_loaded.Name = "weight_loaded";
            this.weight_loaded.Size = new System.Drawing.Size(205, 28);
            this.weight_loaded.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.weight_loaded);
            this.panel4.Location = new System.Drawing.Point(784, 162);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 276);
            this.panel4.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lastet sement (kg)";
            // 
            // label_weight
            // 
            this.label_weight.AutoSize = true;
            this.label_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_weight.ForeColor = System.Drawing.Color.Red;
            this.label_weight.Location = new System.Drawing.Point(4, 494);
            this.label_weight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_weight.Name = "label_weight";
            this.label_weight.Size = new System.Drawing.Size(138, 20);
            this.label_weight.TabIndex = 7;
            this.label_weight.Text = "Vekt ikke tilkoblet";
            // 
            // label_powerSupply
            // 
            this.label_powerSupply.AutoSize = true;
            this.label_powerSupply.BackColor = System.Drawing.SystemColors.Control;
            this.label_powerSupply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_powerSupply.ForeColor = System.Drawing.Color.Red;
            this.label_powerSupply.Location = new System.Drawing.Point(4, 521);
            this.label_powerSupply.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_powerSupply.Name = "label_powerSupply";
            this.label_powerSupply.Size = new System.Drawing.Size(133, 20);
            this.label_powerSupply.TabIndex = 8;
            this.label_powerSupply.Text = "Silo ikke tilkoblet";
            // 
            // buttonConnectSerial
            // 
            this.buttonConnectSerial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonConnectSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnectSerial.Location = new System.Drawing.Point(283, 27);
            this.buttonConnectSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConnectSerial.Name = "buttonConnectSerial";
            this.buttonConnectSerial.Size = new System.Drawing.Size(264, 34);
            this.buttonConnectSerial.TabIndex = 9;
            this.buttonConnectSerial.Text = "Koble til vekt og silo";
            this.buttonConnectSerial.UseVisualStyleBackColor = false;
            this.buttonConnectSerial.Click += new System.EventHandler(this.buttonConnectSerial_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Vekt com port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Silo com port:";
            // 
            // textBoxScaleComPort
            // 
            this.textBoxScaleComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxScaleComPort.Location = new System.Drawing.Point(146, 10);
            this.textBoxScaleComPort.Name = "textBoxScaleComPort";
            this.textBoxScaleComPort.Size = new System.Drawing.Size(101, 27);
            this.textBoxScaleComPort.TabIndex = 12;
            // 
            // textBoxPowerSupplyComPort
            // 
            this.textBoxPowerSupplyComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPowerSupplyComPort.Location = new System.Drawing.Point(146, 48);
            this.textBoxPowerSupplyComPort.Name = "textBoxPowerSupplyComPort";
            this.textBoxPowerSupplyComPort.Size = new System.Drawing.Size(101, 27);
            this.textBoxPowerSupplyComPort.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "La stå åpen for guided tilkobling";
            // 
            // buttonDeviceManager
            // 
            this.buttonDeviceManager.Location = new System.Drawing.Point(1011, 12);
            this.buttonDeviceManager.Name = "buttonDeviceManager";
            this.buttonDeviceManager.Size = new System.Drawing.Size(44, 23);
            this.buttonDeviceManager.TabIndex = 15;
            this.buttonDeviceManager.Text = "EB";
            this.buttonDeviceManager.UseVisualStyleBackColor = true;
            this.buttonDeviceManager.Click += new System.EventHandler(this.buttonDeviceManager_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.buttonDeviceManager);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPowerSupplyComPort);
            this.Controls.Add(this.textBoxScaleComPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonConnectSerial);
            this.Controls.Add(this.label_powerSupply);
            this.Controls.Add(this.label_weight);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "Cement Kontroll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer readWeightTimer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelReadWeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox desiredCementLoad;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button startLoadWeight;
        private System.Windows.Forms.Label weight_loaded;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_weight;
        private System.Windows.Forms.Label label_powerSupply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button stopLoadWeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConnectSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxScaleComPort;
        private System.Windows.Forms.TextBox textBoxPowerSupplyComPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonDeviceManager;
    }
}

