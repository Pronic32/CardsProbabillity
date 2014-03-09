namespace CardsProbability
{
    partial class ExperienceForm
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
      this.nupd_extracted = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.cmbx_total = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.nupd_extracted)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(29, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(146, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Введите параметры опыта:";
      // 
      // nupd_extracted
      // 
      this.nupd_extracted.Location = new System.Drawing.Point(177, 39);
      this.nupd_extracted.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
      this.nupd_extracted.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nupd_extracted.Name = "nupd_extracted";
      this.nupd_extracted.Size = new System.Drawing.Size(39, 20);
      this.nupd_extracted.TabIndex = 0;
      this.nupd_extracted.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 39);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(141, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Количество вынутых карт:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 67);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(146, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Введите параметры опыта:";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(141, 100);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "Готово";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // cmbx_total
      // 
      this.cmbx_total.FormattingEnabled = true;
      this.cmbx_total.Items.AddRange(new object[] {
            "36",
            "52"});
      this.cmbx_total.Location = new System.Drawing.Point(177, 64);
      this.cmbx_total.Name = "cmbx_total";
      this.cmbx_total.Size = new System.Drawing.Size(39, 21);
      this.cmbx_total.TabIndex = 1;
      this.cmbx_total.Text = "36";
      this.cmbx_total.SelectedIndexChanged += new System.EventHandler(this.cmbx_total_SelectedIndexChanged);
      // 
      // ExperienceForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(224, 127);
      this.Controls.Add(this.cmbx_total);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.nupd_extracted);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ExperienceForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Параметры опыта...";
      this.Load += new System.EventHandler(this.Probability_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nupd_extracted)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nupd_extracted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbx_total;
    }
}