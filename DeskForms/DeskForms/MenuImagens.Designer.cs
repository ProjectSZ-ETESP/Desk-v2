
namespace DeskForms
{
    partial class MenuImagens
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuImagens));
            this.pctPerfil = new System.Windows.Forms.PictureBox();
            this.btnMenos = new System.Windows.Forms.Button();
            this.btnMais = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.img = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pctPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // pctPerfil
            // 
            this.pctPerfil.Location = new System.Drawing.Point(58, 33);
            this.pctPerfil.Name = "pctPerfil";
            this.pctPerfil.Size = new System.Drawing.Size(230, 230);
            this.pctPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctPerfil.TabIndex = 0;
            this.pctPerfil.TabStop = false;
            // 
            // btnMenos
            // 
            this.btnMenos.BackColor = System.Drawing.Color.Transparent;
            this.btnMenos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenos.Location = new System.Drawing.Point(12, 132);
            this.btnMenos.Name = "btnMenos";
            this.btnMenos.Size = new System.Drawing.Size(40, 47);
            this.btnMenos.TabIndex = 1;
            this.btnMenos.Text = "<";
            this.btnMenos.UseVisualStyleBackColor = false;
            this.btnMenos.Click += new System.EventHandler(this.btnMenos_Click);
            // 
            // btnMais
            // 
            this.btnMais.BackColor = System.Drawing.Color.Transparent;
            this.btnMais.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMais.Location = new System.Drawing.Point(294, 132);
            this.btnMais.Name = "btnMais";
            this.btnMais.Size = new System.Drawing.Size(40, 47);
            this.btnMais.TabIndex = 2;
            this.btnMais.Text = ">";
            this.btnMais.UseVisualStyleBackColor = false;
            this.btnMais.Click += new System.EventHandler(this.btnMais_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.Font = new System.Drawing.Font("Poppins Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(58, 269);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(230, 47);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Selecionar";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // img
            // 
            this.img.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img.ImageStream")));
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            this.img.Images.SetKeyName(0, "perfil1.png");
            this.img.Images.SetKeyName(1, "perfil2.png");
            this.img.Images.SetKeyName(2, "perfil3.png");
            this.img.Images.SetKeyName(3, "perfil4.png");
            this.img.Images.SetKeyName(4, "perfil5.png");
            this.img.Images.SetKeyName(5, "perfil6.png");
            this.img.Images.SetKeyName(6, "perfil7.png");
            // 
            // MenuImagens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 326);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnMais);
            this.Controls.Add(this.btnMenos);
            this.Controls.Add(this.pctPerfil);
            this.Name = "MenuImagens";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MenuImagens";
            this.Load += new System.EventHandler(this.MenuImagens_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctPerfil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctPerfil;
        private System.Windows.Forms.Button btnMenos;
        private System.Windows.Forms.Button btnMais;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ImageList img;
    }
}