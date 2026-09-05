namespace WindowsForms.DatosMaestros
{
    partial class Especialidades
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
            mainSplitContainer = new SplitContainer();
            listPanel = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            limpiarFiltrosLinkLabel = new LinkLabel();
            filtrarButton = new Button();
            busquedaEspecialidadTextBox = new TextBox();
            busquedaLabel = new Label();
            especialidadesDataGridView = new DataGridView();
            agregarEspecialidadButton = new Button();
            formPanel = new Panel();
            formTitleLabel = new Label();
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            guardarEspecialidadButton = new Button();
            cancelarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            listPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)especialidadesDataGridView).BeginInit();
            formPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 0);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(listPanel);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(formPanel);
            mainSplitContainer.Size = new Size(1082, 672);
            mainSplitContainer.SplitterDistance = 574;
            mainSplitContainer.TabIndex = 0;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(tableLayoutPanel1);
            listPanel.Controls.Add(especialidadesDataGridView);
            listPanel.Controls.Add(agregarEspecialidadButton);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 0);
            listPanel.Name = "listPanel";
            listPanel.Padding = new Padding(8);
            listPanel.Size = new Size(574, 672);
            listPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(limpiarFiltrosLinkLabel, 3, 0);
            tableLayoutPanel1.Controls.Add(filtrarButton, 2, 0);
            tableLayoutPanel1.Controls.Add(busquedaEspecialidadTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(busquedaLabel, 0, 0);
            tableLayoutPanel1.Location = new Point(11, 11);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(552, 34);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Dock = DockStyle.Fill;
            limpiarFiltrosLinkLabel.Location = new Point(374, 0);
            limpiarFiltrosLinkLabel.Name = "limpiarFiltrosLinkLabel";
            limpiarFiltrosLinkLabel.Size = new Size(175, 34);
            limpiarFiltrosLinkLabel.TabIndex = 2;
            limpiarFiltrosLinkLabel.TabStop = true;
            limpiarFiltrosLinkLabel.Text = "Limpiar filtros";
            limpiarFiltrosLinkLabel.TextAlign = ContentAlignment.MiddleLeft;
            limpiarFiltrosLinkLabel.LinkClicked += LimpiarFiltrosLinkLabel_LinkClicked;
            // 
            // filtrarButton
            // 
            filtrarButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtrarButton.AutoSize = true;
            filtrarButton.Location = new Point(278, 3);
            filtrarButton.Name = "filtrarButton";
            filtrarButton.Size = new Size(90, 25);
            filtrarButton.TabIndex = 1;
            filtrarButton.Text = "Filtrar";
            filtrarButton.UseVisualStyleBackColor = true;
            filtrarButton.Click += FiltrarButton_Click;
            // 
            // busquedaEspecialidadTextBox
            // 
            busquedaEspecialidadTextBox.Location = new Point(54, 3);
            busquedaEspecialidadTextBox.Name = "busquedaEspecialidadTextBox";
            busquedaEspecialidadTextBox.Size = new Size(218, 23);
            busquedaEspecialidadTextBox.TabIndex = 0;
            // 
            // busquedaLabel
            // 
            busquedaLabel.AutoSize = true;
            busquedaLabel.Dock = DockStyle.Fill;
            busquedaLabel.Location = new Point(3, 0);
            busquedaLabel.Name = "busquedaLabel";
            busquedaLabel.Size = new Size(45, 34);
            busquedaLabel.TabIndex = 0;
            busquedaLabel.Text = "Buscar:";
            busquedaLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // especialidadesDataGridView
            // 
            especialidadesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            especialidadesDataGridView.Location = new Point(8, 51);
            especialidadesDataGridView.Name = "especialidadesDataGridView";
            especialidadesDataGridView.Size = new Size(555, 472);
            especialidadesDataGridView.TabIndex = 2;
            especialidadesDataGridView.CellContentClick += EspecialidadesDataGridView_CellContentClick;
            especialidadesDataGridView.DataBindingComplete += EspecialidadesDataGridView_DataBindingComplete;
            // 
            // agregarEspecialidadButton
            // 
            agregarEspecialidadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            agregarEspecialidadButton.Location = new Point(8, 1200);
            agregarEspecialidadButton.Name = "agregarEspecialidadButton";
            agregarEspecialidadButton.Size = new Size(160, 30);
            agregarEspecialidadButton.TabIndex = 3;
            agregarEspecialidadButton.Text = "+ Agregar Especialidad";
            agregarEspecialidadButton.UseVisualStyleBackColor = true;
            agregarEspecialidadButton.Click += AgregarEspecialidadButton_Click;
            // 
            // formPanel
            // 
            formPanel.Controls.Add(formTitleLabel);
            formPanel.Controls.Add(nombreLabel);
            formPanel.Controls.Add(nombreTextBox);
            formPanel.Controls.Add(guardarEspecialidadButton);
            formPanel.Controls.Add(cancelarButton);
            formPanel.Dock = DockStyle.Fill;
            formPanel.Location = new Point(0, 0);
            formPanel.Name = "formPanel";
            formPanel.Padding = new Padding(12);
            formPanel.Size = new Size(504, 672);
            formPanel.TabIndex = 0;
            // 
            // formTitleLabel
            // 
            formTitleLabel.AutoSize = true;
            formTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            formTitleLabel.Location = new Point(12, 12);
            formTitleLabel.Name = "formTitleLabel";
            formTitleLabel.Size = new Size(139, 19);
            formTitleLabel.TabIndex = 0;
            formTitleLabel.Text = "Nueva Especialidad";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(15, 50);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(54, 15);
            nombreLabel.TabIndex = 1;
            nombreLabel.Text = "Nombre:";
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(15, 70);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(228, 23);
            nombreTextBox.TabIndex = 0;
            // 
            // guardarEspecialidadButton
            // 
            guardarEspecialidadButton.Location = new Point(104, 99);
            guardarEspecialidadButton.Name = "guardarEspecialidadButton";
            guardarEspecialidadButton.Size = new Size(139, 30);
            guardarEspecialidadButton.TabIndex = 1;
            guardarEspecialidadButton.Text = "Guardar Especialidad";
            guardarEspecialidadButton.UseVisualStyleBackColor = true;
            guardarEspecialidadButton.Click += GuardarEspecialidadButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(15, 99);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(80, 30);
            cancelarButton.TabIndex = 2;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += CancelarButton_Click;
            // 
            // Especialidades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainSplitContainer);
            Name = "Especialidades";
            Size = new Size(1082, 672);
            Load += Especialidades_Load;
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            listPanel.ResumeLayout(false);
            listPanel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)especialidadesDataGridView).EndInit();
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.Label busquedaLabel;
        private System.Windows.Forms.TextBox busquedaEspecialidadTextBox;
        private System.Windows.Forms.Button filtrarButton;
        private System.Windows.Forms.LinkLabel limpiarFiltrosLinkLabel;
        private System.Windows.Forms.DataGridView especialidadesDataGridView;
        private System.Windows.Forms.Button agregarEspecialidadButton;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Label formTitleLabel;
        private System.Windows.Forms.Label nombreLabel;
        private System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.Button guardarEspecialidadButton;
        private System.Windows.Forms.Button cancelarButton;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
