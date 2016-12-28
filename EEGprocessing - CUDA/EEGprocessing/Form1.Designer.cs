namespace EEGprocessing
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьФильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Load0genOfFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CalcAndCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.print0genFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.rOCанализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadRoc1 = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadROC2 = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadROCFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.ROCcalc = new System.Windows.Forms.ToolStripMenuItem();
            this.контрольнаяПроверкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.drawFilterById = new System.Windows.Forms.Button();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.addNewFilterToGraph = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ChBoxFlikker = new System.Windows.Forms.CheckBox();
            this.chBoxBlue = new System.Windows.Forms.CheckBox();
            this.chBoxCrossingover = new System.Windows.Forms.CheckBox();
            this.chBoxPrintConv = new System.Windows.Forms.CheckBox();
            this.chBoxCUDA = new System.Windows.Forms.CheckBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.действияToolStripMenuItem,
            this.печатьToolStripMenuItem,
            this.rOCанализToolStripMenuItem,
            this.контрольнаяПроверкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadMainToolStripMenuItem,
            this.addToCompare,
            this.загрузитьФильтрыToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // LoadMainToolStripMenuItem
            // 
            this.LoadMainToolStripMenuItem.Name = "LoadMainToolStripMenuItem";
            this.LoadMainToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.LoadMainToolStripMenuItem.Text = "Загрузить основное";
            this.LoadMainToolStripMenuItem.Click += new System.EventHandler(this.загрузитьОсновныеToolStripMenuItem_Click);
            // 
            // addToCompare
            // 
            this.addToCompare.Name = "addToCompare";
            this.addToCompare.Size = new System.Drawing.Size(209, 22);
            this.addToCompare.Text = "Добавить для сравнения";
            this.addToCompare.Click += new System.EventHandler(this.addToCompare_Click);
            // 
            // загрузитьФильтрыToolStripMenuItem
            // 
            this.загрузитьФильтрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Load0genOfFilterToolStripMenuItem});
            this.загрузитьФильтрыToolStripMenuItem.Name = "загрузитьФильтрыToolStripMenuItem";
            this.загрузитьФильтрыToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.загрузитьФильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // Load0genOfFilterToolStripMenuItem
            // 
            this.Load0genOfFilterToolStripMenuItem.Name = "Load0genOfFilterToolStripMenuItem";
            this.Load0genOfFilterToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.Load0genOfFilterToolStripMenuItem.Text = "Загрузить 0-е поколение";
            this.Load0genOfFilterToolStripMenuItem.Click += new System.EventHandler(this.Load0genOfFilterToolStripMenuItem_Click);
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CalcAndCreate});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(70, 22);
            this.действияToolStripMenuItem.Text = "Действия";
            // 
            // CalcAndCreate
            // 
            this.CalcAndCreate.Enabled = false;
            this.CalcAndCreate.Name = "CalcAndCreate";
            this.CalcAndCreate.Size = new System.Drawing.Size(226, 22);
            this.CalcAndCreate.Text = "Посчитать и сгенерировать";
            this.CalcAndCreate.Click += new System.EventHandler(this.CalcAndCreate_Click);
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фильтрыToolStripMenuItem});
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(58, 22);
            this.печатьToolStripMenuItem.Text = "Печать";
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.print0genFilter});
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // print0genFilter
            // 
            this.print0genFilter.Name = "print0genFilter";
            this.print0genFilter.Size = new System.Drawing.Size(160, 22);
            this.print0genFilter.Text = "0-го поколения";
            this.print0genFilter.Click += new System.EventHandler(this.print0genToolStripMenuItem_Click);
            // 
            // rOCанализToolStripMenuItem
            // 
            this.rOCанализToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadRoc1,
            this.LoadROC2,
            this.LoadROCFilter,
            this.ROCcalc});
            this.rOCанализToolStripMenuItem.Name = "rOCанализToolStripMenuItem";
            this.rOCанализToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.rOCанализToolStripMenuItem.Text = "ROC-анализ";
            // 
            // LoadRoc1
            // 
            this.LoadRoc1.Name = "LoadRoc1";
            this.LoadRoc1.Size = new System.Drawing.Size(203, 22);
            this.LoadRoc1.Text = "Загрузить истиные 1";
            this.LoadRoc1.Click += new System.EventHandler(this.LoadRoc1_Click);
            // 
            // LoadROC2
            // 
            this.LoadROC2.Name = "LoadROC2";
            this.LoadROC2.Size = new System.Drawing.Size(203, 22);
            this.LoadROC2.Text = "Загрузить истиные 2";
            this.LoadROC2.Click += new System.EventHandler(this.LoadROC2_Click);
            // 
            // LoadROCFilter
            // 
            this.LoadROCFilter.Name = "LoadROCFilter";
            this.LoadROCFilter.Size = new System.Drawing.Size(203, 22);
            this.LoadROCFilter.Text = "Загрузить фильтр";
            this.LoadROCFilter.Click += new System.EventHandler(this.LoadROCFilter_Click);
            // 
            // ROCcalc
            // 
            this.ROCcalc.Name = "ROCcalc";
            this.ROCcalc.Size = new System.Drawing.Size(203, 22);
            this.ROCcalc.Text = "Провести ROC - анализ";
            this.ROCcalc.Click += new System.EventHandler(this.ROCcalc_Click);
            // 
            // контрольнаяПроверкаToolStripMenuItem
            // 
            this.контрольнаяПроверкаToolStripMenuItem.Name = "контрольнаяПроверкаToolStripMenuItem";
            this.контрольнаяПроверкаToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.контрольнаяПроверкаToolStripMenuItem.Text = "Контрольная проверка";
            this.контрольнаяПроверкаToolStripMenuItem.Click += new System.EventHandler(this.контрольнаяПроверкаToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "CSV format|*.csv";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.chart1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 463);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(184, 3);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(727, 225);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.drawFilterById, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown4, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.addNewFilterToGraph, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDown3, 0, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(178, 225);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id фильтра";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(2, 24);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(174, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // drawFilterById
            // 
            this.drawFilterById.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawFilterById.Location = new System.Drawing.Point(2, 56);
            this.drawFilterById.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.drawFilterById.Name = "drawFilterById";
            this.drawFilterById.Size = new System.Drawing.Size(174, 15);
            this.drawFilterById.TabIndex = 2;
            this.drawFilterById.Text = "Добавить новый фильтр";
            this.drawFilterById.UseVisualStyleBackColor = true;
            this.drawFilterById.Click += new System.EventHandler(this.drawFilterById_Click);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown4.Location = new System.Drawing.Point(2, 203);
            this.numericUpDown4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(174, 20);
            this.numericUpDown4.TabIndex = 4;
            this.numericUpDown4.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // addNewFilterToGraph
            // 
            this.addNewFilterToGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addNewFilterToGraph.Location = new System.Drawing.Point(2, 77);
            this.addNewFilterToGraph.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.addNewFilterToGraph.Name = "addNewFilterToGraph";
            this.addNewFilterToGraph.Size = new System.Drawing.Size(174, 15);
            this.addNewFilterToGraph.TabIndex = 3;
            this.addNewFilterToGraph.Text = "Очистить все ";
            this.addNewFilterToGraph.UseVisualStyleBackColor = true;
            this.addNewFilterToGraph.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(2, 179);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Кол-во в поколении";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(2, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Номер канала";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Location = new System.Drawing.Point(2, 119);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(174, 20);
            this.numericUpDown2.TabIndex = 6;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(2, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Кол-во поколений";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown3.Location = new System.Drawing.Point(2, 161);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1001,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(174, 20);
            this.numericUpDown3.TabIndex = 8;
            this.numericUpDown3.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // chart2
            // 
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart2.Location = new System.Drawing.Point(184, 234);
            this.chart2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(727, 226);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.ChBoxFlikker, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chBoxBlue, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.chBoxCrossingover, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.chBoxPrintConv, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.chBoxCUDA, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.radioButton3, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.radioButton2, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.radioButton1, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown5, 0, 5);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 234);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(178, 226);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // ChBoxFlikker
            // 
            this.ChBoxFlikker.AutoSize = true;
            this.ChBoxFlikker.Location = new System.Drawing.Point(2, 3);
            this.ChBoxFlikker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ChBoxFlikker.Name = "ChBoxFlikker";
            this.ChBoxFlikker.Size = new System.Drawing.Size(97, 16);
            this.ChBoxFlikker.TabIndex = 0;
            this.ChBoxFlikker.Text = "Фликкер шум";
            this.ChBoxFlikker.UseVisualStyleBackColor = true;
            // 
            // chBoxBlue
            // 
            this.chBoxBlue.AutoSize = true;
            this.chBoxBlue.Location = new System.Drawing.Point(2, 25);
            this.chBoxBlue.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chBoxBlue.Name = "chBoxBlue";
            this.chBoxBlue.Size = new System.Drawing.Size(81, 16);
            this.chBoxBlue.TabIndex = 1;
            this.chBoxBlue.Text = "Синий шум";
            this.chBoxBlue.UseVisualStyleBackColor = true;
            // 
            // chBoxCrossingover
            // 
            this.chBoxCrossingover.AutoSize = true;
            this.chBoxCrossingover.Location = new System.Drawing.Point(2, 47);
            this.chBoxCrossingover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chBoxCrossingover.Name = "chBoxCrossingover";
            this.chBoxCrossingover.Size = new System.Drawing.Size(98, 16);
            this.chBoxCrossingover.TabIndex = 2;
            this.chBoxCrossingover.Text = "Кроссинговер";
            this.chBoxCrossingover.UseVisualStyleBackColor = true;
            // 
            // chBoxPrintConv
            // 
            this.chBoxPrintConv.AutoSize = true;
            this.chBoxPrintConv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chBoxPrintConv.Location = new System.Drawing.Point(2, 91);
            this.chBoxPrintConv.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chBoxPrintConv.Name = "chBoxPrintConv";
            this.chBoxPrintConv.Size = new System.Drawing.Size(174, 16);
            this.chBoxPrintConv.TabIndex = 9;
            this.chBoxPrintConv.Text = "Печать свертки";
            this.chBoxPrintConv.UseVisualStyleBackColor = true;
            // 
            // chBoxCUDA
            // 
            this.chBoxCUDA.AutoSize = true;
            this.chBoxCUDA.Checked = true;
            this.chBoxCUDA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxCUDA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chBoxCUDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chBoxCUDA.Location = new System.Drawing.Point(2, 135);
            this.chBoxCUDA.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chBoxCUDA.Name = "chBoxCUDA";
            this.chBoxCUDA.Size = new System.Drawing.Size(174, 16);
            this.chBoxCUDA.TabIndex = 11;
            this.chBoxCUDA.Text = "via CUDA";
            this.chBoxCUDA.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton3.Location = new System.Drawing.Point(2, 201);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(174, 22);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Среднее среди ABS(M1-M2)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton2.Location = new System.Drawing.Point(2, 179);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(174, 16);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = "Среднее среди MAX разниц";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton1.Location = new System.Drawing.Point(2, 157);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(174, 16);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.Text = "Среднее среди средних раз.";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(2, 112);
            this.numericUpDown5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown5.TabIndex = 15;
            this.numericUpDown5.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 487);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьФильтрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Load0genOfFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem print0genFilter;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button drawFilterById;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Button addNewFilterToGraph;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem addToCompare;
        private System.Windows.Forms.ToolStripMenuItem CalcAndCreate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.CheckBox chBoxPrintConv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox ChBoxFlikker;
        private System.Windows.Forms.CheckBox chBoxBlue;
        private System.Windows.Forms.CheckBox chBoxCrossingover;
        private System.Windows.Forms.ToolStripMenuItem rOCанализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadRoc1;
        private System.Windows.Forms.ToolStripMenuItem LoadROC2;
        private System.Windows.Forms.ToolStripMenuItem LoadROCFilter;
        private System.Windows.Forms.ToolStripMenuItem ROCcalc;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chBoxCUDA;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ToolStripMenuItem контрольнаяПроверкаToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
    }
}

