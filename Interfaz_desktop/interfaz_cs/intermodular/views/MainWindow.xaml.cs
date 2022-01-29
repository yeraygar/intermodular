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
                    if (Zona.allZones.Count > 0)
                    {
                        cargarZonas(Zona.allZones);
                        Mesa.getZoneTables(Zona.allZones[0]._id).ContinueWith(tasks =>
                        {

                            cargarGridMesas(Zona.allZones[0]);
                        }, TaskScheduler.FromCurrentSynchronizationContext());
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
                btn.Background = (Brush)(new BrushConverter().ConvertFrom("#434343"));
                btn.Foreground = Brushes.White;
                resetGridMesas();
                await Mesa.getZoneTables(zona._id);
                resetGridMesas();
                cargarGridMesas(zonaSelect);
               
            };

            if(btnPressed == null)
            {
                btnPressed = btn;
                btnPressed.Background = (Brush)(new BrushConverter().ConvertFrom("#434343"));
                btnPressed.Foreground = Brushes.White;
            }

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
                    resetGridMesas();
                    cargarGridMesas(zonaSelect);
                   
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
            if (btnPressed.Tag.Equals(id))
            {
                resetGridMesas();
                btnPressed = null;
                zonaSelect = null;
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

        public void addEditableTableBtns(bool isEditableTablesOn)
        {
            if (isEditableTablesOn)
            {
                gridBtnsTable.RowDefinitions[0].Height = gridBtnsTable.RowDefinitions[1].Height;
                gridBtnsTable.RowDefinitions[1].Height = new GridLength(0);
                btnAddTable.IsEnabled = true;
                btnEliminarMesa.IsEnabled = true;
                btnEditarMesa.IsEnabled = true;
                btnSalir.IsEnabled = true;

            }
            else
            {
                gridBtnsTable.RowDefinitions[1].Height = gridBtnsTable.RowDefinitions[0].Height;
                gridBtnsTable.RowDefinitions[0].Height = new GridLength(0);
                btnAddTable.IsEnabled = false;
                btnEliminarMesa.IsEnabled = false;
                btnEditarMesa.IsEnabled = false;
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

        public void cargarGridMesas(Zona zona)
        {
            //int numCol = 6;
            if (Mesa.currentZoneTables != null)
            {
                if (Mesa.currentZoneTables.Count != 0)
                {
                    int numRows = Mesa.currentZoneTables[Mesa.currentZoneTables.Count - 1].num_row + 1;
                    for (int x = 0; x < numRows; x++)
                    {
                        mapaMesas.RowDefinitions.Add(new RowDefinition());
                    }
                    foreach (Mesa mesa in Mesa.currentZoneTables)
                    {
                        Button btn = new Button
                        {
                            Background = mesa.status ? Brushes.Green : Brushes.Red,
                            Tag = mesa._id,
                            Margin = new Thickness(20),
                            Width = 200,
                            Height = 200,
                            Content = mesa.name,
                            Style = Application.Current.TryFindResource("btnRedondo") as Style,
                            FontSize = 50,
                            Cursor = Cursors.Hand
                        };
                        Grid.SetRow(btn, mesa.num_row);
                        Grid.SetColumn(btn, mesa.num_column);
                        mapaMesas.Children.Add(btn);
                    }
                }
            }
        }

        public void resetGridMesas()
        {
            Border border = new Border();
            border.CornerRadius = new CornerRadius(10);
            border.Background = (Brush)(new BrushConverter().ConvertFrom("#E0E0E0")); 
            mapaMesas = new Grid();
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

        private void btnAddTable_Click(object sender, RoutedEventArgs e)
        {
            CrearMesa crearMesa = new CrearMesa(zonaSelect._id);
            crearMesa.ShowDialog();
            if(crearMesa.mesa != null)
            {

                //Mesa.currentZoneTables.Add(crearMesa.mesa);
                crearBtnNuevaMesa(crearMesa.mesa);
            }
        }

        private void crearBtnNuevaMesa(Mesa mesa)
        {
            Button btn = new Button
            {
                Tag = mesa._id,
                Margin = new Thickness(20),
                Width = 200,
                Height = 200,
                Style = Application.Current.TryFindResource("btnRedondo") as Style,
                Background = mesa.status ? Brushes.Green : Brushes.Red,
                Content = mesa.name,
                FontSize = 50,
                Cursor = Cursors.Hand
            };
            if (mapaMesas.RowDefinitions.Count < mesa.num_row + 1)
                mapaMesas.RowDefinitions.Add(new RowDefinition());


                Grid.SetRow(btn, mesa.num_row);
                Grid.SetColumn(btn, mesa.num_column);
                mapaMesas.Children.Add(btn);
        }

        private void btnsZonesClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //btn.Background
            if(Staticresources.isEditableTables)
            {
                //Selecciona la mesa dentro del modo Admin, visualizar btns de Editar Mesa, Eliminar Mesa y guardar cambios
            }
            else
            {
                //Abres una ventana cargando la mesa ---> Vista para realizar pedido, etc.
            }
        }
    }
}
