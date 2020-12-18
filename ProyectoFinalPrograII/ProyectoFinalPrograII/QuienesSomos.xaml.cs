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
    /// Lógica de interacción para QuienesSomos.xaml
    /// </summary>
    public partial class QuienesSomos : Window
    {
        public QuienesSomos()
        {
            InitializeComponent();
            lblDescripcion.Content = "Soy solo un estudiante haciendo un proyecto para "+"\n"+ "el tercer parcial de la materia" + "\n" +
                "Programación II"+"\n"+ "Graduado del Colegio Particular España \nCochabamba Bolivia" + "\n"+
                "Me gustaría que ya fuera navidad ☻♥"+"\nJose Fernando Quiroga Lema";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            VentanaPrincipal mainWindow = new VentanaPrincipal();
            this.Hide();
            mainWindow.ShowDialog();
            this.Close();
        }            
        
    }
}
