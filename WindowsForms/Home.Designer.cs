namespace WindowsForms
{
    partial class Home
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            agendaDeTurnosToolStripMenuItem = new ToolStripMenuItem();
            facturaciónToolStripMenuItem = new ToolStripMenuItem();
            datosMaestrosToolStripMenuItem = new ToolStripMenuItem();
            ContentPanel = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { agendaDeTurnosToolStripMenuItem, facturaciónToolStripMenuItem, datosMaestrosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(904, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // agendaDeTurnosToolStripMenuItem
            // 
            agendaDeTurnosToolStripMenuItem.Name = "agendaDeTurnosToolStripMenuItem";
            agendaDeTurnosToolStripMenuItem.Size = new Size(116, 20);
            agendaDeTurnosToolStripMenuItem.Text = "Agenda de Turnos";
            // 
            // facturaciónToolStripMenuItem
            // 
            facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            facturaciónToolStripMenuItem.Size = new Size(81, 20);
            facturaciónToolStripMenuItem.Text = "Facturación";
            // 
            // datosMaestrosToolStripMenuItem
            // 
            datosMaestrosToolStripMenuItem.CheckOnClick = true;
            datosMaestrosToolStripMenuItem.Name = "datosMaestrosToolStripMenuItem";
            datosMaestrosToolStripMenuItem.Size = new Size(100, 20);
            datosMaestrosToolStripMenuItem.Text = "Datos Maestros";
            datosMaestrosToolStripMenuItem.Click += datosMaestrosToolStripMenuItem_Click;
            // 
            // ContentPanel
            // 
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(0, 24);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(904, 597);
            ContentPanel.TabIndex = 1;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 621);
            Controls.Add(ContentPanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Home";
            StartPosition = FormStartPosition.WindowsDefaultBounds;
            Text = "Sistema de Gestión Médico";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem agendaDeTurnosToolStripMenuItem;
        private ToolStripMenuItem facturaciónToolStripMenuItem;
        private ToolStripMenuItem datosMaestrosToolStripMenuItem;
        private Panel ContentPanel;
    }
}