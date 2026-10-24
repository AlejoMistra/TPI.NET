namespace WindowsForms.DatosMaestros
{
    partial class TurnoModalForm
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
            numeroTurnoLabel = new Label();
            numeroTurnoTextBox = new TextBox();
            especialidadLabel = new Label();
            especialidadComboBox = new ComboBox();
            profesionalLabel = new Label();
            profesionalComboBox = new ComboBox();
            fechaTurnoLabel = new Label();
            fechaTurnoDateTimePicker = new DateTimePicker();
            horaInicioLabel = new Label();
            horaInicioDateTimePicker = new DateTimePicker();
            horaFinLabel = new Label();
            horaFinDateTimePicker = new DateTimePicker();
            estadoLabel = new Label();
            estadoComboBox = new ComboBox();
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
            subtituloLabel.Size = new Size(385, 15);
            subtituloLabel.TabIndex = 1;
            subtituloLabel.Text = "Configure la fecha, horario, profesional y estado del turno de atención.";
            // 
            // tituloLabel
            // 
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            tituloLabel.Location = new Point(18, 12);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(110, 21);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Nuevo Turno";
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(datosGroupBox);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 68);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(16, 12, 16, 8);
            mainPanel.Size = new Size(540, 336);
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
            datosGroupBox.Size = new Size(508, 316);
            datosGroupBox.TabIndex = 0;
            datosGroupBox.TabStop = false;
            datosGroupBox.Text = "Información del Turno";
            // 
            // fieldsTableLayoutPanel
            // 
            fieldsTableLayoutPanel.ColumnCount = 2;
            fieldsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            fieldsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            fieldsTableLayoutPanel.Controls.Add(numeroTurnoLabel, 0, 0);
            fieldsTableLayoutPanel.Controls.Add(numeroTurnoTextBox, 1, 0);
            fieldsTableLayoutPanel.Controls.Add(especialidadLabel, 0, 1);
            fieldsTableLayoutPanel.Controls.Add(especialidadComboBox, 1, 1);
            fieldsTableLayoutPanel.Controls.Add(profesionalLabel, 0, 2);
            fieldsTableLayoutPanel.Controls.Add(profesionalComboBox, 1, 2);
            fieldsTableLayoutPanel.Controls.Add(fechaTurnoLabel, 0, 3);
            fieldsTableLayoutPanel.Controls.Add(fechaTurnoDateTimePicker, 1, 3);
            fieldsTableLayoutPanel.Controls.Add(horaInicioLabel, 0, 4);
            fieldsTableLayoutPanel.Controls.Add(horaInicioDateTimePicker, 1, 4);
            fieldsTableLayoutPanel.Controls.Add(horaFinLabel, 0, 5);
            fieldsTableLayoutPanel.Controls.Add(horaFinDateTimePicker, 1, 5);
            fieldsTableLayoutPanel.Controls.Add(estadoLabel, 0, 6);
            fieldsTableLayoutPanel.Controls.Add(estadoComboBox, 1, 6);
            fieldsTableLayoutPanel.Dock = DockStyle.Fill;
            fieldsTableLayoutPanel.Location = new Point(12, 26);
            fieldsTableLayoutPanel.Name = "fieldsTableLayoutPanel";
            fieldsTableLayoutPanel.RowCount = 7;
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            fieldsTableLayoutPanel.Size = new Size(484, 278);
            fieldsTableLayoutPanel.TabIndex = 0;
            // 
            // numeroTurnoLabel
            // 
            numeroTurnoLabel.Anchor = AnchorStyles.Left;
            numeroTurnoLabel.AutoSize = true;
            numeroTurnoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numeroTurnoLabel.Location = new Point(3, 11);
            numeroTurnoLabel.Name = "numeroTurnoLabel";
            numeroTurnoLabel.Size = new Size(59, 15);
            numeroTurnoLabel.TabIndex = 0;
            numeroTurnoLabel.Text = "N° Turno:";
            // 
            // numeroTurnoTextBox
            // 
            numeroTurnoTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numeroTurnoTextBox.BackColor = SystemColors.Control;
            numeroTurnoTextBox.Location = new Point(172, 7);
            numeroTurnoTextBox.Name = "numeroTurnoTextBox";
            numeroTurnoTextBox.ReadOnly = true;
            numeroTurnoTextBox.Size = new Size(309, 23);
            numeroTurnoTextBox.TabIndex = 1;
            numeroTurnoTextBox.TabStop = false;
            // 
            // especialidadLabel
            // 
            especialidadLabel.Anchor = AnchorStyles.Left;
            especialidadLabel.AutoSize = true;
            especialidadLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            especialidadLabel.Location = new Point(3, 49);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(75, 15);
            especialidadLabel.TabIndex = 2;
            especialidadLabel.Text = "Especialidad:";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            especialidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(172, 45);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(309, 23);
            especialidadComboBox.TabIndex = 3;
            // 
            // profesionalLabel
            // 
            profesionalLabel.Anchor = AnchorStyles.Left;
            profesionalLabel.AutoSize = true;
            profesionalLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            profesionalLabel.Location = new Point(3, 87);
            profesionalLabel.Name = "profesionalLabel";
            profesionalLabel.Size = new Size(82, 15);
            profesionalLabel.TabIndex = 4;
            profesionalLabel.Text = "Profesional *:";
            // 
            // profesionalComboBox
            // 
            profesionalComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            profesionalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profesionalComboBox.FormattingEnabled = true;
            profesionalComboBox.Location = new Point(172, 83);
            profesionalComboBox.Name = "profesionalComboBox";
            profesionalComboBox.Size = new Size(309, 23);
            profesionalComboBox.TabIndex = 5;
            // 
            // fechaTurnoLabel
            // 
            fechaTurnoLabel.Anchor = AnchorStyles.Left;
            fechaTurnoLabel.AutoSize = true;
            fechaTurnoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            fechaTurnoLabel.Location = new Point(3, 125);
            fechaTurnoLabel.Name = "fechaTurnoLabel";
            fechaTurnoLabel.Size = new Size(51, 15);
            fechaTurnoLabel.TabIndex = 6;
            fechaTurnoLabel.Text = "Fecha *:";
            // 
            // fechaTurnoDateTimePicker
            // 
            fechaTurnoDateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            fechaTurnoDateTimePicker.CustomFormat = "dd/MM/yyyy";
            fechaTurnoDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaTurnoDateTimePicker.Location = new Point(172, 121);
            fechaTurnoDateTimePicker.Name = "fechaTurnoDateTimePicker";
            fechaTurnoDateTimePicker.Size = new Size(309, 23);
            fechaTurnoDateTimePicker.TabIndex = 7;
            // 
            // horaInicioLabel
            // 
            horaInicioLabel.Anchor = AnchorStyles.Left;
            horaInicioLabel.AutoSize = true;
            horaInicioLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            horaInicioLabel.Location = new Point(3, 163);
            horaInicioLabel.Name = "horaInicioLabel";
            horaInicioLabel.Size = new Size(79, 15);
            horaInicioLabel.TabIndex = 8;
            horaInicioLabel.Text = "Hora Inicio *:";
            // 
            // horaInicioDateTimePicker
            // 
            horaInicioDateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            horaInicioDateTimePicker.CustomFormat = "HH:mm";
            horaInicioDateTimePicker.Format = DateTimePickerFormat.Custom;
            horaInicioDateTimePicker.Location = new Point(172, 159);
            horaInicioDateTimePicker.Name = "horaInicioDateTimePicker";
            horaInicioDateTimePicker.ShowUpDown = true;
            horaInicioDateTimePicker.Size = new Size(309, 23);
            horaInicioDateTimePicker.TabIndex = 9;
            // 
            // horaFinLabel
            // 
            horaFinLabel.Anchor = AnchorStyles.Left;
            horaFinLabel.AutoSize = true;
            horaFinLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            horaFinLabel.Location = new Point(3, 201);
            horaFinLabel.Name = "horaFinLabel";
            horaFinLabel.Size = new Size(66, 15);
            horaFinLabel.TabIndex = 10;
            horaFinLabel.Text = "Hora Fin *:";
            // 
            // horaFinDateTimePicker
            // 
            horaFinDateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            horaFinDateTimePicker.CustomFormat = "HH:mm";
            horaFinDateTimePicker.Format = DateTimePickerFormat.Custom;
            horaFinDateTimePicker.Location = new Point(172, 197);
            horaFinDateTimePicker.Name = "horaFinDateTimePicker";
            horaFinDateTimePicker.ShowUpDown = true;
            horaFinDateTimePicker.Size = new Size(309, 23);
            horaFinDateTimePicker.TabIndex = 11;
            // 
            // estadoLabel
            // 
            estadoLabel.Anchor = AnchorStyles.Left;
            estadoLabel.AutoSize = true;
            estadoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            estadoLabel.Location = new Point(3, 241);
            estadoLabel.Name = "estadoLabel";
            estadoLabel.Size = new Size(55, 15);
            estadoLabel.TabIndex = 12;
            estadoLabel.Text = "Estado *:";
            // 
            // estadoComboBox
            // 
            estadoComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            estadoComboBox.FormattingEnabled = true;
            estadoComboBox.Location = new Point(172, 237);
            estadoComboBox.Name = "estadoComboBox";
            estadoComboBox.Size = new Size(309, 23);
            estadoComboBox.TabIndex = 13;
            // 
            // footerPanel
            // 
            footerPanel.BackColor = SystemColors.Control;
            footerPanel.Controls.Add(guardarButton);
            footerPanel.Controls.Add(cancelarButton);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.FlowDirection = FlowDirection.RightToLeft;
            footerPanel.Location = new Point(0, 404);
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
            guardarButton.Text = "Guardar Turno";
            guardarButton.UseVisualStyleBackColor = false;
            guardarButton.Click += GuardarButton_Click;
            // 
            // TurnoModalForm
            // 
            AcceptButton = guardarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(540, 460);
            Controls.Add(mainPanel);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TurnoModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Turno";
            Load += TurnoModalForm_Load;
            KeyDown += TurnoModalForm_KeyDown;
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
        private Label numeroTurnoLabel;
        private TextBox numeroTurnoTextBox;
        private Label especialidadLabel;
        private ComboBox especialidadComboBox;
        private Label profesionalLabel;
        private ComboBox profesionalComboBox;
        private Label fechaTurnoLabel;
        private DateTimePicker fechaTurnoDateTimePicker;
        private Label horaInicioLabel;
        private DateTimePicker horaInicioDateTimePicker;
        private Label horaFinLabel;
        private DateTimePicker horaFinDateTimePicker;
        private Label estadoLabel;
        private ComboBox estadoComboBox;
        private FlowLayoutPanel footerPanel;
        private Button guardarButton;
        private Button cancelarButton;
    }
}

