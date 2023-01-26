namespace Gauss_Seidel
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.helpButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.iterationsBar = new System.Windows.Forms.TrackBar();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.resultGridView = new System.Windows.Forms.DataGridView();
            this.label106 = new System.Windows.Forms.Label();
            this.tolerance = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timeElapsedBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(586, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "C# code";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(586, 69);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(97, 17);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Assembly code";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 543);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(671, 23);
            this.progressBar1.TabIndex = 211;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(407, 462);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(126, 50);
            this.helpButton.TabIndex = 212;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(549, 462);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(127, 50);
            this.runButton.TabIndex = 213;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // iterationsBar
            // 
            this.iterationsBar.Location = new System.Drawing.Point(404, 364);
            this.iterationsBar.Maximum = 100000;
            this.iterationsBar.Minimum = 100;
            this.iterationsBar.Name = "iterationsBar";
            this.iterationsBar.Size = new System.Drawing.Size(272, 45);
            this.iterationsBar.TabIndex = 214;
            this.iterationsBar.Value = 50000;
            this.iterationsBar.Scroll += new System.EventHandler(this.iterationsBar_Scroll);
            this.iterationsBar.ValueChanged += new System.EventHandler(this.iterationsBar_ValueChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(509, 348);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(72, 13);
            this.label101.TabIndex = 215;
            this.label101.Text = "Max iterations";
            this.label101.Click += new System.EventHandler(this.label101_Click);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(404, 405);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(25, 13);
            this.label102.TabIndex = 216;
            this.label102.Text = "100";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(643, 405);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(43, 13);
            this.label103.TabIndex = 217;
            this.label103.Text = "100000";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(523, 412);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(0, 13);
            this.label104.TabIndex = 218;
            this.label104.Click += new System.EventHandler(this.label104_Click);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(523, 412);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(10, 13);
            this.label105.TabIndex = 219;
            this.label105.Text = " ";
            // 
            // resultGridView
            // 
            this.resultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGridView.Location = new System.Drawing.Point(12, 12);
            this.resultGridView.Name = "resultGridView";
            this.resultGridView.Size = new System.Drawing.Size(386, 406);
            this.resultGridView.TabIndex = 222;
            this.resultGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(192, 451);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(49, 13);
            this.label106.TabIndex = 224;
            this.label106.Text = "Threads:";
            // 
            // tolerance
            // 
            this.tolerance.AutoSize = true;
            this.tolerance.Location = new System.Drawing.Point(501, 266);
            this.tolerance.Name = "tolerance";
            this.tolerance.Size = new System.Drawing.Size(80, 13);
            this.tolerance.TabIndex = 221;
            this.tolerance.Text = "tolerance value";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(44, 467);
            this.trackBar1.Maximum = 64;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(333, 45);
            this.trackBar1.TabIndex = 223;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(463, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 225;
            this.label1.Text = "Time elapsed";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // timeElapsedBox
            // 
            this.timeElapsedBox.Location = new System.Drawing.Point(445, 42);
            this.timeElapsedBox.Name = "timeElapsedBox";
            this.timeElapsedBox.ReadOnly = true;
            this.timeElapsedBox.Size = new System.Drawing.Size(106, 20);
            this.timeElapsedBox.TabIndex = 226;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 499);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 227;
            this.label2.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 499);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 228;
            this.label3.Text = "32";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 499);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 229;
            this.label4.Text = "64";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 230;
            this.label5.Text = "1";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(407, 282);
            this.trackBar2.Maximum = 10000;
            this.trackBar2.Minimum = 10;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(272, 45);
            this.trackBar2.SmallChange = 1000;
            this.trackBar2.TabIndex = 231;
            this.trackBar2.TickFrequency = 100;
            this.trackBar2.Value = 10;
            this.trackBar2.ValueChanged += new System.EventHandler(this.toleranceTrackbar_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 232;
            this.label6.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 233;
            this.label7.Text = "0.1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(644, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 234;
            this.label8.Text = "0.0001";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 583);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeElapsedBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label106);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.resultGridView);
            this.Controls.Add(this.tolerance);
            this.Controls.Add(this.label105);
            this.Controls.Add(this.label104);
            this.Controls.Add(this.label103);
            this.Controls.Add(this.label102);
            this.Controls.Add(this.label101);
            this.Controls.Add(this.iterationsBar);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Name = "Form1";
            this.Text = "Gauss-Seidel";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iterationsBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TrackBar iterationsBar;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.DataGridView resultGridView;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label tolerance;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox timeElapsedBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

