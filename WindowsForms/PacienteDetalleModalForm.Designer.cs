namespace WindowsForms
{
    partial class PacienteDetalleModalForm
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
            datosPacienteGroupBox = new GroupBox();
            pacienteTableLayoutPanel = new TableLayoutPanel();
            pacienteTitleLabel = new Label();
            pacienteValueLabel = new Label();
            documentoTitleLabel = new Label();
            documentoValueLabel = new Label();
            fechaNacimientoTitleLabel = new Label();
            fechaNacimientoValueLabel = new Label();
            obraSocialTitleLabel = new Label();
            obraSocialValueLabel = new Label();
            telefonoTitleLabel = new Label();
            telefonoValueLabel = new Label();
            emailTitleLabel = new Label();
            emailValueLabel = new Label();
            nroHcTitleLabel = new Label();
            nroHcValueLabel = new Label();
            grupoSanguineoTitleLabel = new Label();
            grupoSanguineoValueLabel = new Label();
            totalRegistrosTitleLabel = new Label();
            totalRegistrosValueLabel = new Label();
            turnosGroupBox = new GroupBox();
            turnosLayoutPanel = new TableLayoutPanel();
            resumenTurnosPanel = new TableLayoutPanel();
            ultimoTurnoTitleLabel = new Label();
            ultimoTurnoValueLabel = new Label();
            proximoTurnoTitleLabel = new Label();
            proximoTurnoValueLabel = new Label();
            turnosPacienteDataGridView = new DataGridView();
            historiaClinicaGroupBox = new GroupBox();
            historiaClinicaLayoutPanel = new TableLayoutPanel();
            filtroRegistrosPanel = new FlowLayoutPanel();
            tipoRegistroFiltroLabel = new Label();
            tipoRegistroFiltroComboBox = new ComboBox();
            limpiarFiltroRegistrosLinkLabel = new LinkLabel();
            registrosDataGridView = new DataGridView();
            footerPanel = new TableLayoutPanel();
            hintLabel = new Label();
            buttonsFlowPanel = new FlowLayoutPanel();
            cerrarButton = new Button();
            mainTableLayoutPanel.SuspendLayout();
            datosPacienteGroupBox.SuspendLayout();
            pacienteTableLayoutPanel.SuspendLayout();
            turnosGroupBox.SuspendLayout();
            turnosLayoutPanel.SuspendLayout();
            resumenTurnosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)turnosPacienteDataGridView).BeginInit();
            historiaClinicaGroupBox.SuspendLayout();
            historiaClinicaLayoutPanel.SuspendLayout();
            filtroRegistrosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).BeginInit();
            footerPanel.SuspendLayout();
            buttonsFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(bannerLabel, 0, 0);
            mainTableLayoutPanel.Controls.Add(datosPacienteGroupBox, 0, 1);
            mainTableLayoutPanel.Controls.Add(turnosGroupBox, 0, 2);
            mainTableLayoutPanel.Controls.Add(historiaClinicaGroupBox, 0, 3);
            mainTableLayoutPanel.Controls.Add(footerPanel, 0, 4);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.Padding = new Padding(10);
            mainTableLayoutPanel.RowCount = 5;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 215F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            mainTableLayoutPanel.Size = new Size(940, 680);
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
            bannerLabel.Size = new Size(914, 26);
            bannerLabel.TabIndex = 0;
            bannerLabel.Text = "Ficha de Paciente e Historia Clínica: consulta integral de datos personales, turnos y registros clínicos.";
            bannerLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // datosPacienteGroupBox
            // 
            datosPacienteGroupBox.Controls.Add(pacienteTableLayoutPanel);
            datosPacienteGroupBox.Dock = DockStyle.Fill;
            datosPacienteGroupBox.Location = new Point(13, 47);
            datosPacienteGroupBox.Name = "datosPacienteGroupBox";
            datosPacienteGroupBox.Size = new Size(914, 112);
            datosPacienteGroupBox.TabIndex = 1;
            datosPacienteGroupBox.TabStop = false;
            datosPacienteGroupBox.Text = "Información del Paciente y Cabecera de Historia Clínica";
            // 
            // pacienteTableLayoutPanel
            // 
            pacienteTableLayoutPanel.ColumnCount = 6;
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            pacienteTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            pacienteTableLayoutPanel.Controls.Add(pacienteTitleLabel, 0, 0);
            pacienteTableLayoutPanel.Controls.Add(pacienteValueLabel, 1, 0);
            pacienteTableLayoutPanel.Controls.Add(documentoTitleLabel, 2, 0);
            pacienteTableLayoutPanel.Controls.Add(documentoValueLabel, 3, 0);
            pacienteTableLayoutPanel.Controls.Add(fechaNacimientoTitleLabel, 4, 0);
            pacienteTableLayoutPanel.Controls.Add(fechaNacimientoValueLabel, 5, 0);
            pacienteTableLayoutPanel.Controls.Add(obraSocialTitleLabel, 0, 1);
            pacienteTableLayoutPanel.Controls.Add(obraSocialValueLabel, 1, 1);
            pacienteTableLayoutPanel.Controls.Add(telefonoTitleLabel, 2, 1);
            pacienteTableLayoutPanel.Controls.Add(telefonoValueLabel, 3, 1);
            pacienteTableLayoutPanel.Controls.Add(emailTitleLabel, 4, 1);
            pacienteTableLayoutPanel.Controls.Add(emailValueLabel, 5, 1);
            pacienteTableLayoutPanel.Controls.Add(nroHcTitleLabel, 0, 2);
            pacienteTableLayoutPanel.Controls.Add(nroHcValueLabel, 1, 2);
            pacienteTableLayoutPanel.Controls.Add(grupoSanguineoTitleLabel, 2, 2);
            pacienteTableLayoutPanel.Controls.Add(grupoSanguineoValueLabel, 3, 2);
            pacienteTableLayoutPanel.Controls.Add(totalRegistrosTitleLabel, 4, 2);
            pacienteTableLayoutPanel.Controls.Add(totalRegistrosValueLabel, 5, 2);
            pacienteTableLayoutPanel.Dock = DockStyle.Fill;
            pacienteTableLayoutPanel.Location = new Point(3, 19);
            pacienteTableLayoutPanel.Name = "pacienteTableLayoutPanel";
            pacienteTableLayoutPanel.Padding = new Padding(5, 2, 5, 2);
            pacienteTableLayoutPanel.RowCount = 3;
            pacienteTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            pacienteTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            pacienteTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            pacienteTableLayoutPanel.Size = new Size(908, 90);
            pacienteTableLayoutPanel.TabIndex = 0;
            // 
            // pacienteTitleLabel
            // 
            pacienteTitleLabel.Anchor = AnchorStyles.Left;
            pacienteTitleLabel.AutoSize = true;
            pacienteTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pacienteTitleLabel.Location = new Point(8, 8);
            pacienteTitleLabel.Name = "pacienteTitleLabel";
            pacienteTitleLabel.Size = new Size(58, 15);
            pacienteTitleLabel.TabIndex = 0;
            pacienteTitleLabel.Text = "Paciente:";
            // 
            // pacienteValueLabel
            // 
            pacienteValueLabel.Anchor = AnchorStyles.Left;
            pacienteValueLabel.AutoSize = true;
            pacienteValueLabel.Location = new Point(123, 8);
            pacienteValueLabel.Name = "pacienteValueLabel";
            pacienteValueLabel.Size = new Size(19, 15);
            pacienteValueLabel.TabIndex = 1;
            pacienteValueLabel.Text = "—";
            // 
            // documentoTitleLabel
            // 
            documentoTitleLabel.Anchor = AnchorStyles.Left;
            documentoTitleLabel.AutoSize = true;
            documentoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            documentoTitleLabel.Location = new Point(318, 8);
            documentoTitleLabel.Name = "documentoTitleLabel";
            documentoTitleLabel.Size = new Size(76, 15);
            documentoTitleLabel.TabIndex = 2;
            documentoTitleLabel.Text = "Documento:";
            // 
            // documentoValueLabel
            // 
            documentoValueLabel.Anchor = AnchorStyles.Left;
            documentoValueLabel.AutoSize = true;
            documentoValueLabel.Location = new Point(438, 8);
            documentoValueLabel.Name = "documentoValueLabel";
            documentoValueLabel.Size = new Size(19, 15);
            documentoValueLabel.TabIndex = 3;
            documentoValueLabel.Text = "—";
            // 
            // fechaNacimientoTitleLabel
            // 
            fechaNacimientoTitleLabel.Anchor = AnchorStyles.Left;
            fechaNacimientoTitleLabel.AutoSize = true;
            fechaNacimientoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fechaNacimientoTitleLabel.Location = new Point(611, 8);
            fechaNacimientoTitleLabel.Name = "fechaNacimientoTitleLabel";
            fechaNacimientoTitleLabel.Size = new Size(100, 15);
            fechaNacimientoTitleLabel.TabIndex = 4;
            fechaNacimientoTitleLabel.Text = "Fecha Nac./Edad:";
            // 
            // fechaNacimientoValueLabel
            // 
            fechaNacimientoValueLabel.Anchor = AnchorStyles.Left;
            fechaNacimientoValueLabel.AutoSize = true;
            fechaNacimientoValueLabel.Location = new Point(731, 8);
            fechaNacimientoValueLabel.Name = "fechaNacimientoValueLabel";
            fechaNacimientoValueLabel.Size = new Size(19, 15);
            fechaNacimientoValueLabel.TabIndex = 5;
            fechaNacimientoValueLabel.Text = "—";
            // 
            // obraSocialTitleLabel
            // 
            obraSocialTitleLabel.Anchor = AnchorStyles.Left;
            obraSocialTitleLabel.AutoSize = true;
            obraSocialTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            obraSocialTitleLabel.Location = new Point(8, 36);
            obraSocialTitleLabel.Name = "obraSocialTitleLabel";
            obraSocialTitleLabel.Size = new Size(72, 15);
            obraSocialTitleLabel.TabIndex = 6;
            obraSocialTitleLabel.Text = "Obra Social:";
            // 
            // obraSocialValueLabel
            // 
            obraSocialValueLabel.Anchor = AnchorStyles.Left;
            obraSocialValueLabel.AutoSize = true;
            obraSocialValueLabel.Location = new Point(123, 36);
            obraSocialValueLabel.Name = "obraSocialValueLabel";
            obraSocialValueLabel.Size = new Size(19, 15);
            obraSocialValueLabel.TabIndex = 7;
            obraSocialValueLabel.Text = "—";
            // 
            // telefonoTitleLabel
            // 
            telefonoTitleLabel.Anchor = AnchorStyles.Left;
            telefonoTitleLabel.AutoSize = true;
            telefonoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            telefonoTitleLabel.Location = new Point(318, 36);
            telefonoTitleLabel.Name = "telefonoTitleLabel";
            telefonoTitleLabel.Size = new Size(59, 15);
            telefonoTitleLabel.TabIndex = 8;
            telefonoTitleLabel.Text = "Teléfono:";
            // 
            // telefonoValueLabel
            // 
            telefonoValueLabel.Anchor = AnchorStyles.Left;
            telefonoValueLabel.AutoSize = true;
            telefonoValueLabel.Location = new Point(438, 36);
            telefonoValueLabel.Name = "telefonoValueLabel";
            telefonoValueLabel.Size = new Size(19, 15);
            telefonoValueLabel.TabIndex = 9;
            telefonoValueLabel.Text = "—";
            // 
            // emailTitleLabel
            // 
            emailTitleLabel.Anchor = AnchorStyles.Left;
            emailTitleLabel.AutoSize = true;
            emailTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            emailTitleLabel.Location = new Point(611, 36);
            emailTitleLabel.Name = "emailTitleLabel";
            emailTitleLabel.Size = new Size(113, 15);
            emailTitleLabel.TabIndex = 10;
            emailTitleLabel.Text = "Correo Electrónico:";
            // 
            // emailValueLabel
            // 
            emailValueLabel.Anchor = AnchorStyles.Left;
            emailValueLabel.AutoSize = true;
            emailValueLabel.Location = new Point(731, 36);
            emailValueLabel.Name = "emailValueLabel";
            emailValueLabel.Size = new Size(19, 15);
            emailValueLabel.TabIndex = 11;
            emailValueLabel.Text = "—";
            // 
            // nroHcTitleLabel
            // 
            nroHcTitleLabel.Anchor = AnchorStyles.Left;
            nroHcTitleLabel.AutoSize = true;
            nroHcTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nroHcTitleLabel.Location = new Point(8, 65);
            nroHcTitleLabel.Name = "nroHcTitleLabel";
            nroHcTitleLabel.Size = new Size(91, 15);
            nroHcTitleLabel.TabIndex = 12;
            nroHcTitleLabel.Text = "Historia Clínica:";
            // 
            // nroHcValueLabel
            // 
            nroHcValueLabel.Anchor = AnchorStyles.Left;
            nroHcValueLabel.AutoSize = true;
            nroHcValueLabel.Location = new Point(123, 65);
            nroHcValueLabel.Name = "nroHcValueLabel";
            nroHcValueLabel.Size = new Size(19, 15);
            nroHcValueLabel.TabIndex = 13;
            nroHcValueLabel.Text = "—";
            // 
            // grupoSanguineoTitleLabel
            // 
            grupoSanguineoTitleLabel.Anchor = AnchorStyles.Left;
            grupoSanguineoTitleLabel.AutoSize = true;
            grupoSanguineoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grupoSanguineoTitleLabel.Location = new Point(318, 65);
            grupoSanguineoTitleLabel.Name = "grupoSanguineoTitleLabel";
            grupoSanguineoTitleLabel.Size = new Size(106, 15);
            grupoSanguineoTitleLabel.TabIndex = 14;
            grupoSanguineoTitleLabel.Text = "Grupo Sanguíneo:";
            // 
            // grupoSanguineoValueLabel
            // 
            grupoSanguineoValueLabel.Anchor = AnchorStyles.Left;
            grupoSanguineoValueLabel.AutoSize = true;
            grupoSanguineoValueLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grupoSanguineoValueLabel.ForeColor = Color.FromArgb(0, 102, 204);
            grupoSanguineoValueLabel.Location = new Point(438, 64);
            grupoSanguineoValueLabel.Name = "grupoSanguineoValueLabel";
            grupoSanguineoValueLabel.Size = new Size(21, 17);
            grupoSanguineoValueLabel.TabIndex = 15;
            grupoSanguineoValueLabel.Text = "—";
            // 
            // totalRegistrosTitleLabel
            // 
            totalRegistrosTitleLabel.Anchor = AnchorStyles.Left;
            totalRegistrosTitleLabel.AutoSize = true;
            totalRegistrosTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            totalRegistrosTitleLabel.Location = new Point(611, 65);
            totalRegistrosTitleLabel.Name = "totalRegistrosTitleLabel";
            totalRegistrosTitleLabel.Size = new Size(106, 15);
            totalRegistrosTitleLabel.TabIndex = 16;
            totalRegistrosTitleLabel.Text = "Registros Clínicos:";
            // 
            // totalRegistrosValueLabel
            // 
            totalRegistrosValueLabel.Anchor = AnchorStyles.Left;
            totalRegistrosValueLabel.AutoSize = true;
            totalRegistrosValueLabel.Location = new Point(731, 65);
            totalRegistrosValueLabel.Name = "totalRegistrosValueLabel";
            totalRegistrosValueLabel.Size = new Size(19, 15);
            totalRegistrosValueLabel.TabIndex = 17;
            totalRegistrosValueLabel.Text = "—";
            // 
            // turnosGroupBox
            // 
            turnosGroupBox.Controls.Add(turnosLayoutPanel);
            turnosGroupBox.Dock = DockStyle.Fill;
            turnosGroupBox.Location = new Point(13, 165);
            turnosGroupBox.Name = "turnosGroupBox";
            turnosGroupBox.Size = new Size(914, 209);
            turnosGroupBox.TabIndex = 2;
            turnosGroupBox.TabStop = false;
            turnosGroupBox.Text = "Turnos del Paciente (Últimas Atenciones y Turnos Asignados)";
            // 
            // turnosLayoutPanel
            // 
            turnosLayoutPanel.ColumnCount = 1;
            turnosLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            turnosLayoutPanel.Controls.Add(resumenTurnosPanel, 0, 0);
            turnosLayoutPanel.Controls.Add(turnosPacienteDataGridView, 0, 1);
            turnosLayoutPanel.Dock = DockStyle.Fill;
            turnosLayoutPanel.Location = new Point(3, 19);
            turnosLayoutPanel.Name = "turnosLayoutPanel";
            turnosLayoutPanel.RowCount = 2;
            turnosLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            turnosLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            turnosLayoutPanel.Size = new Size(908, 187);
            turnosLayoutPanel.TabIndex = 0;
            // 
            // resumenTurnosPanel
            // 
            resumenTurnosPanel.BackColor = Color.FromArgb(246, 248, 250);
            resumenTurnosPanel.ColumnCount = 4;
            resumenTurnosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            resumenTurnosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resumenTurnosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
            resumenTurnosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resumenTurnosPanel.Controls.Add(ultimoTurnoTitleLabel, 0, 0);
            resumenTurnosPanel.Controls.Add(ultimoTurnoValueLabel, 1, 0);
            resumenTurnosPanel.Controls.Add(proximoTurnoTitleLabel, 2, 0);
            resumenTurnosPanel.Controls.Add(proximoTurnoValueLabel, 3, 0);
            resumenTurnosPanel.Dock = DockStyle.Fill;
            resumenTurnosPanel.Location = new Point(3, 2);
            resumenTurnosPanel.Margin = new Padding(3, 2, 3, 4);
            resumenTurnosPanel.Name = "resumenTurnosPanel";
            resumenTurnosPanel.Padding = new Padding(6, 0, 6, 0);
            resumenTurnosPanel.RowCount = 1;
            resumenTurnosPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resumenTurnosPanel.Size = new Size(902, 26);
            resumenTurnosPanel.TabIndex = 0;
            // 
            // ultimoTurnoTitleLabel
            // 
            ultimoTurnoTitleLabel.Anchor = AnchorStyles.Left;
            ultimoTurnoTitleLabel.AutoSize = true;
            ultimoTurnoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ultimoTurnoTitleLabel.Location = new Point(9, 5);
            ultimoTurnoTitleLabel.Name = "ultimoTurnoTitleLabel";
            ultimoTurnoTitleLabel.Size = new Size(137, 15);
            ultimoTurnoTitleLabel.TabIndex = 0;
            ultimoTurnoTitleLabel.Text = "Último Turno Atendido:";
            // 
            // ultimoTurnoValueLabel
            // 
            ultimoTurnoValueLabel.Anchor = AnchorStyles.Left;
            ultimoTurnoValueLabel.AutoSize = true;
            ultimoTurnoValueLabel.ForeColor = Color.FromArgb(40, 120, 60);
            ultimoTurnoValueLabel.Location = new Point(154, 5);
            ultimoTurnoValueLabel.Name = "ultimoTurnoValueLabel";
            ultimoTurnoValueLabel.Size = new Size(19, 15);
            ultimoTurnoValueLabel.TabIndex = 1;
            ultimoTurnoValueLabel.Text = "—";
            ultimoTurnoValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // proximoTurnoTitleLabel
            // 
            proximoTurnoTitleLabel.Anchor = AnchorStyles.Left;
            proximoTurnoTitleLabel.AutoSize = true;
            proximoTurnoTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            proximoTurnoTitleLabel.Location = new Point(449, 5);
            proximoTurnoTitleLabel.Name = "proximoTurnoTitleLabel";
            proximoTurnoTitleLabel.Size = new Size(145, 15);
            proximoTurnoTitleLabel.TabIndex = 2;
            proximoTurnoTitleLabel.Text = "Próximo Turno Asignado:";
            // 
            // proximoTurnoValueLabel
            // 
            proximoTurnoValueLabel.Anchor = AnchorStyles.Left;
            proximoTurnoValueLabel.AutoSize = true;
            proximoTurnoValueLabel.ForeColor = Color.FromArgb(0, 102, 204);
            proximoTurnoValueLabel.Location = new Point(604, 5);
            proximoTurnoValueLabel.Name = "proximoTurnoValueLabel";
            proximoTurnoValueLabel.Size = new Size(19, 15);
            proximoTurnoValueLabel.TabIndex = 3;
            proximoTurnoValueLabel.Text = "—";
            proximoTurnoValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // turnosPacienteDataGridView
            // 
            turnosPacienteDataGridView.AllowUserToAddRows = false;
            turnosPacienteDataGridView.AllowUserToDeleteRows = false;
            turnosPacienteDataGridView.AllowUserToResizeRows = false;
            turnosPacienteDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            turnosPacienteDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            turnosPacienteDataGridView.Cursor = Cursors.Hand;
            turnosPacienteDataGridView.Dock = DockStyle.Fill;
            turnosPacienteDataGridView.EnableHeadersVisualStyles = false;
            turnosPacienteDataGridView.Location = new Point(3, 35);
            turnosPacienteDataGridView.Name = "turnosPacienteDataGridView";
            turnosPacienteDataGridView.ReadOnly = true;
            turnosPacienteDataGridView.RowHeadersVisible = false;
            turnosPacienteDataGridView.Size = new Size(902, 149);
            turnosPacienteDataGridView.TabIndex = 1;
            // 
            // historiaClinicaGroupBox
            // 
            historiaClinicaGroupBox.Controls.Add(historiaClinicaLayoutPanel);
            historiaClinicaGroupBox.Dock = DockStyle.Fill;
            historiaClinicaGroupBox.Location = new Point(13, 380);
            historiaClinicaGroupBox.Name = "historiaClinicaGroupBox";
            historiaClinicaGroupBox.Size = new Size(914, 239);
            historiaClinicaGroupBox.TabIndex = 3;
            historiaClinicaGroupBox.TabStop = false;
            historiaClinicaGroupBox.Text = "Historia Clínica — Registros Clínicos";
            // 
            // historiaClinicaLayoutPanel
            // 
            historiaClinicaLayoutPanel.ColumnCount = 1;
            historiaClinicaLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historiaClinicaLayoutPanel.Controls.Add(filtroRegistrosPanel, 0, 0);
            historiaClinicaLayoutPanel.Controls.Add(registrosDataGridView, 0, 1);
            historiaClinicaLayoutPanel.Dock = DockStyle.Fill;
            historiaClinicaLayoutPanel.Location = new Point(3, 19);
            historiaClinicaLayoutPanel.Name = "historiaClinicaLayoutPanel";
            historiaClinicaLayoutPanel.RowCount = 2;
            historiaClinicaLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            historiaClinicaLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historiaClinicaLayoutPanel.Size = new Size(908, 217);
            historiaClinicaLayoutPanel.TabIndex = 0;
            // 
            // filtroRegistrosPanel
            // 
            filtroRegistrosPanel.Controls.Add(tipoRegistroFiltroLabel);
            filtroRegistrosPanel.Controls.Add(tipoRegistroFiltroComboBox);
            filtroRegistrosPanel.Controls.Add(limpiarFiltroRegistrosLinkLabel);
            filtroRegistrosPanel.Dock = DockStyle.Fill;
            filtroRegistrosPanel.Location = new Point(3, 2);
            filtroRegistrosPanel.Margin = new Padding(3, 2, 3, 2);
            filtroRegistrosPanel.Name = "filtroRegistrosPanel";
            filtroRegistrosPanel.Size = new Size(902, 30);
            filtroRegistrosPanel.TabIndex = 0;
            filtroRegistrosPanel.WrapContents = false;
            // 
            // tipoRegistroFiltroLabel
            // 
            tipoRegistroFiltroLabel.AutoSize = true;
            tipoRegistroFiltroLabel.Location = new Point(3, 6);
            tipoRegistroFiltroLabel.Margin = new Padding(3, 6, 6, 0);
            tipoRegistroFiltroLabel.Name = "tipoRegistroFiltroLabel";
            tipoRegistroFiltroLabel.Size = new Size(88, 15);
            tipoRegistroFiltroLabel.TabIndex = 0;
            tipoRegistroFiltroLabel.Text = "Filtrar por Tipo:";
            // 
            // tipoRegistroFiltroComboBox
            // 
            tipoRegistroFiltroComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoRegistroFiltroComboBox.FormattingEnabled = true;
            tipoRegistroFiltroComboBox.Location = new Point(100, 2);
            tipoRegistroFiltroComboBox.Margin = new Padding(3, 2, 12, 2);
            tipoRegistroFiltroComboBox.Name = "tipoRegistroFiltroComboBox";
            tipoRegistroFiltroComboBox.Size = new Size(190, 23);
            tipoRegistroFiltroComboBox.TabIndex = 1;
            // 
            // limpiarFiltroRegistrosLinkLabel
            // 
            limpiarFiltroRegistrosLinkLabel.AutoSize = true;
            limpiarFiltroRegistrosLinkLabel.Location = new Point(305, 6);
            limpiarFiltroRegistrosLinkLabel.Margin = new Padding(3, 6, 3, 0);
            limpiarFiltroRegistrosLinkLabel.Name = "limpiarFiltroRegistrosLinkLabel";
            limpiarFiltroRegistrosLinkLabel.Size = new Size(56, 15);
            limpiarFiltroRegistrosLinkLabel.TabIndex = 2;
            limpiarFiltroRegistrosLinkLabel.TabStop = true;
            limpiarFiltroRegistrosLinkLabel.Text = "Ver todos";
            // 
            // registrosDataGridView
            // 
            registrosDataGridView.AllowUserToAddRows = false;
            registrosDataGridView.AllowUserToDeleteRows = false;
            registrosDataGridView.AllowUserToResizeRows = false;
            registrosDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            registrosDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            registrosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            registrosDataGridView.Cursor = Cursors.Hand;
            registrosDataGridView.Dock = DockStyle.Fill;
            registrosDataGridView.EnableHeadersVisualStyles = false;
            registrosDataGridView.Location = new Point(3, 37);
            registrosDataGridView.Name = "registrosDataGridView";
            registrosDataGridView.ReadOnly = true;
            registrosDataGridView.RowHeadersVisible = false;
            registrosDataGridView.Size = new Size(902, 177);
            registrosDataGridView.TabIndex = 1;
            // 
            // footerPanel
            // 
            footerPanel.ColumnCount = 2;
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            footerPanel.Controls.Add(hintLabel, 0, 0);
            footerPanel.Controls.Add(buttonsFlowPanel, 1, 0);
            footerPanel.Dock = DockStyle.Fill;
            footerPanel.Location = new Point(13, 625);
            footerPanel.Name = "footerPanel";
            footerPanel.RowCount = 1;
            footerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            footerPanel.Size = new Size(914, 42);
            footerPanel.TabIndex = 4;
            // 
            // hintLabel
            // 
            hintLabel.Anchor = AnchorStyles.Left;
            hintLabel.AutoSize = true;
            hintLabel.ForeColor = SystemColors.ControlDarkDark;
            hintLabel.Location = new Point(3, 13);
            hintLabel.Name = "hintLabel";
            hintLabel.Size = new Size(462, 15);
            hintLabel.TabIndex = 0;
            hintLabel.Text = "Sugerencia: haga doble clic en un turno o registro clínico para ver su detalle completo.";
            // 
            // buttonsFlowPanel
            // 
            buttonsFlowPanel.Anchor = AnchorStyles.Right;
            buttonsFlowPanel.AutoSize = true;
            buttonsFlowPanel.Controls.Add(cerrarButton);
            buttonsFlowPanel.Location = new Point(798, 3);
            buttonsFlowPanel.Name = "buttonsFlowPanel";
            buttonsFlowPanel.Size = new Size(113, 36);
            buttonsFlowPanel.TabIndex = 1;
            buttonsFlowPanel.WrapContents = false;
            // 
            // cerrarButton
            // 
            cerrarButton.Location = new Point(3, 3);
            cerrarButton.Name = "cerrarButton";
            cerrarButton.Size = new Size(107, 32);
            cerrarButton.TabIndex = 0;
            cerrarButton.Text = "Cerrar";
            cerrarButton.UseVisualStyleBackColor = true;
            // 
            // PacienteDetalleModalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(940, 680);
            Controls.Add(mainTableLayoutPanel);
            MinimumSize = new Size(840, 600);
            Name = "PacienteDetalleModalForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle del Paciente e Historia Clínica";
            mainTableLayoutPanel.ResumeLayout(false);
            datosPacienteGroupBox.ResumeLayout(false);
            pacienteTableLayoutPanel.ResumeLayout(false);
            pacienteTableLayoutPanel.PerformLayout();
            turnosGroupBox.ResumeLayout(false);
            turnosLayoutPanel.ResumeLayout(false);
            resumenTurnosPanel.ResumeLayout(false);
            resumenTurnosPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)turnosPacienteDataGridView).EndInit();
            historiaClinicaGroupBox.ResumeLayout(false);
            historiaClinicaLayoutPanel.ResumeLayout(false);
            filtroRegistrosPanel.ResumeLayout(false);
            filtroRegistrosPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)registrosDataGridView).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            buttonsFlowPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel = null!;
        private Label bannerLabel = null!;
        private GroupBox datosPacienteGroupBox = null!;
        private TableLayoutPanel pacienteTableLayoutPanel = null!;
        private Label pacienteTitleLabel = null!;
        private Label pacienteValueLabel = null!;
        private Label documentoTitleLabel = null!;
        private Label documentoValueLabel = null!;
        private Label fechaNacimientoTitleLabel = null!;
        private Label fechaNacimientoValueLabel = null!;
        private Label obraSocialTitleLabel = null!;
        private Label obraSocialValueLabel = null!;
        private Label telefonoTitleLabel = null!;
        private Label telefonoValueLabel = null!;
        private Label emailTitleLabel = null!;
        private Label emailValueLabel = null!;
        private Label nroHcTitleLabel = null!;
        private Label nroHcValueLabel = null!;
        private Label grupoSanguineoTitleLabel = null!;
        private Label grupoSanguineoValueLabel = null!;
        private Label totalRegistrosTitleLabel = null!;
        private Label totalRegistrosValueLabel = null!;
        private GroupBox turnosGroupBox = null!;
        private TableLayoutPanel turnosLayoutPanel = null!;
        private TableLayoutPanel resumenTurnosPanel = null!;
        private Label ultimoTurnoTitleLabel = null!;
        private Label ultimoTurnoValueLabel = null!;
        private Label proximoTurnoTitleLabel = null!;
        private Label proximoTurnoValueLabel = null!;
        private DataGridView turnosPacienteDataGridView = null!;
        private GroupBox historiaClinicaGroupBox = null!;
        private TableLayoutPanel historiaClinicaLayoutPanel = null!;
        private FlowLayoutPanel filtroRegistrosPanel = null!;
        private Label tipoRegistroFiltroLabel = null!;
        private ComboBox tipoRegistroFiltroComboBox = null!;
        private LinkLabel limpiarFiltroRegistrosLinkLabel = null!;
        private DataGridView registrosDataGridView = null!;
        private TableLayoutPanel footerPanel = null!;
        private Label hintLabel = null!;
        private FlowLayoutPanel buttonsFlowPanel = null!;
        private Button cerrarButton = null!;
    }
}
