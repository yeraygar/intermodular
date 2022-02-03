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
    /// Lógica de interacción para VistaProductos.xaml
    /// </summary>
    public partial class VistaProductos : Window
    {
        private Familia selectedFamilia;
        private Button btnFamiliaSelect;
        public VistaProductos()
        {
            InitializeComponent();
            cargarFamilias();
        }

        private void btnAgregarFamilia_Click(object sender, RoutedEventArgs e)
        {
            CrearFamiliaProducto crearFamilia = new CrearFamiliaProducto(null);
            crearFamilia.ShowDialog();
            if(crearFamilia.familia != null)
            {
                agregarFamiliaStackPanel(crearFamilia.familia);
            }
        }


        private void agregarFamiliaStackPanel(Familia familia)
        {

        }

        private async void cargarFamilias()
        {
            /*if (await Familia.getClientFamilies())
            {

            }*/
        }

        private void btnEditarFamilia_Click(object sender, RoutedEventArgs e)
        {
            CrearFamiliaProducto editarFamilia = new CrearFamiliaProducto(selectedFamilia);
            editarFamilia.ShowDialog();
            if (!selectedFamilia.name.Equals(editarFamilia.familia.name))
            {
                btnFamiliaSelect.Content = editarFamilia.familia.name;
            }
        }
    }
}
