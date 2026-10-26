namespace WindowsForms.DatosMaestros
{
    partial class ProfesionalModalForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            subtituloLabel = new Label();
            tituloLabel = new Label();
            mainPanel = new Panel();
            datosGroupBox = new GroupBox();
            fieldsTableLayoutPanel = new TableLayoutPanel();
            apellidoLabel = new Label();
            apellidoTextBox = new TextBox();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            matriculaLabel = new Label();
            matriculaTextBox = new TextBox();
            tipoDocumentoLabel = new Label();
            tipoDocumentoComboBox = new ComboBox();
            nroDocumentoLabel = new Label();
            nroDocumentoTextBox = new TextBox();
            especialidadLabel = new Label();
            especialidadComboBox = new ComboBox();
            telefonoLabel = new Label();
            telefonoTextBox = new TextBox();
            emailLabel = new Label();
            emailTextBox = new TextBox();
            estadoLabel = new Label();
            activoCheckBox = new CheckBox();
            footerPanel = new FlowLayoutPanel();
            cancelarButton = new Button();
            guardarButton = new Button();
            headerPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            datosGroupBox.SuspendLayout();
            fieldsTableLayoutPanel.SuspendLayout();
            footerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(subtituloLabel);
            headerPanel.Controls.Add(tituloLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(18, 14, 18, 14);
            headerPanel.Size = new Size(540, 68);
            headerPanel.TabIndex = 0;
            // 
            // subtituloLabel
            // 
            subtituloLabel.AutoSize = true;
            subtituloLabel.ForeColor = Color.DimGray;
            subtituloLabel.Location = new Point(19, 38);
            subtituloLabel.Name = "subtituloLabel";
            subtituloLabel.Size = new Size(392, 15);
            subtituloLabel.TabIndex = 1;
            subtituloLabel.Text = "Complete los datos personales, matrícula y especialidad del profesional.";
            // 
            // tituloLabel
            // 
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            tituloLabel.Location = new Point(18, 12);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(155, 21);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Nuevo Profesional";
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(datosGroupBox);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 68);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(16, 12, 16, 8);
            mainPanel.Size = new Size(540, 412);
            mainPanel.TabIndex = 1;
            // 
            // datosGroupBox
            // 
            datosGroupBox.Controls.Add(fieldsTableLayoutPanel);
            datosGroupBox.Dock = DockStyle.Fill;
            datosGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            datosGroupBox.Location = new Point(16, 12);
            datosGroupBox.Name = "datosGroupBox";
            datosGroupBox.Padding = new Padding(12, 10, 12, 10);
            datosGroupBox.Size = new Size(508, 392);
            datosGroupBox.TabIndex = 0;
            datosGroupBox.TabStop = false;
            datosGroupBox.Text = "Información del Profesional";
            // 
            // fieldsTableLayoutPanel
            // 
            fieldsTableLayoutPanel.ColumnCount = 2;
            fieldsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            fieldsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            fieldsTableLayoutPanel.Controls.Add(apellidoLabel, 0, 0);
            fieldsTableLayoutPanel.Controls.Add(apellidoTextBox, 1, 0);
            fieldsTableLayoutPanel.Controls.Add(nombreLabel, 0, 1);
            fieldsTableLayoutPanel.Controls.Add(nombreTextBox, 1, 1);
            fieldsTableLayoutPanel.Controls.Add(matriculaLabel, 0, 2);
            fieldsTableLayoutPanel.Controls.Add(matriculaTextBox, 1, 2);
            fieldsTableLayoutPanel.Controls.Add(tipoDocumentoLabel, 0, 3);
            fieldsTableLayoutPanel.Controls.Add(tipoDocumentoComboBox, 1, 3);
            fieldsTableLayoutPanel.Controls.Add(nroDocumentoLabel, 0, 4);
            fieldsTableLayoutPanel.Controls.Add(nroDocumentoTextBox, 1, 4);
            fieldsTableLayoutPanel.Controls.Add(especialidadLabel, 0, 5);
            fieldsTableLayoutPanel.Controls.Add(especialidadComboBox, 1, 5);
            fieldsTableLayoutPanel.Controls.Add(telefonoLabel, 0, 6);
            fieldsTableLayoutPanel.Controls.Add(telefonoTextBox, 1, 6);
            fieldsTableLayoutPanel.Controls.Add(emailLabel, 0, 7);
            fieldsTableLayoutPanel.Controls.Add(emailTextBox, 1, 7);
            fieldsTableLayoutPanel.Controls.Add(estadoLabel, 0, 8);
            fieldsTableLayoutPanel.Controls.Add(activoCheckBox, 1, 8);
            fieldsTableLayoutPanel.Dock = DockStyle.Fill;
            fieldsTableLayoutPanel.Location = new Point(12, 26);
            fieldsTableLayoutPanel.Name = "fieldsTableLayoutPanel";
            fieldsTableLayoutPanel.RowCount = 9;
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.Size = new Size(484, 356);
            fieldsTableLayoutPanel.TabIndex = 0;
            // 
            // apellidoLabel
            // 
            apellidoLabel.Anchor = AnchorStyles.Left;
            apellidoLabel.AutoSize = true;
            apellidoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            apellidoLabel.Location = new Point(3, 11);
            apellidoLabel.Name = "apellidoLabel";
            apellidoLabel.Size = new Size(65, 15);
            apellidoLabel.TabIndex = 0;
            apellidoLabel.Text = "Apellido *:";
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            apellidoTextBox.Location = new Point(172, 7);
            apellidoTextBox.MaxLength = 100;
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(309, 23);
            apellidoTextBox.TabIndex = 1;
            // 
            // nombreLabel
            // 
            nombreLabel.Anchor = AnchorStyles.Left;
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            nombreLabel.Location = new Point(3, 49);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(66, 15);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre *:";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nombreTextBox.Location = new Point(172, 45);
            nombreTextBox.MaxLength = 100;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(309, 23);
            nombreTextBox.TabIndex = 3;
            // 
            // matriculaLabel
            // 
            matriculaLabel.Anchor = AnchorStyles.Left;
            matriculaLabel.AutoSize = true;
            matriculaLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            matriculaLabel.Location = new Point(3, 87);
            matriculaLabel.Name = "matriculaLabel";
            matriculaLabel.Size = new Size(72, 15);
            matriculaLabel.TabIndex = 4;
            matriculaLabel.Text = "Matrícula *:";
            // 
            // matriculaTextBox
            // 
            matriculaTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            matriculaTextBox.Location = new Point(172, 83);
            matriculaTextBox.MaxLength = 50;
            matriculaTextBox.Name = "matriculaTextBox";
            matriculaTextBox.PlaceholderText = "Ej: MP 12345";
            matriculaTextBox.Size = new Size(309, 23);
            matriculaTextBox.TabIndex = 5;
            // 
            // tipoDocumentoLabel
            // 
            tipoDocumentoLabel.Anchor = AnchorStyles.Left;
            tipoDocumentoLabel.AutoSize = true;
            tipoDocumentoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tipoDocumentoLabel.Location = new Point(3, 125);
            tipoDocumentoLabel.Name = "tipoDocumentoLabel";
            tipoDocumentoLabel.Size = new Size(128, 15);
            tipoDocumentoLabel.TabIndex = 6;
            tipoDocumentoLabel.Text = "Tipo de Documento *:";
            // 
            // tipoDocumentoComboBox
            // 
            tipoDocumentoComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tipoDocumentoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoDocumentoComboBox.FormattingEnabled = true;
            tipoDocumentoComboBox.Location = new Point(172, 121);
            tipoDocumentoComboBox.Name = "tipoDocumentoComboBox";
            tipoDocumentoComboBox.Size = new Size(309, 23);
            tipoDocumentoComboBox.TabIndex = 7;
            tipoDocumentoComboBox.SelectedIndexChanged += TipoDocumentoComboBox_SelectedIndexChanged;
            // 
            // nroDocumentoLabel
            // 
            nroDocumentoLabel.Anchor = AnchorStyles.Left;
            nroDocumentoLabel.AutoSize = true;
            nroDocumentoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            nroDocumentoLabel.Location = new Point(3, 163);
            nroDocumentoLabel.Name = "nroDocumentoLabel";
            nroDocumentoLabel.Size = new Size(123, 15);
            nroDocumentoLabel.TabIndex = 8;
            nroDocumentoLabel.Text = "N° de Documento *:";
            // 
            // nroDocumentoTextBox
            // 
            nroDocumentoTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nroDocumentoTextBox.Location = new Point(172, 159);
            nroDocumentoTextBox.MaxLength = 20;
            nroDocumentoTextBox.Name = "nroDocumentoTextBox";
            nroDocumentoTextBox.Size = new Size(309, 23);
            nroDocumentoTextBox.TabIndex = 9;
            // 
            // especialidadLabel
            // 
            especialidadLabel.Anchor = AnchorStyles.Left;
            especialidadLabel.AutoSize = true;
            especialidadLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            especialidadLabel.Location = new Point(3, 201);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(88, 15);
            especialidadLabel.TabIndex = 10;
            especialidadLabel.Text = "Especialidad *:";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            especialidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(172, 197);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(309, 23);
            especialidadComboBox.TabIndex = 11;
            // 
            // telefonoLabel
            // 
            telefonoLabel.Anchor = AnchorStyles.Left;
            telefonoLabel.AutoSize = true;
            telefonoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            telefonoLabel.Location = new Point(3, 239);
            telefonoLabel.Name = "telefonoLabel";
            telefonoLabel.Size = new Size(55, 15);
            telefonoLabel.TabIndex = 12;
            telefonoLabel.Text = "Teléfono:";
            // 
            // telefonoTextBox
            // 
            telefonoTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            telefonoTextBox.Location = new Point(172, 235);
            telefonoTextBox.MaxLength = 20;
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.PlaceholderText = "Ej: 11-4567-8901";
            telefonoTextBox.Size = new Size(309, 23);
            telefonoTextBox.TabIndex = 13;
            // 
            // emailLabel
            // 
            emailLabel.Anchor = AnchorStyles.Left;
            emailLabel.AutoSize = true;
            emailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            emailLabel.Location = new Point(3, 277);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(108, 15);
            emailLabel.TabIndex = 14;
            emailLabel.Text = "Correo Electrónico:";
            // 
            // emailTextBox
            // 
            emailTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            emailTextBox.Location = new Point(172, 273);
            emailTextBox.MaxLength = 100;
            emailTextBox.Name = "emailTextBox";
            emailTextBox.PlaceholderText = "ejemplo@correo.com";
            emailTextBox.Size = new Size(309, 23);
            emailTextBox.TabIndex = 15;
            // 
            // estadoLabel
            // 
            estadoLabel.Anchor = AnchorStyles.Left;
            estadoLabel.AutoSize = true;
            estadoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            estadoLabel.Location = new Point(3, 318);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(45, 15);
            estadoLabel.TabIndex = 16;
            estadoLabel.Text = "Estado:";
            // 
            // activoCheckBox
            // 
            activoCheckBox.Anchor = AnchorStyles.Left;
            activoCheckBox.AutoSize = true;
            activoCheckBox.Checked = true;
            activoCheckBox.CheckState = CheckState.Checked;
            activoCheckBox.Cursor = Cursors.Hand;
            activoCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            activoCheckBox.Location = new Point(172, 316);
            activoCheckBox.Name = "activoCheckBox";
            activoCheckBox.Size = new Size(122, 19);
            activoCheckBox.TabIndex = 17;
            activoCheckBox.Text = "Profesional Activo";
            activoCheckBox.UseVisualStyleBackColor = true;
            // 
            // footerPanel
            // 
            footerPanel.BackColor = SystemColors.Control;
            footerPanel.Controls.Add(guardarButton);
            footerPanel.Controls.Add(cancelarButton);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.FlowDirection = FlowDirection.RightToLeft;
            footerPanel.Location = new Point(0, 480);
            footerPanel.Name = "footerPanel";
            footerPanel.Padding = new Padding(16, 12, 16, 12);
            footerPanel.Size = new Size(540, 56);
            footerPanel.TabIndex = 2;
            // 
            // cancelarButton
            // 
            cancelarButton.Cursor = Cursors.Hand;
            cancelarButton.DialogResult = DialogResult.Cancel;
            cancelarButton.Location = new Point(310, 12);
            cancelarButton.Margin = new Padding(0, 0, 10, 0);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(95, 32);
            cancelarButton.TabIndex = 1;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += CancelarButton_Click;
            // 
            // guardarButton
            // 
            guardarButton.BackColor = Color.FromArgb(13, 110, 253);
            guardarButton.Cursor = Cursors.Hand;
            guardarButton.FlatAppearance.BorderSize = 0;
            guardarButton.FlatStyle = FlatStyle.Flat;
            guardarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            guardarButton.ForeColor = Color.White;
            guardarButton.Location = new Point(415, 12);
            guardarButton.Margin = new Padding(0);
            guardarButton.Name = "guardarButton";
            guardarButton.Size = new Size(130, 32);
            guardarButton.TabIndex = 0;
            guardarButton.Text = "Guardar Profesional";
            guardarButton.UseVisualStyleBackColor = false;
            guardarButton.Click += GuardarButton_Click;
            // 
            // ProfesionalModalForm
            // 
            AcceptButton = guardarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(540, 536);
            Controls.Add(mainPanel);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProfesionalModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Profesional";
            Load += ProfesionalModalForm_Load;
            KeyDown += ProfesionalModalForm_KeyDown;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            datosGroupBox.ResumeLayout(false);
            fieldsTableLayoutPanel.ResumeLayout(false);
            fieldsTableLayoutPanel.PerformLayout();
            footerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel headerPanel;
        private Label subtituloLabel;
        private Label tituloLabel;
        private Panel mainPanel;
        private GroupBox datosGroupBox;
        private TableLayoutPanel fieldsTableLayoutPanel;
        private Label apellidoLabel;
        private TextBox apellidoTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label matriculaLabel;
        private TextBox matriculaTextBox;
        private Label tipoDocumentoLabel;
        private ComboBox tipoDocumentoComboBox;
        private Label nroDocumentoLabel;
        private TextBox nroDocumentoTextBox;
        private Label especialidadLabel;
        private ComboBox especialidadComboBox;
        private Label telefonoLabel;
        private TextBox telefonoTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label estadoLabel;
        private CheckBox activoCheckBox;
        private FlowLayoutPanel footerPanel;
        private Button guardarButton;
        private Button cancelarButton;
    }
}

