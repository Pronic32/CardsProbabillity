namespace CardsProbability
{
    partial class MainScreen
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstbx_events = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_showProbs = new System.Windows.Forms.Button();
            this.btn_delEvent = new System.Windows.Forms.Button();
            this.btn_addExperience = new System.Windows.Forms.Button();
            this.btn_editEvent = new System.Windows.Forms.Button();
            this.btn_addEvent = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstbx_events
            // 
            this.lstbx_events.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbx_events.FormattingEnabled = true;
            this.lstbx_events.Location = new System.Drawing.Point(3, 16);
            this.lstbx_events.Name = "lstbx_events";
            this.lstbx_events.Size = new System.Drawing.Size(251, 410);
            this.lstbx_events.TabIndex = 5;
            this.lstbx_events.TabStop = false;
            this.lstbx_events.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstbx_events);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 429);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Опыты и события";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_showProbs);
            this.groupBox2.Controls.Add(this.btn_delEvent);
            this.groupBox2.Controls.Add(this.btn_addExperience);
            this.groupBox2.Controls.Add(this.btn_editEvent);
            this.groupBox2.Controls.Add(this.btn_addEvent);
            this.groupBox2.Location = new System.Drawing.Point(264, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 426);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Условие задачи";
            // 
            // btn_showProbs
            // 
            this.btn_showProbs.Enabled = false;
            this.btn_showProbs.Location = new System.Drawing.Point(81, 234);
            this.btn_showProbs.Name = "btn_showProbs";
            this.btn_showProbs.Size = new System.Drawing.Size(173, 45);
            this.btn_showProbs.TabIndex = 4;
            this.btn_showProbs.Text = "Вывести вероятности...";
            this.btn_showProbs.UseVisualStyleBackColor = true;
            this.btn_showProbs.Click += new System.EventHandler(this.btn_showProbs_Click);
            // 
            // btn_delEvent
            // 
            this.btn_delEvent.Enabled = false;
            this.btn_delEvent.Location = new System.Drawing.Point(81, 183);
            this.btn_delEvent.Name = "btn_delEvent";
            this.btn_delEvent.Size = new System.Drawing.Size(173, 45);
            this.btn_delEvent.TabIndex = 3;
            this.btn_delEvent.Text = "Удалить...";
            this.btn_delEvent.UseVisualStyleBackColor = true;
            this.btn_delEvent.Click += new System.EventHandler(this.btn_delEvent_Click);
            // 
            // btn_addExperience
            // 
            this.btn_addExperience.Location = new System.Drawing.Point(81, 30);
            this.btn_addExperience.Name = "btn_addExperience";
            this.btn_addExperience.Size = new System.Drawing.Size(173, 45);
            this.btn_addExperience.TabIndex = 0;
            this.btn_addExperience.Text = "Параметры опыта...";
            this.btn_addExperience.UseVisualStyleBackColor = true;
            this.btn_addExperience.Click += new System.EventHandler(this.btn_addExperience_Click);
            // 
            // btn_editEvent
            // 
            this.btn_editEvent.Enabled = false;
            this.btn_editEvent.Location = new System.Drawing.Point(81, 132);
            this.btn_editEvent.Name = "btn_editEvent";
            this.btn_editEvent.Size = new System.Drawing.Size(173, 45);
            this.btn_editEvent.TabIndex = 2;
            this.btn_editEvent.Text = "Изменить...";
            this.btn_editEvent.UseVisualStyleBackColor = true;
            this.btn_editEvent.Click += new System.EventHandler(this.btn_editEvent_Click);
            // 
            // btn_addEvent
            // 
            this.btn_addEvent.Enabled = false;
            this.btn_addEvent.Location = new System.Drawing.Point(81, 81);
            this.btn_addEvent.Name = "btn_addEvent";
            this.btn_addEvent.Size = new System.Drawing.Size(173, 45);
            this.btn_addEvent.TabIndex = 1;
            this.btn_addEvent.Text = "Добавить событие...";
            this.btn_addEvent.UseVisualStyleBackColor = true;
            this.btn_addEvent.Click += new System.EventHandler(this.btn_addEvent_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 430);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное окно";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstbx_events;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_delEvent;
        private System.Windows.Forms.Button btn_editEvent;
        private System.Windows.Forms.Button btn_addEvent;
        private System.Windows.Forms.Button btn_addExperience;
        private System.Windows.Forms.Button btn_showProbs;
    }
}

