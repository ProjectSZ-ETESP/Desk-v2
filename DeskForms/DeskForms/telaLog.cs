using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

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


        MySqlDataReader myreader;
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

        string stringConexão = "server=localhost;database=testDB;uid=root;pwd=etesp";


        private void logIn(string user, string pw)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);

            
            string query = $"Select * from tblCliente where nome = '{user}' AND senha = '{pw}'";

            List<string> strings = new List<string>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);



                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    strings.Add(Convert.ToString(myreader["nome"]));
                }

                if (strings.Count() > 0)
                {
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
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Gray;
                txtPassword.PasswordChar = '*';
                btnEye.Visible = true;
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            txtBox_Click(sender, e);
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtBox_Click(sender, e);
        }

        string tema;

        Timer t1 = new Timer();




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
                    menuBackground.BackgroundImage = Properties.Resources.map_vector;

                    break;
                case "Tema Escuro":
                    panel1.BackColor = white;
                    menuBackground.BackgroundImage = Properties.Resources.Tela_Login;
                    break;
            }
        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CboRemember_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
