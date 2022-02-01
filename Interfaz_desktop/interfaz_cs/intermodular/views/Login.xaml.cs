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
    public partial class Login : Window
    {
        bool paraFichar;
        bool modificar;
        bool admin;

        /// <summary>
        /// <b>paraFichar: </b> modificamos el campo active del empleado <br></br>
        /// <b>modificar: </b> indica si modificamos o solo validamos <br></br>
        /// <b>admin: </b> indica si estamos validando una contraseña de Administrador
        /// </summary>
        public Login(bool paraFichar, bool modificar, bool admin)
        {
            InitializeComponent();
            if (admin) LabelNombre.Content = "Administrador";
            else LabelNombre.Content = User.usuarioElegido.name;

            this.admin = admin;
            this.paraFichar = paraFichar;
            this.modificar = modificar;

            //Si se va a hacer una validacion del passw de cualquier Admin cargamos todos los admin del client de manera asincrona;
            if (admin) User.getAdmins(Staticresources.id_client).ContinueWith(task => { }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private async void btnAceptar_Click(object sender, System.EventArgs e)
        {
            String passwordIntroducido = Encrypt.GetSHA256(passwordBox.Password);

            if (admin) comportamientoAdministrador(passwordIntroducido);
            else await comportamientoUsuario(passwordIntroducido);
            
        }

        /// <summary>
        /// Valida la contraseña de cualquier Usuario del Cliente <br></br>
        /// Permite cambiar el status fichado/sinFichar de cualquier usuario si modifcar == true;
        /// </summary>
        private async Task comportamientoUsuario(string passwordIntroducido)
        {
            String passwordElegido = Encrypt.GetSHA256(User.usuarioElegido.passw); //TODO esta ya vendra cifrada en la version final

            //if (User.usuarioElegido.passw == passwordBox.Password)
            if (passwordElegido.Equals(passwordIntroducido))
            {
                if (modificar) //actualizamos su estado a activo en la bbdd, cuando fichemos salida haremos lo contrario;
                {
                    User.usuarioElegido.active = paraFichar;
                    await User.updateUser(User.usuarioElegido._id, User.usuarioElegido);
                    MessageBox.Show($"Usuario {User.usuarioElegido.name} ha fichado {(paraFichar ? "Entrada" : "Salida")}", "Contraseña Correcta!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show($"Usuario {User.usuarioElegido.name}", "Contraseña Correcta!", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
            }
        }

      
        /// <summary> Permite introducir cualquier contraseña que coincida con un User del Cliente con rol de Admin; </summary>
        private void comportamientoAdministrador(string passwordIntroducido)
        {
            bool passOk = false;
            String nombreOk = "";

            foreach (User admin in User.usuariosAdmin)
            {
                String passwordElegido = Encrypt.GetSHA256(admin.passw); // TODO: en la version final vendra cifrado desde la api
                if (passwordElegido.Equals(passwordIntroducido))
                {
                    nombreOk = admin.name;
                    passOk = true;
                }
            }

            if (passOk)
            {
                MessageBox.Show($"Admin: {nombreOk}", "Contraseña Correcta!", MessageBoxButton.OK, MessageBoxImage.Information);

                Admin admin = new Admin();
                this.Close();
                admin.ShowDialog();

            }
            else MessageBox.Show($"Ningun Administrador coincide", "Contraseña Incorrecta!", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    /************************* BOTONERA *************************/

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
    }
}
