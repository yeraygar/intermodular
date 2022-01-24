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
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            Staticresources.height = this.Height;
            Staticresources.width = this.Width;

            Zona.getAllZones().ContinueWith(task =>
            {
                if(Zona.allZones != null)
                {
                    foreach(Zona z in Zona.allZones)
                    {
                        Button btn = new Button
                        {
                            Content = z.zone_name,
                            Tag = z._id,
                            Height = 70,
                            Style = Application.Current.TryFindResource("btnRedondo") as Style,
                            Margin = new Thickness(10),
                            FontSize = 19
                        };
                        stackZonas.Children.Add(btn);
                        
                    }
                }
                else
                {
                    MessageBox.Show("Error al cargar la BD");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        //Configuramos el click listener para abrir la ventana de opciones y comprobamos que el click se realice con el botón izquierdo del ratón
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                PopUp_Opciones popup = new PopUp_Opciones();
                popup.ShowDialog();
            }
        }
    }
}
