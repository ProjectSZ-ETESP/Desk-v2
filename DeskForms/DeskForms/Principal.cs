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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Droitech.TextFont;

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

            kurtCorners(pfpLateral);
            kurtCorners(imagePerfil);
            kurtCorners(pfpPaciente);
            kurtCorners(editEditarrs);
            kurtCorners(pcbEdit);
            kurtCorners(btnEditRegistro);
            kurtCorners(editEditarrs);

            UpdateRefresh();

        }

        private void kurtCorners(PictureBox picture)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picture.Width - 3, picture.Height - 3);
            Region rg = new Region(gp);
            picture.Region = rg;
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

                    lblNome.Text = str;

                    if (index >= 0)
                    {
                        string result = str.Substring(0, index);
                        lblNome.Text = result;
                    }

                    lblNomeCompleto.Text = myreader["nomeFuncionario"].ToString();
                    lblTelefone.Text = myreader["foneFuncionario"].ToString();
                    lblEmail.Text = Properties.Settings.Default.user.ToString();
                    hosp = myreader["cnpj"].ToString();
                    try
                    {
                        fto = int.Parse(myreader["fotoFuncionario"].ToString());
                    }
                    catch { }
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
            txtCampo_Pac.Text = "";
            txtCampo_Pac.Focus();
        }

        private void btnForum_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabRegistroHosp;

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
                    img = Properties.Resources.perfil1;
                    break;

                case 2:
                    img = Properties.Resources.perfil2;

                    break;

                case 3:
                    img = Properties.Resources.perfil3;

                    break;

                case 4:
                    img = Properties.Resources.perfil4;

                    break;

                case 5:
                    img = Properties.Resources.perfil5;

                    break;

                case 6:
                    img = Properties.Resources.perfil6;

                    break;

                case 7:
                    img = Properties.Resources.perfil7;

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

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" ||
                    txtNome.Text == "" ||
                    txtCPF.Text == "" ||
                    txtDataNasc.Text == "" ||
                    txtTelefone.Text == "" ||
                    Properties.Settings.Default.imgPac == 0
                    )
            {
                MessageBox.Show("Dados inválidos ou não suficientes", "Erro!!");
            }
            else
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
                    cadastrarUser(txtID.Text, txtCPF.Text, txtNome.Text, sex, txtDataNasc.Text, txtTelefone.Text, Properties.Settings.Default.imgPac);
                }

            }
        }

        private void cadastrarUser(string id, string cpf, string nome, string sexo, string data, string numero, int imgPac)
        {
            try
            {
                DateTime dataPura = DateTime.Parse(data);
                string tempoMenor = dataPura.ToString("yyyy-MM-dd");
                try
                {
                    sqlReturn($"call proc_cadastroPac('{id}','{cpf}', '{nome}', '{sexo}', '{tempoMenor}', '{numero}')");
                    MessageBox.Show("Paciente Cadastrado com sucesso", "Sucesso!!");
                    reset();
                }
                catch
                {
                    throw new Exception();
                }

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
            rdoMasc.Checked = true;
            pfpPaciente.BackgroundImage = null;
            txtNome.Focus();
        }

        private void changeTheme(String version)
        {
            Color darkGreen = System.Drawing.ColorTranslator.FromHtml("#161817");
            Color lightGreen = System.Drawing.ColorTranslator.FromHtml("#327960");
            switch (version)
            {

                case "Tema Escuro":

                    foreach (TabPage tab in abasPrincipal.TabPages)
                    {
                        tab.BackColor = Color.Black;

                        foreach (Control ctrl in tab.Controls)
                        {

                            if (!(ctrl.Tag == null))
                            {
                                switch (ctrl.Tag.ToString())
                                {
                                    case "label":
                                        ctrl.ForeColor = Color.White;
                                        ctrl.BackColor = Color.Black;
                                    break;
                                    case "title":
                                        ctrl.ForeColor = lightGreen;
                                    break;
                                }
                            }
                            panelNav.BackColor = darkGreen;
                            this.BackColor = darkGreen;

                            btnLogout.BackgroundImage = Properties.Resources.logout;
                            btnBack.BackgroundImage = Properties.Resources.arrowWhite;
                            Properties.Settings.Default.theme = "Tema Escuro";
                            cboColor.SelectedIndex = 2;
                        }
                    }

                    break;
                case "Tema Claro":

                    foreach (TabPage tab in abasPrincipal.TabPages)
                    {
                        tab.BackColor = Color.White;

                        foreach (Control ctrl in tab.Controls)
                        {
                            if (!(ctrl.Tag == null))
                            {
                                switch (ctrl.Tag.ToString())
                                {
                                    case "label":
                                        ctrl.ForeColor = Color.Black;
                                        ctrl.BackColor = Color.White;
                                        break;
                                    case "title":
                                        ctrl.ForeColor = darkGreen;
                                        break;
                                }
                            }
                            panelNav.BackColor = lightGreen;
                            this.BackColor = lightGreen;
                            btnLogout.BackgroundImage = Properties.Resources.logoutClear;
                            btnBack.BackgroundImage = Properties.Resources.arrowBlack;
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
            txtCPF.Mask = "000.000.000-00";
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

        private void FindUnique()
        {
            
                UpdateEdicao($"select * from tblPaciente where cpfPaciente = '{txtCampo_Pac.Text}'");
            
            
        }

        private void BtnSearchPac_Click(object sender, EventArgs e)
        {
            FindUnique();
        }


        private void UpdateEdicao(String query)        {

            MySqlConnection conn = new MySqlConnection(stringConexão);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                string idCli = "0";
                string cpf = "", nomeCompleto = "", data = "", foto = "", telefone = "", sex = "", condicao = "", sangue = "";
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    idCli = myreader["idPaciente"].ToString();
                    if (idCli != "0")
                    {
                        cpf = myreader["cpfPaciente"].ToString();
                        nomeCompleto = myreader["nomePaciente"].ToString();
                        data = myreader["dataNascPaciente"].ToString();
                        foto = myreader["fotoPaciente"].ToString();
                        sex = myreader["sexoPaciente"].ToString();
                        telefone = myreader["fonePaciente"].ToString();
                        condicao = myreader["condicoesMedicas"].ToString();
                        sangue = myreader["tipoSanguineo"].ToString();
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
                    editCondicao.Text = condicao;
                    editSangue.Text = sangue;
                    pcbEdit.BackgroundImage = getImg(int.Parse(foto));
                    if (sex == "M") { rdoEdit_M.Checked = true; }
                    else { rdoEdit_F.Checked = true; }
                }
                else
                {
                    MessageBox.Show($"Esse CPF não está cadastrado em nosso sistema", "uh oh");
                    txtCampo_Pac.Text = "";
                    txtCampo_Pac.Focus();
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
            

            if (e.KeyData == Keys.Enter)
            {
                FindUnique();
            }
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
            DateTime dataPura = DateTime.Parse(editData.Text);
            string tempoMenor = dataPura.ToString("yyyy-MM-dd");
            string[] tiposSanguineos = new string[]
            {
                "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
            };            

            if (rdoEdit_M.Checked) { sex = "M"; }
            else { sex = "F"; }

            try
            {
                if (!tiposSanguineos.Contains(editSangue.Text))
                { throw new Exception("Esse tipo sanguíneo NÃO existe!!"); }

                foreach(Control ctrl in tabEdição.Controls)
                {
                    if(ctrl is TextBox)
                    {
                        if (ctrl.Text == "")
                        {
                            throw new Exception($"{ctrl.Name} está vazio");
                        }
                    }                    
                }

                sqlReturn($"call proc_editPac({int.Parse(editID.Text)},'{editNome.Text}','{editEmail.Text}','{tempoMenor}','{sex}','{editTelefone.Text}','{editCPF.Text}','{Properties.Settings.Default.imgPacEdit}')");
                MessageBox.Show("Dados do paciente atualizados", "Sucesso!!");
            }
            catch(Exception ex){
                MessageBox.Show($"Erro!!\n\n{ex.Message}\n Mudanças não salvas serão apagadas", "Uh Oh");
                UpdateEdicao($"select * from tblPaciente where cpfPaciente = '{txtCampo_Pac.Text}'");
            }
        }

        private void btnExcluirFicha_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Você deseja MESMO deletar essa ficha\n(Ação Irreversível)", "Registro", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    sqlVoid($"delete from tblNotificacao where idUsuario = {editID.Text}");
                    sqlVoid($"delete from tblPendente where idPaciente = (select idUsuario from tblPaciente where idUsuario = {editID.Text})");
                    sqlVoid($"delete from tblProntuario where idConsulta = (select idUsuario from tblPaciente where idUsuario = {editID.Text})");
                    
                    sqlVoid($"delete from tblConsulta where idPaciente = (select idUsuario from tblPaciente where idUsuario = {editID.Text})");
                    sqlVoid($"delete from tblPaciente where idUsuario = {editID.Text}");

                    MessageBox.Show("Dados do paciente apagados", "BANIDO!!");
                    foreach(Control control in tabEdição.Controls)
                    {
                        if(control is TextBox || control is MaskedTextBox)
                        {
                            control.Text = "";
                            rdoEdit_M.Checked = true;
                            editID.Focus();
                        }
                    }
                }
                catch { }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabRegistroHosp;

        }

        private void btnRegFunc_Click(object sender, EventArgs e)
        {

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabAcesso;
            txtCampo_Pac.Focus();
        }

        private void telHospital_KeyDown(object sender, KeyEventArgs e)
        {
            telHospital.Mask = "(00) 0000-0000";
        }

        private void cnpjHospital_KeyDown(object sender, KeyEventArgs e)
        {
            cnpjHospital.Mask = "00.000.000/0000-00";
        }

        private readonly List<string> diasDaSemana = new List<string>
        {
            "Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"
        };

        private void btnRegistroHospital_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctrl in tabRegistroHosp.Controls)
                {
                    if (ctrl is TextBox || ctrl is MaskedTextBox)
                    {
                        if (ctrl.Text == "")
                        {
                            throw new Exception($"{ctrl.Name} está vazio!!");
                        }
                    }
                }

                List<int> checkedIndices = new List<int>();

                foreach (int index in checkListDias.CheckedIndices)
                {
                    checkedIndices.Add(index);
                }
                checkedIndices.Sort();
                string result = GetConsecutiveDays(checkedIndices);

                string horasA = horaAbertura.Text;
                string horasF = horaFechamento.Text;
                string tudo = $"{result}:{horasA}-{horasF}";


                string query = $"Insert into tblHospital values ('{cnpjHospital.Text}','{nomeHospital.Text}','{nomeDiretor.Text}','{descHospital.Text}','{emailHospital.Text}','{enderecoHospital.Text}','{tudo}','{telHospital.Text}')";

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

                MessageBox.Show("Hospital Cadastrado!!", "Nicee");

                foreach (Control ctrl in tabRegistroHosp.Controls)
                {
                    if (ctrl is TextBox || ctrl is MaskedTextBox)
                    {
                        ctrl.Text = "";
                    }
                }
                for(int i = 0; i < checkListDias.Items.Count; i++)
                {
                    checkListDias.SetItemChecked(i, false);
                }
                horaAbertura.Text = "00:00";
                horaFechamento.Text = "00:00";
                cnpjHospital.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"erro!!\n{ex.Message}","vishh");
            }
            

        }

        private string GetConsecutiveDays(List<int> indices)
        {
            if (indices.Count == 0)
            {
                return "Nenhum dia selecionado";
            }

            List<string> result = new List<string>();

            int start = indices[0];
            int end = indices[0];

            for (int i = 1; i < indices.Count; i++)
            {
                if (indices[i] == end + 1)
                {
                    end = indices[i];
                }
                else
                {
                    result.Add(FormatDays(start, end));
                    start = indices[i];
                    end = indices[i];
                }
            }

            result.Add(FormatDays(start, end));

            return string.Join(", ", result);
        }

        private string FormatDays(int start, int end)
        {
            if (start == end)
            {
                return diasDaSemana[start];
            }
            else
            {
                return $"{diasDaSemana[start]} à {diasDaSemana[end]}";
            }
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void checkCPF_Click(object sender, EventArgs e)
        {
        }

        private void checkCNPJ_Click(object sender, EventArgs e)
        {
        }

        private void checkChanged(object sender, EventArgs e)
        {
            txtCampo_Pac.Text = "";
            txtCampo_Pac.Focus();
        }

        private void btnDeleteHospital_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateHospital_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabHome;
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateRefresh();
        }

        private void UpdateRefresh()
        {
            int quant = int.Parse(Count());

            if (quant == 0)
            {
                flwPendentes.Controls.Clear();
                flwPendentes.AutoScroll = false;

                Panel pnl = new Panel();
                pnl.Size = flwPendentes.Size;
                pnl.BackColor = Color.Red;
                Label lbl = new Label();
                lbl.Text = "Não há nada de \nnovo pra ver aqui ^^";
                lbl.Size = pnl.Size;
                lbl.Font = new Font("Poppins Medium", 16, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;

                pnl.Controls.Add(lbl);

                flwPendentes.Controls.Add(pnl);

                lblAgendamentos.Text = $"{quant} Agendamentos Pendentes";
            }

            else
            {
                lblAgendamentos.Text = $"{quant} Agendamentos Pendentes";
                flwPendentes.Controls.Clear();

                for (int i = 0; i <= quant + 1; i++)
                {
                    addPendentes(i);
                }

            }
        }

        private void addPendentes(int id)
        {
            string query = $"Select * from tblPendente where idPendente = {id}";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string nome = "", hora = "", hospital = "", idPac = "";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    idPac = myreader["idPaciente"].ToString();
                    hospital = myreader["dataPendente"].ToString();
                    hora = myreader["horaPendente"].ToString();
                }

                nome = findName(idPac);

                // Criação do painel
                Panel pnl = new Panel
                {
                    Tag = idPac,
                    Size = new Size(300, 90),
                    BackColor = Color.White,
                    Margin = new Padding(5)
                    
                    // Adiciona espaçamento entre os painéis no FlowLayoutPanel
                };

                // Criação da label para o nome do paciente
                Label lblPaciente = new Label
                {
                    Text = $"Nome do Paciente: {nome}", // Exemplo estático, você pode tornar dinâmico
                    Size = new Size(300, 20),
                    Font = new Font("Poppins Medium", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(10, 10) // Ajusta a posição dentro do painel
                };

                // Criação da label para o hospital
                Label lblHospital = new Label
                {
                    Text = $"Data: {hospital.Split(' ')[0]}", // Exemplo estático, pode ser dinâmico
                    Size = new Size(300, 20),
                    Font = new Font("Poppins Medium", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(10, 35)
                };

                // Criação da label para a hora
                Label lblHora = new Label
                {
                    Text = $"Hora: {hora} ", // Hora atual
                    Size = new Size(300, 20),
                    Font = new Font("Poppins Light", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(10, 60)
                };


                pnl.Click += (sender, e) => Panel_Click(sender, e, idPac);
                lblPaciente.Click += (sender, e) => Panel_Click(sender, e, idPac);
                lblHospital.Click += (sender, e) => Panel_Click(sender, e, idPac);
                lblHora.Click += (sender, e) => Panel_Click(sender, e, idPac);


                // Adicionando labels ao painel
                pnl.Controls.Add(lblPaciente);
                pnl.Controls.Add(lblHospital);
                pnl.Controls.Add(lblHora);

                // Adicionando o painel ao FlowLayoutPanel
                if(nome != "")
                {
                    flwPendentes.Controls.Add(pnl);
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

        private void Panel_Click(object sender, EventArgs e, string idPaciente)
        {
            CheckPendente ckp = new CheckPendente();
            ckp.Tag = idPaciente;
            ckp.ShowDialog();

            int quant = int.Parse(Count());

            if (quant == 0)
            {
                flwPendentes.Controls.Clear();
                flwPendentes.AutoScroll = false;

                Panel pnl = new Panel();
                pnl.Size = flwPendentes.Size;
                pnl.BackColor = Color.Beige;
                Label lbl = new Label();
                lbl.Text = "Não há nada de \nnovo pra ver aqui ^^";
                lbl.Size = pnl.Size;
                lbl.Font = new Font("Poppins Medium", 16, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;

                pnl.Controls.Add(lbl);

                flwPendentes.Controls.Add(pnl);

                lblAgendamentos.Text = $"{quant} Agendamentos Pendentes";
            }

            else
            {
                lblAgendamentos.Text = $"{quant} Agendamentos Pendentes";
                flwPendentes.Controls.Clear();

                for (int i = 0; i <= quant + 1; i++)
                {
                    addPendentes(i);
                }

            }

        }

        public string findName(string id)
        {

            string query = $"Select nomePaciente from tblPaciente where idPaciente = {id}";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string name = "";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if(id != null || id != "")
                {
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        name = myreader["nomePaciente"].ToString();
                    }
                }                
                conn.Close();
            }
            catch (Exception ep)
            {
            }
            finally
            {
                conn.Close();
            }

            return name;
        }

        private string Count()
        {
            string query = $"Select count(*) as quant from tblPendente";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string count = "";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    count = myreader["quant"].ToString();
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

            return count;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = $"";
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

        private void tabHome_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
