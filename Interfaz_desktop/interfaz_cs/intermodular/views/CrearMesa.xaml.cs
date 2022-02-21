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
    /// Lógica de interacción para CrearMesa.xaml
    /// </summary>
    public partial class CrearMesa : Window
    {
        string idZonaSelect;
        public Mesa mesa;
        public CrearMesa(string id)
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.6;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.4;
            idZonaSelect = id;
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
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

        private void txtNumMesas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            txtNumMesas.Text = txtNumMesas.Text.Replace(" ", "");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        //Este método comprueba que el nombre de la zona no este vacío, etc, comprueba que sea válido.
        private bool checkZoneName(string txtZona)
        {
            if (!String.IsNullOrEmpty(txtZona) && !String.IsNullOrWhiteSpace(txtZona))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Este método comprueba que el valor introducido en el textbox del número de mesas sea un valor válido.
        private bool checkZoneNumber(string txtNumZona)
        {
            bool retorno = true;
            if(String.IsNullOrEmpty(txtNumMesas.Text) || String.IsNullOrWhiteSpace(txtNumMesas.Text))
            {
                retorno = false;
            }else if(txtNumMesas.Text.Contains(" "))
            {
                retorno = false;
            }
            else
            {
                if(int.Parse(txtNumZona) <= 0)
                {
                    retorno = false;
                }
            }
            return retorno;
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Comprobamos que el nombre de la zona y el numero de mesas son valores válidos, de ser así creamos la mesa.
        private async void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (checkZoneName(txtMesa.Text) && checkZoneNumber(txtNumMesas.Text))
            {
                    Mesa mesa = new Mesa(txtMesa.Text, true, idZonaSelect, int.Parse(txtNumMesas.Text));
                if (Mesa.currentZoneTables == null ||Mesa.currentZoneTables.Count == 0)
                {
                    mesa.num_row = 0;
                    mesa.num_column = 0;
                    Mesa.currentZoneTables = new List<Mesa>();
                }
                else
                {
                    if (Mesa.currentZoneTables[Mesa.currentZoneTables.Count - 1].num_column == 5)
                    {
                        mesa.num_row = Mesa.currentZoneTables[Mesa.currentZoneTables.Count - 1].num_row + 1;
                        mesa.num_column = 0;
                    }
                    else
                    {
                        mesa.num_row = Mesa.currentZoneTables[Mesa.currentZoneTables.Count - 1].num_row;
                        mesa.num_column = Mesa.currentZoneTables[Mesa.currentZoneTables.Count - 1].num_column + 1;
                    }
                }
                this.mesa = await Mesa.createTable(mesa);
                Mesa.currentZoneTables.Add(this.mesa);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear la zona");
            }

        }

        private void txtMesa_TextChanged(object sender, TextChangedEventArgs e)
        {
                imgValidTableName.Visibility = Visibility.Visible;
                imgValidTableName.Source = checkZoneName(txtMesa.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
        }

        private void txtNumMesas_TextChanged(object sender, TextChangedEventArgs e)
        {
                imgValidNumComensales.Visibility = Visibility.Visible;
                if(String.IsNullOrEmpty(txtNumMesas.Text) || String.IsNullOrWhiteSpace(txtNumMesas.Text))
                {
                    imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgValidNumComensales.ToolTip = "Debes introducir un número de comensales";
                }else if(txtNumMesas.Text.Contains(" "))
                {
                    imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgValidNumComensales.ToolTip = "El campo no puede contener espacios";
                }else
                {
                    if(int.Parse(txtNumMesas.Text) <= 0)
                    {
                    imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgValidNumComensales.ToolTip = "El Número de comensales no puede ser 0";
                }
                    else
                    {
                        imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgValidNumComensales.ToolTip = null;
                    }

                }
        }
    }
}
