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
    /// Lógica de interacción para Cobro.xaml
    /// </summary>
    public partial class Cobro : Window
    {
        public Cobro(string nombreEmpleado,float total)
        {
            InitializeComponent();
            LabelNombre.Content = nombreEmpleado;
            LabelMesa.Content = Mesa.currentMesa.name;
            LabelTotal.Content = "Total " + total + "€";
            //LabelNombre.Content = User.usuarioElegido;
            
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

        private async void boton_efectivo(object sender, RoutedEventArgs e)
        {
            //Cerrar Ticket
            Ticket.currentTicket.cobrado = true;
            try
            {
               if(await Ticket.updateTicket(Ticket.currentTicket))
                {
                    //Actualizar mesa con comensales 0 y ocupada = false
                    Mesa.currentMesa.comensales = 0;
                    Mesa.currentMesa.ocupada = false;
                    if (await Mesa.updateTable(Mesa.currentMesa._id, Mesa.currentMesa))
                    {
                        //CurrentMesa = null
                        //CurrentTicket = null
                        //Productos ticketLines = null
                        Ticket.currentTicket = null;
                        Producto.ticketLines = null;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la mesa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
         
                }
                else
                {
                    MessageBox.Show("No se pudo cobrar el ticket", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al acceder a la BD", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    } 
}
