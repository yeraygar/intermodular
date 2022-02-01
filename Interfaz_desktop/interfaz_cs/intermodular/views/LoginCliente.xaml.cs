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
        private void Btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void boton_registrar(object sender, RoutedEventArgs e)
        {
           
            Registro regis = new Registro();
            regis.ShowDialog();
        }

        private void boton_iniciarSesion(object sender, RoutedEventArgs e)
        {


            MainWindow inicio = new MainWindow();
            inicio.ShowDialog();
        }
    }
}
