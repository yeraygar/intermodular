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
    /// Lógica de interacción para VistaEmpleados.xaml
    /// </summary>
    public partial class VistaEmpleados : Window
    {
        private User empleado;
        private Button btnPressed;
        private bool imgRolPressed = false;
        public VistaEmpleados()
        {
            InitializeComponent();
            User.getClientUsers(Client.currentClient._id).ContinueWith(task =>
            {
                if(User.usuariosDeCliente != null)
                {
                    foreach(User u in User.usuariosDeCliente)
                    {
                        Button btn = new Button
                        {
                            Height = 70,
                            Content = u.name,
                            Tag = u._id,
                            Style = Application.Current.TryFindResource("btnRedondo") as Style,
                            Cursor = Cursors.Hand,
                            Margin = new Thickness(10),
                            FontSize = 19

                        };
                        btn.Click += (object send, RoutedEventArgs a) =>
                        {
                            empleado = u;
                            txtAyuda.Visibility = Visibility.Hidden;
                            imgRolPressed = false;
                            imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                            imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                            imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                            imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                            imgNombre.Visibility = Visibility.Visible;
                            imgEmail.Visibility = Visibility.Visible;
                            imgPassword.Visibility = Visibility.Visible;
                            imgRol.Visibility = Visibility.Visible;
                            txtNombre.Tag = empleado.name;
                            txtEmail.Tag = empleado.email;
                            txtPass.Tag = empleado.passw;
                            comboBoxRol.Tag = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                            txtNombre.Text = empleado.name;
                            txtEmail.Text = empleado.email;
                            txtPass.Text = empleado.passw;
                            comboBoxRol.SelectedItem = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                            txtNombre.IsEnabled = false;
                            txtEmail.IsEnabled = false;
                            txtPass.IsEnabled = false;
                            comboBoxRol.IsEnabled = false;
                            btnCancelar.Visibility = Visibility.Hidden;
                            btnEditarZona.Visibility = Visibility.Hidden;
                            btnCancelar.IsEnabled = false;
                            btnEditarZona.IsEnabled = false;
                            btnEliminarZona.Visibility = Visibility.Visible;
                            if (btnPressed != null)
                            {
                                btnPressed.Background = Brushes.White;
                                btnPressed.Foreground = Brushes.Black;
                            }
                            btnPressed = btn;
                            btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                            btn.Foreground = Brushes.White;
                        };

                        btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) => {
                            if (btnPressed == null || btnPressed.Tag != btn.Tag)
                                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#93d5d5"));
                        };

                        btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
                        {
                            if (btnPressed == null || btnPressed.Tag != btn.Tag)
                                btn.Background = Brushes.White;
                        };
                        stackEmpleados.Children.Add(btn);
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (btnPressed != null)
            {
                btnPressed.Background = Brushes.White;
                btnPressed.Foreground = Brushes.Black;
            }
            btnEliminarZona.Visibility = Visibility.Collapsed;
            btnPressed = null;
            empleado = null;
            txtAyuda.Visibility = Visibility.Visible;
            txtNombre.Tag = "";
            txtEmail.Tag = "";
            txtPass.Tag = "";
            comboBoxRol.Tag = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            comboBoxRol.Text = "";
            txtNombre.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPass.IsEnabled = false;
            comboBoxRol.IsEnabled = false;
            imgNombre.Visibility = Visibility.Hidden;
            imgEmail.Visibility = Visibility.Hidden;
            imgPassword.Visibility = Visibility.Hidden;
            imgRol.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnEditarZona.Visibility = Visibility.Hidden;
            btnCancelar.IsEnabled = false;
            btnEditarZona.IsEnabled = false;
            CrearEmpleado crearEmpleado = new CrearEmpleado();
            crearEmpleado.ShowDialog();
            if (crearEmpleado.empleado != null)
            {
                Button btn = new Button
                {
                    Height = 70,
                    Content = crearEmpleado.empleado.name,
                    Tag = crearEmpleado.empleado._id,
                    Style = Application.Current.TryFindResource("btnRedondo") as Style,
                    Cursor = Cursors.Hand,
                    Margin = new Thickness(10),
                    FontSize = 19

                };
                btn.Click += (object send, RoutedEventArgs a) =>
                {
                    empleado = crearEmpleado.empleado;
                    txtAyuda.Visibility = Visibility.Hidden;
                    imgRolPressed = false;
                    imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgNombre.Visibility = Visibility.Visible;
                    imgEmail.Visibility = Visibility.Visible;
                    imgPassword.Visibility = Visibility.Visible;
                    imgRol.Visibility = Visibility.Visible;
                    txtNombre.Tag = empleado.name;
                    txtEmail.Tag = empleado.email;
                    txtPass.Tag = empleado.passw;
                    comboBoxRol.Tag = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                    txtNombre.Text = empleado.name;
                    txtEmail.Text = empleado.email;
                    txtPass.Text = empleado.passw;
                    comboBoxRol.SelectedItem = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                    txtNombre.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtPass.IsEnabled = false;
                    comboBoxRol.IsEnabled = false;
                    btnCancelar.Visibility = Visibility.Hidden;
                    btnEditarZona.Visibility = Visibility.Hidden;
                    btnCancelar.IsEnabled = false;
                    btnEditarZona.IsEnabled = false;
                    btnEliminarZona.Visibility = Visibility.Visible;

                    if (btnPressed != null)
                    {
                        btnPressed.Background = Brushes.White;
                        btnPressed.Foreground = Brushes.Black;
                    }
                    btnPressed = btn;
                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                    btn.Foreground = Brushes.White;
                };

                btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) => {
                    if (btnPressed == null || btnPressed.Tag != btn.Tag)
                        btn.Background = (Brush)(new BrushConverter().ConvertFrom("#93d5d5"));
                };

                btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
                {
                    if (btnPressed == null || btnPressed.Tag != btn.Tag)
                        btn.Background = Brushes.White;
                };

                stackEmpleados.Children.Add(btn);
                btnPressed = btn;
                empleado = crearEmpleado.empleado;
                imgRolPressed = false;
                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                btn.Foreground = Brushes.White;
                txtAyuda.Visibility = Visibility.Hidden;
                txtNombre.Tag = empleado.name;
                txtEmail.Tag = empleado.email;
                txtPass.Tag = empleado.passw;
                comboBoxRol.Tag = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                txtNombre.Text = empleado.name;
                txtEmail.Text = empleado.email;
                txtPass.Text = empleado.passw;
                comboBoxRol.SelectedItem = !empleado.rol.Equals("Admin") ? comboBoxRol.Items[0] : comboBoxRol.Items[1];
                imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgNombre.Visibility = Visibility.Visible;
                imgEmail.Visibility = Visibility.Visible;
                imgPassword.Visibility = Visibility.Visible;
                imgRol.Visibility = Visibility.Visible;
                btnEliminarZona.Visibility = Visibility.Visible;
                btnCancelar.IsEnabled = false;
                btnCancelar.Visibility = Visibility.Hidden;
                btnEditarZona.IsEnabled = false;
                btnEditarZona.Visibility = Visibility.Hidden;
            }
        }

        private void imgNombre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtNombre.Tag != null && txtNombre.Text.Equals(txtNombre.Tag))
            {
                txtNombre.IsEnabled = true;
                imgNombre.Visibility = Visibility.Hidden;
            }
        }

        private bool validNombre(string nombre) => !String.IsNullOrEmpty(nombre) && !String.IsNullOrWhiteSpace(nombre);

        private bool isValidEmail(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        private bool isValidPass(string pass)
        {
            bool retorno = true;
            if (!String.IsNullOrEmpty(pass) && !String.IsNullOrWhiteSpace(pass))
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


        private async void btnEditarZona_Click(object sender, RoutedEventArgs e)
        {
            if(validNombre(txtNombre.Text) && isValidEmail(txtEmail.Text) && isValidPass(txtPass.Text))
            {
                empleado.name = txtNombre.Text;
                empleado.email = txtEmail.Text;
                empleado.passw = txtPass.Text;
                if(await User.updateUser(empleado._id, empleado))
                {
                    btnPressed.Content = empleado.name;
                    txtAyuda.Visibility = Visibility.Hidden;
                    txtNombre.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtPass.IsEnabled = false;
                    comboBoxRol.IsEnabled = false;
                    txtNombre.Tag = txtNombre.Text;
                    txtEmail.Tag = txtEmail.Tag;
                    txtPass.Tag = txtPass.Text;
                    comboBoxRol.Tag = comboBoxRol.SelectedItem;
                    imgRolPressed = false;
                    imgNombre.Visibility = Visibility.Visible;
                    imgEmail.Visibility = Visibility.Visible;
                    imgPassword.Visibility = Visibility.Visible;
                    imgRol.Visibility = Visibility.Visible;
                    imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    btnEditarZona.Visibility = Visibility.Hidden;
                    btnCancelar.Visibility = Visibility.Hidden;
                }
            }
        }

        private void imgEmail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtEmail.Tag != null && txtEmail.Text.Equals(txtEmail.Tag))
            {
                txtEmail.IsEnabled = true;
                imgEmail.Visibility = Visibility.Hidden;
            }
        }

        private void imgPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtPass.Tag != null && txtPass.Text.Equals(txtPass.Tag))
            {
                txtPass.IsEnabled = true;
                imgPassword.Visibility = Visibility.Hidden;
            }
        }

        private void imgRol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!imgRolPressed)
            {
                comboBoxRol.IsEnabled = true;
                imgRol.Visibility = Visibility.Hidden;
                imgRolPressed = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            imgRolPressed = false;
            btnCancelar.IsEnabled = true;
            btnEditarZona.IsEnabled = true;
            txtNombre.Text = empleado.name;
            txtEmail.Text = empleado.email;
            txtPass.Text = empleado.passw;
            comboBoxRol.SelectedItem = empleado.rol.Equals("Admin") ? comboBoxRol.Items[1] : comboBoxRol.Items[0];
            txtNombre.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPass.IsEnabled = false;
            comboBoxRol.IsEnabled = false;
            imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
            imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
            imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
            imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
            imgNombre.Visibility = Visibility.Visible;
            imgEmail.Visibility = Visibility.Visible;
            imgPassword.Visibility = Visibility.Visible;
            imgRol.Visibility = Visibility.Visible;
            btnEditarZona.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNombre.Tag != null)
            {
                if (!txtNombre.Text.Equals(txtNombre.Tag))
                {
                    btnEditarZona.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                    btnEditarZona.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    imgNombre.Visibility = Visibility.Visible;
                    imgNombre.Source = validNombre(txtNombre.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgNombre.ToolTip = validNombre(txtNombre.Text) ? null : "Debe introducir un nombre";
                }
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtEmail.Tag != null)
            {
                if(!txtEmail.Text.Equals(txtEmail.Tag))
                {
                    btnEditarZona.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                    btnEditarZona.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    imgEmail.Visibility = Visibility.Visible;
                    imgEmail.Source = isValidEmail(txtEmail.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgEmail.ToolTip = isValidEmail(txtEmail.Text) ? null : "El correo no es válido";
                }
            }
        }

        private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtPass.Tag != null)
            {
                if(!txtPass.Text.Equals(txtPass.Tag))
                {
                    btnEditarZona.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                    btnEditarZona.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    imgPassword.Visibility = Visibility.Visible;
                    if(String.IsNullOrEmpty(txtPass.Text) || String.IsNullOrWhiteSpace(txtPass.Text))
                    {
                        imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPassword.ToolTip = "Debe introducir una contraseña";
                    }else if(txtPass.Text.Contains(" "))
                    {
                        imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPassword.ToolTip = "La contraeña no puede incluir espacios";
                    }
                    else
                    {
                        try
                        {
                            int.Parse(txtPass.Text);
                            imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                            imgPassword.ToolTip = null;
                        }
                        catch(Exception ex)
                        {
                            imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                            imgPassword.ToolTip = "La contraeña debe ser numérica";
                        }
                    }
                }
            }
        }

        private void comboBoxRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxRol.Tag != null)
            {
                if(!comboBoxRol.Tag.Equals(comboBoxRol.SelectedItem))
                {
                    btnEditarZona.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;
                    btnEditarZona.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    imgRol.Visibility = Visibility.Visible;
                    imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                }
            }
        }

        private async void btnEliminarZona_Click(object sender, RoutedEventArgs e)
        {
            if(await User.deleteUser(empleado._id))
            {
                empleado = null;
                stackEmpleados.Children.Remove(btnPressed);
                btnPressed = null;
                imgRolPressed = false;
                btnEliminarZona.Visibility = Visibility.Collapsed;
                btnAgregar.Visibility = Visibility.Visible;
                txtAyuda.Visibility = Visibility.Visible;
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtPass.Text = "";
                comboBoxRol.Text = "";
                txtNombre.Tag = "";
                txtEmail.Tag = "";
                txtPass.Tag = "";
                comboBoxRol.Tag = "";
                txtNombre.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPass.IsEnabled = false;
                comboBoxRol.IsEnabled = false;
                imgNombre.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgEmail.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgPassword.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgRol.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgNombre.Visibility = Visibility.Visible;
                imgEmail.Visibility = Visibility.Visible;
                imgPassword.Visibility = Visibility.Visible;
                imgRol.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Hidden;
                btnEditarZona.Visibility = Visibility.Hidden;
                btnCancelar.IsEnabled = false;
                btnEditarZona.IsEnabled = false;

            }
        }
        private void btns_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btn.Foreground = Brushes.White;
            if (btn.Name.Equals("btnAgregar"))
            {
                imgAgregarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar_blanco.png");
            }
            else
            {
                imgEliminarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar_blanco.png");
            }
        }

        private void btns_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.White;
            btn.Foreground = Brushes.Black;
            if (btn.Name.Equals("btnAgregar"))
            {
                imgAgregarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar.png");
            }
            else
            {
                imgEliminarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar.png");
            }
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

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnEditarZona_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditarZona.Background = (Brush)(new BrushConverter().ConvertFrom("#63c554"));
            btnEditarZona.Foreground = Brushes.White;
            imgGuardarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\save_blanco.png");
        }

        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            btnCancelar.Foreground = Brushes.White;
            imgEliminarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel_blanco.png");
        }

        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = Brushes.White;
            btnCancelar.Foreground = Brushes.Black;
            imgEliminarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel.png");
        }

        private void btnEditarZona_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditarZona.Background = Brushes.White;
            btnEditarZona.Foreground = Brushes.Black;
            imgGuardarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\save.png");
        }
    }
}
