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

       
        private string caminhoImagem = null;
        string stringConexão = "server=localhost;database=hospitalar;uid=root;pwd=etesp";

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofl = new OpenFileDialog();
            ofl.Title = "Adicionar Imagem";
            ofl.Filter = "All files (*.*)|*.*";
            if (ofl.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imagePerfil.Image = new Bitmap(ofl.OpenFile());
                    caminhoImagem = ofl.FileName;
                    salvarFoto();
                }
                catch (Exception)
                {
                    MessageBox.Show("Falha ao carregar a imagem");
                }
            }
            ofl.Dispose();
        }

        public static byte[] ImgToByte(string camImg) //converte a imagem em []bytes
        {
            FileStream fs = new FileStream(camImg, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] imgByte = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return imgByte;
        }

        private void salvarFoto()
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string id = getId();
            byte[] imagem = ImgToByte(caminhoImagem);

            try
            {
                using (var connection = new MySqlConnection(stringConexão))
                {
                    connection.Open();

                    string query = $"call Img_Upload({id}, {imagem})";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@blobData", MySqlDbType.Blob);

                        int result = command.ExecuteNonQuery();

                    }
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

        }
        MySqlDataReader myreader;

        

        private string getId()
        {
            MySqlConnection conn = new MySqlConnection(stringConexão);

            clsConexão cls = new clsConexão();

            string id = "";
            string email = cls.getEmail();
            string query = $"Select id from tblCliente where email = '{email}' ";

            List<string> strings = new List<string>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    strings.Add(Convert.ToString(myreader["id"]));
                }

                if (strings.Count() > 0)
                {
                    id = strings[0];
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

            return id;
        }

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
            if (txtNome.Text == "" ||
                    txtCPF.Text == "" ||
                    txtDataNasc.Text == "" ||
                    txtTelefone.Text == "" ||
                    txtEmail.Text == "" ||
                    !txtEmail.Text.Contains("@")
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
                    cadastrarUser(txtEmail.Text, txtCPF.Text, txtNome.Text, sex, txtDataNasc.Text,txtTelefone.Text,txtSangue.Text,txtCondicao.Text) ;

                    MessageBox.Show("Paciente Cadastrado com sucesso", "Sucesso!!");
                    reset();
                }
            }
        }

        private void cadastrarUser(string email, string cpf, string nome, string sexo, string data, string numero, string sangue, string condicao)
        {
            DateTime dataPura = DateTime.Parse(data);
            string tempoMenor = dataPura.ToString("yyyy-MM-dd");

            sqlReturn($"call proc_cadastroPac('{email}','{cpf}','{nome}','{sexo}','{tempoMenor}','{numero}','{sangue}','{condicao}')");

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
    }
}
