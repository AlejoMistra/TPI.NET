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
            limpiarFiltrosLinkLabel = new LinkLabel();
            busquedaEstadoComboBox = new ComboBox();
            AgregarProfesionalButton = new Button();
            profesionalesDataGridView = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            busquedaGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            busquedaGroupBox.TabIndex = 0;
            busquedaGroupBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 5;
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
            tableLayoutPanel1.Controls.Add(limpiarFiltrosLinkLabel, 3, 1);
            tableLayoutPanel1.Controls.Add(busquedaEstadoComboBox, 2, 1);
            tableLayoutPanel1.Controls.Add(AgregarProfesionalButton, 4, 1);
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
            busquedaEspecialidadComboBox.SelectedValueChanged += FiltrarDataGridView;
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
            busquedaProfesionalTextBox.TextChanged += FiltrarDataGridView;
            // 
            // limpiarFiltrosLinkLabel
            // 
            limpiarFiltrosLinkLabel.Anchor = AnchorStyles.None;
            limpiarFiltrosLinkLabel.AutoSize = true;
            limpiarFiltrosLinkLabel.Location = new Point(533, 22);
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
            busquedaEstadoComboBox.SelectedValueChanged += FiltrarDataGridView;
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
            profesionalesDataGridView.Size = new Size(1077, 421);
            profesionalesDataGridView.TabIndex = 1;
            profesionalesDataGridView.CellClick += ProfesionalesDataGridView_CellContentClick;
            profesionalesDataGridView.DataBindingComplete += ProfesionalesDataGridView_DataBindingComplete;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoScroll = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(busquedaGroupBox, 0, 0);
            tableLayoutPanel3.Controls.Add(profesionalesDataGridView, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1083, 499);
            tableLayoutPanel3.TabIndex = 0;
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
        private LinkLabel limpiarFiltrosLinkLabel;
        private ComboBox busquedaEstadoComboBox;
        private Button AgregarProfesionalButton;
        private DataGridView profesionalesDataGridView;
        private TableLayoutPanel tableLayoutPanel3;
    }
}
