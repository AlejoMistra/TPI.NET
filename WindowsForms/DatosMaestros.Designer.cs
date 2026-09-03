namespace WindowsForms
{
    partial class DatosMaestros
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
            profesionalesTabControl = new TabControl();
            profesionalesTabPage = new TabPage();
            busquedaGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            busquedaProfesionalTextBox = new TextBox();
            filtrarButton = new Button();
            limpiarFiltrosLinkLabel = new LinkLabel();
            busquedaEspecialidadComboBox = new ComboBox();
            busquedaEstadoComboBox = new ComboBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label4 = new Label();
            nombreTextBox = new TextBox();
            label7 = new Label();
            matriculaTextBox = new TextBox();
            label10 = new Label();
            emailTextBox = new TextBox();
            label5 = new Label();
            apellidoTextBox = new TextBox();
            label8 = new Label();
            especialidadComboBox = new ComboBox();
            habilitadoCheckBox = new CheckBox();
            label6 = new Label();
            documentoTextBox = new TextBox();
            label9 = new Label();
            telefonoTextBox = new TextBox();
            guardarProfesionalButton = new Button();
            profesionalesDataGridView = new DataGridView();
            matriculaColumn = new DataGridViewTextBoxColumn();
            apellidoColum = new DataGridViewTextBoxColumn();
            apellidoColumn = new DataGridViewTextBoxColumn();
            documentColumn = new DataGridViewTextBoxColumn();
            especialidadColumn = new DataGridViewTextBoxColumn();
            teléfonoColumn = new DataGridViewTextBoxColumn();
            emailColumn = new DataGridViewTextBoxColumn();
            statusColumn = new DataGridViewTextBoxColumn();
            accionesColumn = new DataGridViewButtonColumn();
            especialidadesTabPage = new TabPage();
            profesionalesTabControl.SuspendLayout();
            profesionalesTabPage.SuspendLayout();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // profesionalesTabControl
            // 
            profesionalesTabControl.Controls.Add(profesionalesTabPage);
            profesionalesTabControl.Controls.Add(especialidadesTabPage);
            profesionalesTabControl.Dock = DockStyle.Fill;
            profesionalesTabControl.Location = new Point(0, 0);
            profesionalesTabControl.Name = "profesionalesTabControl";
            profesionalesTabControl.SelectedIndex = 0;
            profesionalesTabControl.Size = new Size(1090, 700);
            profesionalesTabControl.TabIndex = 0;
            // 
            // profesionalesTabPage
            // 
            profesionalesTabPage.Controls.Add(busquedaGroupBox);
            profesionalesTabPage.Controls.Add(groupBox1);
            profesionalesTabPage.Controls.Add(profesionalesDataGridView);
            profesionalesTabPage.Location = new Point(4, 24);
            profesionalesTabPage.Margin = new Padding(10);
            profesionalesTabPage.Name = "profesionalesTabPage";
            profesionalesTabPage.Padding = new Padding(3);
            profesionalesTabPage.Size = new Size(1082, 672);
            profesionalesTabPage.TabIndex = 0;
            profesionalesTabPage.Text = "Profesionales";
            profesionalesTabPage.UseVisualStyleBackColor = true;
            // 
            // busquedaGroupBox
            // 
            busquedaGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            busquedaGroupBox.AutoSize = true;
            busquedaGroupBox.Controls.Add(tableLayoutPanel1);
            busquedaGroupBox.Location = new Point(3, 7);
            busquedaGroupBox.Name = "busquedaGroupBox";
            busquedaGroupBox.Size = new Size(1079, 67);
            busquedaGroupBox.TabIndex = 1;
            busquedaGroupBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(busquedaProfesionalTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(filtrarButton, 3, 1);
            tableLayoutPanel1.Controls.Add(limpiarFiltrosLinkLabel, 4, 1);
            tableLayoutPanel1.Controls.Add(busquedaEspecialidadComboBox, 1, 1);
            tableLayoutPanel1.Controls.Add(busquedaEstadoComboBox, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1073, 45);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 0;
            label1.Text = "Buscar Profesional";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 1;
            label2.Text = "Especialidad Médica";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(299, 0);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 2;
            label3.Text = "Estado";
            // 
            // busquedaProfesionalTextBox
            // 
            busquedaProfesionalTextBox.Location = new Point(3, 18);
            busquedaProfesionalTextBox.Name = "busquedaProfesionalTextBox";
            busquedaProfesionalTextBox.Size = new Size(142, 23);
            busquedaProfesionalTextBox.TabIndex = 3;
            // 
            // filtrarButton
            // 
            filtrarButton.Anchor = AnchorStyles.None;
            filtrarButton.AutoSize = true;
            filtrarButton.Location = new Point(426, 18);
            filtrarButton.Name = "filtrarButton";
            filtrarButton.Size = new Size(75, 25);
            filtrarButton.TabIndex = 6;
            filtrarButton.Text = "Filtrar";
            filtrarButton.UseVisualStyleBackColor = true;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(507, 23);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 7;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(151, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(142, 23);
            busquedaEspecialidadComboBox.TabIndex = 4;
            // 
            // busquedaEstadoComboBox
            // 
            busquedaEstadoComboBox.FormattingEnabled = true;
            busquedaEstadoComboBox.Location = new Point(299, 18);
            busquedaEstadoComboBox.Name = "busquedaEstadoComboBox";
            busquedaEstadoComboBox.Size = new Size(121, 23);
            busquedaEstadoComboBox.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Location = new Point(3, 346);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1076, 201);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Formulario de Alta / Edición de Profesional";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Controls.Add(nombreTextBox, 0, 2);
            tableLayoutPanel2.Controls.Add(label7, 0, 3);
            tableLayoutPanel2.Controls.Add(matriculaTextBox, 0, 4);
            tableLayoutPanel2.Controls.Add(label10, 0, 5);
            tableLayoutPanel2.Controls.Add(emailTextBox, 0, 6);
            tableLayoutPanel2.Controls.Add(label5, 1, 1);
            tableLayoutPanel2.Controls.Add(apellidoTextBox, 1, 2);
            tableLayoutPanel2.Controls.Add(label8, 1, 3);
            tableLayoutPanel2.Controls.Add(especialidadComboBox, 1, 4);
            tableLayoutPanel2.Controls.Add(habilitadoCheckBox, 2, 6);
            tableLayoutPanel2.Controls.Add(label6, 2, 1);
            tableLayoutPanel2.Controls.Add(documentoTextBox, 2, 2);
            tableLayoutPanel2.Controls.Add(label9, 2, 3);
            tableLayoutPanel2.Controls.Add(telefonoTextBox, 2, 4);
            tableLayoutPanel2.Controls.Add(guardarProfesionalButton, 3, 7);
            tableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 8;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1067, 176);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 0;
            label4.Text = "Nombre";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Dock = DockStyle.Fill;
            nombreTextBox.Location = new Point(3, 18);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(234, 23);
            nombreTextBox.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 44);
            label7.Name = "label7";
            label7.Size = new Size(80, 15);
            label7.TabIndex = 6;
            label7.Text = "Nro Matricula";
            // 
            // matriculaTextBox
            // 
            matriculaTextBox.Dock = DockStyle.Fill;
            matriculaTextBox.Location = new Point(3, 62);
            matriculaTextBox.Name = "matriculaTextBox";
            matriculaTextBox.Size = new Size(234, 23);
            matriculaTextBox.TabIndex = 6;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 88);
            label10.Name = "label10";
            label10.Size = new Size(173, 15);
            label10.TabIndex = 12;
            label10.Text = "Correo Electrónico Institucional";
            // 
            // emailTextBox
            // 
            tableLayoutPanel2.SetColumnSpan(emailTextBox, 2);
            emailTextBox.Location = new Point(3, 106);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(474, 23);
            emailTextBox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(243, 0);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 1;
            label5.Text = "Apellido";
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Dock = DockStyle.Fill;
            apellidoTextBox.Location = new Point(243, 18);
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(234, 23);
            apellidoTextBox.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(243, 44);
            label8.Name = "label8";
            label8.Size = new Size(121, 15);
            label8.TabIndex = 7;
            label8.Text = "Especialidad Principal";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.Dock = DockStyle.Fill;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(243, 62);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(234, 23);
            especialidadComboBox.TabIndex = 7;
            // 
            // habilitadoCheckBox
            // 
            habilitadoCheckBox.AutoSize = true;
            habilitadoCheckBox.Location = new Point(483, 106);
            habilitadoCheckBox.Name = "habilitadoCheckBox";
            habilitadoCheckBox.Size = new Size(220, 19);
            habilitadoCheckBox.TabIndex = 9;
            habilitadoCheckBox.Text = "Habilitado para asignación de turnos";
            habilitadoCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(483, 0);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 17;
            label6.Text = "Nro Documento";
            // 
            // documentoTextBox
            // 
            documentoTextBox.Dock = DockStyle.Fill;
            documentoTextBox.Location = new Point(483, 18);
            documentoTextBox.Name = "documentoTextBox";
            documentoTextBox.Size = new Size(234, 23);
            documentoTextBox.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(483, 44);
            label9.Name = "label9";
            label9.Size = new Size(121, 15);
            label9.TabIndex = 19;
            label9.Text = "Teléfono de Contacto";
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Dock = DockStyle.Fill;
            telefonoTextBox.Location = new Point(483, 62);
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(234, 23);
            telefonoTextBox.TabIndex = 7;
            // 
            // guardarProfesionalButton
            // 
            guardarProfesionalButton.Anchor = AnchorStyles.None;
            guardarProfesionalButton.AutoSize = true;
            guardarProfesionalButton.Location = new Point(833, 141);
            guardarProfesionalButton.Name = "guardarProfesionalButton";
            guardarProfesionalButton.Size = new Size(121, 25);
            guardarProfesionalButton.TabIndex = 10;
            guardarProfesionalButton.Text = "Guardar Profesional";
            guardarProfesionalButton.UseVisualStyleBackColor = true;
            // 
            // profesionalesDataGridView
            // 
            profesionalesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            profesionalesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            profesionalesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            profesionalesDataGridView.Columns.AddRange(new DataGridViewColumn[] { matriculaColumn, apellidoColum, apellidoColumn, documentColumn, especialidadColumn, teléfonoColumn, emailColumn, statusColumn, accionesColumn });
            profesionalesDataGridView.Location = new Point(3, 80);
            profesionalesDataGridView.Name = "profesionalesDataGridView";
            profesionalesDataGridView.Size = new Size(1076, 260);
            profesionalesDataGridView.TabIndex = 2;
            // 
            // matriculaColumn
            // 
            matriculaColumn.HeaderText = "Matríacula";
            matriculaColumn.Name = "matriculaColumn";
            // 
            // apellidoColum
            // 
            apellidoColum.HeaderText = "Apellido";
            apellidoColum.Name = "apellidoColum";
            // 
            // apellidoColumn
            // 
            apellidoColumn.HeaderText = "Nombre";
            apellidoColumn.Name = "apellidoColumn";
            // 
            // documentColumn
            // 
            documentColumn.HeaderText = "N° Doc";
            documentColumn.Name = "documentColumn";
            // 
            // especialidadColumn
            // 
            especialidadColumn.HeaderText = "Especialidad";
            especialidadColumn.Name = "especialidadColumn";
            // 
            // teléfonoColumn
            // 
            teléfonoColumn.HeaderText = "Teléfono";
            teléfonoColumn.Name = "teléfonoColumn";
            // 
            // emailColumn
            // 
            emailColumn.HeaderText = "Correo Electrónico";
            emailColumn.Name = "emailColumn";
            // 
            // statusColumn
            // 
            statusColumn.HeaderText = "Estado";
            statusColumn.Name = "statusColumn";
            // 
            // accionesColumn
            // 
            accionesColumn.HeaderText = "Acciones";
            accionesColumn.Name = "accionesColumn";
            // 
            // especialidadesTabPage
            // 
            especialidadesTabPage.Location = new Point(4, 24);
            especialidadesTabPage.Name = "especialidadesTabPage";
            especialidadesTabPage.Padding = new Padding(3);
            especialidadesTabPage.Size = new Size(1082, 672);
            especialidadesTabPage.TabIndex = 1;
            especialidadesTabPage.Text = "Especialidades";
            especialidadesTabPage.UseVisualStyleBackColor = true;
            // 
            // DatosMaestros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(profesionalesTabControl);
            Name = "DatosMaestros";
            Size = new Size(1090, 700);
            profesionalesTabControl.ResumeLayout(false);
            profesionalesTabPage.ResumeLayout(false);
            profesionalesTabPage.PerformLayout();
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl profesionalesTabControl;
        private TabPage profesionalesTabPage;
        private TabPage especialidadesTabPage;
        private DataGridView profesionalesDataGridView;
        private DataGridViewTextBoxColumn matriculaColumn;
        private DataGridViewTextBoxColumn apellidoColum;
        private DataGridViewTextBoxColumn apellidoColumn;
        private DataGridViewTextBoxColumn documentColumn;
        private DataGridViewTextBoxColumn especialidadColumn;
        private DataGridViewTextBoxColumn teléfonoColumn;
        private DataGridViewTextBoxColumn emailColumn;
        private DataGridViewTextBoxColumn statusColumn;
        private DataGridViewButtonColumn accionesColumn;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private TextBox nombreTextBox;
        private Label label7;
        private TextBox matriculaTextBox;
        private TextBox apellidoTextBox;
        private TextBox documentoTextBox;
        private Label label5;
        private TextBox telefonoTextBox;
        private Label label8;
        private ComboBox especialidadComboBox;
        private Label label6;
        private Label label9;
        private GroupBox busquedaGroupBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox busquedaProfesionalTextBox;
        private Button filtrarButton;
        private LinkLabel limpiarFiltrosLinkLabel;
        private ComboBox busquedaEspecialidadComboBox;
        private ComboBox busquedaEstadoComboBox;
        private Label label10;
        private TextBox emailTextBox;
        private CheckBox habilitadoCheckBox;
        private Button guardarProfesionalButton;
    }
}
