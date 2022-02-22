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
    /// Lógica de interacción para Comensales.xaml
    /// </summary>
    public partial class Comensales : Window
    {
        User user;
        public Comensales(User user)
        {
            InitializeComponent();
            nombreMesa.Text = "Mesa: "+Mesa.currentMesa.name + "\t  Num Sillas: " + Mesa.currentMesa.comensalesMax;
            this.user = user;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.5;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.6;
        }
        /************************* BOTONERA *************************/

        private void btn_0(object sender, System.EventArgs e)
        {
            textComensal.Text += boton0.Content.ToString();
        }

        private void btn_1(object sender, System.EventArgs e)
        {
            textComensal.Text += boton1.Content.ToString();
        }

        private void btn_2(object sender, System.EventArgs e)
        {
            textComensal.Text += boton2.Content.ToString();
        }

        private void btn_3(object sender, System.EventArgs e)
        {
            textComensal.Text += boton3.Content.ToString();
        }

        private void btn_4(object sender, System.EventArgs e)
        {
            textComensal.Text += boton4.Content.ToString();
        }

        private void btn_5(object sender, System.EventArgs e)
        {
            textComensal.Text += boton5.Content.ToString();
        }

        private void btn_6(object sender, System.EventArgs e)
        {
            textComensal.Text += boton6.Content.ToString();
        }

        private void btn_7(object sender, System.EventArgs e)
        {
            textComensal.Text += boton7.Content.ToString();
        }

        private void btn_8(object sender, System.EventArgs e)
        {
            textComensal.Text += boton8.Content.ToString();
        }

        private void btn_9(object sender, System.EventArgs e)
        {
            textComensal.Text += boton9.Content.ToString();
        }

        //cierra esta ventana al hacer click en el botón de cerrar    

        private void Btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //cambia el color al situarse encima del boton de admin
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            boton_teclado.Background = Brushes.Black;

            boton_teclado.Foreground = Brushes.White;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            boton_teclado.Background = Brushes.White;

            boton_teclado.Foreground = Brushes.Black;
        }

        private void Button_Aceptar_leave(object sender, MouseEventArgs e)
        {

            var bc = new BrushConverter();
            aceptar.Background = (Brush)bc.ConvertFrom("#5da9b7");

        }

        private void Button_Aceptar_enter(object sender, MouseEventArgs e)
        {
            aceptar.Background = Brushes.DarkSlateGray;
        }

        private void btn_borrar(object sender, RoutedEventArgs e)
        {
            if (textComensal.Text.Length != 0)
            {
                textComensal.Text = textComensal.Text.Substring(0, textComensal.Text.Length - 1);
            }
        }

        //Metodo para cambiar el color de todos los botones cuando se pasa por encima
        private void btns_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Cursor = Cursors.Hand; //Cambiamos el cursor
            btn.Foreground = Brushes.White;
            btn.Background = Brushes.Black;
        }

        //Método para cambiar el color de todos los botones cuando se deja de hacer focus
        private void btns_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.Black;
            btn.Background = Brushes.White;
        }

        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }

        private async void btnAceptar_Click(object sender, System.EventArgs e)
        {
            if (validComensales())
            {
                Mesa.currentMesa.comensales = int.Parse(textComensal.Text);
                if (Mesa.currentMesa.comensales == Mesa.currentMesa.comensalesMax) Mesa.currentMesa.ocupada = true;
                try
                {
                    if (await Mesa.updateTable(Mesa.currentMesa._id, Mesa.currentMesa))
                    {
                        vistaPedidos vistaPed = new vistaPedidos(user.name, textComensal.Text);
                        vistaPed.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la mesa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }catch(Exception exception)
                {
                    MessageBox.Show("Error al cargar la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
             
            }else
            {
                MessageBox.Show("Número de comensales no válidos","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

        private bool validComensales() => !String.IsNullOrEmpty(textComensal.Text) && !String.IsNullOrWhiteSpace(textComensal.Text) && !textComensal.Text.Contains(" ") && int.Parse(textComensal.Text) > 0 && int.Parse(textComensal.Text) <= Mesa.currentMesa.comensalesMax;

        private void textComensal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //bloquear los carácteres no numéricos
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
