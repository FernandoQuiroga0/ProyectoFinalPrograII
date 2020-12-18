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
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        string Login = @"/login.txt";
        string LoginAux = @"/loginAux.txt";
        public Usuarios()
        {
            InitializeComponent();
            MostrarUsuario();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string user = txtUser.Text;
                if (user != "")
                {
                    string linea;
                    string[] datosCliente;
                    char separador = ',';
                    bool eliminado = false;
                    StreamReader tuberiaLectura = File.OpenText(Login);
                    StreamWriter tuberiaEscritura = File.AppendText(LoginAux);
                    linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosCliente = linea.Split(separador);
                        if (user != datosCliente[2])
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
                    File.Delete(Login);
                    File.Move(LoginAux, Login);
                    File.Delete(LoginAux);
                    if (eliminado)
                    {
                        MessageBox.Show("El Usuario se elimino con exito");
                        txtNombre.Text = " ";
                        txtTipo.Text = " ";
                        txtUser.Text = " ";
                        txtPassword.Text = " ";
                        MostrarUsuario();
                    }
                    else
                    {
                        MessageBox.Show("El Usuario no existe");
                    }

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

        private void BtnUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (File.Exists(Login))
                {

                    if (txtUser.Text == " " || txtTipo.Text == "" || txtNombre.Text == " " || txtPassword.Text == " ")
                    {
                        MessageBox.Show("Falta llenar un dato");
                    }
                    else
                    {
                        
                        string nombre = txtNombre.Text.Trim();
                        string tipo = txtTipo.Text.Trim();
                        string user = txtUser.Text.ToLower().Trim();
                        string password = txtPassword.Text;
                       if (VerificarUser())
                        {
                            StreamWriter tuberiaEscritura = File.AppendText(Login);
                            tuberiaEscritura.WriteLine(nombre + "," + tipo + "," + user + "," + password);
                            tuberiaEscritura.Close();
                            MessageBox.Show("Usuario creado con exito");
                            txtNombre.Text = " ";
                            txtTipo.Text = " ";
                            txtUser.Text = " ";
                            txtPassword.Text = " ";
                            MostrarUsuario();

                        }
                        else
                        {
                            MessageBox.Show("El user ya existe");
                        }

                    }
                }

                else
                {
                    File.CreateText(Login);
                    MessageBox.Show("Archivo creado, intente nuevamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error " + ex.Message);
            }
        }
        private void MostrarUsuario()
        {
            try
            {
                Usuario usuario;
                List<Usuario> usuarios = new List<Usuario>();
                string id, nombreProducto, precioCompra, precioVenta;
                string[] datosUsuarios;
                if (File.Exists(Login))
                {
                    StreamReader tuberiaLectura = File.OpenText(Login);
                    string fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        datosUsuarios = fila.Split(',');
                        id = datosUsuarios[0];
                        nombreProducto = datosUsuarios[1];
                        precioCompra = datosUsuarios[2];
                        precioVenta = datosUsuarios[3];
                        usuario = new Usuario(id, nombreProducto, precioCompra, precioVenta);
                        usuarios.Add(usuario);
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgLista.ItemsSource = usuarios;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudó mostrar los productos " + ex.Message);
                Console.WriteLine(ex);
            }
        }
        private bool VerificarUser()
        {
           
                bool respuesta = true;
                string[] datosUsuarios;
                StreamReader tuberiaLectura = File.OpenText(Login);
                string fila = tuberiaLectura.ReadLine();
                while (fila != null)
                {
                    datosUsuarios = fila.Split(',');
                    if (datosUsuarios[2] == txtUser.Text)
                    {
                        respuesta = false;
                    }

                    fila = tuberiaLectura.ReadLine();
                
                }
            tuberiaLectura.Close();
                return respuesta;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
            this.Hide();
            ventanaPrincipal.ShowDialog();
            this.Close();
        }
    }
}
