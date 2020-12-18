using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalPrograII
{
    class SumVenta
    {
        private string id;
        private string nombre;
        private string precioUnitario;
        private string cantidad;
        private string total;

        public SumVenta()
        {

        }
        public SumVenta(string id, string nombre, string precioUnitario, string cantidad, string total)
        {
            this.id = id;
            this.nombre = nombre;
            this.precioUnitario = precioUnitario;
            this.cantidad = cantidad;
            this.total = total;
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string PrecioUnitarioBs
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }
        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public string TotalBs
        {
            get { return total; }
            set { total = value; }
        }
    }
}
