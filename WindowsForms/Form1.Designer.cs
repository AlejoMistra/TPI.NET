namespace WindowsForms
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUser = new TextBox();
            lblUser = new Label();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblTitle = new Label();
            btnLogIn = new Button();
            SuspendLayout();
            // 
            // txtUser
            // 
            txtUser.Location = new Point(49, 87);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(182, 27);
            txtUser.TabIndex = 1;
            txtUser.TextChanged += textBox1_TextChanged;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(48, 64);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(59, 20);
            lblUser.TabIndex = 2;
            lblUser.Text = "Usuario";
            lblUser.Click += label1_Click_1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(49, 131);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(83, 20);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Contraseña";
            lblPassword.Click += label2_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(49, 154);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(182, 27);
            txtPassword.TabIndex = 4;
            txtPassword.TextChanged += textBox2_TextChanged;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(49, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 20);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Bienvenido al Sistema!";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogIn
            // 
            btnLogIn.Location = new Point(211, 201);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(94, 29);
            btnLogIn.TabIndex = 6;
            btnLogIn.Text = "Ingresar";
            btnLogIn.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(337, 252);
            Controls.Add(btnLogIn);
            Controls.Add(lblTitle);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Name = "LoginForm";
            Text = "Login";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtUser;
        private Label lblUser;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblTitle;
        private Button btnLogIn;
    }
}
