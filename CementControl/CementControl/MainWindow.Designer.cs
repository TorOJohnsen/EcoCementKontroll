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
            this.label_weight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readWeightTimer
            // 
            this.readWeightTimer.Enabled = true;
            this.readWeightTimer.Interval = 3000;
            this.readWeightTimer.Tick += new System.EventHandler(this.readWeightTimer_Tick);
            // 
            // label_weight
            // 
            this.label_weight.AutoSize = true;
            this.label_weight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_weight.Location = new System.Drawing.Point(341, 147);
            this.label_weight.Name = "label_weight";
            this.label_weight.Size = new System.Drawing.Size(109, 39);
            this.label_weight.TabIndex = 0;
            this.label_weight.Text = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_weight);
            this.Name = "MainWindow";
            this.Text = "Cement Kontroll";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer readWeightTimer;
        private System.Windows.Forms.Label label_weight;
    }
}

