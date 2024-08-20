using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskForms
{
    class clsConexão
    {
        private static string email;

        public void setEmail(string idget)
        {
            email = idget;
        }

        public string getEmail()
        {
            return email;
        }
    }
}
