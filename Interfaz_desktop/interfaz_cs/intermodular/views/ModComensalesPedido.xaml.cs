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
    /// Lógica de interacción para ModComensalesPedido.xaml
    /// </summary>
    public partial class ModComensalesPedido : Window
    {
        public int comensales;
        public ModComensalesPedido()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.35);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.25);
        }
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
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
            if (!String.IsNullOrEmpty(txtZona) && !String.IsNullOrWhiteSpace(txtZona) && !txtZona.Contains(" ") && int.Parse(txtZona) <= Mesa.currentMesa.comensalesMax && int.Parse(txtZona) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtZona_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtZona_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgValidZoneName.Visibility = Visibility.Visible;
            if(String.IsNullOrEmpty(txtZona.Text) || String.IsNullOrWhiteSpace(txtZona.Text))
            {
                imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgValidZoneName.ToolTip = "Debe introducir un numero de comensales";
            }else if(txtZona.Text.Contains(" "))
            {
                imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgValidZoneName.ToolTip = "El campo no puede contener espacios";
            }
            else
            {
                if (int.Parse(txtZona.Text) <= 0)
                {
                    imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgValidZoneName.ToolTip = "El número de comensales tiene que ser mayor que 0";
                }
                else
                {
                    if (int.Parse(txtZona.Text) <= Mesa.currentMesa.comensalesMax)
                    {
                        imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgValidZoneName.ToolTip = null;
                    }
                    else
                    {
                        imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgValidZoneName.ToolTip = "El número introducido es mayor al número máximo de comensales de la mesa";
                    }
                }
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (checkZoneName(txtZona.Text))
            {
                comensales = int.Parse(txtZona.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Número no válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
