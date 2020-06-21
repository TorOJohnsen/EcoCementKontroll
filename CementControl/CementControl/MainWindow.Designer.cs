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
            this.readWeightTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelReadWeight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.desiredCementLoad = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.startLoadWeight = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.weight_loaded = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_weight = new System.Windows.Forms.Label();
            this.label_powerSupply = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // readWeightTimer
            // 
            this.readWeightTimer.Enabled = true;
            this.readWeightTimer.Interval = 3000;
            this.readWeightTimer.Tick += new System.EventHandler(this.readWeightTimer_Tick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelReadWeight);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(3, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(222, 172);
            this.panel2.TabIndex = 4;
            // 
            // labelReadWeight
            // 
            this.labelReadWeight.AutoSize = true;
            this.labelReadWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelReadWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 44F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReadWeight.ForeColor = System.Drawing.Color.Red;
            this.labelReadWeight.Location = new System.Drawing.Point(12, 45);
            this.labelReadWeight.Name = "labelReadWeight";
            this.labelReadWeight.Size = new System.Drawing.Size(165, 67);
            this.labelReadWeight.TabIndex = 1;
            this.labelReadWeight.Text = "xxx.x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Silo vekt (kg)";
            // 
            // desiredCementLoad
            // 
            this.desiredCementLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desiredCementLoad.Location = new System.Drawing.Point(8, 39);
            this.desiredCementLoad.Name = "desiredCementLoad";
            this.desiredCementLoad.Size = new System.Drawing.Size(100, 29);
            this.desiredCementLoad.TabIndex = 4;
            this.desiredCementLoad.Text = "50";
            this.desiredCementLoad.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.startLoadWeight);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.desiredCementLoad);
            this.panel3.Location = new System.Drawing.Point(231, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 172);
            this.panel3.TabIndex = 5;
            // 
            // startLoadWeight
            // 
            this.startLoadWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.startLoadWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLoadWeight.Location = new System.Drawing.Point(8, 74);
            this.startLoadWeight.Name = "startLoadWeight";
            this.startLoadWeight.Size = new System.Drawing.Size(75, 38);
            this.startLoadWeight.TabIndex = 6;
            this.startLoadWeight.Text = "Start";
            this.startLoadWeight.UseVisualStyleBackColor = false;
            this.startLoadWeight.Click += new System.EventHandler(this.startLoadWeight_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Last sement (kg)";
            // 
            // weight_loaded
            // 
            this.weight_loaded.BackColor = System.Drawing.Color.Silver;
            this.weight_loaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weight_loaded.Location = new System.Drawing.Point(51, 66);
            this.weight_loaded.Name = "weight_loaded";
            this.weight_loaded.Size = new System.Drawing.Size(100, 23);
            this.weight_loaded.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.weight_loaded);
            this.panel4.Location = new System.Drawing.Point(437, 66);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 172);
            this.panel4.TabIndex = 6;
            // 
            // label_weight
            // 
            this.label_weight.AutoSize = true;
            this.label_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_weight.ForeColor = System.Drawing.Color.Red;
            this.label_weight.Location = new System.Drawing.Point(3, 401);
            this.label_weight.Name = "label_weight";
            this.label_weight.Size = new System.Drawing.Size(33, 16);
            this.label_weight.TabIndex = 7;
            this.label_weight.Text = "vekt";
            // 
            // label_powerSupply
            // 
            this.label_powerSupply.AutoSize = true;
            this.label_powerSupply.BackColor = System.Drawing.SystemColors.Control;
            this.label_powerSupply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_powerSupply.ForeColor = System.Drawing.Color.Red;
            this.label_powerSupply.Location = new System.Drawing.Point(3, 423);
            this.label_powerSupply.Name = "label_powerSupply";
            this.label_powerSupply.Size = new System.Drawing.Size(41, 16);
            this.label_powerSupply.TabIndex = 8;
            this.label_powerSupply.Text = "skrue";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_powerSupply);
            this.Controls.Add(this.label_weight);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "MainWindow";
            this.Text = "Cement Kontroll";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
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
    }
}

