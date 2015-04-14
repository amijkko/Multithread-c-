namespace Nikolaev.WorkSpace
{
    partial class Roachs
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
            this.button1 = new System.Windows.Forms.Button();
            this.RaceTrack = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxForNumbers = new System.Windows.Forms.TextBox();
            this.paparoachBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.textBoxMousePosition = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.paparoachBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartClick);
            // 
            // RaceTrack
            // 
            this.RaceTrack.Location = new System.Drawing.Point(204, 188);
            this.RaceTrack.Name = "RaceTrack";
            this.RaceTrack.Size = new System.Drawing.Size(481, 260);
            this.RaceTrack.TabIndex = 5;
            this.RaceTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.RaceTrack_Paint);
            this.RaceTrack.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DeathClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(292, 58);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 124);
            this.textBox1.TabIndex = 6;
            // 
            // textBoxForNumbers
            // 
            this.textBoxForNumbers.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.paparoachBindingSource, "Amount", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "2", "N0"));
            this.textBoxForNumbers.Location = new System.Drawing.Point(39, 99);
            this.textBoxForNumbers.MaxLength = 1;
            this.textBoxForNumbers.Name = "textBoxForNumbers";
            this.textBoxForNumbers.Size = new System.Drawing.Size(151, 20);
            this.textBoxForNumbers.TabIndex = 7;
            this.textBoxForNumbers.WordWrap = false;
            // 
            // paparoachBindingSource
            // 
            this.paparoachBindingSource.DataSource = typeof(Logic.Paparoach);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter Number of Runners";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Namespace = "Nikolaev.WorkSpace";
            // 
            // textBoxMousePosition
            // 
            this.textBoxMousePosition.Location = new System.Drawing.Point(77, 252);
            this.textBoxMousePosition.Name = "textBoxMousePosition";
            this.textBoxMousePosition.Size = new System.Drawing.Size(100, 20);
            this.textBoxMousePosition.TabIndex = 9;
            // 
            // Roachs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 460);
            this.Controls.Add(this.textBoxMousePosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxForNumbers);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RaceTrack);
            this.Controls.Add(this.button1);
            this.Name = "Roachs";
            this.Text = "m";
            ((System.ComponentModel.ISupportInitialize)(this.paparoachBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel RaceTrack;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxForNumbers;
        private System.Windows.Forms.Label label1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.BindingSource paparoachBindingSource;
        private System.Windows.Forms.TextBox textBoxMousePosition;
    }
}

