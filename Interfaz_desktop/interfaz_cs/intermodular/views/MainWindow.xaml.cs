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
        public Zona zonaSelect;
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

        public async void addBtnZona(Zona zona)
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

            btn.Click +=  async (object senderMouseClick, RoutedEventArgs routedEventArg) =>
            {
                if (btnPressed != null)
                {
                    btnPressed.Background = Brushes.White;
                    btnPressed.Foreground = Brushes.Black;
                }
                zonaSelect = zona;
                btnPressed = btn;
                resetGridMesas();
                await Mesa.getZoneTables(zona._id);

                if(Staticresources.isEditableTables)
                {
                    resetGridMesas();
                    cargarGridAdminMesas(zonaSelect);
                }else
                {
                    resetGridMesas();
                    //TODO mostrar las mesas que tiene esa zona
                }
                
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

                btn.Click += async (object senderMouseClick, RoutedEventArgs routedEventArg) =>
                {
                    if(btnPressed != null)
                    {
                        btnPressed.Background = Brushes.White;
                        btnPressed.Foreground = Brushes.Black;
                    }
                    btnPressed = btn;
                    btnPressed.Background = (Brush)(new BrushConverter().ConvertFrom("#434343"));
                    btnPressed.Foreground = Brushes.White;
                    zonaSelect = z;
                    resetGridMesas();
                    await Mesa.getZoneTables(zonaSelect._id);
                    if (Staticresources.isEditableTables)
                    {
                        resetGridMesas();
                        cargarGridAdminMesas(zonaSelect);
                    }
                    else
                    {
                        resetGridMesas();
                        //TODO mostrar las mesas que tiene esa zona
                    }
                };

                stackZonas.Children.Add(btn);
            }
            if (stackZonas.Children.Count > 0)
            {
                Button btnPre = stackZonas.Children[0] as Button;
                btnPre.Background = (Brush)(new BrushConverter().ConvertFrom("#434343"));
                btnPre.Foreground = Brushes.White;
                btnPressed = btnPre;
                zonaSelect = Zona.allZones[0];
                //cargarGridAdminMesas(zonaSelect);
                //TODO cargar grid con las mesas que tiene la zona
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
            resetGridMesas();
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

        public void addEditableTableBtns(bool isEditableTablesOn)
        {
            if (isEditableTablesOn)
            {
                gridBtnsTable.RowDefinitions[0].Height = gridBtnsTable.RowDefinitions[1].Height;
                gridBtnsTable.RowDefinitions[1].Height = new GridLength(0);
                btnAddTable.IsEnabled = true;
                btnEliminarMesa.IsEnabled = true;
                btnEditarMesa.IsEnabled = true;
                btnGuardarCambios.IsEnabled = true;
                btnSalir.IsEnabled = true;

            }
            else
            {
                gridBtnsTable.RowDefinitions[1].Height = gridBtnsTable.RowDefinitions[0].Height;
                gridBtnsTable.RowDefinitions[0].Height = new GridLength(0);
                btnAddTable.IsEnabled = false;
                btnEliminarMesa.IsEnabled = false;
                btnEditarMesa.IsEnabled = false;
                btnGuardarCambios.IsEnabled = false;
                btnSalir.IsEnabled = false;
                
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Staticresources.isEditableTables = false;
            addEditableTableBtns(Staticresources.isEditableTables);
        }

        private void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            //Guardar los cambios en la base de datos
            Staticresources.isEditableTables = false;
            addEditableTableBtns(Staticresources.isEditableTables);
        }

        public void cargarGridAdminMesas(Zona zona)
        {
            int numCol = 6;
            int numRow = 0;
            int numColact = 0;
            for(int x = 0; x < zona.num_tables; x++)
            {
                Button btn = new Button
                {
                    Background = Brushes.Green,
                    Margin = new Thickness(20),
                    Width = 200,
                    Height = 200,
                    Content = "Libre",
                    FontSize = 50
                };

                //if()

                Grid.SetRow(btn, numRow);
                Grid.SetColumn(btn, numColact);
                mapaMesas.Children.Add(btn);
                numColact++;

                if (numColact == 6)
                {
                    numColact = 0;
                    mapaMesas.RowDefinitions.Add(new RowDefinition());
                    numRow++;
                }
            }
        }

        public void resetGridMesas()
        {
            Border border = new Border();
            border.CornerRadius = new CornerRadius(10);
            border.Background = (Brush)(new BrushConverter().ConvertFrom("#E0E0E0")); 
            mapaMesas = new Grid();
            mapaMesas.RowDefinitions.Add(new RowDefinition());
            for(int x = 0; x < 6; x++)
            {
                mapaMesas.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ScrollViewer scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.CanContentScroll = true;
            scroll.Content = mapaMesas;
            border.Child = scroll;
            mapaZonas.Child = border;
        }
    }
}
