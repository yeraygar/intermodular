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
        //public Login(List<User> listaUsuarios)
        public Login()
        {
            InitializeComponent();
            LabelNombre.Content = User.usuarioElegido.name;
           // User.usuarioElegido.passw = "loquemedelagana";
        }

        

        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            String passwordElegido = Encrypt.GetSHA256(User.usuarioElegido.passw);
            String passwordIntroducido = Encrypt.GetSHA256(passwordBox.Password);

            //if (User.usuarioElegido.passw == passwordBox.Password)
            if (passwordElegido.Equals(passwordIntroducido))
            {
                
                MessageBox.Show("Contraseña correcta");
                User.usuariosFichados.Add(User.usuarioElegido);
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");               
            }
        }

        private void btn_0(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton0.Content.ToString();
        }

        private void btn_1(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton1.Content.ToString();
        }

        private void btn_2(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton2.Content.ToString();
        }

        private void btn_3(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton3.Content.ToString();
        }

        private void btn_4(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton4.Content.ToString();
        }

        private void btn_5(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton5.Content.ToString();
        }

        private void btn_6(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton6.Content.ToString();
        }

        private void btn_7(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton7.Content.ToString();            
        }

        private void btn_8(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton8.Content.ToString();
        }

        private void btn_9(object sender, System.EventArgs e)
        {
            passwordBox.Password += boton9.Content.ToString();
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

        private void btn_borrar(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length != 0)
            {
               passwordBox.Password = passwordBox.Password.Substring(0, passwordBox.Password.Length - 1);
            }
        }
    }
}
