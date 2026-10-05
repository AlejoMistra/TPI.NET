namespace WindowsForms
{
    partial class Pacientes
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
            busquedaPacienteLabel = new Label();
            busquedaPacienteTextBox = new TextBox();
            busquedaObraSocialLabel = new Label();
            busquedaObraSocialComboBox = new ComboBox();
            busquedaEspecialidadLabel = new Label();
            busquedaEspecialidadComboBox = new ComboBox();
            busquedaProfesionalLabel = new Label();
            busquedaProfesionalComboBox = new ComboBox();
            limpiarFiltrosLinkLabel = new LinkLabel();
            pacientesDataGridView = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pacientesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(busquedaGroupBox, 0, 0);
            tableLayoutPanel1.Controls.Add(pacientesDataGridView, 0, 1);
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
            busquedaGroupBox.Text = "Búsqueda y Filtrado de Pacientes";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(busquedaPacienteLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(busquedaPacienteTextBox, 0, 1);
            tableLayoutPanel2.Controls.Add(busquedaObraSocialLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(busquedaObraSocialComboBox, 1, 1);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadComboBox, 2, 1);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalComboBox, 3, 1);
            tableLayoutPanel2.Controls.Add(limpiarFiltrosLinkLabel, 4, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(988, 53);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // busquedaPacienteLabel
            // 
            busquedaPacienteLabel.AutoSize = true;
            busquedaPacienteLabel.Location = new Point(3, 0);
            busquedaPacienteLabel.Name = "busquedaPacienteLabel";
            busquedaPacienteLabel.Size = new Size(90, 15);
            busquedaPacienteLabel.TabIndex = 0;
            busquedaPacienteLabel.Text = "Buscar Paciente";
            // 
            // busquedaPacienteTextBox
            // 
            busquedaPacienteTextBox.Location = new Point(3, 18);
            busquedaPacienteTextBox.Name = "busquedaPacienteTextBox";
            busquedaPacienteTextBox.PlaceholderText = "Nombre, Apellido o DNI...";
            busquedaPacienteTextBox.Size = new Size(220, 23);
            busquedaPacienteTextBox.TabIndex = 1;
            busquedaPacienteTextBox.TextChanged += FiltrarDataGridView;
            // 
            // busquedaObraSocialLabel
            // 
            busquedaObraSocialLabel.AutoSize = true;
            busquedaObraSocialLabel.Location = new Point(229, 0);
            busquedaObraSocialLabel.Name = "busquedaObraSocialLabel";
            busquedaObraSocialLabel.Size = new Size(67, 15);
            busquedaObraSocialLabel.TabIndex = 2;
            busquedaObraSocialLabel.Text = "Obra Social";
            // 
            // busquedaObraSocialComboBox
            // 
            busquedaObraSocialComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            busquedaObraSocialComboBox.FormattingEnabled = true;
            busquedaObraSocialComboBox.Location = new Point(229, 18);
            busquedaObraSocialComboBox.Name = "busquedaObraSocialComboBox";
            busquedaObraSocialComboBox.Size = new Size(160, 23);
            busquedaObraSocialComboBox.TabIndex = 2;
            busquedaObraSocialComboBox.SelectedIndexChanged += FiltrarDataGridView;
            // 
            // busquedaEspecialidadLabel
            // 
            busquedaEspecialidadLabel.AutoSize = true;
            busquedaEspecialidadLabel.Location = new Point(395, 0);
            busquedaEspecialidadLabel.Name = "busquedaEspecialidadLabel";
            busquedaEspecialidadLabel.Size = new Size(135, 15);
            busquedaEspecialidadLabel.TabIndex = 3;
            busquedaEspecialidadLabel.Text = "Especialidad (Atención)";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(395, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(175, 23);
            busquedaEspecialidadComboBox.TabIndex = 3;
            busquedaEspecialidadComboBox.SelectedIndexChanged += BusquedaEspecialidadComboBox_SelectedIndexChanged;
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaProfesionalLabel
            // 
            busquedaProfesionalLabel.AutoSize = true;
            busquedaProfesionalLabel.Location = new Point(576, 0);
            busquedaProfesionalLabel.Name = "busquedaProfesionalLabel";
            busquedaProfesionalLabel.Size = new Size(130, 15);
            busquedaProfesionalLabel.TabIndex = 4;
            busquedaProfesionalLabel.Text = "Profesional (Atención)";
            // 
            // busquedaProfesionalComboBox
            // 
            busquedaProfesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaProfesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaProfesionalComboBox.FormattingEnabled = true;
            busquedaProfesionalComboBox.Location = new Point(576, 18);
            busquedaProfesionalComboBox.Name = "busquedaProfesionalComboBox";
            busquedaProfesionalComboBox.Size = new Size(195, 23);
            busquedaProfesionalComboBox.TabIndex = 4;
            busquedaProfesionalComboBox.SelectedIndexChanged += BusquedaProfesionalComboBox_SelectedIndexChanged;
            busquedaProfesionalComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.Left;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(777, 22);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 5;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // pacientesDataGridView
            // 
            pacientesDataGridView.AllowUserToAddRows = false;
            pacientesDataGridView.AllowUserToDeleteRows = false;
            pacientesDataGridView.AllowUserToOrderColumns = true;
            pacientesDataGridView.AllowUserToResizeRows = false;
            pacientesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pacientesDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            pacientesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            pacientesDataGridView.Cursor = Cursors.Hand;
            pacientesDataGridView.Dock = DockStyle.Fill;
            pacientesDataGridView.EnableHeadersVisualStyles = false;
            pacientesDataGridView.Location = new Point(3, 84);
            pacientesDataGridView.Name = "pacientesDataGridView";
            pacientesDataGridView.RowHeadersVisible = false;
            pacientesDataGridView.Size = new Size(994, 513);
            pacientesDataGridView.TabIndex = 1;
            pacientesDataGridView.CellContentClick += PacientesDataGridView_CellContentClick;
            pacientesDataGridView.CellDoubleClick += PacientesDataGridView_CellDoubleClick;
            pacientesDataGridView.CellMouseDown += PacientesDataGridView_CellMouseDown;
            pacientesDataGridView.DataBindingComplete += PacientesDataGridView_DataBindingComplete;
            // 
            // Pacientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Pacientes";
            Size = new Size(1000, 600);
            Load += Pacientes_Load;
            tableLayoutPanel1.ResumeLayout(false);
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pacientesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1 = null!;
        private GroupBox busquedaGroupBox = null!;
        private TableLayoutPanel tableLayoutPanel2 = null!;
        private Label busquedaPacienteLabel = null!;
        private TextBox busquedaPacienteTextBox = null!;
        private Label busquedaObraSocialLabel = null!;
        private ComboBox busquedaObraSocialComboBox = null!;
        private Label busquedaEspecialidadLabel = null!;
        private ComboBox busquedaEspecialidadComboBox = null!;
        private Label busquedaProfesionalLabel = null!;
        private ComboBox busquedaProfesionalComboBox = null!;
        private LinkLabel limpiarFiltrosLinkLabel = null!;
        private DataGridView pacientesDataGridView = null!;
    }
}
