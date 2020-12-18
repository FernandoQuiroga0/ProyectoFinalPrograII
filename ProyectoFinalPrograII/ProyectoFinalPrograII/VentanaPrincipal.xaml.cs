using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void BtnQuienesSomos_Click(object sender, RoutedEventArgs e)
        {
            QuienesSomos quienesSomos = new QuienesSomos();
            this.Hide();
            quienesSomos.ShowDialog();
            this.Close();
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que quiere salir?", "Salir",
             MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.ShowDialog();
                this.Close();
            }
        }
        private void Ventas_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventas = new Ventas();
            this.Hide();
            ventas.ShowDialog();
            this.Close();
        }
        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            Productos productosCrud = new Productos();
            this.Hide();
            productosCrud.ShowDialog();
            this.Close();
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            this.Hide();
            usuarios.ShowDialog();
            this.Close();
        }
    }
}

