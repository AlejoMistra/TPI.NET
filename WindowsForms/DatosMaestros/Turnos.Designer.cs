namespace WindowsForms.DatosMaestros
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
            busquedaProfesionalLabel = new Label();
            busquedaProfesionalComboBox = new ComboBox();
            busquedaEspecialidadLabel = new Label();
            busquedaEspecialidadComboBox = new ComboBox();
            busquedaFechaLabel = new Label();
            busquedaFechaDateTimePicker = new DateTimePicker();
            busquedaEstadoLabel = new Label();
            busquedaEstadoComboBox = new ComboBox();
            limpiarFiltrosLinkLabel = new LinkLabel();
            agregarTurnoButton = new Button();
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
            tableLayoutPanel1.Size = new Size(1000, 561);
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
            tableLayoutPanel2.Controls.Add(busquedaProfesionalLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(busquedaProfesionalComboBox, 0, 1);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadLabel, 1, 0);
            tableLayoutPanel2.Controls.Add(busquedaEspecialidadComboBox, 1, 1);
            tableLayoutPanel2.Controls.Add(busquedaFechaLabel, 2, 0);
            tableLayoutPanel2.Controls.Add(busquedaFechaDateTimePicker, 2, 1);
            tableLayoutPanel2.Controls.Add(busquedaEstadoLabel, 3, 0);
            tableLayoutPanel2.Controls.Add(busquedaEstadoComboBox, 3, 1);
            tableLayoutPanel2.Controls.Add(limpiarFiltrosLinkLabel, 4, 1);
            tableLayoutPanel2.Controls.Add(agregarTurnoButton, 5, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(988, 53);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // busquedaProfesionalLabel
            // 
            busquedaProfesionalLabel.AutoSize = true;
            busquedaProfesionalLabel.Location = new Point(3, 0);
            busquedaProfesionalLabel.Name = "busquedaProfesionalLabel";
            busquedaProfesionalLabel.Size = new Size(69, 15);
            busquedaProfesionalLabel.TabIndex = 0;
            busquedaProfesionalLabel.Text = "Profesional:";
            // 
            // busquedaProfesionalComboBox
            // 
            busquedaProfesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaProfesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaProfesionalComboBox.FormattingEnabled = true;
            busquedaProfesionalComboBox.Location = new Point(3, 18);
            busquedaProfesionalComboBox.Name = "busquedaProfesionalComboBox";
            busquedaProfesionalComboBox.Size = new Size(180, 23);
            busquedaProfesionalComboBox.TabIndex = 1;
            busquedaProfesionalComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaEspecialidadLabel
            // 
            busquedaEspecialidadLabel.AutoSize = true;
            busquedaEspecialidadLabel.Location = new Point(189, 0);
            busquedaEspecialidadLabel.Name = "busquedaEspecialidadLabel";
            busquedaEspecialidadLabel.Size = new Size(75, 15);
            busquedaEspecialidadLabel.TabIndex = 2;
            busquedaEspecialidadLabel.Text = "Especialidad:";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(189, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(170, 23);
            busquedaEspecialidadComboBox.TabIndex = 2;
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaFechaLabel
            // 
            busquedaFechaLabel.AutoSize = true;
            busquedaFechaLabel.Location = new Point(365, 0);
            busquedaFechaLabel.Name = "busquedaFechaLabel";
            busquedaFechaLabel.Size = new Size(41, 15);
            busquedaFechaLabel.TabIndex = 4;
            busquedaFechaLabel.Text = "Fecha:";
            // 
            // busquedaFechaDateTimePicker
            // 
            busquedaFechaDateTimePicker.CustomFormat = "dd/MM/yyyy";
            busquedaFechaDateTimePicker.Format = DateTimePickerFormat.Short;
            busquedaFechaDateTimePicker.Location = new Point(365, 18);
            busquedaFechaDateTimePicker.Name = "busquedaFechaDateTimePicker";
            busquedaFechaDateTimePicker.Size = new Size(120, 23);
            busquedaFechaDateTimePicker.TabIndex = 3;
            busquedaFechaDateTimePicker.ValueChanged += FiltrarDataGridView;
            // 
            // busquedaEstadoLabel
            // 
            busquedaEstadoLabel.AutoSize = true;
            busquedaEstadoLabel.Location = new Point(491, 0);
            busquedaEstadoLabel.Name = "busquedaEstadoLabel";
            busquedaEstadoLabel.Size = new Size(45, 15);
            busquedaEstadoLabel.TabIndex = 6;
            busquedaEstadoLabel.Text = "Estado:";
            // 
            // busquedaEstadoComboBox
            // 
            busquedaEstadoComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEstadoComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEstadoComboBox.FormattingEnabled = true;
            busquedaEstadoComboBox.Location = new Point(491, 18);
            busquedaEstadoComboBox.Name = "busquedaEstadoComboBox";
            busquedaEstadoComboBox.Size = new Size(120, 23);
            busquedaEstadoComboBox.TabIndex = 4;
            busquedaEstadoComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(617, 22);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 6;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // agregarTurnoButton
            // 
            agregarTurnoButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            agregarTurnoButton.AutoSize = true;
            agregarTurnoButton.Location = new Point(888, 18);
            agregarTurnoButton.Name = "agregarTurnoButton";
            agregarTurnoButton.Size = new Size(97, 25);
            agregarTurnoButton.TabIndex = 7;
            agregarTurnoButton.Text = "Nuevo Turno";
            agregarTurnoButton.UseVisualStyleBackColor = true;
            agregarTurnoButton.Click += AgregarTurnoButton_Click;
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
            turnosDataGridView.Size = new Size(994, 474);
            turnosDataGridView.TabIndex = 1;
            // 
            // Turnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Turnos";
            Size = new Size(1000, 561);
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
        private Label busquedaProfesionalLabel;
        private ComboBox busquedaProfesionalComboBox;
        private Label busquedaEspecialidadLabel;
        private ComboBox busquedaEspecialidadComboBox;
        private Label busquedaFechaLabel;
        private DateTimePicker busquedaFechaDateTimePicker;
        private Label busquedaEstadoLabel;
        private ComboBox busquedaEstadoComboBox;
        private LinkLabel limpiarFiltrosLinkLabel;
        private Button agregarTurnoButton;
        private DataGridView turnosDataGridView;
    }
}
