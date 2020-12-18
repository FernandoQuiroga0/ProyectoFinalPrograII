using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalPrograII
{
    class ListaVenta
    {
        private string nit;
        private string nombre;
        private string hora;
        private string fecha;
        private string total;

        public ListaVenta()
        {

        }
        public ListaVenta(string nit,string nombre,string hora,string fecha,string total)
        {
            this.nit = nit;
            this.nombre = nombre;
            this.hora = hora;
            this.fecha = fecha;
            this.total = total;

        }
        public string Nit_CI
        {
            get { return nit; }
            set { nit = value; }
        }
        public string Nombre_RazónSocial
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Hora
        {
            get { return hora; }
            set { hora = value; }
        }
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public string TotalBs
        {
            get { return total; }
            set { total = value; }
        }

    }
}
