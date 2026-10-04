namespace WindowsForms
{
    partial class Facturacion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            busquedaGroupBox = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            fechaDesdeLabel = new Label();
            fechaDesdeDateTimePicker = new DateTimePicker();
            fechaHastaLabel = new Label();
            fechaHastaDateTimePicker = new DateTimePicker();
            busquedaEspecialidadLabel = new Label();
            busquedaEspecialidadComboBox = new ComboBox();
            busquedaProfesionalLabel = new Label();
            busquedaProfesionalComboBox = new ComboBox();
            busquedaPacienteLabel = new Label();
            busquedaPacienteTextBox = new TextBox();
            estadoFacturacionLabel = new Label();
            estadoFacturacionComboBox = new ComboBox();
            limpiarFiltrosLinkLabel = new LinkLabel();
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
            busquedaGroupBox.Text = "Filtros de Facturación de Turnos";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 7;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(fechaDesdeLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(fechaDesdeDateTimePicker, 0, 1);
            tableLayoutPanel2.Controls.Add(fechaHastaLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(fechaHastaDateTimePicker, 1, 1);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadComboBox, 2, 1);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalComboBox, 3, 1);
            tableLayoutPanel2.Controls.Add(busquedaPacienteLabel, 4, 0);
            tableLayoutPanel2.Controls.Add(busquedaPacienteTextBox, 4, 1);
            tableLayoutPanel2.Controls.Add(estadoFacturacionLabel, 5, 0);
            tableLayoutPanel2.Controls.Add(estadoFacturacionComboBox, 5, 1);
            tableLayoutPanel2.Controls.Add(limpiarFiltrosLinkLabel, 6, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(988, 53);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // fechaDesdeLabel
            // 
            fechaDesdeLabel.AutoSize = true;
            fechaDesdeLabel.Location = new Point(3, 0);
            fechaDesdeLabel.Name = "fechaDesdeLabel";
            fechaDesdeLabel.Size = new Size(74, 15);
            fechaDesdeLabel.TabIndex = 0;
            fechaDesdeLabel.Text = "Fecha Desde";
            // 
            // fechaDesdeDateTimePicker
            // 
            fechaDesdeDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaDesdeDateTimePicker.Location = new Point(3, 18);
            fechaDesdeDateTimePicker.Name = "fechaDesdeDateTimePicker";
            fechaDesdeDateTimePicker.Size = new Size(120, 23);
            fechaDesdeDateTimePicker.TabIndex = 1;
            fechaDesdeDateTimePicker.ValueChanged += FechaDesdeDateTimePicker_ValueChanged;
            // 
            // fechaHastaLabel
            // 
            fechaHastaLabel.AutoSize = true;
            fechaHastaLabel.Location = new Point(129, 0);
            fechaHastaLabel.Name = "fechaHastaLabel";
            fechaHastaLabel.Size = new Size(71, 15);
            fechaHastaLabel.TabIndex = 2;
            fechaHastaLabel.Text = "Fecha Hasta";
            // 
            // fechaHastaDateTimePicker
            // 
            fechaHastaDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaHastaDateTimePicker.Location = new Point(129, 18);
            fechaHastaDateTimePicker.Name = "fechaHastaDateTimePicker";
            fechaHastaDateTimePicker.Size = new Size(120, 23);
            fechaHastaDateTimePicker.TabIndex = 2;
            fechaHastaDateTimePicker.ValueChanged += FechaHastaDateTimePicker_ValueChanged;
            // 
            // busquedaEspecialidadLabel
            // 
            busquedaEspecialidadLabel.AutoSize = true;
            busquedaEspecialidadLabel.Location = new Point(255, 0);
            busquedaEspecialidadLabel.Name = "busquedaEspecialidadLabel";
            busquedaEspecialidadLabel.Size = new Size(72, 15);
            busquedaEspecialidadLabel.TabIndex = 3;
            busquedaEspecialidadLabel.Text = "Especialidad";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(255, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(145, 23);
            busquedaEspecialidadComboBox.TabIndex = 3;
            busquedaEspecialidadComboBox.SelectedIndexChanged += BusquedaEspecialidadComboBox_SelectedIndexChanged;
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaProfesionalLabel
            // 
            busquedaProfesionalLabel.AutoSize = true;
            busquedaProfesionalLabel.Location = new Point(406, 0);
            busquedaProfesionalLabel.Name = "busquedaProfesionalLabel";
            busquedaProfesionalLabel.Size = new Size(77, 15);
            busquedaProfesionalLabel.TabIndex = 4;
            busquedaProfesionalLabel.Text = "Profesionales";
            // 
            // busquedaProfesionalComboBox
            // 
            busquedaProfesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaProfesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaProfesionalComboBox.FormattingEnabled = true;
            busquedaProfesionalComboBox.Location = new Point(406, 18);
            busquedaProfesionalComboBox.Name = "busquedaProfesionalComboBox";
            busquedaProfesionalComboBox.Size = new Size(165, 23);
            busquedaProfesionalComboBox.TabIndex = 4;
            busquedaProfesionalComboBox.SelectedIndexChanged += BusquedaProfesionalComboBox_SelectedIndexChanged;
            busquedaProfesionalComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaPacienteLabel
            // 
            busquedaPacienteLabel.AutoSize = true;
            busquedaPacienteLabel.Location = new Point(577, 0);
            busquedaPacienteLabel.Name = "busquedaPacienteLabel";
            busquedaPacienteLabel.Size = new Size(90, 15);
            busquedaPacienteLabel.TabIndex = 5;
            busquedaPacienteLabel.Text = "Buscar Paciente";
            // 
            // busquedaPacienteTextBox
            // 
            busquedaPacienteTextBox.Location = new Point(577, 18);
            busquedaPacienteTextBox.Name = "busquedaPacienteTextBox";
            busquedaPacienteTextBox.PlaceholderText = "Nombre o DNI...";
            busquedaPacienteTextBox.Size = new Size(150, 23);
            busquedaPacienteTextBox.TabIndex = 5;
            busquedaPacienteTextBox.TextChanged += FiltrarDataGridView;
            // 
            // estadoFacturacionLabel
            // 
            estadoFacturacionLabel.AutoSize = true;
            estadoFacturacionLabel.Location = new Point(733, 0);
            estadoFacturacionLabel.Name = "estadoFacturacionLabel";
            estadoFacturacionLabel.Size = new Size(110, 15);
            estadoFacturacionLabel.TabIndex = 6;
            estadoFacturacionLabel.Text = "Estado Facturación";
            // 
            // estadoFacturacionComboBox
            // 
            estadoFacturacionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoFacturacionComboBox.FormattingEnabled = true;
            estadoFacturacionComboBox.Location = new Point(733, 18);
            estadoFacturacionComboBox.Name = "estadoFacturacionComboBox";
            estadoFacturacionComboBox.Size = new Size(155, 23);
            estadoFacturacionComboBox.TabIndex = 6;
            estadoFacturacionComboBox.SelectedIndexChanged += FiltrarDataGridView;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.Left;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(894, 22);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 7;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
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
            turnosDataGridView.DataBindingComplete += TurnosDataGridView_DataBindingComplete;
            // 
            // Facturacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Facturacion";
            Size = new Size(1000, 600);
            Load += Facturacion_Load;
            tableLayoutPanel1.ResumeLayout(false);
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)turnosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1 = null!;
        private GroupBox busquedaGroupBox = null!;
        private TableLayoutPanel tableLayoutPanel2 = null!;
        private Label fechaDesdeLabel = null!;
        private DateTimePicker fechaDesdeDateTimePicker = null!;
        private Label fechaHastaLabel = null!;
        private DateTimePicker fechaHastaDateTimePicker = null!;
        private Label busquedaEspecialidadLabel = null!;
        private ComboBox busquedaEspecialidadComboBox = null!;
        private Label busquedaProfesionalLabel = null!;
        private ComboBox busquedaProfesionalComboBox = null!;
        private Label busquedaPacienteLabel = null!;
        private TextBox busquedaPacienteTextBox = null!;
        private Label estadoFacturacionLabel = null!;
        private ComboBox estadoFacturacionComboBox = null!;
        private LinkLabel limpiarFiltrosLinkLabel = null!;
        private DataGridView turnosDataGridView = null!;
    }
}
