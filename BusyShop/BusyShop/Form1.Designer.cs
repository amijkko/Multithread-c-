namespace BusyShop
{
    partial class Form1
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.ScrollBarRight = new System.Windows.Forms.VScrollBar();
            this.shopBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ScrollBarLeft = new System.Windows.Forms.VScrollBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.shopBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(72, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // ScrollBarRight
            // 
            this.ScrollBarRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollBarRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScrollBarRight.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.shopBindingSource, "AmountOfCasses", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "1", "N0"));
            this.ScrollBarRight.LargeChange = 1;
            this.ScrollBarRight.Location = new System.Drawing.Point(1000, 187);
            this.ScrollBarRight.Maximum = 8;
            this.ScrollBarRight.Name = "ScrollBarRight";
            this.ScrollBarRight.Size = new System.Drawing.Size(62, 163);
            this.ScrollBarRight.TabIndex = 1;
            this.ScrollBarRight.Value = 1;
            this.ScrollBarRight.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarRight_Scroll);
            // 
            // shopBindingSource
            // 
            this.shopBindingSource.DataSource = typeof(Nikolaev.WorkSpace.Shop);
            // 
            // ScrollBarLeft
            // 
            this.ScrollBarLeft.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.shopBindingSource, "AmountOfCustomers", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "1", "N0"));
            this.ScrollBarLeft.LargeChange = 5;
            this.ScrollBarLeft.Location = new System.Drawing.Point(19, 28);
            this.ScrollBarLeft.Minimum = 1;
            this.ScrollBarLeft.Name = "ScrollBarLeft";
            this.ScrollBarLeft.Size = new System.Drawing.Size(37, 122);
            this.ScrollBarLeft.TabIndex = 2;
            this.ScrollBarLeft.Value = 2;
            this.ScrollBarLeft.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarLeft_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(107, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(877, 288);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shopBindingSource, "AmountOfCustomers", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "1", "N0"));
            this.textBox1.Location = new System.Drawing.Point(12, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(962, 366);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(614, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(427, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 417);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ScrollBarLeft);
            this.Controls.Add(this.ScrollBarRight);
            this.Controls.Add(this.radioButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.shopBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.VScrollBar ScrollBarRight;
        private System.Windows.Forms.VScrollBar ScrollBarLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.BindingSource shopBindingSource;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

