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
    /// Lógica de interacción para vistaPedidos.xaml
    /// </summary>
    public partial class vistaPedidos : Window
    {
        private Familia selectedFamilia;
        private Button btnFamiliaSelect;
        private Button btnProdSelect;
        private Label lblProdSelect;
        private Producto productoSelect;
        private bool firstProdSelected = true;
        private int comensales;
        public vistaPedidos(string nomCamarero,string numComensales)
        {
            InitializeComponent();
            lblCamarero.Content += nomCamarero;
            lblComensales.Content += numComensales;
            this.comensales = int.Parse(numComensales);
            lblFecha.Content = calcularFecha();
            cargarFamiliasProd();
        }
       private async void cargarFamiliasProd()
        {
            if (await Familia.getClientFamilies())
            {
                if (Familia.clientFamilias != null)
                {
                    if (Familia.clientFamilias.Count != 0)
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
                                Margin = new Thickness(10, 30, 10, 0),
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
                                    if (Producto.familyProducts.Count > 0)
                                    {
                                        cargarProductos();
                                    }
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show("Error al cargar los productos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }


                                if (btnFamiliaSelect != null)
                                {
                                    btnFamiliaSelect.Background = Brushes.White;
                                    btnFamiliaSelect.Foreground = Brushes.Black;
                                }
                                btnFamiliaSelect = btn;
                                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#3b7a7a"));
                                btn.Foreground = Brushes.White;
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
        private void resetearVistaNuevaFamilia()
        {
            if (btnFamiliaSelect != null)
            {
                btnFamiliaSelect.Background = Brushes.White;
                btnFamiliaSelect.Foreground = Brushes.Black;
                btnFamiliaSelect = null;
                selectedFamilia = null;
            }
            Grid grid = new Grid();
            for(int x = 0; x < 6; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            grid.RowDefinitions.Add(new RowDefinition());
            gridProductos = grid;
            scrollProductos.Content = gridProductos;
        }

        private async void cargarProductos()
        {
            int row = 0;
            int column = -1;
            foreach (Producto p in Producto.familyProducts)
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

                btn.Click +=  async (object sender, RoutedEventArgs rea) =>
                {
                    btnProdSelect = btn;
                    productoSelect = p;
                    Producto.currentProduct = productoSelect;
                    //Añadimos el producto a una línea de pedido
                    if(firstProdSelected)
                    {
                        //Se crea el ticket
                        Ticket ticket = new Ticket(comensales);
                        try
                        {
                            if(await Ticket.createTicket(ticket))
                            {
                                //Crear La línea de ticket
                                try
                                {
                                    if(await Producto.createLineTicket(productoSelect))
                                    {
                                       
                                    }
                                    stackTicket.Children.Clear();
                                    foreach (Producto linea in Producto.ticketLines)
                                    {
                                        //Crear Botón de linea de producto
                                        Button lBtn = new Button
                                        {
                                            Margin = new Thickness(5),
                                            Style = Application.Current.TryFindResource("btnRedondo") as Style

                                        };
                                        Grid gridLinea = new Grid();
                                        for (int x = 0; x < 4; x++)
                                        {
                                            gridLinea.ColumnDefinitions.Add(new ColumnDefinition());
                                        }
                                        gridLinea.ColumnDefinitions[0].Width = new GridLength(20, GridUnitType.Star);
                                        gridLinea.ColumnDefinitions[1].Width = new GridLength(40, GridUnitType.Star);
                                        gridLinea.ColumnDefinitions[2].Width = new GridLength(20, GridUnitType.Star);
                                        gridLinea.ColumnDefinitions[3].Width = new GridLength(20, GridUnitType.Star);

                                        Label lblCantidad = new Label
                                        {
                                            Content = linea.cantidad,
                                            FontSize = 19,
                                            VerticalAlignment = VerticalAlignment.Center,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                            Margin = new Thickness(2, 0, 0, 0)
                                        };

                                        Label lblNombre = new Label
                                        {
                                            Content = linea.name,
                                            FontSize = 19,
                                            VerticalAlignment = VerticalAlignment.Center,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                        };

                                        Label lblPrecioUnidad = new Label
                                        {
                                            Content = linea.precio,
                                            FontSize = 19,
                                            VerticalAlignment = VerticalAlignment.Center,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                        };

                                        Label lblTotal = new Label
                                        {
                                            Content = (float)(Math.Truncate((double)(linea.cantidad * linea.precio) * 100.0) / 100.0),
                                            FontSize = 19,
                                            VerticalAlignment = VerticalAlignment.Center,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                        };

                                        Image img = new Image
                                        {
                                            Width = 50,
                                            Height = 50,
                                            VerticalAlignment = VerticalAlignment.Center,
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                            Cursor = Cursors.Hand
                                        };

                                        Grid gridNombre = new Grid();
                                        for (int x = 0; x < 2; x++)
                                        {
                                            gridNombre.ColumnDefinitions.Add(new ColumnDefinition());
                                        }
                                        gridNombre.ColumnDefinitions[0].Width = new GridLength(80, GridUnitType.Star);
                                        gridNombre.ColumnDefinitions[1].Width = new GridLength(20, GridUnitType.Star);
                                        Grid.SetColumn(lblNombre, 0);
                                        Grid.SetColumn(img, 1);
                                        gridNombre.Children.Add(lblNombre);
                                        gridNombre.Children.Add(img);

                                        Grid.SetColumn(lblCantidad, 0);
                                        Grid.SetColumn(gridNombre, 1);
                                        Grid.SetColumn(lblPrecioUnidad, 2);
                                        Grid.SetColumn(lblTotal, 3);
                                        gridLinea.Children.Add(lblCantidad);
                                        gridLinea.Children.Add(gridNombre);
                                        gridLinea.Children.Add(lblPrecioUnidad);
                                        gridLinea.Children.Add(lblTotal);
                                        lBtn.Content = gridLinea;
                                        stackTicket.Children.Add(lBtn);
                                    }

                                }
                                catch(Exception e)
                                {
                                    MessageBox.Show("Error al acceder a la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("Error al crear el Ticket", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Error al acceder a la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                     
                    }
                    else { 
                    }

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

        private string calcularFecha() => DateTime.UtcNow.ToString("dd/MM/yyyy");

        private void btnCobrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCobrar.Background = (Brush)(new BrushConverter().ConvertFrom("#327784"));
            lblCobrar.Foreground = Brushes.White;
            imgCobrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\totales_blanco.png");
        }

        private void btnComensales_MouseEnter(object sender, MouseEventArgs e)
        {
            btnComensales.Background = (Brush)(new BrushConverter().ConvertFrom("#327784"));
            lblComensal.Foreground = Brushes.White;
            imgComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\comensal_blanco.png");
        }

        private void btnEliminarLinea_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminarLinea.Background = (Brush)(new BrushConverter().ConvertFrom("#327784"));
            lblEliminarLinea.Foreground = Brushes.White;
            imgEliminarLinea.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel_blanco.png");
        }

        private void btnEliminarPedido_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminarPedido.Background = (Brush)(new BrushConverter().ConvertFrom("#327784"));
            lblBorrarPedido.Foreground = Brushes.White;
            imgEliminarPedido.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar_blanco.png");
        }

        private void btnCobrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCobrar.Background = (Brush)(new BrushConverter().ConvertFrom("#97cfda"));
            lblCobrar.Foreground = Brushes.Black;
            imgCobrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\totales.png");
        }

        private void btnComensales_MouseLeave(object sender, MouseEventArgs e)
        {
            btnComensales.Background = (Brush)(new BrushConverter().ConvertFrom("#97cfda"));
            lblComensal.Foreground = Brushes.Black;
            imgComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\comensal.png");
        }

        private void btnEliminarLinea_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEliminarLinea.Background = (Brush)(new BrushConverter().ConvertFrom("#97cfda"));
            lblEliminarLinea.Foreground = Brushes.Black;
            imgEliminarLinea.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel.png");
        }

        private void btnEliminarPedido_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEliminarPedido.Background = (Brush)(new BrushConverter().ConvertFrom("#97cfda"));
            lblBorrarPedido.Foreground = Brushes.Black;
            imgEliminarPedido.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\eliminar.png");
        }
    }
}
