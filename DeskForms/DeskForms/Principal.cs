using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            //Garante que o forms se feche por completo
        }

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

        private void PfpLateral_Click(object sender, EventArgs e)
        {
            abasPrincipal.SelectedTab = tabPerfil;

        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Abrir Imagem";
            dialog.Filter = "Image Files (*.bmp; *.jpg; *.jpeg; *.png; *.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    Image imagem = Image.FromFile(dialog.FileName);


                    imagePerfil.BackgroundImage = imagem;
                    pfpLateral.BackgroundImage = imagem;


                    dialog.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao carregar a imagem: " + ex.Message);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

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
                    MessageBox.Show("Paciente Cadastrado com sucesso", "Sucesso!!");
                    reset();
                }
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            loadConfig();
            maskCPF();
            reset();
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

        private void BtnForum_Click(object sender, EventArgs e)
        {

        }

        private void BtnConsulta_Click(object sender, EventArgs e)
        {

        }

        private void CboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox caixa = (sender as ComboBox);

            string theme = caixa.Text;

            changeTheme(theme);
        }

        private void PanelNav_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
