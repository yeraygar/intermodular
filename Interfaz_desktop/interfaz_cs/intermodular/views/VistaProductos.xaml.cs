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
    /// Lógica de interacción para VistaProductos.xaml
    /// </summary>
    public partial class VistaProductos : Window
    {
        private Familia selectedFamilia;
        private Button btnFamiliaSelect;
        public VistaProductos()
        {
            InitializeComponent();
            cargarFamilias();
        }

        private void btnAgregarFamilia_Click(object sender, RoutedEventArgs e)
        {
            resetearVistaNuevaFamilia();
            CrearFamiliaProducto crearFamilia = new CrearFamiliaProducto(null);
            crearFamilia.ShowDialog();
            if(crearFamilia.familia != null)
            {
                agregarFamiliaStackPanel(crearFamilia.familia);
            }
        }


        private void agregarFamiliaStackPanel(Familia familia)
        {

            Button btn = new Button
            {
                Height = 70,
                Content = familia.name,
                Tag = familia._id,
                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                Cursor = Cursors.Hand,
                Margin = new Thickness(10),
                FontSize = 19
            };

            //Creamos un Evento click para cada botón que se crea y asignamos un valor u otro a ciertos elementos, dependiendo de la información que devuelve cada zona de la BD
            btn.Click += (object send, RoutedEventArgs a) =>
            {
                selectedFamilia = familia;
                Familia.currentFamilia = familia;
                btnEditarFamilia.Visibility = Visibility.Visible;
                btnEliminarFamilia.Visibility = Visibility.Visible;

                if (btnFamiliaSelect != null)
                {
                    btnFamiliaSelect.Background = Brushes.White;
                    btnFamiliaSelect.Foreground = Brushes.Black;
                }
                btnFamiliaSelect = btn;
                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                btn.Foreground = Brushes.White;
                contenedorBtnsProd.Visibility = Visibility.Visible;
                btnAgregarProd.Visibility = Visibility.Visible;
                tituloFamiliaProducto.Text = selectedFamilia.name;
            };

            btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) => {
                if (btnFamiliaSelect == null || btnFamiliaSelect.Tag != btn.Tag)
                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#93d5d5"));
            };

            btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
            {
                if (btnFamiliaSelect == null || btnFamiliaSelect.Tag != btn.Tag)
                    btn.Background = Brushes.White;
            };
            
            stackFamilias.Children.Add(btn);
            if (btnFamiliaSelect != null)
            {
                btnFamiliaSelect.Background = Brushes.White;
                btnFamiliaSelect.Foreground = Brushes.Black;
            }
            btnFamiliaSelect = btn;
            btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
            btn.Foreground = Brushes.White;
            selectedFamilia = familia;
            Familia.currentFamilia = familia;
            contenedorBtnsProd.Visibility = Visibility.Visible;
            btnAgregarProd.Visibility = Visibility.Visible;
            btnEditarFamilia.Visibility = Visibility.Visible;
            btnEliminarFamilia.Visibility = Visibility.Visible;
            tituloFamiliaProducto.Text = selectedFamilia.name;
        }

        private async void cargarFamilias()
        {
            if (await Familia.getClientFamilies())
            {
                if(Familia.clientFamilias != null)
                { if (Familia.clientFamilias.Count != 0)
                    {
                        foreach (Familia f in Familia.clientFamilias)
                        {
                            Button btn = new Button
                            {
                                Height = 70,
                                Content = f.name,
                                Tag = f._id,
                                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                                Cursor = Cursors.Hand,
                                Margin = new Thickness(10,30,10,0),
                                FontSize = 19
                            };

                            //Creamos un Evento click para cada botón que se crea y asignamos un valor u otro a ciertos elementos, dependiendo de la información que devuelve cada zona de la BD
                            btn.Click += (object send, RoutedEventArgs a) =>
                            {
                                selectedFamilia = f;
                                Familia.currentFamilia = f;
                                btnEditarFamilia.Visibility = Visibility.Visible;
                                btnEliminarFamilia.Visibility = Visibility.Visible;

                                if (btnFamiliaSelect != null)
                                {
                                    btnFamiliaSelect.Background = Brushes.White;
                                    btnFamiliaSelect.Foreground = Brushes.Black;
                                }
                                btnFamiliaSelect = btn;
                                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                                btn.Foreground = Brushes.White;
                                contenedorBtnsProd.Visibility = Visibility.Visible;
                                btnAgregarProd.Visibility = Visibility.Visible;
                                tituloFamiliaProducto.Text = selectedFamilia.name;
                            };

                            btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) =>
                            {
                                if (btnFamiliaSelect == null || btnFamiliaSelect.Tag != btn.Tag)
                                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#93d5d5"));
                            };

                            btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
                            {
                                if (btnFamiliaSelect == null || btnFamiliaSelect.Tag != btn.Tag)
                                    btn.Background = Brushes.White;
                            };

                            stackFamilias.Children.Add(btn);
                        }
                    }
                }
            }
            else
            {
                //Mostrar Error
                MessageBox.Show("Error al cargar la BD");
            }
        }

        private void btnEditarFamilia_Click(object sender, RoutedEventArgs e)
        {
            string nomFam = selectedFamilia.name;
            CrearFamiliaProducto editarFamilia = new CrearFamiliaProducto(selectedFamilia);
            editarFamilia.ShowDialog();
            if (!nomFam.Equals(editarFamilia.familia.name))
            {
                btnFamiliaSelect.Content = editarFamilia.familia.name;
            }
        }

        private void resetearVistaNuevaFamilia()
        {
            btnEditarFamilia.Visibility = Visibility.Collapsed;
            btnEliminarFamilia.Visibility = Visibility.Collapsed;
            btnAgregarProd.Visibility = Visibility.Collapsed;
            btnEditarProd.Visibility = Visibility.Collapsed;
            btnEliminarProd.Visibility = Visibility.Collapsed;
            contenedorBtnsProd.Visibility = Visibility.Hidden;
            tituloFamiliaProducto.Text = "Productos";
            Grid grid = new Grid();
            for(int x = 0; x < 3; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            contenedorGridProductos.Child = grid;
        }

        private async void btnEliminarFamilia_Click(object sender, RoutedEventArgs e)
        {
            if(await Familia.deleteFamily(selectedFamilia._id))
            {
                Familia.clientFamilias.Remove(selectedFamilia);
                Familia.currentFamilia = null;
                stackFamilias.Children.Remove(btnFamiliaSelect);
                btnFamiliaSelect = null;
                selectedFamilia = null;
                resetearVistaNuevaFamilia();
            }
            else
            {
                //Mostramos un error
                MessageBox.Show("Error al eliminar la familia de productos");
            }
        }

        private void btnAgregarProd_Click(object sender, RoutedEventArgs e)
        {
            CrearProductos crearProd = new CrearProductos(selectedFamilia._id,null);
            crearProd.ShowDialog();
            if(crearProd.producto != null)
            {
                agregarNuevoBtnProducto(crearProd.producto);
            }
        }


        private void agregarNuevoBtnProducto(Producto prod)
        {

        }
    }
}
