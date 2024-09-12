using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskForms
{
    public partial class load : Form
    {
        public load()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Load_Load(object sender, EventArgs e)
        {
           
                playSimpleSound();
        }

        static int time = 0;


        private void Load_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.arkhamChime);
            simpleSound.Play();
        }

        private void Open_Tick(object sender, EventArgs e)
        {
            time++;
            if(time == 3)
            {
                switch (Properties.Settings.Default.remember)
                {
                    case true:
                        Principal principal = new Principal();
                        principal.ShowDialog();
                        break;
                    case false:
                        telaLog telaLog = new telaLog();
                        telaLog.ShowDialog();
                        break;
                }
            }
        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     
            {
                t1.Stop();
                Close();
            }
            else
                Opacity -= 0.05;
        }

        private void t1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
