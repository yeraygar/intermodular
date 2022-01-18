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
        private Brush btn_enter_color = (Brush)(new BrushConverter().ConvertFrom("#444444"));
        private Brush text_enter_color = Brushes.White;
        private Brush btn_leave_color = Brushes.White;
        private Brush text_leave_color = Brushes.Black;
        public PopUp_Opciones()
        {
            InitializeComponent();
        }

        //Cambia el color al situarse sobre el botón de cerrar
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
        }

        //cambia el color al salir del botón de cerrar
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
        }

        //cierra esta ventana al hacer click en el botón de cerrar
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //cambia el color al situarse encima del boton de fichar entrada
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_firmarEntrada.Background = btn_enter_color;
            img_ficharEntrada.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\card_blanco.png");
            txt_ficharEntrada.Foreground = text_enter_color;
        }

        //cambia el color al salir del botón de fichar entrada
        private void btn_firmarEntrada_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_firmarEntrada.Background = btn_leave_color;
            img_ficharEntrada.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\card.png");
            txt_ficharEntrada.Foreground = text_leave_color;
        }

        //cambia el color al situarse encima del boton de fichar salida
        private void btn_ficharSalida_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_ficharSalida.Background = btn_enter_color;
            img_ficharSalida.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\manos_blanco.png");
            txt_ficharSalida.Foreground = text_enter_color;
        }

        //cambia el color al salir del botón de fichar salida
        private void btn_ficharSalida_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_ficharSalida.Background = btn_leave_color;
            img_ficharSalida.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\manos.png");
            txt_ficharSalida.Foreground = text_leave_color;
        }

        //cambia el color al situarse encima del botón de tickets
        private void btn_tickets_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_tickets.Background = btn_enter_color;
            img_tickets.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\ticket_blanco.png");
            txt_tickets.Foreground = text_enter_color;
        }

        //cambia el color al salir del botón de tickets
        private void btn_tickets_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_tickets.Background = btn_leave_color;
            img_tickets.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\ticket.png");
            txt_tickets.Foreground = text_leave_color;
        }

        //cambia el color al situarse encima del boton de admin
        private void btn_admin_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_admin.Background = btn_enter_color;
            img_admin.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\admin_blanco.png");
            txt_admin.Foreground = text_enter_color;
        }

        //cambia el color al salir del botón de admin
        private void btn_admin_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_admin.Background = btn_leave_color;
            img_admin.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\admin.png");
            txt_admin.Foreground = text_leave_color;
        }

        //abre la ventana de fichar entrada
        private void btn_firmarEntrada_Click(object sender, RoutedEventArgs e)
        {
            //abrir Activity de entradas
            this.Close();
            Empleados empl = new Empleados();
            empl.ShowDialog();

        }

        //abre la ventana de introducir la clave del admin y si la clave es correcta entonces abre el menú de admin.
        private void btn_admin_Click(object sender, RoutedEventArgs e)
        {
            //abrir actividad de NumInt
            this.Close();
            Admin admin = new Admin();
            admin.ShowDialog();
        }
    }
}
