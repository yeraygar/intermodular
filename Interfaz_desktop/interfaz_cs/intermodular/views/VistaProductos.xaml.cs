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
using System.Windows.Media.Effects;
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
        private Button btnProdSelect;
        private Producto productoSelect;
        private Label lblProdSelect;
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
            btn.Click += async (object send, RoutedEventArgs a) =>
            {
                resetearVistaNuevaFamilia();
                selectedFamilia = familia;
                Familia.currentFamilia = familia;
                try
                {
                    await Producto.getFamilyProducts(familia._id);
                    if (Producto.familyProducts.Count > 0)
                    {
                        cargarProductos();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al cargar los productos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

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
            Producto.familyProducts = new List<Producto>();
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
                            btn.Click += async (object send, RoutedEventArgs a) =>
                            {
                                resetearVistaNuevaFamilia();
                                selectedFamilia = f;
                                Familia.currentFamilia = f;
                                try
                                {
                                    await Producto.getFamilyProducts(f._id);
                                    if(Producto.familyProducts.Count > 0)
                                    {
                                        cargarProductos();
                                    }
                                }catch(Exception e)
                                {
                                    MessageBox.Show("Error al cargar los productos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

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
                tituloFamiliaProducto.Text = editarFamilia.familia.name;
            }
        }

        private void resetearVistaNuevaFamilia()
        {
            if(btnFamiliaSelect != null)
            {
                btnFamiliaSelect.Background = Brushes.White;
                btnFamiliaSelect.Foreground = Brushes.Black;
                btnFamiliaSelect = null;
                selectedFamilia = null;
            }
            btnEditarFamilia.Visibility = Visibility.Collapsed;
            btnEliminarFamilia.Visibility = Visibility.Collapsed;
            btnAgregarProd.Visibility = Visibility.Collapsed;
            btnEditarProd.Visibility = Visibility.Collapsed;
            btnEliminarProd.Visibility = Visibility.Collapsed;
            contenedorBtnsProd.Visibility = Visibility.Hidden;
            tituloFamiliaProducto.Text = "Productos";
            Grid grid = new Grid();
            for (int x = 0; x < 3; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            grid.RowDefinitions.Add(new RowDefinition());
            grid.Name = "gridProductos";
            gridProductos = grid;
            ScrollViewer scroll = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                CanContentScroll = true,
                Content = gridProductos
            };
            contenedorGridProductos.Child = scroll;
        }

        private async void btnEliminarFamilia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await Producto.deleteAllFamilyProducts(selectedFamilia._id))
                {
                    if (await Familia.deleteFamily(selectedFamilia._id))
                    {
                        Familia.clientFamilias.Remove(selectedFamilia);
                        Familia.currentFamilia = null;
                        stackFamilias.Children.Remove(btnFamiliaSelect);
                        btnFamiliaSelect = null;
                        selectedFamilia = null;
                        btnEditarFamilia.Visibility = Visibility.Collapsed;
                        btnEliminarFamilia.Visibility = Visibility.Collapsed;
                        btnAgregarProd.Visibility = Visibility.Collapsed;
                        btnEditarProd.Visibility = Visibility.Collapsed;
                        btnEliminarFamilia.Visibility = Visibility.Collapsed;
                        tituloFamiliaProducto.Text = "Productos";
                        resetearVistaNuevaFamilia();
                    }
                    else
                    {
                        //Mostramos un error
                        MessageBox.Show("Error al eliminar la familia de productos");
                    }
                }
                else
                {
                    MessageBox.Show("Error al eliminar los productos de la categoría", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error al conectarse a la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            Button btn = new Button
            {
                Tag = prod._id,
                Margin = new Thickness(0, 50, 0, 0),
                Width = 200,
                Height = 200,
                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                Background = Brushes.White,
                FontSize = 15,
                Cursor = Cursors.Hand,
                VerticalAlignment = VerticalAlignment.Top,
                Effect = new DropShadowEffect
                {
                    Color = new Color { R = 27, G = 27, B = 27 },
                    Direction = 270,
                    ShadowDepth = 5,
                    Opacity = 0.3

                },
            };
            Grid grid = new Grid();
            RowDefinition fila1 = new RowDefinition();
            fila1.Height = new GridLength(80, GridUnitType.Star);
            RowDefinition fila2 = new RowDefinition();
            fila2.Height = new GridLength(20, GridUnitType.Star);
            grid.RowDefinitions.Add(fila1);
            grid.RowDefinitions.Add(fila2);
            //Creamos la imagen y el texto que va a ir dentro del grid del producto
            Label lbl = new Label
            {
                Name = "lblProd",
                Content = prod.name,
                FontSize = 15,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            Grid.SetRow(lbl, 1);
            grid.Children.Add(lbl);
            btn.Content = grid;


            btn.Click += (object sender, RoutedEventArgs rea) =>
            {
                if(btnProdSelect != null)
                {
                    btnProdSelect.Background = Brushes.White;
                    btnProdSelect.Foreground = Brushes.Black;
                }
                lblProdSelect = lbl;
                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                btn.Foreground = Brushes.White;
                btnProdSelect = btn;
                btnProdSelect.Tag = btn.Tag;
                productoSelect = prod;
                btnEditarProd.Visibility = Visibility.Visible;
                btnEliminarProd.Visibility = Visibility.Visible;
            };

            btn.MouseEnter += (object sender, MouseEventArgs MouseEnterevent) =>
            {
                if (btnProdSelect == null || !btnProdSelect.Tag.Equals(btn.Tag))
                {

                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#51aaa7"));
                    btn.Foreground = Brushes.White;
                }
            };

            btn.MouseLeave += (object sender, MouseEventArgs MouseLeaveEvent) =>
            {
                if (btnProdSelect == null || !btnProdSelect.Tag.Equals(btn.Tag))
                {
                    btn.Background = Brushes.White;
                }
            };
            if (Producto.familyProducts != null)
            {
                if (Producto.familyProducts.Count == 0)
                {
                    Grid.SetColumn(btn, 0);
                    Grid.SetRow(btn, 0);
                    gridProductos.Children.Add(btn);
                }
                else
                {
                    int lastCol = Grid.GetColumn(gridProductos.Children[gridProductos.Children.Count - 1]);
                    int lastRow = Grid.GetRow(gridProductos.Children[gridProductos.Children.Count - 1]);
                    if (lastCol == 2)
                    {
                        lastCol = 0;
                        lastRow++;
                        gridProductos.RowDefinitions.Add(new RowDefinition());
                    }
                    else
                    {
                        lastCol++;
                    }
                    Grid.SetColumn(btn, lastCol);
                    Grid.SetRow(btn, lastRow);
                    gridProductos.Children.Add(btn);
                }
            }
            Producto.familyProducts.Add(prod);
        }

        private void cargarProductos()
        {
            int row = 0;
            int column = -1;
            foreach(Producto p in Producto.familyProducts)
            {
                Button btn = new Button
                {
                    Background = Brushes.White,
                    Margin = new Thickness(0, 50, 0, 0),
                    Width = 200,
                    Height = 200,
                    Tag = p._id,
                    Style = Application.Current.TryFindResource("btnRedondo") as Style,
                    FontSize = 15,
                    Cursor = Cursors.Hand,
                    VerticalAlignment = VerticalAlignment.Top,
                    Effect = new DropShadowEffect
                    {
                        Color = new Color { R = 27, G = 27, B = 27 },
                        Direction = 270,
                        ShadowDepth = 5,
                        Opacity = 0.3

                    },
                };

                Grid grid = new Grid();
                RowDefinition fila1 = new RowDefinition();
                fila1.Height = new GridLength(80, GridUnitType.Star);
                RowDefinition fila2 = new RowDefinition();
                fila2.Height = new GridLength(20, GridUnitType.Star);
                grid.RowDefinitions.Add(fila1);
                grid.RowDefinitions.Add(fila2);
                //Creamos la imagen y el texto que va a ir dentro del grid del producto
                Label lbl = new Label
                {
                    Name = "lblProd",
                    Content = p.name,
                    FontSize = 15,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                Grid.SetRow(lbl, 1);
                grid.Children.Add(lbl);
                btn.Content = grid;

                btn.Click += (object sender, RoutedEventArgs rea) =>
                {
                    if (btnProdSelect != null)
                    {
                        btnProdSelect.Background = Brushes.White;
                        btnProdSelect.Foreground = Brushes.Black;
                    }
                    lblProdSelect = lbl;
                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                    btn.Foreground = Brushes.White;
                    btnProdSelect = btn;
                    btnProdSelect.Tag = btn.Tag;
                    productoSelect = p;
                    btnEditarProd.Visibility = Visibility.Visible;
                    btnEliminarProd.Visibility = Visibility.Visible;
                };

                btn.MouseEnter += (object sender, MouseEventArgs MouseEnterevent) =>
                {
                    if (btnProdSelect == null || !btnProdSelect.Tag.Equals(btn.Tag))
                    {

                        btn.Background = (Brush)(new BrushConverter().ConvertFrom("#51aaa7"));
                        btn.Foreground = Brushes.White;
                    }
                };

                btn.MouseLeave += (object sender, MouseEventArgs MouseLeaveEvent) =>
                {
                    if (btnProdSelect == null || !btnProdSelect.Tag.Equals(btn.Tag))
                    {
                        btn.Background = Brushes.White;
                        btn.Foreground = Brushes.Black;
                    }
                };
                if (column == 2)
                {
                    gridProductos.RowDefinitions.Add(new RowDefinition());
                    row++;
                    column = 0;
                }
                else
                {
                    column++;
                }
                Grid.SetColumn(btn, column);
                Grid.SetRow(btn, row);
                gridProductos.Children.Add(btn);
            }
        }

        private void btnEditarProd_Click(object sender, RoutedEventArgs e)
        {
            CrearProductos updateProd = new CrearProductos(null, productoSelect);
            updateProd.ShowDialog();
            if(updateProd.isProdUpdated)
            {
                productoSelect = updateProd.producto;
                lblProdSelect.Content = productoSelect.name;
                Producto.currentProduct = productoSelect;
            }
        }

        private async void btnEliminarProd_Click(object sender, RoutedEventArgs e)
        {
            if(await Producto.deleteProduct(productoSelect._id))
            {
                MessageBox.Show("Producto eliminado", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                Producto.familyProducts.Remove(productoSelect);
                gridProductos.Children.Remove(btnProdSelect);
                btnProdSelect = null;
                productoSelect = null;
                btnEditarProd.Visibility = Visibility.Collapsed;
                btnEliminarProd.Visibility = Visibility.Collapsed;
                Button btn = btnFamiliaSelect;
                Familia familia = selectedFamilia;
                resetearVistaNuevaFamilia();
                contenedorBtnsProd.Visibility = Visibility.Visible;
                btnAgregarProd.Visibility = Visibility.Visible;
                btnAgregarFamilia.Visibility = Visibility.Visible;
                btnEditarFamilia.Visibility = Visibility.Visible;
                btnEliminarFamilia.Visibility = Visibility.Visible;
                cargarProductos();
                btnFamiliaSelect = btn;
                selectedFamilia = familia;
                btnFamiliaSelect.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                btnFamiliaSelect.Foreground = Brushes.White; 
            }
            else
            {
                MessageBox.Show("Error al eliminar el producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAgregarFamilia_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAgregarFamilia.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnAgregarFamilia.Foreground = Brushes.White;
            imgAgregarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar_blanco.png");
        }

        private void btnEditarFamilia_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditarFamilia.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnEditarFamilia.Foreground = Brushes.White;
            imgEditarFamilia.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil_blanco.png");
        }

        private void btnEliminarFamilia_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminarFamilia.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnEliminarFamilia.Foreground = Brushes.White;
            imgEliminarFamilia.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar_blanco.png");
        }

        private void btnAgregarFamilia_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAgregarFamilia.Background = Brushes.White;
            btnAgregarFamilia.Foreground = Brushes.Black;
            imgAgregarZona.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar.png");
        }

        private void btnEditarFamilia_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditarFamilia.Background = Brushes.White;
            btnEditarFamilia.Foreground = Brushes.Black;
            imgEditarFamilia.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
        }

        private void btnEliminarFamilia_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEliminarFamilia.Background = Brushes.White;
            btnEliminarFamilia.Foreground = Brushes.Black;
            imgEliminarFamilia.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar.png");
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

        private void btnAgregarProd_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAgregarProd.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnAgregarProd.Foreground = Brushes.White;
            imgAgregarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar_blanco.png");
        }
        private void btnAgregarProd_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAgregarProd.Background = Brushes.White;
            btnAgregarProd.Foreground = Brushes.Black;
            imgAgregarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\agregar.png");
        }

        private void btnEditarProd_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditarProd.Background = Brushes.White;
            btnEditarProd.Foreground = Brushes.Black;
            imgEditarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil.png");
        }

        private void btnEliminarProd_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminarProd.Background = Brushes.White;
            btnEliminarProd.Foreground = Brushes.Black;
            imgEliminarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar.png");
        }

        private void btnEditarProd_MouseEnter_1(object sender, MouseEventArgs e)
        {
            btnEditarProd.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnEditarProd.Foreground = Brushes.White;
            imgEditarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\pencil_blanco.png");
        }

        private void btnEliminarProd_MouseEnter_1(object sender, MouseEventArgs e)
        {
            btnEliminarProd.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
            btnEliminarProd.Foreground = Brushes.White;
            imgEliminarProd.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar_blanco.png");
        }
    }
}

