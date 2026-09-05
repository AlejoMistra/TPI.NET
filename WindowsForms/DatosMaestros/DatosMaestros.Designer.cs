namespace WindowsForms.DatosMaestros
{
    partial class DatosMaestros
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
            datosMaestrosTabControl = new System.Windows.Forms.TabControl();
            profesionalesTabPage = new System.Windows.Forms.TabPage();
            especialidadesTabPage = new System.Windows.Forms.TabPage();
            datosMaestrosTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // datosMaestrosTabControl
            // 
            datosMaestrosTabControl.Controls.Add(profesionalesTabPage);
            datosMaestrosTabControl.Controls.Add(especialidadesTabPage);
            datosMaestrosTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            datosMaestrosTabControl.Location = new System.Drawing.Point(0, 0);
            datosMaestrosTabControl.Name = "datosMaestrosTabControl";
            datosMaestrosTabControl.SelectedIndex = 0;
            datosMaestrosTabControl.Size = new System.Drawing.Size(1090, 700);
            datosMaestrosTabControl.TabIndex = 0;
            // 
            // profesionalesTabPage
            // 
            profesionalesTabPage.Location = new System.Drawing.Point(4, 24);
            profesionalesTabPage.Margin = new System.Windows.Forms.Padding(10);
            profesionalesTabPage.Name = "profesionalesTabPage";
            profesionalesTabPage.Padding = new System.Windows.Forms.Padding(3);
            profesionalesTabPage.Size = new System.Drawing.Size(1082, 672);
            profesionalesTabPage.TabIndex = 0;
            profesionalesTabPage.Text = "Profesionales";
            profesionalesTabPage.UseVisualStyleBackColor = true;
            profesionalesTabPage.Enter += profesionalesTabPage_Enter;
            // 
            // especialidadesTabPage
            // 
            especialidadesTabPage.Location = new System.Drawing.Point(4, 24);
            especialidadesTabPage.Name = "especialidadesTabPage";
            especialidadesTabPage.Padding = new System.Windows.Forms.Padding(3);
            especialidadesTabPage.Size = new System.Drawing.Size(1082, 672);
            especialidadesTabPage.TabIndex = 1;
            especialidadesTabPage.Text = "Especialidades";
            especialidadesTabPage.UseVisualStyleBackColor = true;
            especialidadesTabPage.Enter += especialidadesTabPage_Enter;
            // 
            // DatosMaestros
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(datosMaestrosTabControl);
            Name = "DatosMaestros";
            Size = new System.Drawing.Size(1090, 700);
            datosMaestrosTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl datosMaestrosTabControl;
        private System.Windows.Forms.TabPage profesionalesTabPage;
        private System.Windows.Forms.TabPage especialidadesTabPage;
    }
}
