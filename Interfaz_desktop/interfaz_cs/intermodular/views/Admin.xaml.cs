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
    /// Lógica de interacción para Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        //Definimos los colores que vamos a usar al hacer hover sobre los botones y el texto
        private Brush btn_enter_color = Colores.oscuro;
        private Brush text_enter_color = Brushes.White;
        private Brush btn_leave_color = Brushes.White;
        private Brush text_leave_color = Brushes.Black;
        public Admin()
        {
            InitializeComponent();
            //this.Width = Staticresources.width * 0.4;
            //this.Height = Staticresources.height * 0.7;
            if(Staticresources.caja.Equals("cerrada"))
            {
                btn_cerrarCaja.IsEnabled = false;
                btn_abrirCaja.IsEnabled = true;
            }else
            {
                btn_cerrarCaja.IsEnabled = true;
                btn_abrirCaja.IsEnabled = false;
            }
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

        private void btn_abrirCaja_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_abrirCaja.Background = btn_enter_color;
            img_abrirCaja.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\abrir_caja_blanco.png");
            txt_abrirCaja.Foreground = text_enter_color;
        }

        private void btn_cerrarCaja_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrarCaja.Background = btn_enter_color;
            img_cerrarCaja.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_caja_blanco.png");
            txt_cerrarCaja.Foreground = text_enter_color;
        }

        private void btn_mesas_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_mesas.Background = btn_enter_color;
            img_mesas.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\mesas_blanco.png");
            txt_mesas.Foreground = text_enter_color;
        }

        private void btn_zonas_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_zonas.Background = btn_enter_color;
            img_zonas.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\zonas_blanco.png");
            txt_zonas.Foreground = text_enter_color;
        }

        private void btn_productos_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_productos.Background = btn_enter_color;
            img_productos.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\producto_blanco.png");
            txt_productos.Foreground = text_enter_color;
        }

        private void btn_empleados_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_empleados.Background = btn_enter_color;
            img_empleados.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\empleado_blanco.png");
            txt_empleados.Foreground = text_enter_color;
        }

        private void btn_totales_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_totales.Background = btn_enter_color;
            img_totales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\totales_blanco.png");
            txt_totales.Foreground = text_enter_color;
        }

        private void btn_salir_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_salir.Background = btn_enter_color;
            img_salir.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\exit_blanco.png");
            txt_salir.Foreground = text_enter_color;
        }

        private void btn_abrirCaja_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_abrirCaja.Background = btn_leave_color;
            img_abrirCaja.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\abrir_caja.png");
            txt_abrirCaja.Foreground = text_leave_color;
        }

        private void btn_cerrarCaja_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrarCaja.Background = btn_leave_color;
            img_cerrarCaja.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_caja.png");
            txt_cerrarCaja.Foreground = text_leave_color;
        }

        private void btn_mesas_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_mesas.Background = btn_leave_color;
            img_mesas.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\mesas.png");
            txt_mesas.Foreground = text_leave_color;
        }

        private void btn_zonas_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_zonas.Background = btn_leave_color;
            img_zonas.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\zonas.png");
            txt_zonas.Foreground = text_leave_color;
        }

        private void btn_productos_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_productos.Background = btn_leave_color;
            img_productos.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\producto.png");
            txt_productos.Foreground = text_leave_color;
        }

        private void btn_empleados_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_empleados.Background = btn_leave_color;
            img_empleados.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\empleado.png");
            txt_empleados.Foreground = text_leave_color;
        }

        private void btn_totales_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_totales.Background = btn_leave_color;
            img_totales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\totales.png");
            txt_totales.Foreground = text_leave_color;
        }

        private void btn_salir_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_salir.Background = btn_leave_color;
            img_salir.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\exit.png");
            txt_salir.Foreground = text_leave_color;
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {

            //antes de cerrar se comprueba que la caja no esta abierta, de estar abierta preguntamos si queremos cerrarla y si esta cerrada se podrá cerrar la aplicación
            Application.Current.Shutdown();
        }

        //Si la caja esta cerrada la abrimos y deshabilitamos el botón de abrir caja
        private void btn_abrirCaja_Click(object sender, RoutedEventArgs e)
        {
            if(Staticresources.caja.Equals("cerrada"))
            {
                Staticresources.caja = "abierta";
                btn_abrirCaja.IsEnabled = false;
                btn_cerrarCaja.IsEnabled = true;
                MessageBox.Show("Caja abierta");
            }
        }

        private void btn_cerrarCaja_Click(object sender, RoutedEventArgs e)
        {
            if (Staticresources.caja.Equals("abierta"))
            {
                Staticresources.caja = "cerrada";
                btn_cerrarCaja.IsEnabled = false;
                btn_abrirCaja.IsEnabled = true;
                MessageBox.Show("Caja cerrada");
            }
        }

        private void btn_zonas_Click(object sender, RoutedEventArgs e)
        {
            Zonas zonas = new Zonas();
            this.Close();
            zonas.ShowDialog();
        }

        private void btn_mesas_Click(object sender, RoutedEventArgs e)
        {
            if(Staticresources.mainWindow.zonaSelect == null)
            {
                //Mostrar un error
                MessageBox.Show("No hay ninguna zona creada");
            }
            else {
                Staticresources.isEditableTables = true;
                Staticresources.mainWindow.addEditableTableBtns(Staticresources.isEditableTables);
                Staticresources.mainWindow.resetGridMesas();
                Staticresources.mainWindow.cargarGridMesas(Staticresources.mainWindow.zonaSelect);
                this.Close();
            }
        }

        private void btn_empleados_Click(object sender, RoutedEventArgs e)
        {
            VistaEmpleados vistaEmpleados = new VistaEmpleados();
            vistaEmpleados.Show();
        }
    }
}
