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
    /// Lógica de interacción para CrearFamiliaProducto.xaml
    /// </summary>
    public partial class CrearFamiliaProducto : Window
    {
        public Familia familia;
        public bool familiaEditada = false;
        public CrearFamiliaProducto(Familia familia)
        {
            InitializeComponent();
            this.familia = familia;
            if(familia == null)
            {
                tituloVentana.Text = "Crear Familia Producto";
            }else
            {
                tituloVentana.Text = "Editar Familia Producto";
                txtNombre.Tag = familia.name;
                txtNombre.Text = familia.name;
                imgNombreFamilia.Visibility = Visibility.Hidden;
            }
        }

        private bool validNombre(string nombre) => !String.IsNullOrEmpty(nombre) && !String.IsNullOrWhiteSpace(nombre);

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(familia == null || !txtNombre.Tag.Equals(txtNombre.Text))
            {
                imgNombreFamilia.Visibility = Visibility.Visible;
                imgNombreFamilia.Source = validNombre(txtNombre.Text) ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgNombreFamilia.ToolTip = validNombre(txtNombre.Text) ? null : "Debe introducir un nombre";
            }
        }
    }
}
