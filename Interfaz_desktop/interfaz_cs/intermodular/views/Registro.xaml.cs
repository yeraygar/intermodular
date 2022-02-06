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
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();
        }
        //cierra esta ventana al hacer click en el botón de cerrar    
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
        }

        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void MouseDownContraseña(object sender, MouseButtonEventArgs e)
        {

            PasswordBox pass = sender as PasswordBox;
            Teclado keyboardWindow = new Teclado(pass, this);
            if (keyboardWindow.ShowDialog() == true)
                pass.Password = keyboardWindow.Result;
        }
        /*
        private void textBoxContraseña(object sender, MouseButtonEventArgs e)
        {
            Button buton = sender as Button;
            //PasswordBox pass = sender as PasswordBox;
            Teclado keyboardWindow = new Teclado(buton, this);
            if (keyboardWindow.ShowDialog() == true)
                passworbox.Password = keyboardWindow.Result;
        }
        */

        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
                       
                if (Contraseña.Password != Contraseña2.Password)
                {
                    MessageBox.Show("Las contraseñas introdicidas no coinciden.");
                } 
                else
                {
                    if(textBoxEmail.Text.Length != 0 || textBoxNombre.Text.Length !=0 || Contraseña.Password.Length !=0 || Contraseña2.Password.Length !=0) 
                    { 

                        Boolean comprobarEmail = await Client.checkEmailExists(textBoxEmail.Text);

                        if (comprobarEmail)
                        {
                            MessageBox.Show("El email ya existe. Indroduzca un email válido.");
                        }
                        else
                        {
                            Client clientprueba = new Client(textBoxNombre.Text, textBoxEmail.Text, Contraseña.Password);
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
        }

    }  
    


