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
    public partial class load : Form
    {
        public load()
        {
            InitializeComponent();

            //this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Load_Load(object sender, EventArgs e)
        {

        }

        private void Load_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(time == 2)
            {
                enginePoweringDown(sender, e);
            }
        }

        private void enginePoweringDown(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //cancel the event so the form won't be closed

            t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
            t1.Start();

            if (Opacity == 0)
            {
                //if the form is completly transparent

                Principal ld = new Principal();
                ld.ShowDialog();
                e.Cancel = false;
            }   //resume the event - the program can be closed

            
        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.1;
        }

        static int time = 0;

        private void Open_Tick(object sender, EventArgs e)
        {
            time++;            
        }
    }
}
