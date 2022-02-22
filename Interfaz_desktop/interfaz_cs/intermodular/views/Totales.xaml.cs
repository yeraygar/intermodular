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
    /// Lógica de interacción para Totales.xaml
    /// </summary>
    public partial class Totales : Window
    {
        public Totales()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.3;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.6;
            cargarTickets();
        }

        private async void cargarTickets()
        {
            await Ticket.getTicketFromCaja(Caja.currentCaja._id);
            foreach(Ticket t in Ticket.cajaTickets)
            {
                Button btn = new Button
                {
                  Style = Application.Current.TryFindResource("btnRedondo") as Style,
                  Height = 60,
                  Tag = t._id,
                  Content = $"Mesa: {t.name_table} \testado: {(t.cobrado ? "Cerrado" : "Abierto")} \tTotal: {t.total}",
                  Margin = new Thickness(5,10,5,0)
                };

                btn.Click += async (object sender, RoutedEventArgs e) =>
                {
                    try
                    {
                        await Producto.getAllTicketLinesFromTicket(t._id);
                        string ticketL = "";
                        float total = 0;
                        foreach (Producto p in Producto.ticketLines)
                        {
                            total += p.total;
                            ticketL += p.cantidad + " " + p.name + " " + p.total + "€\n";
                        }
                        MessageBox.Show(ticketL, "Mesa: " + t.name_table + "\tTotal: " + total + "€", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                };

                btn.MouseEnter += (object sender, MouseEventArgs mve) =>
                {
                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#46b2b2"));
                };

                btn.MouseLeave += (object senderr, MouseEventArgs mvel) =>
                {
                    btn.Background = Brushes.White;
                };
                stackTickets.Children.Add(btn);
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

        //Cambia el color del botón de cerrar, al dejar de estar situado encima.
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }
    }
}
