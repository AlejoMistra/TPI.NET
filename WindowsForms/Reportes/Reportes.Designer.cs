namespace WindowsForms.Reportes
{
    partial class Reportes
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            reportesTabControl = new TabControl();
            tabTurnos = new TabPage();
            tabFacturacion = new TabPage();
            reportesTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // reportesTabControl
            // 
            reportesTabControl.Controls.Add(tabTurnos);
            reportesTabControl.Controls.Add(tabFacturacion);
            reportesTabControl.Dock = DockStyle.Fill;
            reportesTabControl.Location = new Point(0, 0);
            reportesTabControl.Name = "reportesTabControl";
            reportesTabControl.SelectedIndex = 0;
            reportesTabControl.Size = new Size(1000, 650);
            reportesTabControl.TabIndex = 0;
            // 
            // tabTurnos
            // 
            tabTurnos.Location = new Point(4, 24);
            tabTurnos.Name = "tabTurnos";
            tabTurnos.Padding = new Padding(3);
            tabTurnos.Size = new Size(992, 622);
            tabTurnos.TabIndex = 0;
            tabTurnos.Text = "Tablero de Turnos";
            tabTurnos.UseVisualStyleBackColor = true;
            tabTurnos.Enter += tabTurnos_Enter;
            // 
            // tabFacturacion
            // 
            tabFacturacion.Location = new Point(4, 24);
            tabFacturacion.Name = "tabFacturacion";
            tabFacturacion.Padding = new Padding(3);
            tabFacturacion.Size = new Size(992, 622);
            tabFacturacion.TabIndex = 1;
            tabFacturacion.Text = "Facturación";
            tabFacturacion.UseVisualStyleBackColor = true;
            tabFacturacion.Enter += tabFacturacion_Enter;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(reportesTabControl);
            Name = "Reportes";
            Size = new Size(1000, 650);
            Load += Reportes_Load;
            reportesTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl reportesTabControl;
        private TabPage tabTurnos;
        private TabPage tabFacturacion;
    }
}

