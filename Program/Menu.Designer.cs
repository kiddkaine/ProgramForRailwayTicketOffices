
namespace Program
{
    partial class Menu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Список клиентов";
            this.toolTip1.SetToolTip(this.button1, "Поиск и редактирование данных клиента по ID.\r\nВывод всех клиентов в список.\r\n\r\n");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(210, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Список сотрудников";
            this.toolTip1.SetToolTip(this.button2, "Поиск и редактирование данных сотрудника по ID.\r\nВывод всех сотрудников в список." +
        "");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.button3.Location = new System.Drawing.Point(12, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Добавление клиента";
            this.toolTip1.SetToolTip(this.button3, "Добавление нового клиента в базу данных.");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Default;
            this.button4.Location = new System.Drawing.Point(12, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(210, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Удаление клиента";
            this.toolTip1.SetToolTip(this.button4, "Удаление клиента по ID.");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Default;
            this.button5.Location = new System.Drawing.Point(12, 128);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(210, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Добавление сотрудника";
            this.toolTip1.SetToolTip(this.button5, "Добавление нового сотрудника в базу данных.");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Default;
            this.button6.Location = new System.Drawing.Point(12, 157);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(210, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Увольнение сотрудника";
            this.toolTip1.SetToolTip(this.button6, "Увольнение сотрудника по ID.");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Cursor = System.Windows.Forms.Cursors.Default;
            this.button7.Location = new System.Drawing.Point(12, 186);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(210, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "Справочник";
            this.toolTip1.SetToolTip(this.button7, "Поиск информации по номеру маршрута.\r\nРедактирование информации о рейсе.");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Cursor = System.Windows.Forms.Cursors.Default;
            this.button8.Location = new System.Drawing.Point(12, 215);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(210, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "Рейсы";
            this.toolTip1.SetToolTip(this.button8, "Табличное представление базы данных рейсов.\r\nДобавление, удаление, редактирование" +
        " выбранного рейса.");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Cursor = System.Windows.Forms.Cursors.Default;
            this.button9.Location = new System.Drawing.Point(12, 244);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(210, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "Клиенты";
            this.toolTip1.SetToolTip(this.button9, "Табличное представление базы данных клиентов.\r\nДобавление, удаление, редактирован" +
        "ие выбранного клиента.");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Cursor = System.Windows.Forms.Cursors.Default;
            this.button10.Location = new System.Drawing.Point(12, 273);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(210, 23);
            this.button10.TabIndex = 9;
            this.button10.Text = "Сотрудники";
            this.toolTip1.SetToolTip(this.button10, "Табличное представление базы данных сотрудников.\r\nДобавление, увольнение, редакти" +
        "рование выбранного сотрудника.");
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Cursor = System.Windows.Forms.Cursors.Default;
            this.button11.Location = new System.Drawing.Point(12, 302);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(210, 23);
            this.button11.TabIndex = 10;
            this.button11.Text = "Оформление билета";
            this.toolTip1.SetToolTip(this.button11, "Оформление заказа.\r\nБронирование билета на определённого клиента.");
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Информация";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(12, 331);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(210, 23);
            this.button12.TabIndex = 11;
            this.button12.Text = "Заказы";
            this.toolTip1.SetToolTip(this.button12, "Просмотр заказов, вывод информации по ID.\r\nРаспечатка таблицы с заказами.");
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(12, 360);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(210, 23);
            this.button13.TabIndex = 12;
            this.button13.Text = "Билеты";
            this.toolTip1.SetToolTip(this.button13, "Просмотр билетов, вывод информации по ID.\r\nРаспечатка таблицы с билетами.");
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(12, 389);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(210, 23);
            this.button14.TabIndex = 13;
            this.button14.Text = "Закрыть";
            this.toolTip1.SetToolTip(this.button14, "Закрыть приложение.");
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(1)))), ((int)(((byte)(14)))));
            this.ClientSize = new System.Drawing.Size(234, 424);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(250, 462);
            this.MinimumSize = new System.Drawing.Size(250, 462);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
    }
}

