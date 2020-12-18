using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFinalPrograII
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        string pathName = @"/registroProducto.txt";
        string pathNameAuxiliar = @"registroProductoAux.txt";
        public Productos()
        {
            InitializeComponent();
            MostrarProducto();
        }


        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = txtID.Text;
                if (id != "")
                {
                    if (VerificarProducto(txtNombre.Text) ){ 
                        string nombre, precioP, precio, cantidad, codigo;
                        string linea;
                        string[] datosProducto;
                        char separador = ',';
                        bool modificar = false;
                        StreamReader tuberiaLectura = File.OpenText(pathName);
                        StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                        linea = tuberiaLectura.ReadLine();
                        while (linea != null)
                        {
                            datosProducto = linea.Split(separador);
                            if (id != datosProducto[0])
                            {
                                tuberiaEscritura.WriteLine(linea);
                            }
                            else
                            {
                                modificar = true;

                                if (txtNombre.Text != "")
                                {
                                    nombre = txtNombre.Text;
                                }
                                else
                                {
                                    nombre = datosProducto[1];
                                }
                                if (txtPrecioP.Text != "")
                                {
                                    precioP = txtPrecioP.Text;
                                }
                                else
                                {
                                    precioP = datosProducto[2];
                                }
                                if (txtPrecio.Text != "")
                                {
                                    precio = txtPrecio.Text;
                                }
                                else
                                {
                                    precio = datosProducto[3];
                                }
                                if (txtCantidad.Text != "")
                                {
                                    cantidad = txtCantidad.Text;
                                }
                                else
                                {
                                    cantidad = datosProducto[4];
                                }
                                if (txtCodigo.Text != "")
                                {
                                    codigo = txtCodigo.Text;
                                }
                                else
                                {
                                    codigo = datosProducto[5];
                                }
                                tuberiaEscritura.WriteLine(id + "," + nombre + "," + precioP + "," + precio + "," + cantidad + "," + codigo);
                            }
                            linea = tuberiaLectura.ReadLine();
                        }
                        tuberiaEscritura.Close();
                        tuberiaLectura.Close();
                        File.Delete(pathName);
                        File.Move(pathNameAuxiliar, pathName);
                        File.Delete(pathNameAuxiliar);
                        if (modificar)
                        {
                                MessageBox.Show("El Producto se modificó con éxito");
                                txtID.Text = "";
                                txtNombre.Text = "";
                                txtPrecioP.Text = "";
                                txtPrecio.Text = "";
                                txtCantidad.Text = "";
                                txtCodigo.Text = "";
                            MostrarProducto();
                        }
                        else
                        {
                            MessageBox.Show("El Producto no existe");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El nombre del producto ya existe, o algún dato es inválido");
                    }
                    
                }
                else
                {
                    MessageBox.Show("El ID es necesario");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error" + ex.Message);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (File.Exists(pathName))
                {

                    if (txtID.Text == " " || txtCantidad.Text == "" || txtNombre.Text == " " || txtPrecioP.Text == " " || txtPrecio.Text == "")
                    {
                        MessageBox.Show("Necesario:ID,Producto,Cantidad,Precio Compra y Precio Venta");
                    }
                    else
                    {
                        string producto = txtNombre.Text.Trim();
                        int cantidad = int.Parse(txtCantidad.Text.Trim());
                        double precio = double.Parse(txtPrecio.Text.Trim());
                        double precioC = double.Parse(txtPrecioP.Text.Trim());
                        int id = int.Parse(txtID.Text);
                        string codigo;
                        precio = Math.Round(precio, 2);
                        precioC = Math.Round(precioC, 2);
                        if (txtCodigo.Text == " ")
                        {
                            codigo = "";
                        }
                        else
                        {
                            codigo = txtCodigo.Text;
                        }
                        if (VerificarProducto(producto) )
                        {
                            if (VerificarIDProducto(id) )
                            {
                                if (VerificarCantidad(cantidad) && VerificarPrecio(precio, precioC))
                                {

                                    StreamWriter tuberiaEscritura = File.AppendText(pathName);
                                    tuberiaEscritura.WriteLine(id + "," + producto + "," + precioC + "," + precio + "," + cantidad + "," + codigo);
                                    tuberiaEscritura.Close();
                                    MessageBox.Show("Producto creado con exito");
                                    txtID.Text = " ";
                                    txtNombre.Text = " ";
                                    txtCantidad.Text = " ";
                                    txtPrecioP.Text = " ";
                                    txtCodigo.Text = " ";
                                    txtPrecio.Text = " ";
                                    MostrarProducto();
                                }

                                else
                                {
                                    MessageBox.Show("Los números de:Cantidad y/o Precio no son válidos");
                                }
                            }
                            else
                            {
                                MessageBox.Show("La ID ya fue utilizada");
                            }

                        }
                        else
                        {
                            MessageBox.Show("El producto ya existe");
                        }
                    }
                }
                else
                {
                    File.CreateText(pathName);
                    MessageBox.Show("Archivo creado, intente nuevamente");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error " + ex.Message);
            }

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = txtID.Text;
                if (id != "")
                {
                    string linea;
                    string[] datosCliente;
                    char separador = ',';
                    bool eliminado = false;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                    linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosCliente = linea.Split(separador);
                        if (id != datosCliente[0])
                        {
                            tuberiaEscritura.WriteLine(linea);
                        }
                        else
                        {
                            eliminado = true;
                        }
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaEscritura.Close();
                    tuberiaLectura.Close();


                    File.Delete(pathName);
                    File.Move(pathNameAuxiliar, pathName);
                    File.Delete(pathNameAuxiliar);
                    if (eliminado)
                    {
                        MessageBox.Show("El Producto se elimino con exito");

                        MostrarProducto();
                    }
                    else
                    {
                        MessageBox.Show("El Producto no existe");
                    }
                    txtID.Text = "";
                    txtNombre.Text = "";
                    txtPrecioP.Text = "";
                    txtPrecio.Text = "";
                    txtCantidad.Text = "";
                    txtCodigo.Text = "";
                }
                else
                {
                    MessageBox.Show("No se pemite vacio");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error" + ex.Message);
            }
        }
        private bool VerificarProducto(string producto)
        {
            bool respuesta = true;
            string[] datosProducto;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosProducto = linea.Split(',');
                if (datosProducto[1] == producto)
                {
                    respuesta = false;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
        private bool VerificarIDProducto(int id)
        {
            bool respuesta = true;
            string[] datosProducto;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosProducto = linea.Split(',');
                if (datosProducto[0] == id.ToString())
                {
                    respuesta = false;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
        private bool VerificarID(int id)
        {
            bool respuesta = false;
            string[] datosProducto;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosProducto = linea.Split(',');
                if (datosProducto[0] == id.ToString())
                {
                    respuesta = true;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
        private bool VerificarCantidad(int cantidad)
        {
            bool respuesta = false;
            if (cantidad >= 0)
            {
                respuesta = true;
            }
            return respuesta;
        }
        private bool VerificarPrecio(double precio, double precioC)
        {
            bool respuesta = false;
            if (precio > 0 && precioC > 0)
            {
                respuesta = true;
            }
            else
            {
                MessageBox.Show("El precio es inválido");
            }

            return respuesta;
        }
        private void MostrarProducto()
        {
            try
            {
                Producto producto;
                List<Producto> productos = new List<Producto>();
                string id, nombreProducto, precioCompra, precioVenta, cantidad, codigoBarra;
                string[] datosProducto;
                if (File.Exists(pathName))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    string fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        datosProducto = fila.Split(',');
                        id = datosProducto[0];
                        nombreProducto = datosProducto[1];
                        precioCompra = datosProducto[2];
                        precioVenta = datosProducto[3];
                        cantidad = datosProducto[4];
                        codigoBarra = datosProducto[5];
                        producto = new Producto(id, nombreProducto, precioCompra, precioVenta, cantidad, codigoBarra);
                        productos.Add(producto);
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgLista.ItemsSource = productos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudó mostrar los productos " + ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

                VentanaPrincipal mainWindow = new VentanaPrincipal();
                this.Hide();
                mainWindow.ShowDialog();
                this.Close();
            
        }
        private bool VerificarCodigo(string codigo)
        {
            bool respuesta = false;
            string[] datosProducto;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosProducto = linea.Split(',');
                if (datosProducto[5] == codigo)
                {
                    respuesta = true;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
    }
}
