using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskForms
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            //Garante que o forms se feche por completo
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            loadConfig();
            maskCPF();
            reset();
            if (Properties.Settings.Default.integer != 0)
            {
                loadInfo();
            }
        }


        private void loadInfo()
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string query = $"select * from tblFuncionario where idUsuario = {Properties.Settings.Default.integer}";
            string hosp = "";
                
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string str = myreader["nomeFuncionario"].ToString();
                    int index = str.IndexOf(' ');

                    string result = str.Substring(0, index);

                    lblNome.Text = result;
                    lblNomeCompleto.Text = myreader["nomeFuncionario"].ToString();
                    lblTelefone.Text = myreader["foneFuncionario"].ToString();
                    lblEmail.Text = Properties.Settings.Default.user.ToString();
                    hosp = myreader["cnpj"].ToString();
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
            lblHospital.Text = 
           loadHospital(hosp);
        }

        private string loadHospital(string cnpj)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string query = $"select * from tblHospital where cnpj = {cnpj}";
            string retorno = "";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    retorno = myreader["nomeHosp"].ToString();
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
            return retorno;
        }

        #region Botões de Navegação

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabRegistro;
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabConsulta;

        }

        private void btnForum_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabForum;

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabConfig;

        }

        private void PfpLateral_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabPerfil;

        }


        #endregion

        #region Perfil

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.remember = false;
            Properties.Settings.Default.user = "";
            Properties.Settings.Default.pw = "";

            Properties.Settings.Default.Save();

            DeskForms.telaLog log = new DeskForms.telaLog();
            this.Hide();
            log.ShowDialog();
        }

        string stringConexão = "server=localhost;database=hospitalar;uid=root;pwd=etesp";

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            MenuImagens mrh = new MenuImagens();
            mrh.ShowDialog();
            int index = Properties.Settings.Default.img;

            Image img = Properties.Resources.dog;

            switch (index)
            {
                case 1:
                    img = Properties.Resources._1;
                    break;

                case 2:
                    img = Properties.Resources._2;

                    break;

                case 3:
                    img = Properties.Resources._3;

                    break;

                case 4:
                    img = Properties.Resources._4;

                    break;

                case 5:
                    img = Properties.Resources._5;

                    break;

                case 6:
                    img = Properties.Resources._6;

                    break;

                case 7:
                    img = Properties.Resources._7;

                    break;

                case 8:
                    img = Properties.Resources._8;

                    break;
            }
            imagePerfil.Image = img;
            pfpLateral.Image = img;
        }

        MySqlDataReader myreader;     

        #endregion

        private void BtnAddFoto_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Abrir Imagem";
            dialog.Filter = "Image Files (*.bmp; *.jpg; *.jpeg; *.png; *.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image imagem = Image.FromFile(dialog.FileName);

                    pfpPaciente.BackgroundImage = imagem;

                    dialog.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao carregar a imagem: " + ex.Message);
                }
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (    txtID.Text == "" ||
                    txtNome.Text == "" ||
                    txtCPF.Text == "" ||
                    txtDataNasc.Text == "" ||
                    txtTelefone.Text == "" ||
                    txtEmail.Text == ""
                    || !txtEmail.Text.Contains("@")
                    )
            {
                MessageBox.Show("Dados inválidos ou não suficientes", "Erro!!");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("🦋 Você está certo de suas escolhas?", "Registro", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string sex;
                    if (rdoFem.Checked)
                    {
                        sex = "F";
                    }
                    else
                    {
                        sex = "M";
                    }

                    int idFunc = Properties.Settings.Default.integer;


                    if (txtID.Text == idFunc + "")
                    {
                        MessageBox.Show("Você não pode se cadastrar como paciente oh zé!!", "PESSOAL O SOM");
                    }
                    else
                    {
                        cadastrarUser(txtID.Text,txtCPF.Text,txtNome.Text,sex,txtDataNasc.Text,txtTelefone.Text);
                    }
                }
            }
        }

        private void cadastrarUser(string id, string cpf, string nome, string sexo, string data, string numero)
        {
            try
            {
                DateTime dataPura = DateTime.Parse(data);
                string tempoMenor = dataPura.ToString("yyyy-MM-dd");

                sqlReturn($"call proc_cadastroPac('{id}','{cpf}', '{nome}', '{sexo}', '{tempoMenor}', '{numero}')");
                
                MessageBox.Show("Paciente Cadastrado com sucesso", "Sucesso!!");
                reset();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Pane no sistema, e nem foi a Pitty\n{e}", "Uh oh!!");

            }
        }

        private List<string> sqlReturn(string query)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            List<string> returns = new List<string>();
            clsConexão cls = new clsConexão();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.ExecuteNonQuery();
                
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

        private void maskCPF()
        {
            txtCPF.Text = "12345678910";
            txtCPF.ForeColor = Color.Gray;
        }

        private void loadConfig()
        {
            cboColor.Items.Add("Padrão (Sistema)");

            cboColor.Items.Add("Tema Claro");
            cboColor.Items.Add("Tema Escuro");

            cboLanguage.Items.Add("PT-BR");
            cboLanguage.Items.Add("EN-US");

            cboNotific.Items.Add("Todos");

            cboNotific.Items.Add("Somente Forúm");
            cboNotific.Items.Add("Silencioso");

            cboColor.SelectedIndex = 0;
            cboLanguage.SelectedIndex = 0;
            cboNotific.SelectedIndex = 0;

            changeTheme(Properties.Settings.Default.theme);

        }
        private void reset()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtDataNasc.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            rdoMasc.Checked = true;
            pfpPaciente.BackgroundImage = null;
            txtNome.Focus();
        }

        private void changeTheme(String version)
        {

            switch (version)
            {

                case "Tema Escuro":

                    foreach (TabPage tab in abasPrincipal.TabPages)
                    {
                        tab.BackColor = Color.Black;

                        foreach (var control in tab.Controls)
                        {

                            var label = control as Label;
                            if (label == null) continue;
                            label.ForeColor = Color.White;
                            Color dark = System.Drawing.ColorTranslator.FromHtml("#161817");
                            panelNav.BackColor = dark;
                            rdoMasc.ForeColor = Color.White;
                            rdoFem.ForeColor = Color.White;
                            txtCPF.ForeColor = Color.White;
                            Principal frm = new Principal();
                            this.BackColor = dark;

                            btnLogout.BackgroundImage = Properties.Resources.logout;
                            Properties.Settings.Default.theme = "Tema Escuro";
                            cboColor.SelectedIndex = 2;
                        }
                    }

                    break;
                case "Tema Claro":

                    foreach (TabPage tab in abasPrincipal.TabPages)
                    {
                        tab.BackColor = Color.White;

                        foreach (var control in tab.Controls)
                        {

                            Color back = System.Drawing.ColorTranslator.FromHtml("#161817");
                            var label = control as Label;
                            if (label == null) continue;
                            label.ForeColor = back;
                            Color white = System.Drawing.ColorTranslator.FromHtml("#161817");
                            rdoMasc.ForeColor = Color.Black;
                            rdoFem.ForeColor = Color.Black;
                            txtCPF.ForeColor = Color.Black;
                            panelNav.BackColor = white;
                            btnLogout.BackgroundImage = Properties.Resources.logoutClear;
                            Properties.Settings.Default.theme = "Tema Claro"; 
                            cboColor.SelectedIndex = 1;
                        }
                    }
                    break;

            }

            Properties.Settings.Default.Save();
        }

        private void CboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox caixa = (sender as ComboBox);
            string theme = caixa.Text;
            changeTheme(theme);
        }

        private void TxtCPF_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private bool mouseDown;
        private Point lastLocation;


        private void Principal_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Principal_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Principal_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }
    }
}
