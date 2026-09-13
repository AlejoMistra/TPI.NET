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
            label6 = new Label();
            documentoTextBox = new TextBox();
            label9 = new Label();
            telefonoTextBox = new TextBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            guardarProfesionalButton = new Button();
            cancelarButton = new Button();
            label11 = new Label();
            habilitadoCheckBox = new CheckBox();
            profesionalesDataGridView = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            formGroupBox.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // busquedaGroupBox
            // 
            busquedaGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            busquedaGroupBox.Controls.Add(tableLayoutPanel1);
            busquedaGroupBox.Dock = DockStyle.Fill;
            busquedaGroupBox.Location = new Point(3, 3);
            busquedaGroupBox.Name = "busquedaGroupBox";
            busquedaGroupBox.Size = new Size(1077, 66);
            busquedaGroupBox.TabIndex = 7;
            busquedaGroupBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
            tableLayoutPanel1.Size = new Size(1071, 44);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // busquedaEspecialidadComboBox
            // 
            busquedaEspecialidadComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            busquedaEspecialidadComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            busquedaEspecialidadComboBox.FormattingEnabled = true;
            busquedaEspecialidadComboBox.Location = new Point(244, 18);
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
            label2.Location = new Point(244, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 1;
            label2.Text = "Especialidad Médica";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(402, 0);
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
            busquedaProfesionalTextBox.Size = new Size(235, 23);
            busquedaProfesionalTextBox.TabIndex = 3;
            // 
            // filtrarButton
            // 
            filtrarButton.Location = new Point(533, 18);
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
            limpiarFiltrosLinkLabel.Location = new Point(664, 22);
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
            busquedaEstadoComboBox.Location = new Point(402, 18);
            busquedaEstadoComboBox.Name = "busquedaEstadoComboBox";
            busquedaEstadoComboBox.Size = new Size(125, 23);
            busquedaEstadoComboBox.TabIndex = 5;
            // 
            // AgregarProfesionalButton
            // 
            AgregarProfesionalButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AgregarProfesionalButton.Location = new Point(943, 18);
            AgregarProfesionalButton.Name = "AgregarProfesionalButton";
            AgregarProfesionalButton.Size = new Size(125, 23);
            AgregarProfesionalButton.TabIndex = 8;
            AgregarProfesionalButton.Text = "Nuevo Profesional";
            AgregarProfesionalButton.UseVisualStyleBackColor = true;
            AgregarProfesionalButton.Click += AgregarProfesionalButton_Click;
            // 
            // formGroupBox
            // 
            formGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            formGroupBox.Controls.Add(tableLayoutPanel2);
            formGroupBox.Dock = DockStyle.Fill;
            formGroupBox.Location = new Point(3, 291);
            formGroupBox.Name = "formGroupBox";
            formGroupBox.Size = new Size(1077, 205);
            formGroupBox.TabIndex = 9;
            formGroupBox.TabStop = false;
            formGroupBox.Text = "Formulario de Alta / Edición de Profesional";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
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
            tableLayoutPanel2.Controls.Add(label6, 2, 1);
            tableLayoutPanel2.Controls.Add(documentoTextBox, 2, 2);
            tableLayoutPanel2.Controls.Add(label9, 2, 3);
            tableLayoutPanel2.Controls.Add(telefonoTextBox, 2, 4);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 2, 8);
            tableLayoutPanel2.Controls.Add(label11, 2, 7);
            tableLayoutPanel2.Controls.Add(habilitadoCheckBox, 1, 6);
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
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(1071, 183);
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
            emailTextBox.Location = new Point(3, 106);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(350, 23);
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
            apellidoTextBox.Size = new Size(351, 23);
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
            especialidadComboBox.Size = new Size(351, 23);
            especialidadComboBox.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(716, 0);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 17;
            label6.Text = "Nro Documento";
            // 
            // documentoTextBox
            // 
            documentoTextBox.Dock = DockStyle.Fill;
            documentoTextBox.Location = new Point(716, 18);
            documentoTextBox.Name = "documentoTextBox";
            documentoTextBox.Size = new Size(352, 23);
            documentoTextBox.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(716, 44);
            label9.Name = "label9";
            label9.Size = new Size(121, 15);
            label9.TabIndex = 19;
            label9.Text = "Teléfono de Contacto";
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Dock = DockStyle.Fill;
            telefonoTextBox.Location = new Point(716, 62);
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(352, 23);
            telefonoTextBox.TabIndex = 7;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(guardarProfesionalButton, 0, 0);
            tableLayoutPanel4.Controls.Add(cancelarButton, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Right;
            tableLayoutPanel4.Location = new Point(716, 135);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RightToLeft = RightToLeft.Yes;
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(352, 45);
            tableLayoutPanel4.TabIndex = 21;
            // 
            // guardarProfesionalButton
            // 
            guardarProfesionalButton.Location = new Point(224, 3);
            guardarProfesionalButton.Name = "guardarProfesionalButton";
            guardarProfesionalButton.RightToLeft = RightToLeft.No;
            guardarProfesionalButton.Size = new Size(125, 25);
            guardarProfesionalButton.TabIndex = 24;
            guardarProfesionalButton.Text = "Guardar Profesional";
            guardarProfesionalButton.UseVisualStyleBackColor = true;
            guardarProfesionalButton.Click += GuardarProfesionalButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(93, 3);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.RightToLeft = RightToLeft.No;
            cancelarButton.Size = new Size(125, 25);
            cancelarButton.TabIndex = 23;
            cancelarButton.Text = "Cancelar / Limpiar campos";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += CancelarButton_Click;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Left;
            label11.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label11, 2);
            label11.Location = new Point(3, 150);
            label11.Name = "label11";
            label11.Size = new Size(401, 15);
            label11.TabIndex = 20;
            label11.Text = "Los campos marcados con (*) son obligatorios para registrar al Profesional.";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            label11.Click += label11_Click;
            // 
            // habilitadoCheckBox
            // 
            habilitadoCheckBox.Location = new Point(359, 106);
            habilitadoCheckBox.Name = "habilitadoCheckBox";
            habilitadoCheckBox.Size = new Size(220, 19);
            habilitadoCheckBox.TabIndex = 9;
            habilitadoCheckBox.Text = "Habilitado para asignación de turnos";
            habilitadoCheckBox.UseVisualStyleBackColor = true;
            // 
            // profesionalesDataGridView
            // 
            profesionalesDataGridView.AllowUserToAddRows = false;
            profesionalesDataGridView.AllowUserToDeleteRows = false;
            profesionalesDataGridView.AllowUserToOrderColumns = true;
            profesionalesDataGridView.AllowUserToResizeRows = false;
            profesionalesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            profesionalesDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            profesionalesDataGridView.Dock = DockStyle.Fill;
            profesionalesDataGridView.Location = new Point(3, 75);
            profesionalesDataGridView.Name = "profesionalesDataGridView";
            profesionalesDataGridView.RowHeadersWidth = 51;
            profesionalesDataGridView.Size = new Size(1077, 210);
            profesionalesDataGridView.TabIndex = 8;
            profesionalesDataGridView.CellClick += ProfesionalesDataGridView_CellContentClick;
            profesionalesDataGridView.DataBindingComplete += ProfesionalesDataGridView_DataBindingComplete;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoScroll = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(busquedaGroupBox, 0, 0);
            tableLayoutPanel3.Controls.Add(formGroupBox, 0, 2);
            tableLayoutPanel3.Controls.Add(profesionalesDataGridView, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(1083, 499);
            tableLayoutPanel3.TabIndex = 10;
            // 
            // Profesionales
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel3);
            Name = "Profesionales";
            Size = new Size(1083, 499);
            Load += Profesionales_Load;
            busquedaGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            formGroupBox.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)profesionalesDataGridView).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
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
        private Button cancelarButton;
        private DataGridView profesionalesDataGridView;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Button guardarProfesionalButton;
    }
}
