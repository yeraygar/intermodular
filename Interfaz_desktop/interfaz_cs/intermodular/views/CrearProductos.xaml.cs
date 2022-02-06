using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para CrearProductos.xaml
    /// </summary>
    public partial class CrearProductos : Window
    {
        private bool isPhotoChanged = false;
        private string idFamilia;
        private bool isProdUpdated = false;
        public Producto producto;
        public CrearProductos(string idFamilia,Producto producto)
        {
            InitializeComponent();
            this.idFamilia = idFamilia;
            this.producto = producto;
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if(fileDiag.ShowDialog() == true)
            {
                imgProd.Source = new BitmapImage(new Uri(fileDiag.FileName));
                isPhotoChanged = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgNombre.Visibility = Visibility.Visible;
            imgNombre.Source = validNombre() ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
            imgNombre.ToolTip = validNombre() ? null : "Debe introducir un nombre";
        }

        private bool validNombre() => !String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrWhiteSpace(txtNombre.Text);

        private bool isValidPrice()
        {
            bool retorno = true;
            if(String.IsNullOrEmpty(txtPrecio.Text) || String.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                retorno = false;
            }else if(txtPrecio.Text.Contains(" "))
            {
                retorno = false;
            }
            else
            {
                try
                {
                   float precio = float.Parse(txtPrecio.Text);
                    if(precio <= 0)
                    {
                        retorno = false;
                    }
                }catch(Exception e)
                {
                    retorno = false;
                }
            }
            return retorno;
        }

        private void txtPrecio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
                if (e.Text == ".")
                {
                    if (!((TextBox)sender).Text.Contains("."))
                        approvedDecimalPoint = true;
                }

                if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                    e.Handled = true;
        }

        private void txtPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgPrecio.Visibility = Visibility.Visible;
            if(String.IsNullOrEmpty(txtPrecio.Text) || String.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                imgPrecio.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgPrecio.ToolTip = "Debe introducir un precio";
            }else if(txtPrecio.Text.Contains(" "))
            {
                imgPrecio.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgPrecio.ToolTip = "El campo no admite espacios.";
            }
            else
            {
                try
                {
                   float precio = float.Parse(txtPrecio.Text);
                 
                   if(precio > 0)
                    {
                        imgPrecio.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                        imgPrecio.ToolTip = null;
                    }
                    else
                    {
                        imgPrecio.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                        imgPrecio.ToolTip = "El precio debe ser mayor que 0";
                    }
                }
                catch(Exception ex)
                {
                    imgPrecio.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgPrecio.ToolTip = "El Precio debe ser de tipo numérico";
                }
            }
        }

        private async void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if(!isPhotoChanged)
            {
                MessageBox.Show("Debe agregar una foto al producto","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }else if(!validNombre())
            {
                MessageBox.Show("Debe introducir un nombre al producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!isValidPrice())
            {
                MessageBox.Show("Precio de producto no válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (producto != null)
                {

                }
                else
                {
                    Producto prod = new Producto(txtNombre.Text, 1, float.Parse(txtPrecio.Text), int.Parse(txtStock.Text), idFamilia);
                    if(await Producto.createProduct(prod))
                    {
                        producto = Producto.currentProduct;
                    }
                    else
                    {
                        MessageBox.Show("Error al crear el producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool validStock()
        {
            bool retorno = true;
            if(String.IsNullOrEmpty(txtStock.Text) || String.IsNullOrWhiteSpace(txtStock.Text))
            {
                retorno = false;
            }else if(txtStock.Text.Contains(" "))
            {
                retorno = false;
            }
            else if(int.Parse(txtStock.Text) <= 0)
            {
                retorno = false;
            }
            return retorno;
        }

        private void txtStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            imgStock.Visibility = Visibility.Visible;
            if(String.IsNullOrEmpty(txtStock.Text) || String.IsNullOrWhiteSpace(txtStock.Text))
            {
                imgStock.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgStock.ToolTip = "Debe introducir el stock";
            }else if(txtStock.Text.Contains(" "))
            {
                imgStock.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgStock.ToolTip = "El campo no admite espacios";
            }
            else
            {
                if(int.Parse(txtStock.Text) > 0)
                {
                    imgStock.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                    imgStock.ToolTip = null;
                }
                else
                {
                    imgStock.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgStock.ToolTip = "El stock no puede ser de 0";
                }
            }
        }
        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
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
    }
}
