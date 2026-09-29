namespace WindowsForms
{
    partial class Turnos
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
            busquedaProfesionalLabel = new Label();
            busquedaProfesionalComboBox = new ComboBox();
            busquedaPacienteLabel = new Label();
            busquedaPacienteTextBox = new TextBox();
            filtrarButton = new Button();
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
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(busquedaFechaLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(busquedaFechaDateTimePicker, 0, 1);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalComboBox, 1, 1);
            tableLayoutPanel2.Controls.Add(busquedaPacienteLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(busquedaPacienteTextBox, 2, 1);
            tableLayoutPanel2.Controls.Add(filtrarButton, 3, 1);
            tableLayoutPanel2.Controls.Add(limpiarFiltrosLinkLabel, 4, 1);
            tableLayoutPanel2.Controls.Add(nuevoTurnoButton, 5, 1);
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
            // 
            // busquedaProfesionalLabel
            // 
            busquedaProfesionalLabel.AutoSize = true;
            busquedaProfesionalLabel.Location = new Point(139, 0);
            busquedaProfesionalLabel.Name = "busquedaProfesionalLabel";
            busquedaProfesionalLabel.Size = new Size(77, 15);
            busquedaProfesionalLabel.TabIndex = 2;
            busquedaProfesionalLabel.Text = "Profesionales";
            // 
            // busquedaProfesionalComboBox
            // 
            busquedaProfesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaProfesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaProfesionalComboBox.FormattingEnabled = true;
            busquedaProfesionalComboBox.Location = new Point(139, 18);
            busquedaProfesionalComboBox.Name = "busquedaProfesionalComboBox";
            busquedaProfesionalComboBox.Size = new Size(180, 23);
            busquedaProfesionalComboBox.TabIndex = 2;
            // 
            // busquedaPacienteLabel
            // 
            busquedaPacienteLabel.AutoSize = true;
            busquedaPacienteLabel.Location = new Point(325, 0);
            busquedaPacienteLabel.Name = "busquedaPacienteLabel";
            busquedaPacienteLabel.Size = new Size(90, 15);
            busquedaPacienteLabel.TabIndex = 3;
            busquedaPacienteLabel.Text = "Buscar Paciente";
            // 
            // busquedaPacienteTextBox
            // 
            busquedaPacienteTextBox.Location = new Point(325, 18);
            busquedaPacienteTextBox.Name = "busquedaPacienteTextBox";
            busquedaPacienteTextBox.PlaceholderText = "Nombre o DNI...";
            busquedaPacienteTextBox.Size = new Size(180, 23);
            busquedaPacienteTextBox.TabIndex = 3;
            // 
            // filtrarButton
            // 
            filtrarButton.Location = new Point(511, 18);
            filtrarButton.Name = "filtrarButton";
            filtrarButton.Size = new Size(100, 23);
            filtrarButton.TabIndex = 4;
            filtrarButton.Text = "Filtrar";
            filtrarButton.UseVisualStyleBackColor = true;
            filtrarButton.Click += FiltrarButton_Click;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(617, 22);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 5;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // nuevoTurnoButton
            // 
            nuevoTurnoButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nuevoTurnoButton.AutoSize = true;
            nuevoTurnoButton.Location = new Point(888, 18);
            nuevoTurnoButton.Name = "nuevoTurnoButton";
            nuevoTurnoButton.Size = new Size(97, 25);
            nuevoTurnoButton.TabIndex = 6;
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
            turnosDataGridView.Dock = DockStyle.Fill;
            turnosDataGridView.EnableHeadersVisualStyles = false;
            turnosDataGridView.Location = new Point(3, 84);
            turnosDataGridView.Name = "turnosDataGridView";
            turnosDataGridView.RowHeadersVisible = false;
            turnosDataGridView.Size = new Size(994, 513);
            turnosDataGridView.TabIndex = 1;
            // 
            // Turnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Turnos";
            Size = new Size(1000, 600);
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
        private Label busquedaProfesionalLabel;
        private ComboBox busquedaProfesionalComboBox;
        private Label busquedaPacienteLabel;
        private TextBox busquedaPacienteTextBox;
        private Button filtrarButton;
        private LinkLabel limpiarFiltrosLinkLabel;
        private Button nuevoTurnoButton;
        private DataGridView turnosDataGridView;
    }
}
