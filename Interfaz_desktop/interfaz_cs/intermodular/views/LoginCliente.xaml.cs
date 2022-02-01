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
            TextBox textbox = sender as TextBox;
            Teclado keyboardWindow = new Teclado(textbox, this);
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
        }

        private void textBoxContraseña(object sender, MouseButtonEventArgs e)
        {
            PasswordBox pass = sender as PasswordBox;
            Teclado keyboardWindow = new Teclado(pass, this);
            if (keyboardWindow.ShowDialog() == true)
                pass.Password = keyboardWindow.Result;
        }
      

        private void boton_registrar(object sender, RoutedEventArgs e)
        {
           
            Registro regis = new Registro();
            regis.ShowDialog();
        }

        private async void boton_iniciarSesion(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != null && passworbox.Password != null)
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
    }
}
