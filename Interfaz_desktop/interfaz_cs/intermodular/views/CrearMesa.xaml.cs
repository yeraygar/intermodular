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
            //this.Width = Static
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
            if (!String.IsNullOrEmpty(txtNumZona) && !String.IsNullOrWhiteSpace(txtNumZona) && isInteger(txtNumZona))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Este método comprueba que el string que le pasamos por parametro sea de tipo integer
        private bool isInteger(string value)
        {
            try
            {
                int.Parse(value);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Este evento saltará cuando salimos del focus del text box y pintara la imagen correspondiente segun si existe algun campo erroneo o no.
        private void txtZona_LostFocus(object sender, RoutedEventArgs e)
        {
            imgValidTableName.Visibility = Visibility.Visible;
            if (checkZoneName(txtMesa.Text))
            {
                //MessageBox.Show("Nombre de zona válido");
                imgValidTableName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                imgValidTableName.ToolTip = null;
            }
            else
            {
                imgValidTableName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgValidTableName.ToolTip = "El nombre no puede estar vacío";
            }
        }

        //Este evento saltará cuando salimos del focus del text box y pintara la imagen correspondiente segun si existe algun campo erroneo o no.
        private void txtNumMesas_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNumMesas.Text = txtNumMesas.Text.Replace(" ", "");
            imgValidNumComensales.Visibility = Visibility.Visible;
            if (checkZoneNumber(txtNumMesas.Text))
            {
                imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                imgValidNumComensales.ToolTip = null;
            }
            else
            {
                imgValidNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgValidNumComensales.ToolTip = "valor no válido, introduzca otro valor.";
            }
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
                    Mesa mesa = new Mesa(txtMesa.Text, Mesa.currentZoneTables.Count + 1, true, idZonaSelect, int.Parse(txtNumMesas.Text));
                if (Mesa.currentZoneTables.Count == 0)
                {
                    mesa.num_row = 0;
                    mesa.num_column = 0;
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
    }
}
