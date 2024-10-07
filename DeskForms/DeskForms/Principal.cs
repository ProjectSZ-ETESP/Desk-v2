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
            abasPrincipal.ItemSize = new Size(0, 1);
        }


        private void loadInfo()
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string query = $"select * from tblFuncionario where idUsuario = {Properties.Settings.Default.integer}";
            string hosp = "";
            int fto = 1;
                
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
                    fto = int.Parse(myreader["fotoFuncionario"].ToString());
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

            Image perfil = getImg(fto);

            imagePerfil.Image = perfil;
            pfpLateral.Image = perfil;
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
            abasPrincipal.SelectedTab = tabAcesso;
            txtCPF_Pac.Text = "";
            txtCPF_Pac.Focus();
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
            Properties.Settings.Default.img = 0;
            Properties.Settings.Default.integer = 0;
            Properties.Settings.Default.Save();

            DeskForms.telaLog log = new DeskForms.telaLog();
            this.Hide();
            log.ShowDialog();
        }

        string stringConexão = "server=localhost;database=hospitalar;uid=root;pwd=etesp";

        private Image getImg(int index)
        {
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

            return img;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            MenuImagens mrh = new MenuImagens();
            mrh.ShowDialog();
            int index = Properties.Settings.Default.img;

            sqlVoid($"Update tblFuncionario Set fotoFuncionario = {index} where idUsuario = {Properties.Settings.Default.integer}");

            Image perfil = getImg(index);
           
            imagePerfil.Image = perfil;
            pfpLateral.Image = perfil;
        }

        MySqlDataReader myreader;

        #endregion

        int imgPac = 0;

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (    txtID.Text == "" ||
                    txtNome.Text == "" ||
                    txtCPF.Text == "" ||
                    txtDataNasc.Text == "" ||
                    txtTelefone.Text == "" ||
                    txtEmail.Text == "" ||
                    !txtEmail.Text.Contains("@") ||
                    Properties.Settings.Default.imgPac == 0
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
                        cadastrarUser(txtID.Text,txtCPF.Text,txtNome.Text,sex,txtDataNasc.Text,txtTelefone.Text, Properties.Settings.Default.imgPac);
                    }
                }
            }
        }

        private void cadastrarUser(string id, string cpf, string nome, string sexo, string data, string numero, int imgPac)
        {
            try
            {
                DateTime dataPura = DateTime.Parse(data);
                string tempoMenor = dataPura.ToString("yyyy-MM-dd");

                sqlReturn($"call proc_cadastroPac('{id}','{cpf}', '{nome}', '{sexo}', '{tempoMenor}', '{numero}','{imgPac}')");
                
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

        private void sqlVoid(string query)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);

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

                            Color lightGreen = System.Drawing.ColorTranslator.FromHtml("#327960");

                            var label = control as Label;
                            if (label != null)
                            {
                                label.ForeColor = lightGreen;
                            }
                            var tabBack = control as TabPage;
                            if(tabBack != null)
                            {
                                tabBack.BackColor = System.Drawing.ColorTranslator.FromHtml("#2e3331");
                            }
  


                            Color dark = System.Drawing.ColorTranslator.FromHtml("#161817");
                            panelNav.BackColor = dark;
                            rdoMasc.ForeColor = Color.White;
                            rdoFem.ForeColor = Color.White;
                            txtCPF.ForeColor = Color.White;
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
                            Color darkGreen = System.Drawing.ColorTranslator.FromHtml("#161817");
                            Color lightGreen = System.Drawing.ColorTranslator.FromHtml("#327960");

                            var label = control as Label;
                            if (label != null)
                            {
                                label.ForeColor = darkGreen;
                            }
                            
                            rdoMasc.ForeColor = Color.Black;
                            rdoFem.ForeColor = Color.Black;
                            txtCPF.ForeColor = Color.Black;
                            
                            btnLogout.BackgroundImage = Properties.Resources.logoutClear;
                            Properties.Settings.Default.theme = "Tema Claro"; 
                            cboColor.SelectedIndex = 1;
                            panelNav.BackColor = lightGreen;
                            this.BackColor = lightGreen;
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

        private void BtnAddFoto_Click(object sender, EventArgs e)
        {
            MenuImagens mrh = new MenuImagens();
            mrh.ShowDialog();
            int garrafa = Properties.Settings.Default.imgPac;
            pfpPaciente.BackgroundImage = getImg(garrafa);
        }

        private void BtnSearchPac_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string query = $"select * from tblPaciente where cpfPaciente = '{txtCPF_Pac.Text}'";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                string idCli = "0";
                string cpf = "", nomeCompleto = "", data = "", foto = "", telefone = "", emailPac = "", sex = "";
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    idCli = myreader["idPaciente"].ToString();
                    if (idCli != "0")
                    {
                        cpf = myreader["cpfPaciente"].ToString();
                        nomeCompleto = myreader["nomePaciente"].ToString();
                        data = myreader["dataNascPaciente"].ToString();
                        foto = Properties.Settings.Default.imgPac.ToString();
                        sex = myreader["sexoPaciente"].ToString();
                        telefone = myreader["fonePaciente"].ToString();
                    }
                }
                conn.Close();

                if (idCli != "0")
                {
                    abasPrincipal.SelectedTab = tabEdição;
                    editID.Text = idCli;
                    editCPF.Text = cpf;
                    editNome.Text = nomeCompleto;
                    editData.Text = data;
                    editTelefone.Text = telefone;
                    pcbEdit.BackgroundImage = getImg(int.Parse(foto));
                    if(sex == "M"){ rdoEdit_M.Checked = true; }
                    else { rdoEdit_F.Checked = true; }
                }
                else
                {
                    MessageBox.Show($"Esse CPF não está cadastrado em nosso sistema", "uh oh");

                }

            }
            catch (Exception ep)
            {
                MessageBox.Show($"Erro na conexão\n{ep}", "uh oh");
            }
            finally
            {
                conn.Close();
            }

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select email from tblUsuario where idUsuario = '{editID.Text}'", conn);
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    editEmail.Text = myreader["email"].ToString();
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

        private void TxtCPF_Pac_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BtnChangePhoto_Click(object sender, EventArgs e)
        {
            MenuImagens mrh = new MenuImagens();
            mrh.ShowDialog();
            int garrafa = Properties.Settings.Default.imgPacEdit;
            pcbEdit.BackgroundImage = getImg(garrafa);
        }

        private void BtnAlterarFoto_Click(object sender, EventArgs e)
        {
            string sex = "";
            if (rdoEdit_M.Checked) {sex = "M";}
            else {sex = "F";}

            DateTime dataPura = DateTime.Parse(editData.Text);
            string tempoMenor = dataPura.ToString("yyyy-MM-dd");
            try
            {
                sqlReturn($"call proc_editPac({int.Parse(editID.Text)},'{editNome.Text}','{editEmail.Text}','{tempoMenor}','{sex}','{editTelefone.Text}','{editCPF.Text}')");
            }
            catch { }
            MessageBox.Show("Dados do paciente atualizados", "Sucesso!!");
        }

        private void EditCPF_KeyDown(object sender, KeyEventArgs e)
        {
            btnChangePhoto.Text = editCPF.Text;
        }
    }
}
