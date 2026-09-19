namespace SIBankDesktop
{
    partial class Form1
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

        // Componentes UI
        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblLogoIcon;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel pnlUsuarioUnderline;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPasswordUnderline;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkMostrarPassword;
        private System.Windows.Forms.Label lblCapsLock;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnCerrar;

        private void InitializeComponent()
        {
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblLogoIcon = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pnlUsuarioUnderline = new System.Windows.Forms.Panel();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlPasswordUnderline = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkMostrarPassword = new System.Windows.Forms.CheckBox();
            this.lblCapsLock = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();

            this.pnlBackground.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // ===== Form1 =====
            this.ClientSize = new System.Drawing.Size(950, 620);
            this.Text = "SIBank Enterprise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(10, 15, 29); // Azul súper oscuro / Cyberpunk Dark
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // Sin bordes tradicionales
            this.DoubleBuffered = true;

            // ===== pnlBackground (Permite arrastrar la ventana) =====
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Controls.Add(this.btnCerrar);
            this.pnlBackground.Controls.Add(this.pnlCard);

            // ===== btnCerrar =====
            this.btnCerrar.Text = "✕";
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(140, 150, 170);
            this.btnCerrar.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(232, 17, 35);
            this.btnCerrar.Location = new System.Drawing.Point(900, 10);
            this.btnCerrar.Size = new System.Drawing.Size(40, 35);
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Click += (s, e) => System.Windows.Forms.Application.Exit();

            // ===== pnlCard (Tarjeta Elevada Modern / Neobanca) =====
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(18, 25, 45); // Dark Slate Blue
            this.pnlCard.Location = new System.Drawing.Point(285, 60);
            this.pnlCard.Size = new System.Drawing.Size(380, 500);
            this.pnlCard.Controls.Add(this.lblLogoIcon);
            this.pnlCard.Controls.Add(this.lblTitulo);
            this.pnlCard.Controls.Add(this.lblSubtitulo);
            this.pnlCard.Controls.Add(this.lblUsuario);
            this.pnlCard.Controls.Add(this.txtUsuario);
            this.pnlCard.Controls.Add(this.pnlUsuarioUnderline);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.pnlPasswordUnderline);
            this.pnlCard.Controls.Add(this.chkMostrarPassword);
            this.pnlCard.Controls.Add(this.lblCapsLock);
            this.pnlCard.Controls.Add(this.btnLogin);
            this.pnlCard.Controls.Add(this.lblMensaje);

            // ===== Logo Isotipo Generativo =====
            this.lblLogoIcon.Text = "❖";
            this.lblLogoIcon.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblLogoIcon.ForeColor = System.Drawing.Color.FromArgb(0, 210, 255); // Cyan Neón
            this.lblLogoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogoIcon.Location = new System.Drawing.Point(0, 20);
            this.lblLogoIcon.Size = new System.Drawing.Size(380, 45);

            // ===== lblTitulo =====
            this.lblTitulo.Text = "SIBANK";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Location = new System.Drawing.Point(0, 65);
            this.lblTitulo.Size = new System.Drawing.Size(380, 40);

            // ===== lblSubtitulo =====
            this.lblSubtitulo.Text = "ENTERPRISE BANKING PLATFORM";
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(0, 210, 255);
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubtitulo.Location = new System.Drawing.Point(0, 105);
            this.lblSubtitulo.Size = new System.Drawing.Size(380, 20);

            // ===== lblUsuario =====
            this.lblUsuario.Text = "USUARIO";
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(140, 150, 170);
            this.lblUsuario.Location = new System.Drawing.Point(45, 145);
            this.lblUsuario.AutoSize = true;

            // ===== txtUsuario =====
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsuario.ForeColor = System.Drawing.Color.White;
            this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(18, 25, 45);
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Location = new System.Drawing.Point(45, 168);
            this.txtUsuario.Size = new System.Drawing.Size(290, 25);

            // ===== pnlUsuarioUnderline =====
            this.pnlUsuarioUnderline.BackColor = System.Drawing.Color.FromArgb(50, 65, 95);
            this.pnlUsuarioUnderline.Location = new System.Drawing.Point(45, 195);
            this.pnlUsuarioUnderline.Size = new System.Drawing.Size(290, 2);

            // ===== lblPassword =====
            this.lblPassword.Text = "CONTRASEÑA";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(140, 150, 170);
            this.lblPassword.Location = new System.Drawing.Point(45, 215);
            this.lblPassword.AutoSize = true;

            // ===== txtPassword =====
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(18, 25, 45);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Location = new System.Drawing.Point(45, 238);
            this.txtPassword.Size = new System.Drawing.Size(290, 25);

            // ===== pnlPasswordUnderline =====
            this.pnlPasswordUnderline.BackColor = System.Drawing.Color.FromArgb(50, 65, 95);
            this.pnlPasswordUnderline.Location = new System.Drawing.Point(45, 265);
            this.pnlPasswordUnderline.Size = new System.Drawing.Size(290, 2);

            // ===== chkMostrarPassword =====
            this.chkMostrarPassword.Text = "Mostrar contraseña";
            this.chkMostrarPassword.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkMostrarPassword.ForeColor = System.Drawing.Color.FromArgb(140, 150, 170);
            this.chkMostrarPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMostrarPassword.Location = new System.Drawing.Point(45, 278);
            this.chkMostrarPassword.AutoSize = true;

            // ===== lblCapsLock =====
            this.lblCapsLock.Text = "⚠ Bloq Mayús activado";
            this.lblCapsLock.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCapsLock.ForeColor = System.Drawing.Color.FromArgb(255, 175, 0);
            this.lblCapsLock.Location = new System.Drawing.Point(45, 305);
            this.lblCapsLock.Size = new System.Drawing.Size(290, 18);
            this.lblCapsLock.Visible = false;

            // ===== btnLogin =====
            this.btnLogin.Text = "INICIAR SESIÓN";
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(10, 15, 29);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 210, 255); // Cyan Neón
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Location = new System.Drawing.Point(45, 335);
            this.btnLogin.Size = new System.Drawing.Size(290, 45);
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;

            // ===== lblMensaje =====
            this.lblMensaje.Text = "";
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(255, 80, 100);
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMensaje.Location = new System.Drawing.Point(20, 395);
            this.lblMensaje.Size = new System.Drawing.Size(340, 80);

            // ===== Agregar controles =====
            this.Controls.Add(this.pnlBackground);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlBackground.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}