namespace WindowsForms.DatosMaestros
{
    partial class Profesionales
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
            panel1 = new Panel();
            busquedaGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            busquedaEspecialidadComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            busquedaProfesionalTextBox = new TextBox();
            filtrarButton = new Button();
            limpiarFiltrosLinkLabel = new LinkLabel();
            busquedaEstadoComboBox = new ComboBox();
            AgregarProfesionalButton = new Button();
            formGroupBox = new GroupBox();
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
            label11 = new Label();
            buttonsFlowLayoutPanel = new FlowLayoutPanel();
            cancelarButton = new Button();
            guardarProfesionalButton = new Button();
            profesionalesDataGridView = new DataGridView();
            panel1.SuspendLayout();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            formGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            buttonsFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(busquedaGroupBox);
            panel1.Controls.Add(formGroupBox);
            panel1.Controls.Add(profesionalesDataGridView);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1082, 672);
            panel1.TabIndex = 0;
            // 
            // busquedaGroupBox
            // 
            busquedaGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            busquedaGroupBox.AutoSize = true;
            busquedaGroupBox.Controls.Add(tableLayoutPanel1);
            busquedaGroupBox.Location = new Point(3, 3);
            busquedaGroupBox.Name = "busquedaGroupBox";
            busquedaGroupBox.Size = new Size(1079, 67);
            busquedaGroupBox.TabIndex = 7;
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
            tableLayoutPanel1.Controls.Add(busquedaEspecialidadComboBox, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(busquedaProfesionalTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(filtrarButton, 3, 1);
            tableLayoutPanel1.Controls.Add(limpiarFiltrosLinkLabel, 4, 1);
            tableLayoutPanel1.Controls.Add(busquedaEstadoComboBox, 2, 1);
            tableLayoutPanel1.Controls.Add(AgregarProfesionalButton, 5, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1073, 45);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(182, 18);
            busquedaEspecialidadComboBox.Name = "busquedaEspecialidadComboBox";
            busquedaEspecialidadComboBox.Size = new Size(152, 23);
            busquedaEspecialidadComboBox.TabIndex = 4;
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
            label2.Location = new Point(182, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 1;
            label2.Text = "Especialidad Médica";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(340, 0);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 2;
            label3.Text = "Estado";
            // 
            // busquedaProfesionalTextBox
            // 
            busquedaProfesionalTextBox.Dock = DockStyle.Fill;
            busquedaProfesionalTextBox.Location = new Point(3, 18);
            busquedaProfesionalTextBox.Name = "busquedaProfesionalTextBox";
            busquedaProfesionalTextBox.PlaceholderText = "DNI, Apellido o Matrícula...";
            busquedaProfesionalTextBox.Size = new Size(173, 23);
            busquedaProfesionalTextBox.TabIndex = 3;
            // 
            // filtrarButton
            // 
            filtrarButton.Location = new Point(471, 18);
            filtrarButton.Name = "filtrarButton";
            filtrarButton.Size = new Size(125, 23);
            filtrarButton.TabIndex = 6;
            filtrarButton.Text = "Filtrar";
            filtrarButton.UseVisualStyleBackColor = true;
            filtrarButton.Click += FiltrarButton_Click;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(602, 23);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(80, 15);
            limpiarFiltrosLinkLabel.TabIndex = 7;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // busquedaEstadoComboBox
            // 
            busquedaEstadoComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEstadoComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEstadoComboBox.FormattingEnabled = true;
            busquedaEstadoComboBox.Location = new Point(340, 18);
            busquedaEstadoComboBox.Name = "busquedaEstadoComboBox";
            busquedaEstadoComboBox.Size = new Size(125, 23);
            busquedaEstadoComboBox.TabIndex = 5;
            // 
            // AgregarProfesionalButton
            // 
            AgregarProfesionalButton.Anchor = AnchorStyles.Right;
            AgregarProfesionalButton.AutoSize = true;
            AgregarProfesionalButton.Location = new Point(949, 18);
            AgregarProfesionalButton.Name = "AgregarProfesionalButton";
            AgregarProfesionalButton.Size = new Size(121, 25);
            AgregarProfesionalButton.TabIndex = 8;
            AgregarProfesionalButton.Text = "Nuevo Profesional";
            AgregarProfesionalButton.UseVisualStyleBackColor = true;
            AgregarProfesionalButton.Click += AgregarProfesionalButton_Click;
            // 
            // formGroupBox
            // 
            formGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            formGroupBox.AutoSize = true;
            formGroupBox.Controls.Add(tableLayoutPanel2);
            formGroupBox.Location = new Point(3, 342);
            formGroupBox.Name = "formGroupBox";
            formGroupBox.Size = new Size(1076, 217);
            formGroupBox.TabIndex = 9;
            formGroupBox.TabStop = false;
            formGroupBox.Text = "Formulario de Alta / Edición de Profesional";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
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
            tableLayoutPanel2.Controls.Add(label11, 2, 7);
            tableLayoutPanel2.Controls.Add(buttonsFlowLayoutPanel, 3, 7);
            tableLayoutPanel2.Dock = DockStyle.Fill;
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
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1070, 195);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 0;
            label4.Text = "Nombre *";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Dock = DockStyle.Fill;
            nombreTextBox.Location = new Point(3, 18);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(350, 23);
            nombreTextBox.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 44);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 6;
            label7.Text = "Nro Matricula *";
            // 
            // matriculaTextBox
            // 
            matriculaTextBox.Dock = DockStyle.Fill;
            matriculaTextBox.Location = new Point(3, 62);
            matriculaTextBox.Name = "matriculaTextBox";
            matriculaTextBox.Size = new Size(350, 23);
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
            label5.Location = new Point(359, 0);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 1;
            label5.Text = "Apellido *";
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Dock = DockStyle.Fill;
            apellidoTextBox.Location = new Point(359, 18);
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(350, 23);
            apellidoTextBox.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(359, 44);
            label8.Name = "label8";
            label8.Size = new Size(129, 15);
            label8.TabIndex = 7;
            label8.Text = "Especialidad Principal *";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            especialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            especialidadComboBox.Dock = DockStyle.Fill;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(359, 62);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(350, 23);
            especialidadComboBox.TabIndex = 7;
            // 
            // habilitadoCheckBox
            // 
            habilitadoCheckBox.AutoSize = true;
            habilitadoCheckBox.Location = new Point(715, 106);
            habilitadoCheckBox.Name = "habilitadoCheckBox";
            habilitadoCheckBox.Size = new Size(220, 19);
            habilitadoCheckBox.TabIndex = 9;
            habilitadoCheckBox.Text = "Habilitado para asignación de turnos";
            habilitadoCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(715, 0);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 17;
            label6.Text = "Nro Documento";
            // 
            // documentoTextBox
            // 
            documentoTextBox.Dock = DockStyle.Fill;
            documentoTextBox.Location = new Point(715, 18);
            documentoTextBox.Name = "documentoTextBox";
            documentoTextBox.Size = new Size(352, 23);
            documentoTextBox.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(715, 44);
            label9.Name = "label9";
            label9.Size = new Size(121, 15);
            label9.TabIndex = 19;
            label9.Text = "Teléfono de Contacto";
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Dock = DockStyle.Fill;
            telefonoTextBox.Location = new Point(715, 62);
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(352, 23);
            telefonoTextBox.TabIndex = 7;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Left;
            label11.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label11, 2);
            label11.Location = new Point(3, 156);
            label11.Name = "label11";
            label11.Size = new Size(401, 15);
            label11.TabIndex = 20;
            label11.Text = "Los campos marcados con (*) son obligatorios para registrar al Profesional.";
            // 
            // buttonsFlowLayoutPanel
            // 
            buttonsFlowLayoutPanel.Anchor = AnchorStyles.Right;
            buttonsFlowLayoutPanel.AutoSize = true;
            buttonsFlowLayoutPanel.Controls.Add(cancelarButton);
            buttonsFlowLayoutPanel.Controls.Add(guardarProfesionalButton);
            buttonsFlowLayoutPanel.Location = new Point(775, 148);
            buttonsFlowLayoutPanel.Name = "buttonsFlowLayoutPanel";
            buttonsFlowLayoutPanel.Size = new Size(292, 31);
            buttonsFlowLayoutPanel.TabIndex = 21;
            buttonsFlowLayoutPanel.WrapContents = false;
            // 
            // cancelarButton
            // 
            cancelarButton.Anchor = AnchorStyles.Right;
            cancelarButton.AutoSize = true;
            cancelarButton.Location = new Point(3, 3);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(159, 25);
            cancelarButton.TabIndex = 23;
            cancelarButton.Text = "Cancelar / Limpiar campos";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += CancelarButton_Click;
            // 
            // guardarProfesionalButton
            // 
            guardarProfesionalButton.Anchor = AnchorStyles.Right;
            guardarProfesionalButton.AutoSize = true;
            guardarProfesionalButton.Location = new Point(168, 3);
            guardarProfesionalButton.Name = "guardarProfesionalButton";
            guardarProfesionalButton.Size = new Size(121, 25);
            guardarProfesionalButton.TabIndex = 22;
            guardarProfesionalButton.Text = "Guardar Profesional";
            guardarProfesionalButton.UseVisualStyleBackColor = true;
            guardarProfesionalButton.Click += GuardarProfesionalButton_Click;
            // 
            // profesionalesDataGridView
            // 
            profesionalesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            profesionalesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            profesionalesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            profesionalesDataGridView.Location = new Point(3, 76);
            profesionalesDataGridView.Name = "profesionalesDataGridView";
            profesionalesDataGridView.Size = new Size(1076, 260);
            profesionalesDataGridView.TabIndex = 8;
            profesionalesDataGridView.CellClick += ProfesionalesDataGridView_CellContentClick;
            profesionalesDataGridView.DataBindingComplete += ProfesionalesDataGridView_DataBindingComplete;
            // 
            // Profesionales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "Profesionales";
            Size = new Size(1082, 672);
            Load += Profesionales_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            formGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            buttonsFlowLayoutPanel.ResumeLayout(false);
            buttonsFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox busquedaGroupBox;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox busquedaEspecialidadComboBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox busquedaProfesionalTextBox;
        private Button filtrarButton;
        private LinkLabel limpiarFiltrosLinkLabel;
        private ComboBox busquedaEstadoComboBox;
        private Button AgregarProfesionalButton;
        private GroupBox formGroupBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private TextBox nombreTextBox;
        private Label label7;
        private TextBox matriculaTextBox;
        private Label label10;
        private TextBox emailTextBox;
        private Label label5;
        private TextBox apellidoTextBox;
        private Label label8;
        private ComboBox especialidadComboBox;
        private CheckBox habilitadoCheckBox;
        private Label label6;
        private TextBox documentoTextBox;
        private Label label9;
        private TextBox telefonoTextBox;
        private Label label11;
        private FlowLayoutPanel buttonsFlowLayoutPanel;
        private Button cancelarButton;
        private Button guardarProfesionalButton;
        private DataGridView profesionalesDataGridView;
    }
}
