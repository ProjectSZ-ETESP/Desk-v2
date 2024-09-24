using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DeskForms
{
    public partial class telaLog : Form
    {
        public telaLog()
        {
            InitializeComponent();

            SetInitialTextAndColor(txtEmail, "Insira seu e-mail");
            SetInitialTextAndColor(txtPassword, "Insira sua senha");
            txtPassword.PasswordChar = '\0';
        }

        private void SetInitialTextAndColor(TextBox textBox, string initialText)
        {
            textBox.Text = initialText;
            textBox.ForeColor = Color.Gray;
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            if (cboRemember.Checked)
            {
                Properties.Settings.Default.remember = true;
                Properties.Settings.Default.user = txtEmail.Text;
                Properties.Settings.Default.pw = txtPassword.Text;

                Properties.Settings.Default.Save();
            }

            logIn(txtEmail.Text, txtPassword.Text);
        }

        string stringConexão = "server=localhost;database=hospitalar;uid=root;pwd=etesp;AllowUserVariables=True";

        MySqlDataReader myreader;

        private List<string> sqlReturn(string query)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            List<string> returns = new List<string>();
            clsConexão cls = new clsConexão();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    returns.Add(Convert.ToString(myreader["email"]));
                }               

                conn.Close();
            }
            catch (Exception ep)
            {
                MessageBox.Show($"Erro na conexão\n{ep}", "uh oh");
            }
            finally
            {
                conn.Close();
            }

            return returns;
        }

       

        private void logIn(string user, string pw)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);            

            sqlReturn($"call proc_loginFunc('{user}','{pw}', @p_retorno);");

            string query = "select @p_retorno;";

            List<string> returns = new List<string>();

            clsConexão cls = new clsConexão();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    returns.Add(Convert.ToString(myreader["@p_retorno"]));                    
                }

                string pass = returns[0];

                if (pass == "1")
                {
                    cls.setEmail(txtEmail.Text);
                    Principal frm = new Principal();
                    this.Hide();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"XIHHHH", "uh oh");
                }

                conn.Close();
            }
            catch (Exception ep)
            {
                MessageBox.Show($"Erro na conexão\n{ep}", "uh oh");
            }
            finally
            {
                conn.Close();
            }
        }


        private void TogglePasswordVisibility()
        {
            txtPassword.PasswordChar = (txtPassword.PasswordChar == '*') ? '\0' : '*';
            btnEye.BackgroundImage = (txtPassword.PasswordChar == '*') ?
            Properties.Resources.dormi : Properties.Resources.zoio;
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }

        private void txtBox_Click(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = "";
            textBox.ForeColor = Color.White;
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            txtBox_Click(sender, e);
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtBox_Click(sender, e);
            txtPassword.PasswordChar = '*';
            btnEye.Visible = true;
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(btnEye.Visible != true)
            {
                txtPassword.PasswordChar = '*';
            }
            btnEye.Visible = true;
            if (e.KeyData == Keys.Enter)
            {
                logIn(txtEmail.Text, txtPassword.Text);
            }
        }

        string tema;

        
        private void TelaLog_Load(object sender, EventArgs e)
        {
            

            if (Properties.Settings.Default.remember)
            {
                string user = Properties.Settings.Default.user;
                string pw = Properties.Settings.Default.pw;
                logIn(user, pw);
            }

            btnEye.Visible = false;
            tema = Properties.Settings.Default.theme;

            Color white = System.Drawing.ColorTranslator.FromHtml("#161817");
            Color dark = System.Drawing.ColorTranslator.FromHtml("#387c64");

            switch (tema)
            {
                case "Tema Claro":
                    panel1.BackColor = dark;
                    menuBackground.BackgroundImage = Properties.Resources.mapaClaro;
                    btnLogar.BackgroundImage = Properties.Resources.logLight;
                    break;

                case "Tema Escuro":
                    panel1.BackColor = white;
                    menuBackground.BackgroundImage = Properties.Resources.mapaEscuro;
                    btnLogar.BackgroundImage = Properties.Resources.logDark;
                    pctLogo.BackgroundImage = Properties.Resources.logoWhite;
                    break;
            }
        }

        #region CAPETAS INAPAGAVEIS
        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CboRemember_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            load ld = new load();
            ld.ShowDialog(); 
        }

        private void telaLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
