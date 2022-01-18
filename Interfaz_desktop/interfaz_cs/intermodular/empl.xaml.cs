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

    public partial class Empleados : Window
    {

        public Empleados() {
            InitializeComponent();
            

            User.getAllUsers().ContinueWith(task =>
            {

                
                foreach(User user in User.allUsers)
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
                        Login empl = new Login();
                        empl.ShowDialog();
                    };


                }
             }, TaskScheduler.FromCurrentSynchronizationContext());
        }




        //cierra esta ventana al hacer click en el botón de cerrar    

        private void Btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
