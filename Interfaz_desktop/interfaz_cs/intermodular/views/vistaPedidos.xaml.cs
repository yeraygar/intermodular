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
        private Grid lineaSelect;
        public vistaPedidos(string nomCamarero,string numComensales)
        {
            InitializeComponent();
            gridProductos.Height = (SystemParameters.PrimaryScreenHeight * 0.55) * 0.9;
            gridProductos.Width = SystemParameters.PrimaryScreenWidth * 0.8;
            gridCamarero.Height = (SystemParameters.PrimaryScreenHeight * 0.55) * 0.1;
            gridCamarero.Width = SystemParameters.PrimaryScreenWidth * 0.8;
            lblCamarero.Content += nomCamarero;
            lblComensales.Content += numComensales;
            this.comensales = int.Parse(numComensales);
            lblFecha.Content = calcularFecha();
            cargarFamiliasProd();
            cargarTicketMesa();
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
                    Margin = new Thickness(0, calcSize() * 0.1, 0, 0),
                    Width = calcSize(),
                    Height = calcSize(),
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
                Viewbox vb = new Viewbox();
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
                vb.Child = grid;
                btn.Content = vb;

                btn.Click +=  async (object sender, RoutedEventArgs rea) =>
                {
                    btnProdSelect = btn;
                    Producto pr = new Producto(p);
                    pr.id_ticket = Ticket.currentTicket == null ? null : Ticket.currentTicket._id;
                    productoSelect = pr;
                    Producto.currentProduct = productoSelect;
                    //Añadimos el producto a una línea de pedido
                    firstProdSelected = !await Ticket.getTicketFromTable(Mesa.currentMesa._id);
                    if(firstProdSelected)
                    {
                        //Se crea el ticket
                        Ticket ticket = new Ticket(comensales);
                        try
                        {
                            if(await Ticket.createTicket(ticket))
                            {
                                try
                                {
                                    await Mesa.updateTable(Mesa.currentMesa._id,Mesa.currentMesa);
                                    if(await Producto.createLineTicket(productoSelect))
                                    {
                                       
                                    }
                                    stackTicket.Children.Clear();
                                    cargarTicketMesa();

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
                        try
                        {
                            if (await Producto.createLineTicket(productoSelect))
                            {

                            }
                            stackTicket.Children.Clear();
                            cargarTicketMesa();

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error al acceder a la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                };

                btn.MouseEnter += (object sender, MouseEventArgs MouseEnterevent) =>
                {
                        btn.Background = (Brush)(new BrushConverter().ConvertFrom("#51aaa7"));
                        btn.Foreground = Brushes.White;
                };

                btn.MouseLeave += (object sender, MouseEventArgs MouseLeaveEvent) =>
                {
                        btn.Background = Brushes.White;
                        btn.Foreground = Brushes.Black;
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
        private async void cargarTicketMesa()
        {
            try
            {
                if(await Ticket.getTicketFromTable(Mesa.currentMesa._id))
                {
                    if(Ticket.currentTicket != null)
                    {
                        await Producto.getAllTicketLinesFromTicket(Ticket.currentTicket._id);

                        ///
                        if (Producto.ticketLines != null && Producto.ticketLines.Count > 0)
                        {
                            Border borde = new Border
                            {
                                Background = Brushes.LightBlue,
                                BorderThickness = new Thickness(1),
                                BorderBrush = Brushes.LightBlue
                            };
                            //Crear Botón de linea de producto
                            Grid gridLine = new Grid();
                            borde.Child = gridLine;
                            for (int x = 0; x < 4; x++)
                            {
                                gridLine.ColumnDefinitions.Add(new ColumnDefinition());
                            }
                            gridLine.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            gridLine.ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                            gridLine.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                            gridLine.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);

                            Label lblCantida = new Label
                            {
                                Content = "Cantidad",
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(2, 0, 0, 0)
                            };

                            Label lblNombr = new Label
                            {
                                Content = "Producto",
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Label lblPrecioUnida = new Label
                            {
                                Content = "€/U",
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Label lblTota = new Label
                            {
                                Content = "Total = " + calcTotal().ToString() + "€",
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Image im = new Image
                            {
                                Width = 50,
                                Height = 50,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Cursor = Cursors.Hand
                            };

                            Grid gridNombr = new Grid();
                            for (int x = 0; x < 2; x++)
                            {
                                gridNombr.ColumnDefinitions.Add(new ColumnDefinition());
                            }
                            gridNombr.ColumnDefinitions[0].Width = new GridLength(80, GridUnitType.Star);
                            gridNombr.ColumnDefinitions[1].Width = new GridLength(20, GridUnitType.Star);
                            Grid.SetColumn(lblNombr, 0);
                            Grid.SetColumn(im, 1);
                            gridNombr.Children.Add(lblNombr);
                            gridNombr.Children.Add(im);

                            Grid.SetColumn(lblCantida, 0);
                            Grid.SetColumn(gridNombr, 1);
                            Grid.SetColumn(lblPrecioUnida, 2);
                            Grid.SetColumn(lblTota, 3);
                            gridLine.Children.Add(lblCantida);
                            gridLine.Children.Add(gridNombr);
                            gridLine.Children.Add(lblPrecioUnida);
                            gridLine.Children.Add(lblTota);
                            stackTicket.Children.Add(borde);
                        }
                        ///
                        foreach (Producto t in Producto.ticketLines)
                            {
                            Border border = new Border
                            {
                                Background = Brushes.White,
                                BorderThickness = new Thickness(1),
                                BorderBrush = Brushes.LightBlue
                            };
                            //Crear Botón de linea de producto
                            Grid gridLinea = new Grid();
                            border.Child = gridLinea;
                            gridLinea.Background = Brushes.White;
                            gridLinea.Tag = t._id;
                            gridLinea.Cursor = Cursors.Hand;

                            //Añadimos el MouseDown event para el grid
                            gridLinea.MouseLeftButtonDown += (object senderr, MouseButtonEventArgs mbea) =>
                            {
                                if(lineaSelect != null)
                                {
                                    lineaSelect.Background = Brushes.White;
                                }

                                btnEliminarLinea.Visibility = Visibility.Visible;
                                lineaSelect = gridLinea;
                                gridLinea.Background = (Brush)(new BrushConverter().ConvertFrom("#dde8f2"));

                            };
                            for (int x = 0; x < 4; x++)
                            {
                                gridLinea.ColumnDefinitions.Add(new ColumnDefinition());
                            }
                            gridLinea.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                            gridLinea.ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                            gridLinea.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                            gridLinea.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);

                            Label lblCantidad = new Label
                            {
                                Content = t.cantidad,
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(2, 0, 0, 0)
                            };

                            Label lblNombre = new Label
                            {
                                Content = t.name,
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Label lblPrecioUnidad = new Label
                            {
                                Content = t.precio,
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Label lblTotal = new Label
                            {
                                Content = (float)(Math.Truncate((double)(t.cantidad * t.precio) * 100.0) / 100.0),
                                FontSize = 19,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            };

                            Image img = new Image
                            {
                                Width = 30,
                                Height = 30,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\comentario.png"),
                                Cursor = Cursors.Hand
                            };
                            if(t.comentario != null && !t.comentario.Equals(""))
                            {
                                img.Tag = t.comentario;
                            }

                            img.MouseLeftButtonDown += async (object sender, MouseButtonEventArgs mva) =>
                            {
                                if(img.Tag == null || img.Tag.Equals(""))
                                {
                                    Comentario comentario = new Comentario();
                                    comentario.ShowDialog();
                                    if (comentario.comentario != null)
                                    {
                                        t.comentario = comentario.comentario;
                                        try
                                        {
                                            if(await Producto.updateLineTicket(t))
                                            {
                                                img.Tag = t.comentario;
                                            }
                                        }
                                        catch(Exception ex)
                                        {
                                            MessageBox.Show("Error al añadir el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(img.Tag.ToString(), "Comentario", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            };

                            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
                            Grid gridNombre = new Grid();
                            for (int x = 0; x < 2; x++)
                            {
                                gridNombre.ColumnDefinitions.Add(new ColumnDefinition());
                            }
                            gridNombre.ColumnDefinitions[0].Width = new GridLength(60, GridUnitType.Star);
                            gridNombre.ColumnDefinitions[1].Width = new GridLength(40, GridUnitType.Star);
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
                            stackTicket.Children.Add(border);
                        }
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show("Error al cargar la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Staticresources.mainWindow.resetGridMesas();
            Staticresources.mainWindow.cargarGridMesas(Staticresources.mainWindow.zonaSelect);
            Ticket.currentTicket = null;
            Producto.ticketLines = null;
        }

        private void btnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = (Brush)(new BrushConverter().ConvertFrom("#cd2323"));
            lblVolver.Foreground = Brushes.White;
            imgVolver.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\back_blanco.png");
        }

        private void btnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = (Brush)(new BrushConverter().ConvertFrom("#df7777"));
            lblVolver.Foreground = Brushes.Black;
            imgVolver.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\back.png");
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private float calcTotal()
        {
            float total = 0;
            foreach(Producto t in Producto.ticketLines)
            {
                total += t.total;
            }
            return total;
        }

        private void btnCobrar_Click(object sender, RoutedEventArgs e)
        {
            Cobro cobro = new Cobro(lblCamarero.Content.ToString(),calcTotal());
            cobro.ShowDialog();
            this.Close();
        }

        private async void btnComensales_Click(object sender, RoutedEventArgs e)
        {
            ModComensalesPedido modCom = new ModComensalesPedido();
            modCom.ShowDialog();
            if(modCom.comensales != 0)
            {
                comensales = modCom.comensales;
                try
                {
                    Mesa.currentMesa.comensales = modCom.comensales;
                    if(Mesa.currentMesa.comensales == Mesa.currentMesa.comensalesMax)
                    {
                        Mesa.currentMesa.ocupada = true;
                    }
                    else
                    {
                        Mesa.currentMesa.ocupada = false;
                    }
                    Ticket.currentTicket.comensales = comensales;
                    await Ticket.updateTicket(Ticket.currentTicket);
                    await Mesa.updateTable(Mesa.currentMesa._id, Mesa.currentMesa);
                    lblComensales.Content = "Comensales: " + comensales.ToString();
                }catch(Exception ex)
                {
                    MessageBox.Show("Error al actualizar el número de comensales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void btnEliminarPedido_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(await Producto.deleteAllTicketLinesFromTicket(Ticket.currentTicket._id))
                {
                    stackTicket.Children.Clear();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error al elimina las lineas de pedido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEliminarLinea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(await Producto.deleteTicketLine(lineaSelect.Tag.ToString()))
                {
                    btnEliminarLinea.Visibility = Visibility.Collapsed;
                    lineaSelect = null;
                    Producto.ticketLines = null;
                    Producto.currentTicketLine = null;
                    stackTicket.Children.Clear();
                    cargarTicketMesa();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error al eliminar línea de pedido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       private double calcSize()
        {
            double size = (SystemParameters.PrimaryScreenWidth * 0.8);
            return (size / 6) * 0.8; 
        }
    }
}
