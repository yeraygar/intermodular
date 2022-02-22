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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace intermodular
{

    public partial class FicharEmpleado : Window
    {
        /// <summary>
        /// <b>paraFichar: </b> True: muestra todos los empleados disponibles del cliente, False: muestra solo los que hayan fichado
        /// <b>modificar: </b> True: modifica el status de cliente (true = ha fichado entrada), False: no modifica, solo valida pass 
        /// </summary>
        public FicharEmpleado(bool paraFichar, bool modificar) {

            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.3;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.65;
           
                User.getUsersFichados(Client.currentClient._id, !paraFichar).ContinueWith(task =>
                {
                    List<User> listaElegida = paraFichar ? User.usuariosNoFichados : User.usuariosFichados;

                    if (listaElegida != null)

                        if (listaElegida.Count == 0) MessageBox.Show("No hay empleados disponibles", "Lista Vacia", MessageBoxButton.OK, MessageBoxImage.Error);
                        else foreach (User user in listaElegida) addButton(user, paraFichar, modificar);
                    
                    else MessageBox.Show("No se ha podido cargar los usuarios", "Error de Conexion", MessageBoxButton.OK, MessageBoxImage.Error);

                    Loading.Visibility = Visibility.Collapsed;

                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void addButton(User user, bool paraFichar, bool modificar)
        {
            Button boton = new Button
            {
                Content = user.name,
                Height = 70,
                MinHeight = 40,
                FontSize = 20,
                Style = (Style)Application.Current.Resources["btnRedondo"]

            };

            //Margin = new Thickness(2)
            StackPanel2.Children.Add(boton);

            boton.MouseEnter += (object sender, MouseEventArgs e) =>
            {
                boton.Background = Brushes.DarkSlateGray;
                boton.Foreground = Brushes.White;

            };
            boton.MouseLeave += (object sender, MouseEventArgs e) =>
            {
                boton.Background = Brushes.White;
                boton.Foreground = Brushes.DarkSlateGray;

            };
            boton.Click += (object sender, RoutedEventArgs e) =>
            {
                this.Close();
                User.usuarioElegido = user;
                Login empl = new Login(paraFichar, modificar, false);
                empl.ShowDialog();
            };
        }

        //cierra esta ventana al hacer click en el botón de cerrar    
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
