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
        private Button btnPressed;
        private Zona zonaSelect;
        public MainWindow()
        {

            InitializeComponent();
            Staticresources.height = this.Height;
            Staticresources.width = this.Width;
            Staticresources.mainWindow = this;
            

            Zona.getAllZones().ContinueWith(task =>
            {
                if(Zona.allZones != null)
                {
                    cargarZonas(Zona.allZones);
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

        public void addBtnZona(Zona zona)
        {
            //stackZonas.Children.Add(btn);
            Button btn = new Button
            {
                Content = zona.zone_name,
                Tag = zona._id,
                Height = 70,
                Margin = new Thickness(10),
                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                Cursor = Cursors.Hand,
                FontSize = 19
            };

            btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) =>
            {
                if (btnPressed == null || btnPressed.Tag != btn.Tag)
                    btn.Background = (Brush)(new BrushConverter().ConvertFrom("#bcbcbc"));
            };

            btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
            {
                if (btnPressed == null || btnPressed.Tag != btn.Tag)
                    btn.Background = Brushes.White;
            };

            btn.Click += (object senderMouseClick, RoutedEventArgs routedEventArg) =>
            {
                zonaSelect = zona;
                //Mostrar el grid dependiendo del tamaño de cada zona
            };

            zonaSelect = zona;
            stackZonas.Children.Add(btn);
        }

        private void cargarZonas(List<Zona>zonas)
        {
            foreach (Zona z in zonas)
            {
                Button btn = new Button
                {
                    Content = z.zone_name,
                    Tag = z._id,
                    Height = 70,
                    Margin = new Thickness(10),
                    Style = Application.Current.TryFindResource("btnRedondo") as Style,
                    Cursor = Cursors.Hand,
                    FontSize = 19
                };

                btn.MouseEnter += (object senderMouseEnter, MouseEventArgs mouseEventArg) =>
                {
                    if (btnPressed == null || btnPressed.Tag != btn.Tag)
                        btn.Background = (Brush)(new BrushConverter().ConvertFrom("#bcbcbc"));
                };

                btn.MouseLeave += (object senderMouseLeave, MouseEventArgs mouseLeaveArgs) =>
                {
                    if (btnPressed == null || btnPressed.Tag != btn.Tag)
                        btn.Background = Brushes.White;
                };

                btn.Click += (object senderMouseClick, RoutedEventArgs routedEventArg) =>
                {
                    zonaSelect = z;
                    //Mostrar el grid dependiendo del tamaño de cada zona
                };

                stackZonas.Children.Add(btn);
            }
        }

        //Buscamos entre todos los elementos del stackPanel y eliminamos el que tenga el id que le pasamos como atributo
        public  void eliminarZona(string id)
        {
            bool found = false;
            for(int x = 0; x < stackZonas.Children.Count && !found; x++)
            {
                Button btn = stackZonas.Children[x] as Button;
                if(btn.Tag.Equals(id))
                {
                    stackZonas.Children.Remove(btn);
                    found = true;
                }
            }
        }

        //Actualiza la zona, como el tag no se cambia, solamente actualizamos el nombre de la zona ya que sí que puede variar, el resto de atributos se actualizan en la ventana de Zonas
        public void updateZona(Zona zona)
        {
            bool found = false;
            for(int x = 0; x < stackZonas.Children.Count && !found; x++)
            {
                Button btn = stackZonas.Children[x] as Button;
                if(btn.Tag.Equals(zona._id))
                {
                    btn.Content = zona.zone_name;
                    found = true;
                }
            }
        }

    }
}
