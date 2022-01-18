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

namespace intermodular
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            LabelNombre.Content = User.usuarioElegido.name;
           // User.usuarioElegido.passw = "loquemedelagana";
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
            aceptar.Background = (Brush)bc.ConvertFrom("#48C9B0");

        }

        private void Button_Aceptar_enter(object sender, MouseEventArgs e)
        {
            aceptar.Background = Brushes.DarkSlateGray;
        }
    }
}
