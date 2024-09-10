namespace DeskForms
{
    partial class load
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
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.open = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // t1
            // 
            this.t1.Interval = 5;
            this.t1.Tick += new System.EventHandler(this.t1_Tick);
            // 
            // open
            // 
            this.open.Enabled = true;
            this.open.Interval = 1000;
            this.open.Tick += new System.EventHandler(this.Open_Tick);
            // 
            // load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "load";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "load";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Load_FormClosing);
            this.Load += new System.EventHandler(this.Load_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer t1;
        private System.Windows.Forms.Timer open;
    }
}