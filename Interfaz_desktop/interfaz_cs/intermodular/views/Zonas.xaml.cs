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
    /// Lógica de interacción para Zonas.xaml
    /// </summary>
    public partial class Zonas : Window
    {
        private Zona _zonaSelect;
        private Button _btnPressed;

        public Button btnPressed
        {
            get
            {
                return _btnPressed;
            }
            set
            {
                _btnPressed = value;
            }
        }
        public Zona zonaSelect
        {
            get
            {
                return _zonaSelect;
            }set
            {
                _zonaSelect = value;
            }
        }
        public Zonas()
        {
            InitializeComponent();
            { 
            //Obtenemos todas las Zonas que hay en la BD y creamos un botón por cada Zona encontrada
               Zona.getAllZones().ContinueWith(task =>
                {
                    if (Zona.allZones != null)
                    {
                        foreach (Zona z in Zona.allZones)
                        {
                            Button btn = new Button
                            {
                                Height = 70,
                                Content = z.zone_name,
                                Tag = z._id,
                                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                                Cursor = Cursors.Hand,
                                Margin = new Thickness(10),
                                FontSize = 19
                            };

                        //Creamos un Evento click para cada botón que se crea y asignamos un valor u otro a ciertos elementos, dependiendo de la información que devuelve cada zona de la BD
                        btn.Click += (object send, RoutedEventArgs a) =>
                            {
                                zonaSelect = z;
                                txtAyuda.Visibility = Visibility.Hidden;
                                imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                                imgEditNombreZona.Visibility = Visibility.Visible;
                                txtEditarNombreZona.Tag = z.zone_name;
                                txtEditarNombreZona.Text = z.zone_name;
                                txtEditarNombreZona.IsEnabled = false;
                                btnCancelar.Visibility = Visibility.Hidden;
                                btnEditarZona.Visibility = Visibility.Hidden;
                                btnCancelar.IsEnabled = false;
                                btnEditarZona.IsEnabled = false;
                                btnEliminarZona.Visibility = Visibility.Visible;
                                
                                if(btnPressed != null)
                                {
                                    btnPressed.Background = Brushes.White;
                                    btnPressed.Foreground = Brushes.Black;
                                }
                                btnPressed = btn;
                                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                                btn.Foreground = Brushes.White;
                            };

                            btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) => {
                                if(btnPressed == null || btnPressed.Tag != btn.Tag) 
                                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#93d5d5"));
                            };

                            btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
                            {
                                if (btnPressed == null || btnPressed.Tag != btn.Tag)
                                    btn.Background = Brushes.White;
                            };

                            stackZonas.Children.Add(btn);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar la BD");
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Si el btnPressed no es igual a nulo entonces cambiamos su color.
            if (btnPressed != null)
            {
                btnPressed.Background = Brushes.White;
                btnPressed.Foreground = Brushes.Black;
            }
            btnEliminarZona.Visibility = Visibility.Collapsed;
            btnPressed = null;
            zonaSelect = null;
            btnCancelar.IsEnabled = false;
            btnCancelar.Visibility = Visibility.Hidden;
            btnEditarZona.IsEnabled = false;
            btnEditarZona.Visibility = Visibility.Hidden;
            txtAyuda.Visibility = Visibility.Visible;
            txtEditarNombreZona.Tag = "";
            txtEditarNombreZona.Text = "";
            txtEditarNombreZona.IsEnabled = false;
            imgEditNombreZona.Visibility = Visibility.Hidden;
            CrearZona crearzona = new CrearZona();
            crearzona.ShowDialog(); 
            if (crearzona.zona != null)
            {
                Button btn = new Button
                {
                    Height = 70,
                    Content = crearzona.zona.zone_name,
                    Tag = crearzona.zona._id,
                    Style = Application.Current.TryFindResource("btnRedondo") as Style,
                    Cursor = Cursors.Hand,
                    Margin = new Thickness(10),
                    FontSize = 19

                };

                btn.Click += (object send, RoutedEventArgs a) =>
                {
                    zonaSelect = crearzona.zona;
                    txtAyuda.Visibility = Visibility.Hidden;
                    imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgEditNombreZona.Visibility = Visibility.Visible;
                    txtEditarNombreZona.Tag = crearzona.zona.zone_name;
                    txtEditarNombreZona.Text = crearzona.zona.zone_name;
                    txtEditarNombreZona.IsEnabled = false;
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
                
                stackZonas.Children.Add(btn);
                Staticresources.mainWindow.addBtnZona(crearzona.zona);
                btnPressed = btn;
                zonaSelect = crearzona.zona;
                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                btn.Foreground = Brushes.White;
                txtAyuda.Visibility = Visibility.Hidden;
                txtEditarNombreZona.Tag = zonaSelect.zone_name;
                txtEditarNombreZona.Text = zonaSelect.zone_name;
                imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                imgEditNombreZona.Visibility = Visibility.Visible;
                btnEliminarZona.Visibility = Visibility.Visible;
                btnEditarZona.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
                btnEditarZona.IsEnabled = false;
                btnCancelar.IsEnabled = false;
            }
        }

        private async void btnEditarZona_Click(object sender, RoutedEventArgs e)
        {
            //Aplicar los cambios a la zona seleccionada

            //Comprobamos que la zona Seleccionada no es null, para evitar posibles errores.
            if(zonaSelect != null)
            {

                    //Comprobamos que los datos introducidos son válidos
                if(validZoneName(txtEditarNombreZona.Text))
                {
                    zonaSelect.zone_name = txtEditarNombreZona.Text;
                    
                    //Realizamos el update y comprobamos que el update se hizo correctamente
                    if(await Zona.updateZona(zonaSelect._id,zonaSelect))
                    {
                        //Reseteamos todos los campos con la información actualizada
                        Staticresources.mainWindow.updateZona(zonaSelect);
                        btnPressed.Content = zonaSelect.zone_name;
                        txtAyuda.Visibility = Visibility.Hidden;
                        txtEditarNombreZona.Text = zonaSelect.zone_name;
                        txtEditarNombreZona.IsEnabled = false;
                        imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                        imgEditNombreZona.Visibility = Visibility.Visible;
                        btnCancelar.Visibility = Visibility.Hidden;
                        btnEditarZona.Visibility = Visibility.Hidden;
                        btnEliminarZona.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //Error conexión API
                        MessageBox.Show("No se pudo acceder a la base de datos");
                    }
                }
                else
                {
                    MessageBox.Show("Error al actualizar la zona(Campos no válidos)");
                }
            }

            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //Resetear info de la zona
            txtEditarNombreZona.Text = zonaSelect.zone_name;
            txtEditarNombreZona.IsEnabled = false;
            imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
            imgEditNombreZona.Visibility = Visibility.Visible;
            btnCancelar.IsEnabled = false;
            btnEditarZona.IsEnabled = false;
            btnCancelar.Visibility = Visibility.Hidden;
            btnEditarZona.Visibility = Visibility.Hidden;
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

        private void imgEditNombreZona_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                imgEditNombreZona.Visibility = Visibility.Hidden;
                txtEditarNombreZona.IsEnabled = true;
            }
        }

        private void txtEditarNombreZona_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtEditarNombreZona.Tag != null)
            {
                if(!txtEditarNombreZona.Text.Equals(txtEditarNombreZona.Tag))
                {
                    btnCancelar.Visibility = Visibility.Visible;
                    btnEditarZona.Visibility = Visibility.Visible;
                    btnCancelar.IsEnabled = true;
                    btnEditarZona.IsEnabled = true;
                    imgEditNombreZona.Visibility = Visibility.Visible;
                    imgEditNombreZona.Source = validZoneName(txtEditarNombreZona.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                }
            }
        }



        private async void btnEliminarZona_Click(object sender, RoutedEventArgs e)
        {
            //Comprobar que hay una zona seleccionada
            if(btnPressed != null && zonaSelect != null)
            {
                //Mostramos un mensaje de confirmación para eliminar la zona

                //Eliminamos la zona de la base de datos

                if( await Zona.deleteZone(zonaSelect._id))
                {
                    await Mesa.removeZoneTables(zonaSelect._id);
                    //Eliminar el botón que hace referencia a esa zona, de la ventana principal del tpv
                    //Eliminar el botón que hace referencia a esa zona del panel
                    stackZonas.Children.Remove(btnPressed);

                    //Eliminar la zona del ArrayList
                    Zona.allZones.RemoveAt(Zona.allZones.IndexOf(zonaSelect));
                    Staticresources.mainWindow.eliminarZona(zonaSelect._id);

                    //Resetear todos los campos al estado principal
                    if (btnPressed != null)
                    {
                        btnPressed.Background = Brushes.White;
                        btnPressed.Foreground = Brushes.Black;
                    }
                    btnPressed = null;
                    zonaSelect = null;
                    txtAyuda.Visibility = Visibility.Visible;
                    txtEditarNombreZona.IsEnabled = false;
                    txtEditarNombreZona.Text = "";
                    imgEditNombreZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
                    imgEditNombreZona.Visibility = Visibility.Hidden;
                    btnEditarZona.IsEnabled = false;
                    btnCancelar.IsEnabled = false;
                    btnEditarZona.Visibility = Visibility.Hidden;
                    btnCancelar.Visibility = Visibility.Hidden;
                    btnEliminarZona.Visibility = Visibility.Collapsed;

                }
                else
                {
                    //Error al borrar en la base de datos, mostrar un error.
                    MessageBox.Show("Error al borrar la zona.");
                }
                
            }
           
        }

        private bool validZoneName(string zoneName) => !String.IsNullOrEmpty(zoneName) && !String.IsNullOrWhiteSpace(zoneName);



        private void btns_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btn.Foreground = Brushes.White;
            if(btn.Name.Equals("btnAgregar"))
            {
                imgAgregarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar_blanco.png");
            }else
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
    }
}
