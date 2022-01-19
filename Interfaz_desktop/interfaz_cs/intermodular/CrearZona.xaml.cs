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
        Window win;
        public CrearZona(Window window)
        {
            InitializeComponent();
            win = window;
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
            if(checkZoneName(txtZona.Text) && checkZoneNumber(txtNumMesas.Text))
            {
                Zona zona = new Zona(Staticresources.id_client,txtZona.Text,int.Parse(txtNumMesas.Text),true,null);
                zona = await Zona.createZone(zona);
                if(zona == null)
                {
                    //error
                }else
                {
                    Zonas ventanaZona = (Zonas)win;
                    Button btn = new Button();
                    btn.Height = 50;
                    btn.Width = 150;
                    btn.Content = zona.zone_name;
                    //btn.Click += MessageBox.Show("asdf")
                    ventanaZona.stackZonas.Children.Add(btn);
                }
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
        }

        //Cambia el color del botón de cerrar, al dejar de estar situado encima.
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
        }

        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            btnCancelar.Foreground = Brushes.White;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\cancel_blanco.png");
        }

        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = Brushes.White;
            btnCancelar.Foreground = Brushes.Black;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\cancel.png");
        }

        private void btnSiguiente_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = (Brush)(new BrushConverter().ConvertFrom("#63c554"));
            btnSiguiente.Foreground = Brushes.White;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\next_blanco.png");
        }

        private void btnSiguiente_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = Brushes.White;
            btnSiguiente.Foreground = Brushes.Black;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\next.png");
        }


        //Comprobamos que no se escribe ninguna tecla que no sea un número, (los espacios no los detecta).
        private void txtNumMesas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            txtNumMesas.Text = txtNumMesas.Text.Replace(" ", "");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

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

        //Este método comprueba que el valor introducido en el textbox del número de mesas sea un valor válido.
        private bool checkZoneNumber(string txtNumZona)
        {
            if(!String.IsNullOrEmpty(txtNumZona) && !String.IsNullOrWhiteSpace(txtNumZona) && isInteger(txtNumZona))
            {
                return true;
            }else
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
            }catch(Exception e)
            {
                return false;
            }
        }


        //Este evento saltará cuando salimos del focus del text box y pintara la imagen correspondiente segun si existe algun campo erroneo o no.
        private void txtZona_LostFocus(object sender, RoutedEventArgs e)
        {
            imgValidZoneName.Visibility = Visibility.Visible;
            if (checkZoneName(txtZona.Text))
            {
                //MessageBox.Show("Nombre de zona válido");
                imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\verify.png");
                imgValidZoneName.ToolTip = null;
            }
            else
            {
                imgValidZoneName.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\error.png");
                imgValidZoneName.ToolTip = "El nombre no puede estar vacío";
            }
        }

        //Este evento saltará cuando salimos del focus del text box y pintara la imagen correspondiente segun si existe algun campo erroneo o no.
        private void txtNumMesas_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNumMesas.Text = txtNumMesas.Text.Replace(" ", "");
            imgValidNumTables.Visibility = Visibility.Visible;
            if(checkZoneNumber(txtNumMesas.Text))
            {
                imgValidNumTables.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\verify.png");
                imgValidNumTables.ToolTip = null;
            }else
            {
                imgValidNumTables.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\error.png");
                imgValidNumTables.ToolTip = "valor no válido, introduzca otro valor.";
            }
        }
    }
}
