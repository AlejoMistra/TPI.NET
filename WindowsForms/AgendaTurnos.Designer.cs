namespace WindowsForms
{
    partial class AgendaTurnos
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            busquedaGroupBox = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            busquedaFechaLabel = new Label();
            busquedaFechaDateTimePicker = new DateTimePicker();
            busquedaEspecialidadLabel = new Label();
            busquedaEspecialidadComboBox = new ComboBox();
            busquedaProfesionalLabel = new Label();
            busquedaProfesionalComboBox = new ComboBox();
            busquedaPacienteLabel = new Label();
            busquedaPacienteTextBox = new TextBox();
            salaEsperaCheckBox = new CheckBox();
            limpiarFiltrosLinkLabel = new LinkLabel();
            nuevoTurnoButton = new Button();
            turnosDataGridView = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)turnosDataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(busquedaGroupBox, 0, 0);
            tableLayoutPanel1.Controls.Add(turnosDataGridView, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1000, 600);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // busquedaGroupBox
            // 
            busquedaGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            busquedaGroupBox.Controls.Add(tableLayoutPanel2);
            busquedaGroupBox.Dock = DockStyle.Fill;
            busquedaGroupBox.Location = new Point(3, 3);
            busquedaGroupBox.Name = "busquedaGroupBox";
            busquedaGroupBox.Size = new Size(994, 75);
            busquedaGroupBox.TabIndex = 0;
            busquedaGroupBox.TabStop = false;
            busquedaGroupBox.Text = "Búsqueda de Turnos";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 8;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(busquedaFechaLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(busquedaFechaDateTimePicker, 0, 1);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadComboBox, 1, 1);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalComboBox, 2, 1);
            tableLayoutPanel2.Controls.Add(busquedaPacienteLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(busquedaPacienteTextBox, 3, 1);
            tableLayoutPanel2.Controls.Add(salaEsperaCheckBox, 4, 1);
            tableLayoutPanel2.Controls.Add(limpiarFiltrosLinkLabel, 6, 1);
            tableLayoutPanel2.Controls.Add(nuevoTurnoButton, 7, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(988, 53);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // busquedaFechaLabel
            // 
            busquedaFechaLabel.AutoSize = true;
            busquedaFechaLabel.Location = new Point(3, 0);
            busquedaFechaLabel.Name = "busquedaFechaLabel";
            busquedaFechaLabel.Size = new Size(38, 15);
            busquedaFechaLabel.TabIndex = 0;
            busquedaFechaLabel.Text = "Fecha";
            // 
            // busquedaFechaDateTimePicker
            // 
            busquedaFechaDateTimePicker.Format = DateTimePickerFormat.Short;
            busquedaFechaDateTimePicker.Location = new Point(3, 18);
            busquedaFechaDateTimePicker.Name = "busquedaFechaDateTimePicker";
            busquedaFechaDateTimePicker.Size = new Size(130, 23);
            busquedaFechaDateTimePicker.TabIndex = 1;
            busquedaFechaDateTimePicker.ValueChanged += FiltrarDataGridView;
            busquedaFechaDateTimePicker.BindingContextChanged += FiltrarDataGridView;
            // 
            // busquedaEspecialidadLabel
            // 
            busquedaEspecialidadLabel.AutoSize = true;
            busquedaEspecialidadLabel.Location = new Point(139, 0);
            busquedaEspecialidadLabel.Name = "busquedaEspecialidadLabel";
            busquedaEspecialidadLabel.Size = new Size(72, 15);
            busquedaEspecialidadLabel.TabIndex = 2;
            busquedaEspecialidadLabel.Text = "Especialidad";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(139, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(160, 23);
            busquedaEspecialidadComboBox.TabIndex = 2;
            busquedaEspecialidadComboBox.SelectedIndexChanged += BusquedaEspecialidadComboBox_SelectedIndexChanged;
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaProfesionalLabel
            // 
            busquedaProfesionalLabel.AutoSize = true;
            busquedaProfesionalLabel.Location = new Point(305, 0);
            busquedaProfesionalLabel.Name = "busquedaProfesionalLabel";
            busquedaProfesionalLabel.Size = new Size(77, 15);
            busquedaProfesionalLabel.TabIndex = 3;
            busquedaProfesionalLabel.Text = "Profesionales";
            // 
            // busquedaProfesionalComboBox
            // 
            busquedaProfesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaProfesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaProfesionalComboBox.FormattingEnabled = true;
            busquedaProfesionalComboBox.Location = new Point(305, 18);
            busquedaProfesionalComboBox.Name = "busquedaProfesionalComboBox";
            busquedaProfesionalComboBox.Size = new Size(180, 23);
            busquedaProfesionalComboBox.TabIndex = 2;
            busquedaProfesionalComboBox.SelectedIndexChanged += BusquedaProfesionalComboBox_SelectedIndexChanged;
            busquedaProfesionalComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaPacienteLabel
            // 
            busquedaPacienteLabel.AutoSize = true;
            busquedaPacienteLabel.Location = new Point(491, 0);
            busquedaPacienteLabel.Name = "busquedaPacienteLabel";
            busquedaPacienteLabel.Size = new Size(90, 15);
            busquedaPacienteLabel.TabIndex = 3;
            busquedaPacienteLabel.Text = "Buscar Paciente";
            // 
            // busquedaPacienteTextBox
            // 
            busquedaPacienteTextBox.Location = new Point(491, 18);
            busquedaPacienteTextBox.Name = "busquedaPacienteTextBox";
            busquedaPacienteTextBox.PlaceholderText = "Nombre o DNI...";
            busquedaPacienteTextBox.Size = new Size(160, 23);
            busquedaPacienteTextBox.TabIndex = 3;
            busquedaPacienteTextBox.TextChanged += FiltrarDataGridView;
            // 
            // salaEsperaCheckBox
            // 
            salaEsperaCheckBox.Anchor = AnchorStyles.Left;
            salaEsperaCheckBox.AutoSize = true;
            salaEsperaCheckBox.Location = new Point(657, 24);
            salaEsperaCheckBox.Name = "salaEsperaCheckBox";
            salaEsperaCheckBox.Size = new Size(161, 19);
            salaEsperaCheckBox.TabIndex = 4;
            salaEsperaCheckBox.Text = "Sala de espera (Presentes)";
            salaEsperaCheckBox.UseVisualStyleBackColor = true;
            salaEsperaCheckBox.CheckedChanged += FiltrarDataGridView;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(824, 26);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 6;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // nuevoTurnoButton
            // 
            nuevoTurnoButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nuevoTurnoButton.AutoSize = true;
            nuevoTurnoButton.Cursor = Cursors.Hand;
            nuevoTurnoButton.Location = new Point(910, 18);
            nuevoTurnoButton.Name = "nuevoTurnoButton";
            nuevoTurnoButton.Size = new Size(97, 25);
            nuevoTurnoButton.TabIndex = 7;
            nuevoTurnoButton.Text = "Nuevo Turno";
            nuevoTurnoButton.UseVisualStyleBackColor = true;
            nuevoTurnoButton.Click += nuevoTurnoButton_Click;
            // 
            // turnosDataGridView
            // 
            turnosDataGridView.AllowUserToAddRows = false;
            turnosDataGridView.AllowUserToDeleteRows = false;
            turnosDataGridView.AllowUserToOrderColumns = true;
            turnosDataGridView.AllowUserToResizeRows = false;
            turnosDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            turnosDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            turnosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            turnosDataGridView.Cursor = Cursors.Hand;
            turnosDataGridView.Dock = DockStyle.Fill;
            turnosDataGridView.EnableHeadersVisualStyles = false;
            turnosDataGridView.Location = new Point(3, 84);
            turnosDataGridView.Name = "turnosDataGridView";
            turnosDataGridView.RowHeadersVisible = false;
            turnosDataGridView.Size = new Size(994, 513);
            turnosDataGridView.TabIndex = 1;
            turnosDataGridView.CellContentClick += TurnosDataGridView_CellContentClick;
            turnosDataGridView.CellFormatting += TurnosDataGridView_CellFormatting;
            turnosDataGridView.CellMouseDown += TurnosDataGridView_CellMouseDown;
            turnosDataGridView.CellToolTipTextNeeded += TurnosDataGridView_CellToolTipTextNeeded;
            turnosDataGridView.DataBindingComplete += TurnosDataGridView_DataBindingComplete;
            // 
            // AgendaTurnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "AgendaTurnos";
            Size = new Size(1000, 600);
            Load += Turnos_Load;
            tableLayoutPanel1.ResumeLayout(false);
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)turnosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox busquedaGroupBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label busquedaFechaLabel;
        private DateTimePicker busquedaFechaDateTimePicker;
        private Label busquedaEspecialidadLabel;
        private ComboBox busquedaEspecialidadComboBox;
        private Label busquedaProfesionalLabel;
        private ComboBox busquedaProfesionalComboBox;
        private Label busquedaPacienteLabel;
        private TextBox busquedaPacienteTextBox;
        private CheckBox salaEsperaCheckBox;
        private LinkLabel limpiarFiltrosLinkLabel;
        private Button nuevoTurnoButton;
        private DataGridView turnosDataGridView;
    }
}
