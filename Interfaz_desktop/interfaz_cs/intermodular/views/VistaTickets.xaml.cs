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
    /// Lógica de interacción para VistaTickets.xaml
    /// </summary>
    public partial class VistaTickets : Window
    {
        public VistaTickets()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.3;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.65;
            loadTickets();
        }

        public async void loadTickets()
        {
            try
            {
                await Ticket.getClientOpenTickets();
                foreach (Ticket t in Ticket.openTickets)
                {
                    Button btn = new Button
                    {
                        Style = Application.Current.TryFindResource("btnRedondo") as Style,
                        Margin = new Thickness(0, 10, 0, 0),
                        Height = 70,
                        Width = 450,
                        Tag = "",
                        Cursor = Cursors.Hand
                    };
                    Grid grid = new Grid();

                    for (int x = 0; x < 2; x++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    Label lblNombreMesa = new Label
                    {
                        Content = "Mesa: " + t.name_table,
                        FontSize = 19
                       
                    };
                    Label lblTotal = new Label
                    {
                        Content = "Comensales: " + t.comensales,
                        FontSize = 19
                    };
                    Grid.SetColumn(lblNombreMesa, 0);
                    Grid.SetColumn(lblTotal, 1);
                    grid.Children.Add(lblNombreMesa);
                    grid.Children.Add(lblTotal);
                    btn.Content = grid;
                    stackTickets.Children.Add(btn);

                    btn.Click += async (object sender, RoutedEventArgs re) =>
                    {
                        try
                        {
                            await Producto.getAllTicketLinesFromTicket(t._id);
                            string ticketL ="";
                            float total = 0;
                            foreach(Producto p in Producto.ticketLines)
                            {
                                total += p.total;
                                ticketL += p.cantidad + " " + p.name + " " + p.total+"€\n";
                            }
                            MessageBox.Show(ticketL, "Mesa: " + t.name_table + " Total: " + total + "€", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error al cargar BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    };
                    btn.MouseEnter += (object sender, MouseEventArgs meva) =>
                    {
                        btn.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
                        btn.Foreground = Brushes.White;
                    };
                    btn.MouseLeave += (object sender, MouseEventArgs mevaL) =>
                    {
                        btn.Background = Brushes.White;
                        btn.Foreground = Brushes.Black;
                    };
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error al cargar BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        private void Btn_cerrar_Click(object sender, RoutedEventArgs e)
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
    }
}
