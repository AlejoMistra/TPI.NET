namespace WindowsForms.Reportes
{
    partial class ReporteTurnos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LimpiarArchivosTemporales(forzar: true);
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanelPrincipal = new TableLayoutPanel();
            filtrosGroupBox = new GroupBox();
            filtrosTableLayoutPanel = new TableLayoutPanel();
            fechaDesdeLabel = new Label();
            fechaDesdeDateTimePicker = new DateTimePicker();
            fechaHastaLabel = new Label();
            fechaHastaDateTimePicker = new DateTimePicker();
            especialidadLabel = new Label();
            especialidadComboBox = new ComboBox();
            profesionalLabel = new Label();
            profesionalComboBox = new ComboBox();
            accionesFlowLayoutPanel = new FlowLayoutPanel();
            generarReporteButton = new Button();
            descargarPdfButton = new Button();
            webViewReporte = new Microsoft.Web.WebView2.WinForms.WebView2();
            tableLayoutPanelPrincipal.SuspendLayout();
            filtrosGroupBox.SuspendLayout();
            filtrosTableLayoutPanel.SuspendLayout();
            accionesFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewReporte).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelPrincipal
            // 
            tableLayoutPanelPrincipal.AutoScroll = true;
            tableLayoutPanelPrincipal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelPrincipal.ColumnCount = 1;
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelPrincipal.Controls.Add(filtrosGroupBox, 0, 0);
            tableLayoutPanelPrincipal.Controls.Add(webViewReporte, 0, 1);
            tableLayoutPanelPrincipal.Dock = DockStyle.Fill;
            tableLayoutPanelPrincipal.Location = new Point(0, 0);
            tableLayoutPanelPrincipal.Name = "tableLayoutPanelPrincipal";
            tableLayoutPanelPrincipal.RowCount = 2;
            tableLayoutPanelPrincipal.RowStyles.Add(new RowStyle());
            tableLayoutPanelPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelPrincipal.Size = new Size(1000, 650);
            tableLayoutPanelPrincipal.TabIndex = 0;
            // 
            // filtrosGroupBox
            // 
            filtrosGroupBox.AutoSize = true;
            filtrosGroupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filtrosGroupBox.Controls.Add(filtrosTableLayoutPanel);
            filtrosGroupBox.Dock = DockStyle.Fill;
            filtrosGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            filtrosGroupBox.Location = new Point(3, 3);
            filtrosGroupBox.Name = "filtrosGroupBox";
            filtrosGroupBox.Padding = new Padding(6);
            filtrosGroupBox.Size = new Size(994, 78);
            filtrosGroupBox.TabIndex = 0;
            filtrosGroupBox.TabStop = false;
            filtrosGroupBox.Text = "Filtros del Tablero de Turnos";
            // 
            // filtrosTableLayoutPanel
            // 
            filtrosTableLayoutPanel.AutoSize = true;
            filtrosTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            filtrosTableLayoutPanel.ColumnCount = 5;
            filtrosTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            filtrosTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            filtrosTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            filtrosTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            filtrosTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            filtrosTableLayoutPanel.Controls.Add(fechaDesdeLabel, 0, 0);
            filtrosTableLayoutPanel.Controls.Add(fechaDesdeDateTimePicker, 0, 1);
            filtrosTableLayoutPanel.Controls.Add(fechaHastaLabel, 1, 0);
            filtrosTableLayoutPanel.Controls.Add(fechaHastaDateTimePicker, 1, 1);
            filtrosTableLayoutPanel.Controls.Add(especialidadLabel, 2, 0);
            filtrosTableLayoutPanel.Controls.Add(especialidadComboBox, 2, 1);
            filtrosTableLayoutPanel.Controls.Add(profesionalLabel, 3, 0);
            filtrosTableLayoutPanel.Controls.Add(profesionalComboBox, 3, 1);
            filtrosTableLayoutPanel.Controls.Add(accionesFlowLayoutPanel, 4, 1);
            filtrosTableLayoutPanel.Dock = DockStyle.Fill;
            filtrosTableLayoutPanel.Location = new Point(6, 22);
            filtrosTableLayoutPanel.Name = "filtrosTableLayoutPanel";
            filtrosTableLayoutPanel.RowCount = 2;
            filtrosTableLayoutPanel.RowStyles.Add(new RowStyle());
            filtrosTableLayoutPanel.RowStyles.Add(new RowStyle());
            filtrosTableLayoutPanel.Size = new Size(982, 50);
            filtrosTableLayoutPanel.TabIndex = 0;
            // 
            // fechaDesdeLabel
            // 
            fechaDesdeLabel.AutoSize = true;
            fechaDesdeLabel.Location = new Point(3, 0);
            fechaDesdeLabel.Name = "fechaDesdeLabel";
            fechaDesdeLabel.Size = new Size(42, 15);
            fechaDesdeLabel.TabIndex = 0;
            fechaDesdeLabel.Text = "Desde:";
            // 
            // fechaDesdeDateTimePicker
            // 
            fechaDesdeDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaDesdeDateTimePicker.Location = new Point(3, 18);
            fechaDesdeDateTimePicker.Name = "fechaDesdeDateTimePicker";
            fechaDesdeDateTimePicker.Size = new Size(110, 23);
            fechaDesdeDateTimePicker.TabIndex = 1;
            // 
            // fechaHastaLabel
            // 
            fechaHastaLabel.AutoSize = true;
            fechaHastaLabel.Location = new Point(119, 0);
            fechaHastaLabel.Name = "fechaHastaLabel";
            fechaHastaLabel.Size = new Size(40, 15);
            fechaHastaLabel.TabIndex = 2;
            fechaHastaLabel.Text = "Hasta:";
            // 
            // fechaHastaDateTimePicker
            // 
            fechaHastaDateTimePicker.Format = DateTimePickerFormat.Short;
            fechaHastaDateTimePicker.Location = new Point(119, 18);
            fechaHastaDateTimePicker.Name = "fechaHastaDateTimePicker";
            fechaHastaDateTimePicker.Size = new Size(110, 23);
            fechaHastaDateTimePicker.TabIndex = 3;
            // 
            // especialidadLabel
            // 
            especialidadLabel.AutoSize = true;
            especialidadLabel.Location = new Point(235, 0);
            especialidadLabel.Name = "especialidadLabel";
            especialidadLabel.Size = new Size(75, 15);
            especialidadLabel.TabIndex = 4;
            especialidadLabel.Text = "Especialidad:";
            // 
            // especialidadComboBox
            // 
            especialidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            especialidadComboBox.FormattingEnabled = true;
            especialidadComboBox.Location = new Point(235, 18);
            especialidadComboBox.Name = "especialidadComboBox";
            especialidadComboBox.Size = new Size(190, 23);
            especialidadComboBox.TabIndex = 5;
            especialidadComboBox.SelectedIndexChanged += especialidadComboBox_SelectedIndexChanged;
            // 
            // profesionalLabel
            // 
            profesionalLabel.AutoSize = true;
            profesionalLabel.Location = new Point(431, 0);
            profesionalLabel.Name = "profesionalLabel";
            profesionalLabel.Size = new Size(69, 15);
            profesionalLabel.TabIndex = 6;
            profesionalLabel.Text = "Profesional:";
            // 
            // profesionalComboBox
            // 
            profesionalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profesionalComboBox.FormattingEnabled = true;
            profesionalComboBox.Location = new Point(431, 18);
            profesionalComboBox.Name = "profesionalComboBox";
            profesionalComboBox.Size = new Size(210, 23);
            profesionalComboBox.TabIndex = 7;
            // 
            // accionesFlowLayoutPanel
            // 
            accionesFlowLayoutPanel.AutoSize = true;
            accionesFlowLayoutPanel.Controls.Add(generarReporteButton);
            accionesFlowLayoutPanel.Controls.Add(descargarPdfButton);
            accionesFlowLayoutPanel.Location = new Point(647, 15);
            accionesFlowLayoutPanel.Margin = new Padding(3, 0, 3, 0);
            accionesFlowLayoutPanel.Name = "accionesFlowLayoutPanel";
            accionesFlowLayoutPanel.Size = new Size(244, 32);
            accionesFlowLayoutPanel.TabIndex = 8;
            accionesFlowLayoutPanel.WrapContents = false;
            // 
            // generarReporteButton
            // 
            generarReporteButton.BackColor = SystemColors.Highlight;
            generarReporteButton.FlatStyle = FlatStyle.Flat;
            generarReporteButton.ForeColor = SystemColors.HighlightText;
            generarReporteButton.Location = new Point(3, 3);
            generarReporteButton.Name = "generarReporteButton";
            generarReporteButton.Size = new Size(115, 26);
            generarReporteButton.TabIndex = 0;
            generarReporteButton.Text = "Generar Reporte";
            generarReporteButton.UseVisualStyleBackColor = false;
            generarReporteButton.Click += generarReporteButton_Click;
            // 
            // descargarPdfButton
            // 
            descargarPdfButton.BackColor = Color.ForestGreen;
            descargarPdfButton.Enabled = false;
            descargarPdfButton.FlatStyle = FlatStyle.Flat;
            descargarPdfButton.ForeColor = SystemColors.HighlightText;
            descargarPdfButton.Location = new Point(124, 3);
            descargarPdfButton.Name = "descargarPdfButton";
            descargarPdfButton.Size = new Size(117, 26);
            descargarPdfButton.TabIndex = 1;
            descargarPdfButton.Text = "Descargar PDF";
            descargarPdfButton.UseVisualStyleBackColor = false;
            descargarPdfButton.Click += descargarPdfButton_Click;
            // 
            // webViewReporte
            // 
            webViewReporte.AllowExternalDrop = true;
            webViewReporte.CreationProperties = null;
            webViewReporte.DefaultBackgroundColor = Color.White;
            webViewReporte.Dock = DockStyle.Fill;
            webViewReporte.Location = new Point(3, 87);
            webViewReporte.Name = "webViewReporte";
            webViewReporte.Size = new Size(994, 560);
            webViewReporte.TabIndex = 1;
            webViewReporte.ZoomFactor = 1D;
            // 
            // ReporteTurnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelPrincipal);
            Name = "ReporteTurnos";
            Size = new Size(1000, 650);
            Load += ReporteTurnos_Load;
            tableLayoutPanelPrincipal.ResumeLayout(false);
            tableLayoutPanelPrincipal.PerformLayout();
            filtrosGroupBox.ResumeLayout(false);
            filtrosGroupBox.PerformLayout();
            filtrosTableLayoutPanel.ResumeLayout(false);
            filtrosTableLayoutPanel.PerformLayout();
            accionesFlowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewReporte).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelPrincipal;
        private GroupBox filtrosGroupBox;
        private TableLayoutPanel filtrosTableLayoutPanel;
        private Label fechaDesdeLabel;
        private DateTimePicker fechaDesdeDateTimePicker;
        private Label fechaHastaLabel;
        private DateTimePicker fechaHastaDateTimePicker;
        private Label especialidadLabel;
        private ComboBox especialidadComboBox;
        private Label profesionalLabel;
        private ComboBox profesionalComboBox;
        private FlowLayoutPanel accionesFlowLayoutPanel;
        private Button generarReporteButton;
        private Button descargarPdfButton;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewReporte;
    }
}

