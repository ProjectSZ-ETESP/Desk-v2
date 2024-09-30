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
            pctPerfil.Image = img.Images[0];
            max = img.Images.Count;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.img = index+1;
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
