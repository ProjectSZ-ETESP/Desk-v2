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
    public partial class MenuImagens : Form
    {
        public MenuImagens()
        {
            InitializeComponent();
        }

        int max = 0;
        static int index = 0;


        private void MenuImagens_Load(object sender, EventArgs e)
        {
            int sabao = int.Parse(Properties.Settings.Default.img.ToString());
            pctPerfil.Image = img.Images[sabao-1]; /*ou será que não?*/

            max = img.Images.Count;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.img = index+1;
            Properties.Settings.Default.imgPac = index + 1;
            Properties.Settings.Default.imgPacEdit = index + 1;

            this.Close();
        }

        private void btnMais_Click(object sender, EventArgs e)
        {
            if(index < max-1)
            {
                index++;
            }
            changeImg(index);
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {
            if(index > 0)
            {
                index--;
            }
            changeImg(index);
        }

        private void changeImg(int i)
        {
            pctPerfil.Image = img.Images[i];        
        }
    }
}
