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
    /// Lógica de interacción para PopUp_Opciones.xaml
    /// </summary>
    public partial class PopUp_Opciones : Window
    {
        //Definimos los colores que vamos a usar al hacer hover sobre los botones y el texto
        //private Brush btn_enter_color = (Brush)(new BrushConverter().ConvertFrom("#444444"));
        //private Brush text_enter_color = Brushes.White;
        //private Brush btn_leave_color = Brushes.White;
        //private Brush text_leave_color = Brushes.Black;
        public PopUp_Opciones()
        {
            InitializeComponent();
            //this.Height = Staticresources.height * 0.5;
            //this.Width = Staticresources.width * 0.5;
        }

        //Cambia el color al situarse sobre el botón de cerrar
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        //cambia el color al salir del botón de cerrar
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }

        //cierra esta ventana al hacer click en el botón de cerrar
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //cambia el color al situarse encima del boton de fichar entrada
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_firmarEntrada.Background = Colores.oscuro;            
            img_ficharEntrada.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\card_blanco.png");
            txt_ficharEntrada.Foreground = Colores.blanco;
        }

        //cambia el color al salir del botón de fichar entrada
        private void btn_firmarEntrada_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_firmarEntrada.Background = Colores.blanco;
            img_ficharEntrada.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\card.png");
            txt_ficharEntrada.Foreground = Colores.oscuro;
        }

        //cambia el color al situarse encima del boton de fichar salida
        private void btn_ficharSalida_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_ficharSalida.Background = Colores.oscuro;
            img_ficharSalida.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\manos_blanco.png");
            txt_ficharSalida.Foreground = Colores.blanco;
        }

        //cambia el color al salir del botón de fichar salida
        private void btn_ficharSalida_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_ficharSalida.Background = Colores.blanco;
            img_ficharSalida.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\manos.png");
            txt_ficharSalida.Foreground = Colores.oscuro;
        }

        //cambia el color al situarse encima del botón de tickets
        private void btn_tickets_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_tickets.Background = Colores.oscuro;
            img_tickets.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\ticket_blanco.png");
            txt_tickets.Foreground = Colores.blanco;
        }

        //cambia el color al salir del botón de tickets
        private void btn_tickets_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_tickets.Background = Colores.blanco;
            img_tickets.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\ticket.png");
            txt_tickets.Foreground = Colores.oscuro;
        }

        //cambia el color al situarse encima del boton de admin
        private void btn_admin_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_admin.Background = Colores.oscuro;
            img_admin.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\admin_blanco.png");
            txt_admin.Foreground = Colores.blanco;
        }

        //cambia el color al salir del botón de admin
        private void btn_admin_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_admin.Background = Colores.blanco;
            img_admin.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\admin.png");
            txt_admin.Foreground = Colores.oscuro;
        }

        //abre la ventana de fichar entrada
        private void btn_firmarEntrada_Click(object sender, RoutedEventArgs e)
        {
            //abrir Activity de entradas
            this.Close();
            FicharEmpleado empl = new FicharEmpleado(true, true); 
            empl.ShowDialog();

        }

        private void btn_firmarSalida_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            FicharEmpleado empl = new FicharEmpleado(false, true);
            empl.ShowDialog();
        }

        //abre la ventana de introducir la clave del admin y si la clave es correcta entonces abre el menú de admin.
        private void btn_admin_Click(object sender, RoutedEventArgs e)
        {
            //abrir actividad de NumInt
            this.Close();
            Login login = new Login(false, false, true);
            login.ShowDialog();
        }

       
    }
}
