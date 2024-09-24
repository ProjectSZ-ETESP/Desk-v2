namespace DeskForms
{
    partial class telaLog
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(telaLog));
            this.btnForgetei = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnEye = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.cboRemember = new System.Windows.Forms.CheckBox();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.btnLogar = new System.Windows.Forms.PictureBox();
            this.menuBackground = new System.Windows.Forms.MenuStrip();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEye)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnForgetei
            // 
            this.btnForgetei.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForgetei.AutoSize = true;
            this.btnForgetei.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForgetei.ForeColor = System.Drawing.SystemColors.Control;
            this.btnForgetei.Location = new System.Drawing.Point(55, 436);
            this.btnForgetei.Name = "btnForgetei";
            this.btnForgetei.Size = new System.Drawing.Size(161, 20);
            this.btnForgetei.TabIndex = 10;
            this.btnForgetei.Text = "Esqueci minha senha";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(124)))), ((int)(((byte)(100)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cboRemember);
            this.panel1.Controls.Add(this.pctLogo);
            this.panel1.Controls.Add(this.btnLogar);
            this.panel1.Controls.Add(this.btnForgetei);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 600);
            this.panel1.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::DeskForms.Properties.Resources.rec;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.txtPassword);
            this.panel3.Controls.Add(this.btnEye);
            this.panel3.Location = new System.Drawing.Point(3, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(289, 55);
            this.panel3.TabIndex = 18;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel3_Paint);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPassword.Location = new System.Drawing.Point(21, 15);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(215, 19);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.Text = "a";
            this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // btnEye
            // 
            this.btnEye.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEye.BackColor = System.Drawing.Color.Transparent;
            this.btnEye.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEye.BackgroundImage")));
            this.btnEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEye.Location = new System.Drawing.Point(242, 12);
            this.btnEye.Name = "btnEye";
            this.btnEye.Size = new System.Drawing.Size(26, 26);
            this.btnEye.TabIndex = 13;
            this.btnEye.TabStop = false;
            this.btnEye.Click += new System.EventHandler(this.btnEye_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::DeskForms.Properties.Resources.rec;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Location = new System.Drawing.Point(3, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 55);
            this.panel2.TabIndex = 17;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(20, 13);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(247, 26);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.Text = "alencar@gmail.com";
            this.txtEmail.Click += new System.EventHandler(this.txtEmail_Click);
            this.txtEmail.TextChanged += new System.EventHandler(this.TxtEmail_TextChanged);
            // 
            // cboRemember
            // 
            this.cboRemember.AutoSize = true;
            this.cboRemember.FlatAppearance.BorderColor = System.Drawing.SystemColors.HighlightText;
            this.cboRemember.FlatAppearance.BorderSize = 0;
            this.cboRemember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRemember.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRemember.ForeColor = System.Drawing.SystemColors.Control;
            this.cboRemember.Location = new System.Drawing.Point(23, 376);
            this.cboRemember.Name = "cboRemember";
            this.cboRemember.Size = new System.Drawing.Size(133, 21);
            this.cboRemember.TabIndex = 15;
            this.cboRemember.Text = "Mantenha o login";
            this.cboRemember.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cboRemember.UseVisualStyleBackColor = true;
            this.cboRemember.CheckedChanged += new System.EventHandler(this.CboRemember_CheckedChanged);
            // 
            // pctLogo
            // 
            this.pctLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctLogo.BackgroundImage = global::DeskForms.Properties.Resources.logoDark_1;
            this.pctLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctLogo.Location = new System.Drawing.Point(93, 61);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(100, 100);
            this.pctLogo.TabIndex = 12;
            this.pctLogo.TabStop = false;
            // 
            // btnLogar
            // 
            this.btnLogar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogar.BackgroundImage = global::DeskForms.Properties.Resources.logLight;
            this.btnLogar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogar.Location = new System.Drawing.Point(114, 503);
            this.btnLogar.Name = "btnLogar";
            this.btnLogar.Size = new System.Drawing.Size(60, 60);
            this.btnLogar.TabIndex = 14;
            this.btnLogar.TabStop = false;
            this.btnLogar.Click += new System.EventHandler(this.btnLogar_Click);
            // 
            // menuBackground
            // 
            this.menuBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuBackground.Location = new System.Drawing.Point(0, 0);
            this.menuBackground.Name = "menuBackground";
            this.menuBackground.Size = new System.Drawing.Size(934, 599);
            this.menuBackground.TabIndex = 18;
            this.menuBackground.Text = "menuStrip1";
            // 
            // telaLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 599);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(950, 638);
            this.Name = "telaLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.telaLog_FormClosed);
            this.Load += new System.EventHandler(this.TelaLog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEye)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label btnForgetei;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox btnEye;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cboRemember;
        private System.Windows.Forms.PictureBox pctLogo;
        private System.Windows.Forms.PictureBox btnLogar;
        private System.Windows.Forms.MenuStrip menuBackground;
        private System.Windows.Forms.TextBox txtPassword;
    }
}

