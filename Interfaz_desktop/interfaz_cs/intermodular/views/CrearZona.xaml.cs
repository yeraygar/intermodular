using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace intermodular
{
    /// <summary>
    /// Lógica de interacción para CrearZona.xaml
    /// </summary>
    public partial class CrearZona : Window
    {
        public Zona zona;
        private bool editedZone = false;
        public CrearZona()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.3;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.3;
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        //Comprobamos que el nombre de la zona y el numero de mesas son valores válidos, de ser así creamos la mesa.
        private async void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if(checkZoneName(txtZona.Text))
            {
                zona = await Zona.createZone(new Zona(txtZona.Text));
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear la zona");
            }

        }

        //Cambia el color del botón de cerrar, al pasar por encima.
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        //Cambia el color del botón de cerrar, al dejar de estar situado encima.
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }

        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            btnCancelar.Foreground = Brushes.White;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel_blanco.png");
        }

        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = Brushes.White;
            btnCancelar.Foreground = Brushes.Black;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel.png");
        }

        private void btnSiguiente_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = (Brush)(new BrushConverter().ConvertFrom("#63c554"));
            btnSiguiente.Foreground = Brushes.White;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\next_blanco.png");
        }

        private void btnSiguiente_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = Brushes.White;
            btnSiguiente.Foreground = Brushes.Black;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\next.png");
        }

        //Este método comprueba que el nombre de la zona no este vacío, etc, comprueba que sea válido.
        private bool checkZoneName(string txtZona)
        {
            if(!String.IsNullOrEmpty(txtZona) && !String.IsNullOrWhiteSpace(txtZona))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtZona_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!editedZone)
            {
                editedZone = true;
            }
            else
            {
                imgValidZoneName.Visibility = Visibility.Visible;
                imgValidZoneName.Source = checkZoneName(txtZona.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
            }
        }
    }
}
