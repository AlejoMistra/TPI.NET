namespace WindowsForms
{
    partial class AsignacionTurno
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
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            especialidadComboBox = new ComboBox();
            ProfesionalComboBox = new ComboBox();
            fechaDesdeDateTimePicker = new DateTimePicker();
            label5 = new Label();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            label6 = new Label();
            groupBox2 = new GroupBox();
            patientTableLayoutPanel = new TableLayoutPanel();
            label7 = new Label();
            searchTableLayoutPanel = new TableLayoutPanel();
            busquedaTextBox = new TextBox();
            button4 = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            nombreTextBox = new TextBox();
            apellidoTextBox = new TextBox();
            label11 = new Label();
            label12 = new Label();
            documentoTextBox = new TextBox();
            telefonoTextBox = new TextBox();
            label13 = new Label();
            label14 = new Label();
            motivoTextBox = new TextBox();
            label15 = new Label();
            observacionesTextBox = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            patientTableLayoutPanel.SuspendLayout();
            searchTableLayoutPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1MinSize = 450;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Panel2MinSize = 350;
            splitContainer1.Size = new Size(1034, 555);
            splitContainer1.SplitterDistance = 580;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(580, 555);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "1. Búsqueda y Selección de Turno Disponible";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel3.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel3.Controls.Add(label6, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(574, 533);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label3, 1, 0);
            tableLayoutPanel2.Controls.Add(label4, 2, 0);
            tableLayoutPanel2.Controls.Add(especialidadComboBox, 0, 1);
            tableLayoutPanel2.Controls.Add(ProfesionalComboBox, 1, 1);
            tableLayoutPanel2.Controls.Add(fechaDesdeDateTimePicker, 2, 1);
            tableLayoutPanel2.Controls.Add(label5, 0, 2);
            tableLayoutPanel2.Controls.Add(button3, 2, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(568, 75);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 0;
            label2.Text = "Especialidad";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(192, 0);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 1;
            label3.Text = "Profesional";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(381, 0);
            label4.Name = "label4";
            label4.Size = new Size(73, 15);
            label4.TabIndex = 2;
            label4.Text = "Fecha Desde";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.Dock = DockStyle.Fill;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(3, 18);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(183, 23);
            especialidadComboBox.TabIndex = 3;
            // 
            // ProfesionalComboBox
            // 
            ProfesionalComboBox.Dock = DockStyle.Fill;
            ProfesionalComboBox.FormattingEnabled = true;
            ProfesionalComboBox.Location = new Point(192, 18);
            ProfesionalComboBox.Name = "ProfesionalComboBox";
            ProfesionalComboBox.Size = new Size(183, 23);
            ProfesionalComboBox.TabIndex = 4;
            // 
            // fechaDesdeDateTimePicker
            // 
            fechaDesdeDateTimePicker.Dock = DockStyle.Fill;
            fechaDesdeDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaDesdeDateTimePicker.Location = new Point(381, 18);
            fechaDesdeDateTimePicker.Name = "fechaDesdeDateTimePicker";
            fechaDesdeDateTimePicker.Size = new Size(184, 23);
            fechaDesdeDateTimePicker.TabIndex = 5;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label5, 2);
            label5.Location = new Point(3, 52);
            label5.Name = "label5";
            label5.Size = new Size(356, 15);
            label5.TabIndex = 6;
            label5.Text = "Mostrando únicamente turnos precargados con estado Disponible";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Right;
            button3.Location = new Point(453, 47);
            button3.Name = "button3";
            button3.Size = new Size(112, 25);
            button3.TabIndex = 7;
            button3.Text = "Buscar Turnos";
            button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 84);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(568, 406);
            dataGridView1.TabIndex = 1;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(235, 245, 255);
            label6.Dock = DockStyle.Fill;
            label6.ForeColor = Color.FromArgb(0, 70, 150);
            label6.Location = new Point(3, 497);
            label6.Margin = new Padding(3, 4, 3, 3);
            label6.Name = "label6";
            label6.Padding = new Padding(8, 0, 8, 0);
            label6.Size = new Size(568, 33);
            label6.TabIndex = 2;
            label6.Text = "Turno Seleccionado: {fecha} - {hora} | {Profesional.Nombre} ({Especialidad.Nombre})";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(patientTableLayoutPanel);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(8, 10, 8, 8);
            groupBox2.Size = new Size(450, 555);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "2. Paciente y Asignación";
            // 
            // patientTableLayoutPanel
            // 
            patientTableLayoutPanel.ColumnCount = 2;
            patientTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            patientTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            patientTableLayoutPanel.Controls.Add(label7, 0, 0);
            patientTableLayoutPanel.Controls.Add(searchTableLayoutPanel, 0, 1);
            patientTableLayoutPanel.Controls.Add(label8, 0, 2);
            patientTableLayoutPanel.Controls.Add(label9, 0, 3);
            patientTableLayoutPanel.Controls.Add(label10, 1, 3);
            patientTableLayoutPanel.Controls.Add(nombreTextBox, 0, 4);
            patientTableLayoutPanel.Controls.Add(apellidoTextBox, 1, 4);
            patientTableLayoutPanel.Controls.Add(label11, 0, 5);
            patientTableLayoutPanel.Controls.Add(label12, 1, 5);
            patientTableLayoutPanel.Controls.Add(documentoTextBox, 0, 6);
            patientTableLayoutPanel.Controls.Add(telefonoTextBox, 1, 6);
            patientTableLayoutPanel.Controls.Add(label13, 0, 7);
            patientTableLayoutPanel.Controls.Add(label14, 0, 8);
            patientTableLayoutPanel.Controls.Add(motivoTextBox, 0, 9);
            patientTableLayoutPanel.Controls.Add(label15, 0, 10);
            patientTableLayoutPanel.Controls.Add(observacionesTextBox, 0, 11);
            patientTableLayoutPanel.Dock = DockStyle.Fill;
            patientTableLayoutPanel.Location = new Point(8, 26);
            patientTableLayoutPanel.Name = "patientTableLayoutPanel";
            patientTableLayoutPanel.RowCount = 13;
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle());
            patientTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            patientTableLayoutPanel.Size = new Size(434, 521);
            patientTableLayoutPanel.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            patientTableLayoutPanel.SetColumnSpan(label7, 2);
            label7.Location = new Point(3, 0);
            label7.Margin = new Padding(3, 0, 3, 2);
            label7.Name = "label7";
            label7.Size = new Size(196, 15);
            label7.TabIndex = 0;
            label7.Text = "Búsqueda rápida por DNI o Apellido";
            // 
            // searchTableLayoutPanel
            // 
            searchTableLayoutPanel.AutoSize = true;
            searchTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            searchTableLayoutPanel.ColumnCount = 2;
            patientTableLayoutPanel.SetColumnSpan(searchTableLayoutPanel, 2);
            searchTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            searchTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            searchTableLayoutPanel.Controls.Add(busquedaTextBox, 0, 0);
            searchTableLayoutPanel.Controls.Add(button4, 1, 0);
            searchTableLayoutPanel.Dock = DockStyle.Fill;
            searchTableLayoutPanel.Location = new Point(0, 17);
            searchTableLayoutPanel.Margin = new Padding(0);
            searchTableLayoutPanel.Name = "searchTableLayoutPanel";
            searchTableLayoutPanel.RowCount = 1;
            searchTableLayoutPanel.RowStyles.Add(new RowStyle());
            searchTableLayoutPanel.Size = new Size(434, 29);
            searchTableLayoutPanel.TabIndex = 1;
            // 
            // busquedaTextBox
            // 
            busquedaTextBox.Dock = DockStyle.Fill;
            busquedaTextBox.Location = new Point(3, 3);
            busquedaTextBox.Name = "busquedaTextBox";
            busquedaTextBox.PlaceholderText = "Ingrese DNI o apellido...";
            busquedaTextBox.Size = new Size(347, 23);
            busquedaTextBox.TabIndex = 0;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Right;
            button4.AutoSize = true;
            button4.Location = new Point(356, 2);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(75, 25);
            button4.TabIndex = 1;
            button4.Text = "Buscar";
            button4.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.BackColor = Color.FromArgb(235, 247, 238);
            patientTableLayoutPanel.SetColumnSpan(label8, 2);
            label8.Dock = DockStyle.Fill;
            label8.ForeColor = Color.FromArgb(40, 120, 60);
            label8.Location = new Point(3, 50);
            label8.Margin = new Padding(3, 4, 3, 8);
            label8.Name = "label8";
            label8.Padding = new Padding(6, 0, 6, 0);
            label8.Size = new Size(428, 26);
            label8.TabIndex = 2;
            label8.Text = "Paciente encontrado: {Apellido y Nombre} ({DNI})";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 84);
            label9.Margin = new Padding(3, 0, 3, 2);
            label9.Name = "label9";
            label9.Size = new Size(51, 15);
            label9.TabIndex = 3;
            label9.Text = "Nombre";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(220, 84);
            label10.Margin = new Padding(3, 0, 3, 2);
            label10.Name = "label10";
            label10.Size = new Size(51, 15);
            label10.TabIndex = 4;
            label10.Text = "Apellido";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Dock = DockStyle.Fill;
            nombreTextBox.Location = new Point(3, 103);
            nombreTextBox.Margin = new Padding(3, 2, 3, 6);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(211, 23);
            nombreTextBox.TabIndex = 5;
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Dock = DockStyle.Fill;
            apellidoTextBox.Location = new Point(220, 103);
            apellidoTextBox.Margin = new Padding(3, 2, 3, 6);
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(211, 23);
            apellidoTextBox.TabIndex = 6;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(3, 132);
            label11.Margin = new Padding(3, 0, 3, 2);
            label11.Name = "label11";
            label11.Size = new Size(124, 15);
            label11.TabIndex = 7;
            label11.Text = "Nro Documento (DNI)";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(220, 132);
            label12.Margin = new Padding(3, 0, 3, 2);
            label12.Name = "label12";
            label12.Size = new Size(53, 15);
            label12.TabIndex = 8;
            label12.Text = "Teléfono";
            // 
            // documentoTextBox
            // 
            documentoTextBox.Dock = DockStyle.Fill;
            documentoTextBox.Location = new Point(3, 151);
            documentoTextBox.Margin = new Padding(3, 2, 3, 6);
            documentoTextBox.Name = "documentoTextBox";
            documentoTextBox.Size = new Size(211, 23);
            documentoTextBox.TabIndex = 9;
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Dock = DockStyle.Fill;
            telefonoTextBox.Location = new Point(220, 151);
            telefonoTextBox.Margin = new Padding(3, 2, 3, 6);
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(211, 23);
            telefonoTextBox.TabIndex = 10;
            // 
            // label13
            // 
            label13.AutoSize = true;
            patientTableLayoutPanel.SetColumnSpan(label13, 2);
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label13.ForeColor = Color.FromArgb(0, 102, 204);
            label13.Location = new Point(3, 190);
            label13.Margin = new Padding(3, 10, 3, 4);
            label13.Name = "label13";
            label13.Size = new Size(195, 15);
            label13.TabIndex = 11;
            label13.Text = "DATOS ADICIONALES DEL TURNO";
            // 
            // label14
            // 
            label14.AutoSize = true;
            patientTableLayoutPanel.SetColumnSpan(label14, 2);
            label14.Location = new Point(3, 209);
            label14.Margin = new Padding(3, 0, 3, 2);
            label14.Name = "label14";
            label14.Size = new Size(111, 15);
            label14.TabIndex = 12;
            label14.Text = "Motivo de Consulta";
            // 
            // motivoTextBox
            // 
            patientTableLayoutPanel.SetColumnSpan(motivoTextBox, 2);
            motivoTextBox.Dock = DockStyle.Fill;
            motivoTextBox.Location = new Point(3, 228);
            motivoTextBox.Margin = new Padding(3, 2, 3, 6);
            motivoTextBox.Name = "motivoTextBox";
            motivoTextBox.Size = new Size(428, 23);
            motivoTextBox.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            patientTableLayoutPanel.SetColumnSpan(label15, 2);
            label15.Location = new Point(3, 257);
            label15.Margin = new Padding(3, 0, 3, 2);
            label15.Name = "label15";
            label15.Size = new Size(161, 15);
            label15.TabIndex = 14;
            label15.Text = "Observaciones / Indicaciones";
            // 
            // observacionesTextBox
            // 
            patientTableLayoutPanel.SetColumnSpan(observacionesTextBox, 2);
            observacionesTextBox.Dock = DockStyle.Top;
            observacionesTextBox.Location = new Point(3, 276);
            observacionesTextBox.Margin = new Padding(3, 2, 3, 4);
            observacionesTextBox.Multiline = true;
            observacionesTextBox.Name = "observacionesTextBox";
            observacionesTextBox.PlaceholderText = "Ej: Trae libreta de vacunación y análisis previos...";
            observacionesTextBox.ScrollBars = ScrollBars.Vertical;
            observacionesTextBox.Size = new Size(428, 72);
            observacionesTextBox.TabIndex = 15;
            observacionesTextBox.TextChanged += observacionesTextBox_TextChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(button2, 1, 0);
            tableLayoutPanel1.Controls.Add(button1, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 555);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(8, 6, 8, 6);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1034, 45);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(11, 15);
            label1.Name = "label1";
            label1.Size = new Size(269, 15);
            label1.TabIndex = 0;
            label1.Text = "Presione Enter para confirmar o Esc para cancelar.";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Right;
            button2.Location = new Point(738, 9);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(85, 27);
            button2.TabIndex = 1;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.AutoSize = true;
            button1.Location = new Point(831, 9);
            button1.Margin = new Padding(4, 3, 0, 3);
            button1.Name = "button1";
            button1.Size = new Size(195, 27);
            button1.TabIndex = 2;
            button1.Text = "Confirmar y Asignar Turno";
            button1.UseVisualStyleBackColor = true;
            // 
            // AsignacionTurno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 600);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(950, 550);
            Name = "AsignacionTurno";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Asignar Turno a Paciente";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            patientTableLayoutPanel.ResumeLayout(false);
            patientTableLayoutPanel.PerformLayout();
            searchTableLayoutPanel.ResumeLayout(false);
            searchTableLayoutPanel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Label label1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox especialidadComboBox;
        private ComboBox ProfesionalComboBox;
        private DateTimePicker fechaDesdeDateTimePicker;
        private Label label5;
        private Button button3;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView dataGridView1;
        private Label label6;
        private TableLayoutPanel patientTableLayoutPanel;
        private TableLayoutPanel searchTableLayoutPanel;
        private Label label7;
        private TextBox busquedaTextBox;
        private Button button4;
        private Label label8;
        private Label label9;
        private TextBox nombreTextBox;
        private Label label10;
        private TextBox apellidoTextBox;
        private Label label11;
        private TextBox documentoTextBox;
        private Label label12;
        private TextBox telefonoTextBox;
        private Label label13;
        private Label label14;
        private TextBox motivoTextBox;
        private Label label15;
        private TextBox observacionesTextBox;
    }
}