using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalPrograII
{
    class Usuario
    {
        private string nombre;
        private string tipo;
        private string user;
        private string password;
        public Usuario()
        {

        }
        public Usuario(string nombre,string tipo,string user,string password)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.user = user;
            this.password = password;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }
}
