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
                familiaEditada = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            btnCancelar.Foreground = Brushes.White;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel_blanco.png");
        }

        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = Brushes.White;
            btnCancelar.Foreground = Brushes.Black;
            imgCancel.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel.png");
        }

        private void btnSiguiente_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = (Brush)(new BrushConverter().ConvertFrom("#63c554"));
            btnSiguiente.Foreground = Brushes.White;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\next_blanco.png");
        }

        private void btnSiguiente_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSiguiente.Background = Brushes.White;
            btnSiguiente.Foreground = Brushes.Black;
            imgNext.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\next.png");
        }
        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        //Cambia el color del botón de cerrar, al dejar de estar situado encima.
        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if(familia == null)
            {
                crearFamilia();
            }
            else
            {
                updateFamilia();
            }
        }

        private async void crearFamilia()
        {
            if (validNombre(txtNombre.Text))
            {
                if (await Familia.createFamily(new Familia(txtNombre.Text)))
                {
                    familia = Familia.currentFamilia;
                    this.Close();
                }
                else
                {
                    //Mostrar Error
                    MessageBox.Show("Error al guardar la Familia de productos");
                }
            }
            else
            {
                MessageBox.Show("Error al crear la familia, debe indicar un nombre");
            }
        }

        private async void updateFamilia()
        {
            if(validNombre(txtNombre.Text))
            {
                if(familiaEditada)
                {
                    familia.name = txtNombre.Text;
                    if(await Familia.updateFamilia(familia._id,familia))
                    {
                        this.Close();
                    }
                    else
                    {
                        //Mostrar Error
                        MessageBox.Show("Error al actualizar la familia de Productos");
                    }
                }
            }
        }
    }
}
