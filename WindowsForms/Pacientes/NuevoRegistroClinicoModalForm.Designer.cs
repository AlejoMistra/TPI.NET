namespace WindowsForms
{
    partial class NuevoRegistroClinicoModalForm
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
            mainTableLayoutPanel = new TableLayoutPanel();
            pacienteHeaderPanel = new Panel();
            avatarLabel = new Label();
            pacienteNombreLabel = new Label();
            pacienteMetaLabel = new Label();
            tipoYFechaTableLayoutPanel = new TableLayoutPanel();
            tipoRegistroTitleLabel = new Label();
            fechaTitleLabel = new Label();
            tipoRegistroComboBox = new ComboBox();
            fechaDateTimePicker = new DateTimePicker();
            tipoInfoTextLabel = new Label();
            turnoSelectorTableLayoutPanel = new TableLayoutPanel();
            turnoTitleLabel = new Label();
            turnoOrigenComboBox = new ComboBox();
            desvincularTurnoButton = new Button();
            descripcionTableLayoutPanel = new TableLayoutPanel();
            descripcionHeaderPanel = new TableLayoutPanel();
            descripcionTitleLabel = new Label();
            caracteresCounterLabel = new Label();
            descripcionTextBox = new TextBox();
            footerPanel = new TableLayoutPanel();
            limpiarButton = new Button();
            botonesDerechaFlowPanel = new FlowLayoutPanel();
            guardarButton = new Button();
            cancelarButton = new Button();
            mainTableLayoutPanel.SuspendLayout();
            pacienteHeaderPanel.SuspendLayout();
            tipoYFechaTableLayoutPanel.SuspendLayout();
            turnoSelectorTableLayoutPanel.SuspendLayout();
            descripcionTableLayoutPanel.SuspendLayout();
            descripcionHeaderPanel.SuspendLayout();
            footerPanel.SuspendLayout();
            botonesDerechaFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(pacienteHeaderPanel, 0, 0);
            mainTableLayoutPanel.Controls.Add(tipoYFechaTableLayoutPanel, 0, 1);
            mainTableLayoutPanel.Controls.Add(turnoSelectorTableLayoutPanel, 0, 2);
            mainTableLayoutPanel.Controls.Add(descripcionTableLayoutPanel, 0, 3);
            mainTableLayoutPanel.Controls.Add(footerPanel, 0, 4);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Margin = new Padding(0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.Padding = new Padding(16, 12, 16, 12);
            mainTableLayoutPanel.RowCount = 5;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 106F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            mainTableLayoutPanel.Size = new Size(780, 570);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // pacienteHeaderPanel
            // 
            pacienteHeaderPanel.BackColor = Color.FromArgb(243, 246, 250);
            pacienteHeaderPanel.Controls.Add(avatarLabel);
            pacienteHeaderPanel.Controls.Add(pacienteNombreLabel);
            pacienteHeaderPanel.Controls.Add(pacienteMetaLabel);
            pacienteHeaderPanel.Dock = DockStyle.Fill;
            pacienteHeaderPanel.Location = new Point(16, 12);
            pacienteHeaderPanel.Margin = new Padding(0, 0, 0, 6);
            pacienteHeaderPanel.Name = "pacienteHeaderPanel";
            pacienteHeaderPanel.Padding = new Padding(8, 6, 8, 6);
            pacienteHeaderPanel.Size = new Size(748, 46);
            pacienteHeaderPanel.TabIndex = 0;
            // 
            // avatarLabel
            // 
            avatarLabel.BackColor = Color.FromArgb(219, 234, 254);
            avatarLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            avatarLabel.ForeColor = Color.FromArgb(29, 78, 216);
            avatarLabel.Location = new Point(8, 6);
            avatarLabel.Name = "avatarLabel";
            avatarLabel.Size = new Size(34, 34);
            avatarLabel.TabIndex = 0;
            avatarLabel.Text = "CG";
            avatarLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pacienteNombreLabel
            // 
            pacienteNombreLabel.AutoSize = true;
            pacienteNombreLabel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            pacienteNombreLabel.ForeColor = Color.FromArgb(30, 41, 59);
            pacienteNombreLabel.Location = new Point(50, 4);
            pacienteNombreLabel.Name = "pacienteNombreLabel";
            pacienteNombreLabel.Size = new Size(153, 19);
            pacienteNombreLabel.TabIndex = 1;
            pacienteNombreLabel.Text = "Carlos Alberto García";
            // 
            // pacienteMetaLabel
            // 
            pacienteMetaLabel.AutoSize = true;
            pacienteMetaLabel.Font = new Font("Segoe UI", 9F);
            pacienteMetaLabel.ForeColor = Color.FromArgb(71, 85, 105);
            pacienteMetaLabel.Location = new Point(50, 24);
            pacienteMetaLabel.Name = "pacienteMetaLabel";
            pacienteMetaLabel.Size = new Size(329, 15);
            pacienteMetaLabel.TabIndex = 2;
            pacienteMetaLabel.Text = "DNI: 23.456.789 • Obra Social: OSDE 310 (Afiliado #772819-02)";
            // 
            // tipoYFechaTableLayoutPanel
            // 
            tipoYFechaTableLayoutPanel.ColumnCount = 2;
            tipoYFechaTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tipoYFechaTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tipoYFechaTableLayoutPanel.Controls.Add(tipoRegistroTitleLabel, 0, 0);
            tipoYFechaTableLayoutPanel.Controls.Add(fechaTitleLabel, 1, 0);
            tipoYFechaTableLayoutPanel.Controls.Add(tipoRegistroComboBox, 0, 1);
            tipoYFechaTableLayoutPanel.Controls.Add(fechaDateTimePicker, 1, 1);
            tipoYFechaTableLayoutPanel.Controls.Add(tipoInfoTextLabel, 0, 2);
            tipoYFechaTableLayoutPanel.Dock = DockStyle.Fill;
            tipoYFechaTableLayoutPanel.Location = new Point(16, 64);
            tipoYFechaTableLayoutPanel.Margin = new Padding(0, 0, 0, 6);
            tipoYFechaTableLayoutPanel.Name = "tipoYFechaTableLayoutPanel";
            tipoYFechaTableLayoutPanel.RowCount = 3;
            tipoYFechaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tipoYFechaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tipoYFechaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tipoYFechaTableLayoutPanel.Size = new Size(748, 100);
            tipoYFechaTableLayoutPanel.TabIndex = 1;
            // 
            // tipoRegistroTitleLabel
            // 
            tipoRegistroTitleLabel.Anchor = AnchorStyles.Left;
            tipoRegistroTitleLabel.AutoSize = true;
            tipoRegistroTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tipoRegistroTitleLabel.Location = new Point(0, 3);
            tipoRegistroTitleLabel.Margin = new Padding(0);
            tipoRegistroTitleLabel.Name = "tipoRegistroTitleLabel";
            tipoRegistroTitleLabel.Size = new Size(145, 15);
            tipoRegistroTitleLabel.TabIndex = 0;
            tipoRegistroTitleLabel.Text = "Tipo de Registro Clínico *";
            // 
            // fechaTitleLabel
            // 
            fechaTitleLabel.Anchor = AnchorStyles.Left;
            fechaTitleLabel.AutoSize = true;
            fechaTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fechaTitleLabel.Location = new Point(441, 3);
            fechaTitleLabel.Margin = new Padding(8, 0, 0, 0);
            fechaTitleLabel.Name = "fechaTitleLabel";
            fechaTitleLabel.Size = new Size(86, 15);
            fechaTitleLabel.TabIndex = 1;
            fechaTitleLabel.Text = "Fecha y Hora *";
            // 
            // tipoRegistroComboBox
            // 
            tipoRegistroComboBox.Dock = DockStyle.Fill;
            tipoRegistroComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoRegistroComboBox.Font = new Font("Segoe UI", 9.5F);
            tipoRegistroComboBox.FormattingEnabled = true;
            tipoRegistroComboBox.Location = new Point(0, 24);
            tipoRegistroComboBox.Margin = new Padding(0, 2, 12, 2);
            tipoRegistroComboBox.Name = "tipoRegistroComboBox";
            tipoRegistroComboBox.Size = new Size(421, 25);
            tipoRegistroComboBox.TabIndex = 2;
            tipoRegistroComboBox.SelectedIndexChanged += tipoRegistroComboBox_SelectedIndexChanged;
            // 
            // fechaDateTimePicker
            // 
            fechaDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            fechaDateTimePicker.Dock = DockStyle.Fill;
            fechaDateTimePicker.Font = new Font("Segoe UI", 9.5F);
            fechaDateTimePicker.Format = DateTimePickerFormat.Custom;
            fechaDateTimePicker.Location = new Point(441, 24);
            fechaDateTimePicker.Margin = new Padding(8, 2, 0, 2);
            fechaDateTimePicker.Name = "fechaDateTimePicker";
            fechaDateTimePicker.Size = new Size(307, 24);
            fechaDateTimePicker.TabIndex = 3;
            // 
            // tipoInfoTextLabel
            // 
            tipoInfoTextLabel.BackColor = Color.FromArgb(240, 247, 255);
            tipoInfoTextLabel.Dock = DockStyle.Fill;
            tipoInfoTextLabel.Font = new Font("Segoe UI", 8.25F);
            tipoInfoTextLabel.ForeColor = Color.FromArgb(30, 64, 120);
            tipoInfoTextLabel.Location = new Point(0, 54);
            tipoInfoTextLabel.Margin = new Padding(0, 2, 12, 0);
            tipoInfoTextLabel.Name = "tipoInfoTextLabel";
            tipoInfoTextLabel.Padding = new Padding(6, 4, 6, 4);
            tipoInfoTextLabel.Size = new Size(421, 46);
            tipoInfoTextLabel.TabIndex = 4;
            tipoInfoTextLabel.Text = "Evolución: Registra la actualización clínica del cuadro, hallazgos semiológicos y respuesta al tratamiento.";
            // 
            // turnoSelectorTableLayoutPanel
            // 
            turnoSelectorTableLayoutPanel.ColumnCount = 2;
            turnoSelectorTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            turnoSelectorTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
            turnoSelectorTableLayoutPanel.Controls.Add(turnoTitleLabel, 0, 0);
            turnoSelectorTableLayoutPanel.Controls.Add(turnoOrigenComboBox, 0, 1);
            turnoSelectorTableLayoutPanel.Controls.Add(desvincularTurnoButton, 1, 1);
            turnoSelectorTableLayoutPanel.Dock = DockStyle.Fill;
            turnoSelectorTableLayoutPanel.Location = new Point(16, 170);
            turnoSelectorTableLayoutPanel.Margin = new Padding(0, 0, 0, 6);
            turnoSelectorTableLayoutPanel.Name = "turnoSelectorTableLayoutPanel";
            turnoSelectorTableLayoutPanel.RowCount = 2;
            turnoSelectorTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            turnoSelectorTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            turnoSelectorTableLayoutPanel.Size = new Size(748, 52);
            turnoSelectorTableLayoutPanel.TabIndex = 2;
            // 
            // turnoTitleLabel
            // 
            turnoTitleLabel.Anchor = AnchorStyles.Left;
            turnoTitleLabel.AutoSize = true;
            turnoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            turnoTitleLabel.Location = new Point(0, 3);
            turnoTitleLabel.Margin = new Padding(0);
            turnoTitleLabel.Name = "turnoTitleLabel";
            turnoTitleLabel.Size = new Size(153, 15);
            turnoTitleLabel.TabIndex = 0;
            turnoTitleLabel.Text = "Turno de Origen";
            // 
            // turnoOrigenComboBox
            // 
            turnoOrigenComboBox.Dock = DockStyle.Fill;
            turnoOrigenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            turnoOrigenComboBox.Font = new Font("Segoe UI", 9.5F);
            turnoOrigenComboBox.FormattingEnabled = true;
            turnoOrigenComboBox.Location = new Point(0, 24);
            turnoOrigenComboBox.Margin = new Padding(0, 2, 8, 2);
            turnoOrigenComboBox.Name = "turnoOrigenComboBox";
            turnoOrigenComboBox.Size = new Size(614, 25);
            turnoOrigenComboBox.TabIndex = 1;
            // 
            // desvincularTurnoButton
            // 
            desvincularTurnoButton.Dock = DockStyle.Fill;
            desvincularTurnoButton.FlatStyle = FlatStyle.Flat;
            desvincularTurnoButton.Font = new Font("Segoe UI", 8.5F);
            desvincularTurnoButton.Location = new Point(622, 24);
            desvincularTurnoButton.Margin = new Padding(0, 2, 0, 2);
            desvincularTurnoButton.Name = "desvincularTurnoButton";
            desvincularTurnoButton.Size = new Size(126, 26);
            desvincularTurnoButton.TabIndex = 2;
            desvincularTurnoButton.Text = "Desvincular";
            desvincularTurnoButton.UseVisualStyleBackColor = true;
            desvincularTurnoButton.Click += desvincularTurnoButton_Click;
            // 
            // descripcionTableLayoutPanel
            // 
            descripcionTableLayoutPanel.ColumnCount = 1;
            descripcionTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            descripcionTableLayoutPanel.Controls.Add(descripcionHeaderPanel, 0, 0);
            descripcionTableLayoutPanel.Controls.Add(descripcionTextBox, 0, 1);
            descripcionTableLayoutPanel.Dock = DockStyle.Fill;
            descripcionTableLayoutPanel.Location = new Point(16, 228);
            descripcionTableLayoutPanel.Margin = new Padding(0, 0, 0, 6);
            descripcionTableLayoutPanel.Name = "descripcionTableLayoutPanel";
            descripcionTableLayoutPanel.RowCount = 2;
            descripcionTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            descripcionTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            descripcionTableLayoutPanel.Size = new Size(748, 292);
            descripcionTableLayoutPanel.TabIndex = 3;
            // 
            // descripcionHeaderPanel
            // 
            descripcionHeaderPanel.ColumnCount = 2;
            descripcionHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            descripcionHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            descripcionHeaderPanel.Controls.Add(descripcionTitleLabel, 0, 0);
            descripcionHeaderPanel.Controls.Add(caracteresCounterLabel, 1, 0);
            descripcionHeaderPanel.Dock = DockStyle.Fill;
            descripcionHeaderPanel.Location = new Point(0, 0);
            descripcionHeaderPanel.Margin = new Padding(0);
            descripcionHeaderPanel.Name = "descripcionHeaderPanel";
            descripcionHeaderPanel.RowCount = 1;
            descripcionHeaderPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            descripcionHeaderPanel.Size = new Size(748, 22);
            descripcionHeaderPanel.TabIndex = 0;
            // 
            // descripcionTitleLabel
            // 
            descripcionTitleLabel.Anchor = AnchorStyles.Left;
            descripcionTitleLabel.AutoSize = true;
            descripcionTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            descripcionTitleLabel.Location = new Point(0, 3);
            descripcionTitleLabel.Margin = new Padding(0);
            descripcionTitleLabel.Name = "descripcionTitleLabel";
            descripcionTitleLabel.Size = new Size(187, 15);
            descripcionTitleLabel.TabIndex = 0;
            descripcionTitleLabel.Text = "Descripción del Registro Clínico *";
            // 
            // caracteresCounterLabel
            // 
            caracteresCounterLabel.Anchor = AnchorStyles.Right;
            caracteresCounterLabel.AutoSize = true;
            caracteresCounterLabel.Font = new Font("Segoe UI", 8.5F);
            caracteresCounterLabel.ForeColor = Color.Gray;
            caracteresCounterLabel.Location = new Point(684, 3);
            caracteresCounterLabel.Margin = new Padding(0);
            caracteresCounterLabel.Name = "caracteresCounterLabel";
            caracteresCounterLabel.Size = new Size(64, 15);
            caracteresCounterLabel.TabIndex = 1;
            caracteresCounterLabel.Text = "0 / 500 car.";
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.Dock = DockStyle.Fill;
            descripcionTextBox.Font = new Font("Segoe UI", 9.5F);
            descripcionTextBox.Location = new Point(0, 24);
            descripcionTextBox.Margin = new Padding(0, 2, 0, 0);
            descripcionTextBox.MaxLength = 500;
            descripcionTextBox.Multiline = true;
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.ScrollBars = ScrollBars.Vertical;
            descripcionTextBox.Size = new Size(748, 268);
            descripcionTextBox.TabIndex = 1;
            descripcionTextBox.TextChanged += descripcionTextBox_TextChanged;
            // 
            // footerPanel
            // 
            footerPanel.ColumnCount = 2;
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            footerPanel.Controls.Add(limpiarButton, 0, 0);
            footerPanel.Controls.Add(botonesDerechaFlowPanel, 1, 0);
            footerPanel.Dock = DockStyle.Fill;
            footerPanel.Location = new Point(16, 526);
            footerPanel.Margin = new Padding(0);
            footerPanel.Name = "footerPanel";
            footerPanel.RowCount = 1;
            footerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            footerPanel.Size = new Size(748, 44);
            footerPanel.TabIndex = 4;
            // 
            // limpiarButton
            // 
            limpiarButton.Anchor = AnchorStyles.Left;
            limpiarButton.FlatStyle = FlatStyle.Flat;
            limpiarButton.Font = new Font("Segoe UI", 9F);
            limpiarButton.Location = new Point(0, 5);
            limpiarButton.Margin = new Padding(0);
            limpiarButton.Name = "limpiarButton";
            limpiarButton.Size = new Size(130, 34);
            limpiarButton.TabIndex = 0;
            limpiarButton.Text = "Limpiar campos";
            limpiarButton.UseVisualStyleBackColor = true;
            limpiarButton.Click += limpiarButton_Click;
            // 
            // botonesDerechaFlowPanel
            // 
            botonesDerechaFlowPanel.Anchor = AnchorStyles.Right;
            botonesDerechaFlowPanel.AutoSize = true;
            botonesDerechaFlowPanel.Controls.Add(guardarButton);
            botonesDerechaFlowPanel.Controls.Add(cancelarButton);
            botonesDerechaFlowPanel.FlowDirection = FlowDirection.RightToLeft;
            botonesDerechaFlowPanel.Location = new Point(376, 3);
            botonesDerechaFlowPanel.Margin = new Padding(0);
            botonesDerechaFlowPanel.Name = "botonesDerechaFlowPanel";
            botonesDerechaFlowPanel.Size = new Size(372, 38);
            botonesDerechaFlowPanel.TabIndex = 1;
            botonesDerechaFlowPanel.WrapContents = false;
            // 
            // guardarButton
            // 
            guardarButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guardarButton.BackColor = Color.FromArgb(0, 102, 204);
            guardarButton.FlatAppearance.BorderSize = 0;
            guardarButton.FlatStyle = FlatStyle.Flat;
            guardarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guardarButton.ForeColor = Color.White;
            guardarButton.Location = new Point(142, 2);
            guardarButton.Margin = new Padding(6, 2, 0, 2);
            guardarButton.Name = "guardarButton";
            guardarButton.Size = new Size(230, 34);
            guardarButton.TabIndex = 0;
            guardarButton.Text = "Guardar Registro Clínico (F5)";
            guardarButton.UseVisualStyleBackColor = false;
            guardarButton.Click += guardarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.FlatStyle = FlatStyle.Flat;
            cancelarButton.Font = new Font("Segoe UI", 9F);
            cancelarButton.Location = new Point(0, 2);
            cancelarButton.Margin = new Padding(0, 2, 6, 2);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(130, 34);
            cancelarButton.TabIndex = 1;
            cancelarButton.Text = "Cancelar (ESC)";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // NuevoRegistroClinicoModalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(780, 570);
            Controls.Add(mainTableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 500);
            Name = "NuevoRegistroClinicoModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo Registro Clínico - Historia Clínica";
            KeyDown += NuevoRegistroClinicoModalForm_KeyDown;
            mainTableLayoutPanel.ResumeLayout(false);
            pacienteHeaderPanel.ResumeLayout(false);
            pacienteHeaderPanel.PerformLayout();
            tipoYFechaTableLayoutPanel.ResumeLayout(false);
            tipoYFechaTableLayoutPanel.PerformLayout();
            turnoSelectorTableLayoutPanel.ResumeLayout(false);
            turnoSelectorTableLayoutPanel.PerformLayout();
            descripcionTableLayoutPanel.ResumeLayout(false);
            descripcionTableLayoutPanel.PerformLayout();
            descripcionHeaderPanel.ResumeLayout(false);
            descripcionHeaderPanel.PerformLayout();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            botonesDerechaFlowPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel;
        private Panel pacienteHeaderPanel;
        private Label avatarLabel;
        private Label pacienteNombreLabel;
        private Label pacienteMetaLabel;
        private TableLayoutPanel tipoYFechaTableLayoutPanel;
        private Label tipoRegistroTitleLabel;
        private Label fechaTitleLabel;
        private ComboBox tipoRegistroComboBox;
        private DateTimePicker fechaDateTimePicker;
        private Label tipoInfoTextLabel;
        private TableLayoutPanel turnoSelectorTableLayoutPanel;
        private Label turnoTitleLabel;
        private ComboBox turnoOrigenComboBox;
        private Button desvincularTurnoButton;
        private TableLayoutPanel descripcionTableLayoutPanel;
        private TableLayoutPanel descripcionHeaderPanel;
        private Label descripcionTitleLabel;
        private Label caracteresCounterLabel;
        private TextBox descripcionTextBox;
        private TableLayoutPanel footerPanel;
        private Button limpiarButton;
        private FlowLayoutPanel botonesDerechaFlowPanel;
        private Button cancelarButton;
        private Button guardarButton;
    }
}
