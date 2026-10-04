namespace WindowsForms
{
    partial class FacturaModalForm
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
            bannerLabel = new Label();
            datosTurnoGroupBox = new GroupBox();
            turnoTableLayoutPanel = new TableLayoutPanel();
            turnoIdTitleLabel = new Label();
            turnoIdValueLabel = new Label();
            fechaHoraTitleLabel = new Label();
            fechaHoraValueLabel = new Label();
            estadoTurnoTitleLabel = new Label();
            estadoTurnoValueLabel = new Label();
            pacienteTitleLabel = new Label();
            pacienteValueLabel = new Label();
            obraSocialTitleLabel = new Label();
            obraSocialValueLabel = new Label();
            profesionalTitleLabel = new Label();
            profesionalValueLabel = new Label();
            cabeceraGroupBox = new GroupBox();
            cabeceraTableLayoutPanel = new TableLayoutPanel();
            nroFacturaTitleLabel = new Label();
            nroFacturaValueLabel = new Label();
            fechaEmisionTitleLabel = new Label();
            fechaEmisionValueLabel = new Label();
            estadoFacturaTitleLabel = new Label();
            estadoFacturaValueLabel = new Label();
            metodoPagoTitleLabel = new Label();
            metodoPagoComboBox = new ComboBox();
            detalleGroupBox = new GroupBox();
            detalleLayoutPanel = new TableLayoutPanel();
            agregarItemPanel = new TableLayoutPanel();
            nuevoConceptoLabel = new Label();
            nuevoConceptoTextBox = new TextBox();
            nuevaCantidadLabel = new Label();
            nuevaCantidadNumericUpDown = new NumericUpDown();
            nuevoPrecioLabel = new Label();
            nuevoPrecioNumericUpDown = new NumericUpDown();
            agregarItemButton = new Button();
            detallesDataGridView = new DataGridView();
            footerPanel = new TableLayoutPanel();
            totalLabel = new Label();
            buttonsFlowPanel = new FlowLayoutPanel();
            anularButton = new Button();
            confirmarButton = new Button();
            cancelarButton = new Button();
            mainTableLayoutPanel.SuspendLayout();
            datosTurnoGroupBox.SuspendLayout();
            turnoTableLayoutPanel.SuspendLayout();
            cabeceraGroupBox.SuspendLayout();
            cabeceraTableLayoutPanel.SuspendLayout();
            detalleGroupBox.SuspendLayout();
            detalleLayoutPanel.SuspendLayout();
            agregarItemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nuevaCantidadNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nuevoPrecioNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)detallesDataGridView).BeginInit();
            footerPanel.SuspendLayout();
            buttonsFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(bannerLabel, 0, 0);
            mainTableLayoutPanel.Controls.Add(datosTurnoGroupBox, 0, 1);
            mainTableLayoutPanel.Controls.Add(cabeceraGroupBox, 0, 2);
            mainTableLayoutPanel.Controls.Add(detalleGroupBox, 0, 3);
            mainTableLayoutPanel.Controls.Add(footerPanel, 0, 4);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.Padding = new Padding(10);
            mainTableLayoutPanel.RowCount = 5;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            mainTableLayoutPanel.Size = new Size(820, 600);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // bannerLabel
            // 
            bannerLabel.BackColor = Color.FromArgb(235, 243, 255);
            bannerLabel.Dock = DockStyle.Fill;
            bannerLabel.Font = new Font("Segoe UI", 9.5F);
            bannerLabel.ForeColor = Color.FromArgb(0, 102, 204);
            bannerLabel.Location = new Point(13, 13);
            bannerLabel.Margin = new Padding(3, 3, 3, 5);
            bannerLabel.Name = "bannerLabel";
            bannerLabel.Padding = new Padding(8, 0, 8, 0);
            bannerLabel.Size = new Size(794, 26);
            bannerLabel.TabIndex = 0;
            bannerLabel.Text = "Emisión de Factura (Maestro/Detalle): ingrese el precio unitario de la consulta y agregue ítems adicionales si corresponde.";
            bannerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // datosTurnoGroupBox
            // 
            datosTurnoGroupBox.Controls.Add(turnoTableLayoutPanel);
            datosTurnoGroupBox.Dock = DockStyle.Fill;
            datosTurnoGroupBox.Location = new Point(13, 47);
            datosTurnoGroupBox.Name = "datosTurnoGroupBox";
            datosTurnoGroupBox.Size = new Size(794, 90);
            datosTurnoGroupBox.TabIndex = 1;
            datosTurnoGroupBox.TabStop = false;
            datosTurnoGroupBox.Text = "Datos del Turno y Paciente";
            // 
            // turnoTableLayoutPanel
            // 
            turnoTableLayoutPanel.ColumnCount = 6;
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            turnoTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            turnoTableLayoutPanel.Controls.Add(turnoIdTitleLabel, 0, 0);
            turnoTableLayoutPanel.Controls.Add(turnoIdValueLabel, 1, 0);
            turnoTableLayoutPanel.Controls.Add(fechaHoraTitleLabel, 2, 0);
            turnoTableLayoutPanel.Controls.Add(fechaHoraValueLabel, 3, 0);
            turnoTableLayoutPanel.Controls.Add(estadoTurnoTitleLabel, 4, 0);
            turnoTableLayoutPanel.Controls.Add(estadoTurnoValueLabel, 5, 0);
            turnoTableLayoutPanel.Controls.Add(pacienteTitleLabel, 0, 1);
            turnoTableLayoutPanel.Controls.Add(pacienteValueLabel, 1, 1);
            turnoTableLayoutPanel.Controls.Add(obraSocialTitleLabel, 2, 1);
            turnoTableLayoutPanel.Controls.Add(obraSocialValueLabel, 3, 1);
            turnoTableLayoutPanel.Controls.Add(profesionalTitleLabel, 4, 1);
            turnoTableLayoutPanel.Controls.Add(profesionalValueLabel, 5, 1);
            turnoTableLayoutPanel.Dock = DockStyle.Fill;
            turnoTableLayoutPanel.Location = new Point(3, 19);
            turnoTableLayoutPanel.Name = "turnoTableLayoutPanel";
            turnoTableLayoutPanel.Padding = new Padding(5, 2, 5, 2);
            turnoTableLayoutPanel.RowCount = 2;
            turnoTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            turnoTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            turnoTableLayoutPanel.Size = new Size(788, 68);
            turnoTableLayoutPanel.TabIndex = 0;
            // 
            // turnoIdTitleLabel
            // 
            turnoIdTitleLabel.Anchor = AnchorStyles.Left;
            turnoIdTitleLabel.AutoSize = true;
            turnoIdTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            turnoIdTitleLabel.Location = new Point(8, 10);
            turnoIdTitleLabel.Name = "turnoIdTitleLabel";
            turnoIdTitleLabel.Size = new Size(59, 15);
            turnoIdTitleLabel.TabIndex = 0;
            turnoIdTitleLabel.Text = "N° Turno:";
            // 
            // turnoIdValueLabel
            // 
            turnoIdValueLabel.Anchor = AnchorStyles.Left;
            turnoIdValueLabel.AutoSize = true;
            turnoIdValueLabel.Location = new Point(98, 10);
            turnoIdValueLabel.Name = "turnoIdValueLabel";
            turnoIdValueLabel.Size = new Size(19, 15);
            turnoIdValueLabel.TabIndex = 1;
            turnoIdValueLabel.Text = "—";
            // 
            // fechaHoraTitleLabel
            // 
            fechaHoraTitleLabel.Anchor = AnchorStyles.Left;
            fechaHoraTitleLabel.AutoSize = true;
            fechaHoraTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fechaHoraTitleLabel.Location = new Point(267, 10);
            fechaHoraTitleLabel.Name = "fechaHoraTitleLabel";
            fechaHoraTitleLabel.Size = new Size(80, 15);
            fechaHoraTitleLabel.TabIndex = 2;
            fechaHoraTitleLabel.Text = "Fecha / Hora:";
            // 
            // fechaHoraValueLabel
            // 
            fechaHoraValueLabel.Anchor = AnchorStyles.Left;
            fechaHoraValueLabel.AutoSize = true;
            fechaHoraValueLabel.Location = new Point(362, 10);
            fechaHoraValueLabel.Name = "fechaHoraValueLabel";
            fechaHoraValueLabel.Size = new Size(19, 15);
            fechaHoraValueLabel.TabIndex = 3;
            fechaHoraValueLabel.Text = "—";
            // 
            // estadoTurnoTitleLabel
            // 
            estadoTurnoTitleLabel.Anchor = AnchorStyles.Left;
            estadoTurnoTitleLabel.AutoSize = true;
            estadoTurnoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            estadoTurnoTitleLabel.Location = new Point(541, 10);
            estadoTurnoTitleLabel.Name = "estadoTurnoTitleLabel";
            estadoTurnoTitleLabel.Size = new Size(81, 15);
            estadoTurnoTitleLabel.TabIndex = 4;
            estadoTurnoTitleLabel.Text = "Estado Turno:";
            // 
            // estadoTurnoValueLabel
            // 
            estadoTurnoValueLabel.Anchor = AnchorStyles.Left;
            estadoTurnoValueLabel.AutoSize = true;
            estadoTurnoValueLabel.Location = new Point(636, 10);
            estadoTurnoValueLabel.Name = "estadoTurnoValueLabel";
            estadoTurnoValueLabel.Size = new Size(19, 15);
            estadoTurnoValueLabel.TabIndex = 5;
            estadoTurnoValueLabel.Text = "—";
            // 
            // pacienteTitleLabel
            // 
            pacienteTitleLabel.Anchor = AnchorStyles.Left;
            pacienteTitleLabel.AutoSize = true;
            pacienteTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pacienteTitleLabel.Location = new Point(8, 42);
            pacienteTitleLabel.Name = "pacienteTitleLabel";
            pacienteTitleLabel.Size = new Size(58, 15);
            pacienteTitleLabel.TabIndex = 6;
            pacienteTitleLabel.Text = "Paciente:";
            // 
            // pacienteValueLabel
            // 
            pacienteValueLabel.Anchor = AnchorStyles.Left;
            pacienteValueLabel.AutoSize = true;
            pacienteValueLabel.Location = new Point(98, 42);
            pacienteValueLabel.Name = "pacienteValueLabel";
            pacienteValueLabel.Size = new Size(19, 15);
            pacienteValueLabel.TabIndex = 7;
            pacienteValueLabel.Text = "—";
            // 
            // obraSocialTitleLabel
            // 
            obraSocialTitleLabel.Anchor = AnchorStyles.Left;
            obraSocialTitleLabel.AutoSize = true;
            obraSocialTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            obraSocialTitleLabel.Location = new Point(267, 42);
            obraSocialTitleLabel.Name = "obraSocialTitleLabel";
            obraSocialTitleLabel.Size = new Size(72, 15);
            obraSocialTitleLabel.TabIndex = 8;
            obraSocialTitleLabel.Text = "Obra Social:";
            // 
            // obraSocialValueLabel
            // 
            obraSocialValueLabel.Anchor = AnchorStyles.Left;
            obraSocialValueLabel.AutoSize = true;
            obraSocialValueLabel.Location = new Point(362, 42);
            obraSocialValueLabel.Name = "obraSocialValueLabel";
            obraSocialValueLabel.Size = new Size(19, 15);
            obraSocialValueLabel.TabIndex = 9;
            obraSocialValueLabel.Text = "—";
            // 
            // profesionalTitleLabel
            // 
            profesionalTitleLabel.Anchor = AnchorStyles.Left;
            profesionalTitleLabel.AutoSize = true;
            profesionalTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            profesionalTitleLabel.Location = new Point(541, 42);
            profesionalTitleLabel.Name = "profesionalTitleLabel";
            profesionalTitleLabel.Size = new Size(72, 15);
            profesionalTitleLabel.TabIndex = 10;
            profesionalTitleLabel.Text = "Profesional:";
            // 
            // profesionalValueLabel
            // 
            profesionalValueLabel.Anchor = AnchorStyles.Left;
            profesionalValueLabel.AutoSize = true;
            profesionalValueLabel.Location = new Point(636, 42);
            profesionalValueLabel.Name = "profesionalValueLabel";
            profesionalValueLabel.Size = new Size(19, 15);
            profesionalValueLabel.TabIndex = 11;
            profesionalValueLabel.Text = "—";
            // 
            // cabeceraGroupBox
            // 
            cabeceraGroupBox.Controls.Add(cabeceraTableLayoutPanel);
            cabeceraGroupBox.Dock = DockStyle.Fill;
            cabeceraGroupBox.Location = new Point(13, 143);
            cabeceraGroupBox.Name = "cabeceraGroupBox";
            cabeceraGroupBox.Size = new Size(794, 70);
            cabeceraGroupBox.TabIndex = 2;
            cabeceraGroupBox.TabStop = false;
            cabeceraGroupBox.Text = "Cabecera de Factura";
            // 
            // cabeceraTableLayoutPanel
            // 
            cabeceraTableLayoutPanel.ColumnCount = 8;
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            cabeceraTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            cabeceraTableLayoutPanel.Controls.Add(nroFacturaTitleLabel, 0, 0);
            cabeceraTableLayoutPanel.Controls.Add(nroFacturaValueLabel, 1, 0);
            cabeceraTableLayoutPanel.Controls.Add(fechaEmisionTitleLabel, 2, 0);
            cabeceraTableLayoutPanel.Controls.Add(fechaEmisionValueLabel, 3, 0);
            cabeceraTableLayoutPanel.Controls.Add(estadoFacturaTitleLabel, 4, 0);
            cabeceraTableLayoutPanel.Controls.Add(estadoFacturaValueLabel, 5, 0);
            cabeceraTableLayoutPanel.Controls.Add(metodoPagoTitleLabel, 6, 0);
            cabeceraTableLayoutPanel.Controls.Add(metodoPagoComboBox, 7, 0);
            cabeceraTableLayoutPanel.Dock = DockStyle.Fill;
            cabeceraTableLayoutPanel.Location = new Point(3, 19);
            cabeceraTableLayoutPanel.Name = "cabeceraTableLayoutPanel";
            cabeceraTableLayoutPanel.Padding = new Padding(5, 4, 5, 4);
            cabeceraTableLayoutPanel.RowCount = 1;
            cabeceraTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cabeceraTableLayoutPanel.Size = new Size(788, 48);
            cabeceraTableLayoutPanel.TabIndex = 0;
            // 
            // nroFacturaTitleLabel
            // 
            nroFacturaTitleLabel.Anchor = AnchorStyles.Left;
            nroFacturaTitleLabel.AutoSize = true;
            nroFacturaTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nroFacturaTitleLabel.Location = new Point(8, 16);
            nroFacturaTitleLabel.Name = "nroFacturaTitleLabel";
            nroFacturaTitleLabel.Size = new Size(68, 15);
            nroFacturaTitleLabel.TabIndex = 0;
            nroFacturaTitleLabel.Text = "Factura N°:";
            // 
            // nroFacturaValueLabel
            // 
            nroFacturaValueLabel.Anchor = AnchorStyles.Left;
            nroFacturaValueLabel.AutoSize = true;
            nroFacturaValueLabel.Location = new Point(93, 16);
            nroFacturaValueLabel.Name = "nroFacturaValueLabel";
            nroFacturaValueLabel.Size = new Size(41, 15);
            nroFacturaValueLabel.TabIndex = 1;
            nroFacturaValueLabel.Text = "Nueva";
            // 
            // fechaEmisionTitleLabel
            // 
            fechaEmisionTitleLabel.Anchor = AnchorStyles.Left;
            fechaEmisionTitleLabel.AutoSize = true;
            fechaEmisionTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fechaEmisionTitleLabel.Location = new Point(187, 16);
            fechaEmisionTitleLabel.Name = "fechaEmisionTitleLabel";
            fechaEmisionTitleLabel.Size = new Size(87, 15);
            fechaEmisionTitleLabel.TabIndex = 2;
            fechaEmisionTitleLabel.Text = "Fecha Emisión:";
            // 
            // fechaEmisionValueLabel
            // 
            fechaEmisionValueLabel.Anchor = AnchorStyles.Left;
            fechaEmisionValueLabel.AutoSize = true;
            fechaEmisionValueLabel.Location = new Point(282, 16);
            fechaEmisionValueLabel.Name = "fechaEmisionValueLabel";
            fechaEmisionValueLabel.Size = new Size(19, 15);
            fechaEmisionValueLabel.TabIndex = 3;
            fechaEmisionValueLabel.Text = "—";
            // 
            // estadoFacturaTitleLabel
            // 
            estadoFacturaTitleLabel.Anchor = AnchorStyles.Left;
            estadoFacturaTitleLabel.AutoSize = true;
            estadoFacturaTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            estadoFacturaTitleLabel.Location = new Point(401, 16);
            estadoFacturaTitleLabel.Name = "estadoFacturaTitleLabel";
            estadoFacturaTitleLabel.Size = new Size(46, 15);
            estadoFacturaTitleLabel.TabIndex = 4;
            estadoFacturaTitleLabel.Text = "Estado:";
            // 
            // estadoFacturaValueLabel
            // 
            estadoFacturaValueLabel.Anchor = AnchorStyles.Left;
            estadoFacturaValueLabel.AutoSize = true;
            estadoFacturaValueLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            estadoFacturaValueLabel.ForeColor = Color.FromArgb(40, 120, 60);
            estadoFacturaValueLabel.Location = new Point(461, 16);
            estadoFacturaValueLabel.Name = "estadoFacturaValueLabel";
            estadoFacturaValueLabel.Size = new Size(46, 15);
            estadoFacturaValueLabel.TabIndex = 5;
            estadoFacturaValueLabel.Text = "Pagada";
            // 
            // metodoPagoTitleLabel
            // 
            metodoPagoTitleLabel.Anchor = AnchorStyles.Left;
            metodoPagoTitleLabel.AutoSize = true;
            metodoPagoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            metodoPagoTitleLabel.Location = new Point(546, 16);
            metodoPagoTitleLabel.Name = "metodoPagoTitleLabel";
            metodoPagoTitleLabel.Size = new Size(101, 15);
            metodoPagoTitleLabel.TabIndex = 6;
            metodoPagoTitleLabel.Text = "Método de Pago:";
            // 
            // metodoPagoComboBox
            // 
            metodoPagoComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metodoPagoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            metodoPagoComboBox.FormattingEnabled = true;
            metodoPagoComboBox.Location = new Point(656, 12);
            metodoPagoComboBox.Name = "metodoPagoComboBox";
            metodoPagoComboBox.Size = new Size(124, 23);
            metodoPagoComboBox.TabIndex = 0;
            // 
            // detalleGroupBox
            // 
            detalleGroupBox.Controls.Add(detalleLayoutPanel);
            detalleGroupBox.Dock = DockStyle.Fill;
            detalleGroupBox.Location = new Point(13, 219);
            detalleGroupBox.Name = "detalleGroupBox";
            detalleGroupBox.Size = new Size(794, 316);
            detalleGroupBox.TabIndex = 3;
            detalleGroupBox.TabStop = false;
            detalleGroupBox.Text = "Renglones de Factura (Detalle)";
            // 
            // detalleLayoutPanel
            // 
            detalleLayoutPanel.ColumnCount = 1;
            detalleLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detalleLayoutPanel.Controls.Add(agregarItemPanel, 0, 0);
            detalleLayoutPanel.Controls.Add(detallesDataGridView, 0, 1);
            detalleLayoutPanel.Dock = DockStyle.Fill;
            detalleLayoutPanel.Location = new Point(3, 19);
            detalleLayoutPanel.Name = "detalleLayoutPanel";
            detalleLayoutPanel.RowCount = 2;
            detalleLayoutPanel.RowStyles.Add(new RowStyle());
            detalleLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detalleLayoutPanel.Size = new Size(788, 294);
            detalleLayoutPanel.TabIndex = 0;
            // 
            // agregarItemPanel
            // 
            agregarItemPanel.ColumnCount = 4;
            agregarItemPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            agregarItemPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            agregarItemPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            agregarItemPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            agregarItemPanel.Controls.Add(nuevoConceptoLabel, 0, 0);
            agregarItemPanel.Controls.Add(nuevoConceptoTextBox, 0, 1);
            agregarItemPanel.Controls.Add(nuevaCantidadLabel, 1, 0);
            agregarItemPanel.Controls.Add(nuevaCantidadNumericUpDown, 1, 1);
            agregarItemPanel.Controls.Add(nuevoPrecioLabel, 2, 0);
            agregarItemPanel.Controls.Add(nuevoPrecioNumericUpDown, 2, 1);
            agregarItemPanel.Controls.Add(agregarItemButton, 3, 1);
            agregarItemPanel.Dock = DockStyle.Fill;
            agregarItemPanel.Location = new Point(3, 3);
            agregarItemPanel.Name = "agregarItemPanel";
            agregarItemPanel.Padding = new Padding(0, 0, 0, 4);
            agregarItemPanel.RowCount = 2;
            agregarItemPanel.RowStyles.Add(new RowStyle());
            agregarItemPanel.RowStyles.Add(new RowStyle());
            agregarItemPanel.Size = new Size(782, 50);
            agregarItemPanel.TabIndex = 0;
            // 
            // nuevoConceptoLabel
            // 
            nuevoConceptoLabel.AutoSize = true;
            nuevoConceptoLabel.Location = new Point(3, 0);
            nuevoConceptoLabel.Name = "nuevoConceptoLabel";
            nuevoConceptoLabel.Size = new Size(150, 15);
            nuevoConceptoLabel.TabIndex = 0;
            nuevoConceptoLabel.Text = "Nuevo Concepto / Práctica";
            // 
            // nuevoConceptoTextBox
            // 
            nuevoConceptoTextBox.Dock = DockStyle.Fill;
            nuevoConceptoTextBox.Location = new Point(3, 18);
            nuevoConceptoTextBox.Name = "nuevoConceptoTextBox";
            nuevoConceptoTextBox.PlaceholderText = "Ej: Electrocardiograma, Práctica adicional, Insumos...";
            nuevoConceptoTextBox.Size = new Size(376, 23);
            nuevoConceptoTextBox.TabIndex = 0;
            // 
            // nuevaCantidadLabel
            // 
            nuevaCantidadLabel.AutoSize = true;
            nuevaCantidadLabel.Location = new Point(385, 0);
            nuevaCantidadLabel.Name = "nuevaCantidadLabel";
            nuevaCantidadLabel.Size = new Size(55, 15);
            nuevaCantidadLabel.TabIndex = 1;
            nuevaCantidadLabel.Text = "Cantidad";
            // 
            // nuevaCantidadNumericUpDown
            // 
            nuevaCantidadNumericUpDown.Dock = DockStyle.Fill;
            nuevaCantidadNumericUpDown.Location = new Point(385, 18);
            nuevaCantidadNumericUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nuevaCantidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nuevaCantidadNumericUpDown.Name = "nuevaCantidadNumericUpDown";
            nuevaCantidadNumericUpDown.Size = new Size(104, 23);
            nuevaCantidadNumericUpDown.TabIndex = 1;
            nuevaCantidadNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nuevoPrecioLabel
            // 
            nuevoPrecioLabel.AutoSize = true;
            nuevoPrecioLabel.Location = new Point(495, 0);
            nuevoPrecioLabel.Name = "nuevoPrecioLabel";
            nuevoPrecioLabel.Size = new Size(102, 15);
            nuevoPrecioLabel.TabIndex = 2;
            nuevoPrecioLabel.Text = "Precio Unitario ($)";
            // 
            // nuevoPrecioNumericUpDown
            // 
            nuevoPrecioNumericUpDown.DecimalPlaces = 2;
            nuevoPrecioNumericUpDown.Dock = DockStyle.Fill;
            nuevoPrecioNumericUpDown.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nuevoPrecioNumericUpDown.Location = new Point(495, 18);
            nuevoPrecioNumericUpDown.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nuevoPrecioNumericUpDown.Name = "nuevoPrecioNumericUpDown";
            nuevoPrecioNumericUpDown.Size = new Size(154, 23);
            nuevoPrecioNumericUpDown.TabIndex = 2;
            nuevoPrecioNumericUpDown.ThousandsSeparator = true;
            // 
            // agregarItemButton
            // 
            agregarItemButton.Dock = DockStyle.Fill;
            agregarItemButton.Location = new Point(655, 18);
            agregarItemButton.Name = "agregarItemButton";
            agregarItemButton.Size = new Size(124, 25);
            agregarItemButton.TabIndex = 3;
            agregarItemButton.Text = "+ Agregar Ítem";
            agregarItemButton.UseVisualStyleBackColor = true;
            // 
            // detallesDataGridView
            // 
            detallesDataGridView.AllowUserToAddRows = false;
            detallesDataGridView.AllowUserToDeleteRows = false;
            detallesDataGridView.AllowUserToResizeRows = false;
            detallesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            detallesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            detallesDataGridView.Dock = DockStyle.Fill;
            detallesDataGridView.EnableHeadersVisualStyles = false;
            detallesDataGridView.Location = new Point(3, 59);
            detallesDataGridView.Name = "detallesDataGridView";
            detallesDataGridView.RowHeadersVisible = false;
            detallesDataGridView.Size = new Size(782, 232);
            detallesDataGridView.TabIndex = 1;
            // 
            // footerPanel
            // 
            footerPanel.ColumnCount = 2;
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            footerPanel.Controls.Add(totalLabel, 0, 0);
            footerPanel.Controls.Add(buttonsFlowPanel, 1, 0);
            footerPanel.Dock = DockStyle.Fill;
            footerPanel.Location = new Point(13, 541);
            footerPanel.Name = "footerPanel";
            footerPanel.RowCount = 1;
            footerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            footerPanel.Size = new Size(794, 46);
            footerPanel.TabIndex = 4;
            // 
            // totalLabel
            // 
            totalLabel.Anchor = AnchorStyles.Left;
            totalLabel.AutoSize = true;
            totalLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            totalLabel.ForeColor = Color.FromArgb(20, 90, 45);
            totalLabel.Location = new Point(3, 10);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(132, 25);
            totalLabel.TabIndex = 0;
            totalLabel.Text = "TOTAL: $ 0,00";
            // 
            // buttonsFlowPanel
            // 
            buttonsFlowPanel.Anchor = AnchorStyles.Right;
            buttonsFlowPanel.AutoSize = true;
            buttonsFlowPanel.Controls.Add(cancelarButton);
            buttonsFlowPanel.Controls.Add(anularButton);
            buttonsFlowPanel.Controls.Add(confirmarButton);
            buttonsFlowPanel.Location = new Point(360, 4);
            buttonsFlowPanel.Name = "buttonsFlowPanel";
            buttonsFlowPanel.Size = new Size(431, 38);
            buttonsFlowPanel.TabIndex = 1;
            buttonsFlowPanel.WrapContents = false;
            // 
            // anularButton
            // 
            anularButton.BackColor = Color.FromArgb(255, 235, 235);
            anularButton.ForeColor = Color.DarkRed;
            anularButton.Location = new Point(114, 3);
            anularButton.Name = "anularButton";
            anularButton.Size = new Size(140, 32);
            anularButton.TabIndex = 0;
            anularButton.Text = "Anular Factura";
            anularButton.UseVisualStyleBackColor = false;
            anularButton.Visible = false;
            // 
            // confirmarButton
            // 
            confirmarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            confirmarButton.Location = new Point(260, 3);
            confirmarButton.Name = "confirmarButton";
            confirmarButton.Size = new Size(170, 32);
            confirmarButton.TabIndex = 1;
            confirmarButton.Text = "Confirmar Factura";
            confirmarButton.UseVisualStyleBackColor = true;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(3, 3);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(105, 32);
            cancelarButton.TabIndex = 2;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            // 
            // FacturaModalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 600);
            Controls.Add(mainTableLayoutPanel);
            MinimumSize = new Size(760, 540);
            Name = "FacturaModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Facturación de Turno";
            mainTableLayoutPanel.ResumeLayout(false);
            datosTurnoGroupBox.ResumeLayout(false);
            turnoTableLayoutPanel.ResumeLayout(false);
            turnoTableLayoutPanel.PerformLayout();
            cabeceraGroupBox.ResumeLayout(false);
            cabeceraTableLayoutPanel.ResumeLayout(false);
            cabeceraTableLayoutPanel.PerformLayout();
            detalleGroupBox.ResumeLayout(false);
            detalleLayoutPanel.ResumeLayout(false);
            agregarItemPanel.ResumeLayout(false);
            agregarItemPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nuevaCantidadNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)nuevoPrecioNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)detallesDataGridView).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            buttonsFlowPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel = null!;
        private Label bannerLabel = null!;
        private GroupBox datosTurnoGroupBox = null!;
        private TableLayoutPanel turnoTableLayoutPanel = null!;
        private Label turnoIdTitleLabel = null!;
        private Label turnoIdValueLabel = null!;
        private Label fechaHoraTitleLabel = null!;
        private Label fechaHoraValueLabel = null!;
        private Label estadoTurnoTitleLabel = null!;
        private Label estadoTurnoValueLabel = null!;
        private Label pacienteTitleLabel = null!;
        private Label pacienteValueLabel = null!;
        private Label obraSocialTitleLabel = null!;
        private Label obraSocialValueLabel = null!;
        private Label profesionalTitleLabel = null!;
        private Label profesionalValueLabel = null!;
        private GroupBox cabeceraGroupBox = null!;
        private TableLayoutPanel cabeceraTableLayoutPanel = null!;
        private Label nroFacturaTitleLabel = null!;
        private Label nroFacturaValueLabel = null!;
        private Label fechaEmisionTitleLabel = null!;
        private Label fechaEmisionValueLabel = null!;
        private Label estadoFacturaTitleLabel = null!;
        private Label estadoFacturaValueLabel = null!;
        private Label metodoPagoTitleLabel = null!;
        private ComboBox metodoPagoComboBox = null!;
        private GroupBox detalleGroupBox = null!;
        private TableLayoutPanel detalleLayoutPanel = null!;
        private TableLayoutPanel agregarItemPanel = null!;
        private Label nuevoConceptoLabel = null!;
        private TextBox nuevoConceptoTextBox = null!;
        private Label nuevaCantidadLabel = null!;
        private NumericUpDown nuevaCantidadNumericUpDown = null!;
        private Label nuevoPrecioLabel = null!;
        private NumericUpDown nuevoPrecioNumericUpDown = null!;
        private Button agregarItemButton = null!;
        private DataGridView detallesDataGridView = null!;
        private TableLayoutPanel footerPanel = null!;
        private Label totalLabel = null!;
        private FlowLayoutPanel buttonsFlowPanel = null!;
        private Button anularButton = null!;
        private Button confirmarButton = null!;
        private Button cancelarButton = null!;
    }
}
