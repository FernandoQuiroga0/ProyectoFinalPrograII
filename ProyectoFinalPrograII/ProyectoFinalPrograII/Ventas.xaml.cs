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
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        string pathName = @"/registroProducto.txt";
        string aux = @"/registroAux.txt";
        string sum = @"/sumVenta.txt";
        string reporte = @"/ReporteVenta.txt";

        public Ventas()
        {
            InitializeComponent();
            MostrarProducto();
            MostrarReporte();
        }

        private void BtnVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text != "" && txtNit.Text != "")
                {
                    if (txtTotal.Text != "")
                    {
                        string nit = txtNit.Text;
                        string nombre = txtNombre.Text;
                        string hora = DateTime.Now.ToString("hh:mm:ss");
                        string fecha = DateTime.Now.ToLongDateString();
                        string totalBs = txtTotal.Text;
                        string[] datosProductos;
                        StreamReader tuberiaLectura = File.OpenText(pathName);
                        string linea = tuberiaLectura.ReadLine();
                        while (linea != null)
                        {
                            datosProductos = linea.Split(',');
                            StreamWriter tuberiaEscritura = File.AppendText(reporte);
                            tuberiaEscritura.WriteLine(nit + "," + nombre + "," + hora + " " + fecha + "," + totalBs);
                            tuberiaEscritura.Close();
                            linea = tuberiaLectura.ReadLine();
                            MostrarReporte();
                        }
                        txtID.Text = " ";
                        txtCantidad.Text = " ";
                        txtNit.Text = "";
                        txtNombre.Text = " ";
                        txtTotal.Text = " ";
                        File.Delete(sum);
                    }
                    else
                    {
                        MessageBox.Show("No se realizó ninguna venta");
                    }
                }
                else
                {
                    MessageBox.Show("Se necesita el Nit/CI y el Nombre/Razón Social");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error " + ex.Message);
            }

        }

        private void BtnSumar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                if (txtID.Text != "")
                {

                    int id = int.Parse(txtID.Text);
                    double precioUnitario = 0;
                    string nombre;
                    double cantidad = double.Parse(txtCantidad.Text);
                    double total;
                    double num;
                    double suma = 0;
                    if (VerificarID(id) || VerificarCodigo(id))
                    {
                        string[] datosProductos;
                        StreamReader tuberiaLectura = File.OpenText(pathName);
                        string linea = tuberiaLectura.ReadLine();
                        while (linea != null)
                        {
                            datosProductos = linea.Split(',');
                            if (datosProductos[0] == id.ToString() || datosProductos[5] == id.ToString())
                            {
                                if (double.Parse(datosProductos[4]) >= cantidad)
                                {
                                    id = int.Parse(datosProductos[0]);
                                    num = double.Parse(datosProductos[4]);
                                    num = num - cantidad;
                                    datosProductos[4] = num.ToString();                                   
                                    nombre = datosProductos[1];
                                    precioUnitario = double.Parse(datosProductos[3]);
                                    total = cantidad * precioUnitario;

                                    StreamWriter tuberiaEscritura = File.AppendText(sum);
                                    tuberiaEscritura.WriteLine(id + "," + nombre + "," + precioUnitario + "," + cantidad + "," + total);
                                    tuberiaEscritura.Close();
                                    txtID.Text = " ";
                                    txtCantidad.Text = " ";
                                    

                                    MostrarProducto();
                                    MostrarSuma();
                                    
                                }
                                else
                                {
                                    MessageBox.Show("La cantidad de productos excede la del registro");
                                }
                            }
                            else
                            {

                            }
                            linea = tuberiaLectura.ReadLine();
                        }
                        tuberiaLectura.Close();
                        string[] datosSuma;
                        StreamReader tuberiaSuma = File.OpenText(sum);
                        string lin = tuberiaSuma.ReadLine();
                        while (lin != null)
                        {
                            datosSuma = lin.Split(',');
                            num = double.Parse(datosSuma[4]);
                            suma = num + suma;
                            lin = tuberiaSuma.ReadLine();
                        }
                        txtTotal.Text = suma.ToString();
                        tuberiaSuma.Close();
                    }
                    else
                    {
                        MessageBox.Show("El Producto no existe");
                    }
                }
                else
                {
                    MessageBox.Show("El ID es necesario");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error " + ex.Message);
            }
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
                MessageBox.Show("No se pudó mostrar los Productos " + ex.Message);
                Console.WriteLine(ex);
            }
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
        private bool VerificarCodigo(int id)
        {
            bool respuesta = false;
            string[] datosProducto;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosProducto = linea.Split(',');
                if (datosProducto[5] == id.ToString())
                {
                    respuesta = true;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }



        private void MostrarSuma()
        {
            try
            {
                SumVenta sumVenta;
                List<SumVenta> productos = new List<SumVenta>();
                string id, nombre, precioUnitario, cantidad, total;
                string[] datosProducto;
                if (File.Exists(sum))
                {
                    StreamReader tuberiaLectura = File.OpenText(sum);
                    string fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        datosProducto = fila.Split(',');
                        id = datosProducto[0];
                        nombre = datosProducto[1];
                        precioUnitario = datosProducto[2];
                        cantidad = datosProducto[3];
                        total = datosProducto[4];
                        sumVenta = new SumVenta(id, nombre, precioUnitario, cantidad, total);
                        productos.Add(sumVenta);
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();

                    dgSuma.ItemsSource = productos;
                }
                else
                {
                    File.CreateText(sum);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudó mostrar los productos Suma " + ex.Message);
                Console.WriteLine(ex);
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {


                VentanaPrincipal mainWindow = new VentanaPrincipal();
                this.Hide();
                mainWindow.ShowDialog();
                this.Close();
                File.Delete(sum);
            
        }

        private void MostrarReporte()
        {
            try
            {
                ListaVenta listaVenta;
                List<ListaVenta> productos = new List<ListaVenta>();
                string nit, nombre, hora, fecha, total;
                string[] datosProducto;
                if (File.Exists(reporte))
                {
                    StreamReader tuberiaLectura = File.OpenText(reporte);
                    string fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        datosProducto = fila.Split(',');
                        nit = datosProducto[0];
                        nombre = datosProducto[1];
                        hora = datosProducto[2];
                        fecha = datosProducto[3];
                        total = datosProducto[4];
                        listaVenta = new ListaVenta(nit, nombre, hora, fecha, total);
                        productos.Add(listaVenta);
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgReporte.ItemsSource = productos;
                }
                else
                {
                    File.CreateText(reporte);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudó mostrar los productos Reporte " + ex.Message);
                Console.WriteLine(ex);
            }

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(sum);
        }
    }
}
