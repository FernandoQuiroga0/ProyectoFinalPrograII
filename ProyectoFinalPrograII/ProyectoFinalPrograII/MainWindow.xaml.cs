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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoFinalPrograII
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Login = @"/login.txt";
        public MainWindow()
        {
            InitializeComponent();
            VerificarArchivo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (usuario != "" && password != "")
                {
                    if (ValidarUsuario(usuario, password))
                    {

                        VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();
                        this.Hide();
                        ventanaPrincipal.ShowDialog();
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("Datos Incorrectos");
                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private bool ValidarUsuario(string usuario, string password)
        {
            bool resultado = false;
            string[] datosUsuario;
            StreamReader tuberiaLectura = File.OpenText(Login);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosUsuario = linea.Split(',');
                if (datosUsuario[2] == usuario && datosUsuario[3] == password)
                {
                    resultado = true;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return resultado;
        }
        private void VerificarArchivo()
        {
            try
            {
                if (!File.Exists(Login))
                {
                    File.Create(Login).Dispose();
                    Escribir("1,1,1,1");
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void Escribir(string mensaje)
        {
            StreamWriter tuberiaEscritura = File.AppendText(Login);
            tuberiaEscritura.WriteLine(mensaje);
            tuberiaEscritura.Close();
        }
    }
}
