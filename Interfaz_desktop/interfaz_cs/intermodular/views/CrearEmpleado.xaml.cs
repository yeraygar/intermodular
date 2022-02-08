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
    /// Lógica de interacción para CrearEmpleado.xaml
    /// </summary>
    public partial class CrearEmpleado : Window
    {
        public User empleado;
        private bool editedNombre = false;
        private bool editedEmail = false;
        private bool editedPassword = false;
        private bool editedRol = false;
        public CrearEmpleado()
        {
            InitializeComponent();
        }

        private bool isValidEmail(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        private bool isValidNombre(string nombre) => !String.IsNullOrEmpty(nombre) && !String.IsNullOrWhiteSpace(nombre);

        private bool isValidPass(string pass)
        {
            bool retorno = true;
            if(!String.IsNullOrEmpty(pass) && !String.IsNullOrWhiteSpace(pass))
            {
                if (pass.Contains(" "))
                {
                    retorno = false;
                }
                else
                {
                    try
                    {
                        int.Parse(pass);
                    }
                    catch (Exception e)
                    {
                        retorno = false;
                    }
                }
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        private void passwordVisible_LostFocus(object sender, RoutedEventArgs e)
        {
            string pass = passwordVisible.Text;
            imgPass.Visibility = Visibility.Visible;
            if (!String.IsNullOrEmpty(pass) && !String.IsNullOrWhiteSpace(pass))
            {
                if (pass.Contains(" "))
                {
                    imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPass.ToolTip = "La contraseña no puede contener espacios";
                }
                else
                {
                    try
                    {
                        int.Parse(pass);
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgPass.ToolTip = null;
                    }
                    catch (Exception ex)
                    {
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPass.ToolTip = "La contraseña debe ser de tipo númerica";
                    }
                }
            }
            else
            {
                imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgPass.ToolTip = "La contraseña no puede estar vacía";
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if(imgVerPass.Tag.Equals("hidden"))
                {
                    imgVerPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eye_closed.png");
                    columnaPass.Width = new GridLength(0);
                    columnaPassVisible.Width = new GridLength(1, GridUnitType.Star);
                    passwordVisible.Text = password.Password.ToString();
                    imgVerPass.Tag = "visible";
                }
                else
                {
                    imgVerPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eye.png");
                    columnaPass.Width = new GridLength(1,GridUnitType.Star);
                    columnaPassVisible.Width = new GridLength(0);
                    password.Password = passwordVisible.Text;
                    imgVerPass.Tag = "hidden";
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private async void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if(isValidNombre(txtNombre.Text) && isValidEmail(txtEmail.Text) && isValidPass(password.Password))
            {
                empleado = await User.createUser(new User(txtNombre.Text, txtEmail.Text, password.Password, Client.currentClient._id, true, comboBoxRol.SelectedItem.ToString()));
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear el usuario, campos no válidos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtNombre.Text == null && !editedNombre)
            {
                editedNombre = true;
            }
            else
            {
                imgNombre.Visibility = Visibility.Visible;
                if(String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgNombre.ToolTip = "Debe introducir un nombre";
                }
                else
                {

                    imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                    imgNombre.ToolTip = null;
                }
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtEmail.Text == null && !editedEmail)
            {
                editedEmail = true;
            }
            else
            {
                imgEmail.Visibility = Visibility.Visible;
                imgEmail.Source = isValidEmail(txtEmail.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgEmail.ToolTip = isValidEmail(txtEmail.Text) ? null : "Debe introducir un correo válido";
            }
        }

        private void passwordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(passwordVisible.Text == null && !editedPassword)
            {
                editedPassword = true;
            }
            else
            {
                imgPass.Visibility = Visibility.Visible;
                if(String.IsNullOrEmpty(password.Password) || String.IsNullOrWhiteSpace(password.Password))
                {
                    imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPass.ToolTip = "Debe introducir una contraseña";
                }else if(password.Password.Contains(" "))
                {
                    imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPass.ToolTip = "La contraseña no puede incluir espacios";
                }
                else
                {
                    try
                    {
                        int.Parse(passwordVisible.Text);
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgPass.ToolTip = null;
                    }
                    catch(Exception exc)
                    {
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPass.ToolTip = "La contraseña debe ser numérica";
                    }
                }
            }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password == null && !editedPassword)
            {
                editedPassword = true;
            }
            else
            {
                imgPass.Visibility = Visibility.Visible;
                if (String.IsNullOrEmpty(password.Password) || String.IsNullOrWhiteSpace(password.Password))
                {
                    imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPass.ToolTip = "Debe introducir una contraseña";
                }
                else if (password.Password.Contains(" "))
                {
                    imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPass.ToolTip = "La contraseña no puede incluir espacios";
                }
                else
                {
                    try
                    {
                        int.Parse(password.Password);
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgPass.ToolTip = null;
                    }
                    catch (Exception exc)
                    {
                        imgPass.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPass.ToolTip = "La contraseña debe ser numérica";
                    }
                }
            }
        }
    }
}
