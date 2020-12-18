using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalPrograII
{
    class Producto
    {
        private string id;
        private string nombreProducto;
        private string precioCompra;
        private string precioVenta;
        private string cantidad;
        private string codigoBarra;
        public Producto()
        {

        }
        public Producto(string id, string nombreProducto, string precioCompra, string precioVenta, string cantidad, string codigoBarra)
        {
            this.id = id;
            this.nombreProducto = nombreProducto;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
            this.cantidad = cantidad;
            this.codigoBarra = codigoBarra;
        }
        public Producto(string i, string n)
        {
            id = i;
            nombreProducto = n;
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Productos
        {
            get { return nombreProducto; }
            set { nombreProducto = value; }
        }
        public string PrecioCompraBs
        {
            get { return precioCompra; }
            set { precioCompra = value; }
        }
        public string PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }
        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public string CodigoBarra
        {
            get { return codigoBarra; }
            set { codigoBarra = value; }
        }
    }
}
