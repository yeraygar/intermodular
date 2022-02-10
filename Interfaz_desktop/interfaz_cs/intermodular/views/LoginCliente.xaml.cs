using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para LoginCliente.xaml
    /// </summary>
    public partial class LoginCliente : Window
    {
        public LoginCliente()
        {
            InitializeComponent(); 
        }
      
        private void textBoxUsuario(object sender, MouseButtonEventArgs e)
        {
            Button buton = sender as Button;            
            //TextBox textBox = sender as TextBox;
            Teclado keyboardWindow = new Teclado(buton,this);
            if (keyboardWindow.ShowDialog() == true)
                textBox.Text = keyboardWindow.Result;
        }

        private void textBoxNombre(object sender, MouseButtonEventArgs e)
        {
            Button buton = sender as Button;
            //TextBox textBox = sender as TextBox;
            Teclado keyboardWindow = new Teclado(buton, this);
            if (keyboardWindow.ShowDialog() == true)
                textBoxN.Text = keyboardWindow.Result;
        }

        private void textBoxContraseña(object sender, MouseButtonEventArgs e)
        {
            Button buton = sender as Button;
            //PasswordBox pass = sender as PasswordBox;
            Teclado keyboardWindow = new Teclado(buton, this);
            if (keyboardWindow.ShowDialog() == true)
                passworbox.Password = keyboardWindow.Result;
        }

        private void textBoxContraseña2(object sender, MouseButtonEventArgs e)
        {
            Button buton = sender as Button;
            //PasswordBox pass = sender as PasswordBox;
            Teclado keyboardWindow = new Teclado(buton, this);
            if (keyboardWindow.ShowDialog() == true)
                passworbox2.Password = keyboardWindow.Result;
        }


       

        private async void boton_iniciarSesion(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != null && passworbox.Password != null)
            {
                try
                {
                    Boolean comprobar = await Client.validateClient(textBox.Text, passworbox.Password);
                    if (comprobar)
                    {
                        MainWindow inicio = new MainWindow();
                        inicio.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Error al conectarse a la base de datos");
                }
               
            }
            else
            {
                MessageBox.Show("Los campos no pueden estar vacios");
            }
        }

        private void cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
        }

        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
        }

        private void boton_Registro(object sender, RoutedEventArgs e)
        {
            stackpanelNombre.Visibility = Visibility.Visible;
            stackpanelcontraseña2.Visibility = Visibility.Visible;
            botonRegistrarse.Visibility = Visibility.Visible;
            botonInicio.Visibility = Visibility.Collapsed;
            //botonInicio.Content = "Registrarse";
        }

        private void boton_Login(object sender, RoutedEventArgs e)
        {
            stackpanelNombre.Visibility = Visibility.Collapsed;
            stackpanelcontraseña2.Visibility = Visibility.Collapsed;
            botonRegistrarse.Visibility = Visibility.Collapsed;
            botonInicio.Visibility = Visibility.Visible;
        }
                

        private async void boton_Registrarse(object sender, RoutedEventArgs e)
        {

            if (passworbox.Password != passworbox2.Password)
            {
                MessageBox.Show("Las contraseñas introdicidas no coinciden.");
            }
            else
            {
                if (textBox.Text.Length != 0 || textBoxN.Text.Length != 0 || passworbox.Password.Length != 0 || passworbox2.Password.Length != 0)
                {

                    Boolean comprobarEmail = await Client.checkEmailExists(textBox.Text);

                    if (comprobarEmail)
                    {
                        MessageBox.Show("El email ya existe. Indroduzca un email válido.");
                    }
                    else
                    {
                        Client clientprueba = new Client(textBoxN.Text, textBox.Text, passworbox.Password);
                        Boolean crearCliente = await Client.createClient(clientprueba);
                        MessageBox.Show("Usuario creado correctamente");
                        LoginCliente log = new LoginCliente();
                        log.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No puede haber campos vacios.");
                }

            }
        }

        private void inicio_Enter(object sender, MouseEventArgs e)
        {
            botonInicio.Background = Brushes.DarkSlateGray;           
        }

        private void inicio_Leave(object sender, MouseEventArgs e)
        {
            botonInicio.Background = (Brush)(new BrushConverter().ConvertFrom("#5da9b7"));            
        }

        private void registro_Enter(object sender, MouseEventArgs e)
        {
            botonRegistro.Background = Brushes.DarkSlateGray;
        }

        private void registro_Leave(object sender, MouseEventArgs e)
        {
            botonRegistro.Background = (Brush)(new BrushConverter().ConvertFrom("#5da9b7"));
        }
        private void login_Enter(object sender, MouseEventArgs e)
        {
            botonLogin.Background = Brushes.DarkSlateGray;
        }

        private void login_Leave(object sender, MouseEventArgs e)
        {
            botonLogin.Background = (Brush)(new BrushConverter().ConvertFrom("#5da9b7"));
        }
        private void registrarse_Enter(object sender, MouseEventArgs e)
        {
            botonRegistrarse.Background = Brushes.DarkSlateGray;
        }

        private void registrarse_Leave(object sender, MouseEventArgs e)
        {
            botonRegistrarse.Background = (Brush)(new BrushConverter().ConvertFrom("#5da9b7"));
        }
    }
}

