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

namespace DeskForms
{
    public partial class CheckPendente : Form
    {
        public CheckPendente()
        {
            InitializeComponent();
        }

        string stringConexão = "server=localhost;database=hospitalar;uid=root;pwd=etesp";
        MySqlDataReader myreader;

        private void CheckPendente_Load(object sender, EventArgs e)
        {
            string query = $"select * from tblPendente where idPendente = {this.Tag.ToString()}";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string idPac = "", name = "", data = "", cnpj = "", hospital = "", desc = "";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    idPac = myreader["idPaciente"].ToString();
                    data = myreader["dataPendente"].ToString().Split(' ')[0];
                    cnpj = myreader["cnpj"].ToString();
                    desc = myreader["descPaciente"].ToString();

                }
                name = FindName(idPac);
                hospital = FindHospital(cnpj);
                txtNome.Text = name;
                txtData.Text = data;
                txtHospital.Text = hospital;
                txtDesc.Text = desc;


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

        private string FindHospital(string cnpj)
        {
            string query = $"select nomeHosp from tblHospital where cnpj = {cnpj}";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string nome = "";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    nome = myreader["nomeHosp"].ToString();
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
            return nome;
        }

        private string FindName(string id)
        {
            string query = $"select nomePaciente from tblPaciente where idPaciente = {id}";
            MySqlConnection conn = new MySqlConnection(stringConexão);
            string nome = "";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    nome = myreader["nomePaciente"].ToString();
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
            return nome;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Confirmar aprovação", "Confirmação",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                deletePendente();
                this.Close();
            }

        }

        private void deletePendente()
        {
            string query = $"delete from tblPendente where idPendente = {this.Tag}";
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
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
