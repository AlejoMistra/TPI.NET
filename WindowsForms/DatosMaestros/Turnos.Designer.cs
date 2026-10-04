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
            formGroupBox = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            numeroTurnoLabel = new Label();
            numeroTurnoTextBox = new TextBox();
            profesionalLabel = new Label();
            profesionalComboBox = new ComboBox();
            especialidadLabel = new Label();
            especialidadComboBox = new ComboBox();
            fechaTurnoLabel = new Label();
            fechaTurnoDateTimePicker = new DateTimePicker();
            horaInicioLabel = new Label();
            horaInicioDateTimePicker = new DateTimePicker();
            horaFinLabel = new Label();
            horaFinDateTimePicker = new DateTimePicker();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
            obligatorioLabel = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            guardarTurnoButton = new Button();
            cancelarButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)turnosDataGridView).BeginInit();
            formGroupBox.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
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
            tableLayoutPanel1.Controls.Add(formGroupBox, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
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
            busquedaProfesionalLabel.Size = new Size(66, 15);
            busquedaProfesionalLabel.TabIndex = 0;
            busquedaProfesionalLabel.Text = "Profesional";
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
            busquedaEspecialidadLabel.Size = new Size(72, 15);
            busquedaEspecialidadLabel.TabIndex = 2;
            busquedaEspecialidadLabel.Text = "Especialidad";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(189, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(160, 23);
            busquedaEspecialidadComboBox.TabIndex = 2;
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
            // 
            // busquedaFechaLabel
            // 
            busquedaFechaLabel.AutoSize = true;
            busquedaFechaLabel.Location = new Point(355, 0);
            busquedaFechaLabel.Name = "busquedaFechaLabel";
            busquedaFechaLabel.Size = new Size(38, 15);
            busquedaFechaLabel.TabIndex = 3;
            busquedaFechaLabel.Text = "Fecha";
            // 
            // busquedaFechaDateTimePicker
            // 
            busquedaFechaDateTimePicker.Format = DateTimePickerFormat.Short;
            busquedaFechaDateTimePicker.Location = new Point(355, 18);
            busquedaFechaDateTimePicker.Name = "busquedaFechaDateTimePicker";
            busquedaFechaDateTimePicker.Size = new Size(130, 23);
            busquedaFechaDateTimePicker.TabIndex = 3;
            busquedaFechaDateTimePicker.ValueChanged += FiltrarDataGridView;
            busquedaFechaDateTimePicker.BindingContextChanged += FiltrarDataGridView;
            // 
            // busquedaEstadoLabel
            // 
            busquedaEstadoLabel.AutoSize = true;
            busquedaEstadoLabel.Location = new Point(491, 0);
            busquedaEstadoLabel.Name = "busquedaEstadoLabel";
            busquedaEstadoLabel.Size = new Size(42, 15);
            busquedaEstadoLabel.TabIndex = 4;
            busquedaEstadoLabel.Text = "Estado";
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
            limpiarFiltrosLinkLabel.Location = new Point(617, 26);
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
            turnosDataGridView.Size = new Size(994, 329);
            turnosDataGridView.TabIndex = 1;
            // 
            // formGroupBox
            // 
            formGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            formGroupBox.Controls.Add(tableLayoutPanel3);
            formGroupBox.Dock = DockStyle.Fill;
            formGroupBox.Location = new Point(3, 419);
            formGroupBox.Name = "formGroupBox";
            formGroupBox.Size = new Size(994, 139);
            formGroupBox.TabIndex = 2;
            formGroupBox.TabStop = false;
            formGroupBox.Text = "Formulario de Alta / Edición de Turno";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Controls.Add(numeroTurnoLabel, 0, 0);
            tableLayoutPanel3.Controls.Add(numeroTurnoTextBox, 0, 1);
            tableLayoutPanel3.Controls.Add(profesionalLabel, 1, 0);
            tableLayoutPanel3.Controls.Add(profesionalComboBox, 1, 1);
            tableLayoutPanel3.Controls.Add(especialidadLabel, 2, 0);
            tableLayoutPanel3.Controls.Add(especialidadComboBox, 2, 1);
            tableLayoutPanel3.Controls.Add(fechaTurnoLabel, 3, 0);
            tableLayoutPanel3.Controls.Add(fechaTurnoDateTimePicker, 3, 1);
            tableLayoutPanel3.Controls.Add(horaInicioLabel, 0, 2);
            tableLayoutPanel3.Controls.Add(horaInicioDateTimePicker, 0, 3);
            tableLayoutPanel3.Controls.Add(horaFinLabel, 1, 2);
            tableLayoutPanel3.Controls.Add(horaFinDateTimePicker, 1, 3);
            tableLayoutPanel3.Controls.Add(estadoLabel, 2, 2);
            tableLayoutPanel3.Controls.Add(estadoComboBox, 2, 3);
            tableLayoutPanel3.Controls.Add(obligatorioLabel, 0, 4);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 2, 4);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(988, 117);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // numeroTurnoLabel
            // 
            numeroTurnoLabel.AutoSize = true;
            numeroTurnoLabel.Location = new Point(3, 0);
            numeroTurnoLabel.Name = "numeroTurnoLabel";
            numeroTurnoLabel.Size = new Size(149, 15);
            numeroTurnoLabel.TabIndex = 0;
            numeroTurnoLabel.Text = "Nro Turno (Autogenerado)";
            // 
            // numeroTurnoTextBox
            // 
            numeroTurnoTextBox.Dock = DockStyle.Fill;
            numeroTurnoTextBox.Location = new Point(3, 18);
            numeroTurnoTextBox.Name = "numeroTurnoTextBox";
            numeroTurnoTextBox.ReadOnly = true;
            numeroTurnoTextBox.Size = new Size(241, 23);
            numeroTurnoTextBox.TabIndex = 1;
            // 
            // profesionalLabel
            // 
            profesionalLabel.AutoSize = true;
            profesionalLabel.Location = new Point(250, 0);
            profesionalLabel.Name = "profesionalLabel";
            profesionalLabel.Size = new Size(74, 15);
            profesionalLabel.TabIndex = 2;
            profesionalLabel.Text = "Profesional *";
            // 
            // profesionalComboBox
            // 
            profesionalComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            profesionalComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            profesionalComboBox.Dock = DockStyle.Fill;
            profesionalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profesionalComboBox.FormattingEnabled = true;
            profesionalComboBox.Location = new Point(250, 18);
            profesionalComboBox.Name = "profesionalComboBox";
            profesionalComboBox.Size = new Size(241, 23);
            profesionalComboBox.TabIndex = 2;
            // 
            // especialidadLabel
            // 
            especialidadLabel.AutoSize = true;
            especialidadLabel.Location = new Point(497, 0);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(80, 15);
            especialidadLabel.TabIndex = 3;
            especialidadLabel.Text = "Especialidad *";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            especialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            especialidadComboBox.Dock = DockStyle.Fill;
            especialidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(497, 18);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(241, 23);
            especialidadComboBox.TabIndex = 3;
            // 
            // fechaTurnoLabel
            // 
            fechaTurnoLabel.AutoSize = true;
            fechaTurnoLabel.Location = new Point(744, 0);
            fechaTurnoLabel.Name = "fechaTurnoLabel";
            fechaTurnoLabel.Size = new Size(100, 15);
            fechaTurnoLabel.TabIndex = 4;
            fechaTurnoLabel.Text = "Fecha del Turno *";
            // 
            // fechaTurnoDateTimePicker
            // 
            fechaTurnoDateTimePicker.Dock = DockStyle.Fill;
            fechaTurnoDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaTurnoDateTimePicker.Location = new Point(744, 18);
            fechaTurnoDateTimePicker.Name = "fechaTurnoDateTimePicker";
            fechaTurnoDateTimePicker.Size = new Size(241, 23);
            fechaTurnoDateTimePicker.TabIndex = 4;
            // 
            // horaInicioLabel
            // 
            horaInicioLabel.AutoSize = true;
            horaInicioLabel.Location = new Point(3, 44);
            horaInicioLabel.Name = "horaInicioLabel";
            horaInicioLabel.Size = new Size(73, 15);
            horaInicioLabel.TabIndex = 5;
            horaInicioLabel.Text = "Hora Inicio *";
            // 
            // horaInicioDateTimePicker
            // 
            horaInicioDateTimePicker.CustomFormat = "HH:mm";
            horaInicioDateTimePicker.Dock = DockStyle.Fill;
            horaInicioDateTimePicker.Format = DateTimePickerFormat.Custom;
            horaInicioDateTimePicker.Location = new Point(3, 62);
            horaInicioDateTimePicker.Name = "horaInicioDateTimePicker";
            horaInicioDateTimePicker.ShowUpDown = true;
            horaInicioDateTimePicker.Size = new Size(241, 23);
            horaInicioDateTimePicker.TabIndex = 5;
            // 
            // horaFinLabel
            // 
            horaFinLabel.AutoSize = true;
            horaFinLabel.Location = new Point(250, 44);
            horaFinLabel.Name = "horaFinLabel";
            horaFinLabel.Size = new Size(60, 15);
            horaFinLabel.TabIndex = 6;
            horaFinLabel.Text = "Hora Fin *";
            // 
            // horaFinDateTimePicker
            // 
            horaFinDateTimePicker.CustomFormat = "HH:mm";
            horaFinDateTimePicker.Dock = DockStyle.Fill;
            horaFinDateTimePicker.Format = DateTimePickerFormat.Custom;
            horaFinDateTimePicker.Location = new Point(250, 62);
            horaFinDateTimePicker.Name = "horaFinDateTimePicker";
            horaFinDateTimePicker.ShowUpDown = true;
            horaFinDateTimePicker.Size = new Size(241, 23);
            horaFinDateTimePicker.TabIndex = 6;
            // 
            // estadoLabel
            // 
            estadoLabel.AutoSize = true;
            estadoLabel.Location = new Point(497, 44);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(84, 15);
            estadoLabel.TabIndex = 7;
            estadoLabel.Text = "Estado Inicial *";
            // 
            // estadoComboBox
            // 
            estadoComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            estadoComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            estadoComboBox.Dock = DockStyle.Fill;
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(497, 62);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(241, 23);
            estadoComboBox.TabIndex = 7;
            // 
            // obligatorioLabel
            // 
            obligatorioLabel.Anchor = AnchorStyles.Left;
            obligatorioLabel.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(obligatorioLabel, 2);
            obligatorioLabel.Location = new Point(3, 99);
            obligatorioLabel.Name = "obligatorioLabel";
            obligatorioLabel.Size = new Size(390, 15);
            obligatorioLabel.TabIndex = 9;
            obligatorioLabel.Text = "Los campos marcados con (*) son obligatorios para el registro del Turno.";
            obligatorioLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel3.SetColumnSpan(tableLayoutPanel4, 2);
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(guardarTurnoButton, 0, 0);
            tableLayoutPanel4.Controls.Add(cancelarButton, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Right;
            tableLayoutPanel4.Location = new Point(691, 91);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RightToLeft = RightToLeft.Yes;
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(294, 31);
            tableLayoutPanel4.TabIndex = 10;
            // 
            // guardarTurnoButton
            // 
            guardarTurnoButton.Location = new Point(166, 3);
            guardarTurnoButton.Name = "guardarTurnoButton";
            guardarTurnoButton.RightToLeft = RightToLeft.No;
            guardarTurnoButton.Size = new Size(125, 25);
            guardarTurnoButton.TabIndex = 11;
            guardarTurnoButton.Text = "Guardar Turno";
            guardarTurnoButton.UseVisualStyleBackColor = true;
            guardarTurnoButton.Click += GuardarTurnoButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(5, 3);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.RightToLeft = RightToLeft.No;
            cancelarButton.Size = new Size(155, 25);
            cancelarButton.TabIndex = 12;
            cancelarButton.Text = "Cancelar / Limpiar campos";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += CancelarButton_Click;
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
            formGroupBox.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
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
        private GroupBox formGroupBox;
        private TableLayoutPanel tableLayoutPanel3;
        private Label numeroTurnoLabel;
        private TextBox numeroTurnoTextBox;
        private Label profesionalLabel;
        private ComboBox profesionalComboBox;
        private Label especialidadLabel;
        private ComboBox especialidadComboBox;
        private Label fechaTurnoLabel;
        private DateTimePicker fechaTurnoDateTimePicker;
        private Label horaInicioLabel;
        private DateTimePicker horaInicioDateTimePicker;
        private Label horaFinLabel;
        private DateTimePicker horaFinDateTimePicker;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private Label obligatorioLabel;
        private TableLayoutPanel tableLayoutPanel4;
        private Button guardarTurnoButton;
        private Button cancelarButton;
    }
}

