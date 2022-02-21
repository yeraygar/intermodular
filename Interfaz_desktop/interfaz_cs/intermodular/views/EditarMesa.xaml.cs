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
    /// Lógica de interacción para EditarMesa.xaml
    /// </summary>
    public partial class EditarMesa : Window
    {
        private Mesa mesa;
        public bool mesaUpdated = false;
        public EditarMesa(Mesa mesa)
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth * 0.6;
            this.Height = SystemParameters.PrimaryScreenHeight * 0.4;
            this.mesa = mesa;
            cargarMesa();
        }

        private void visualizarNumChar()
        {
            numChar.Content = txtNombreMesa.Text.Length.ToString() + "/5";
            numChar.Foreground = txtNombreMesa.Text.Length == 5 ? Brushes.Red : Brushes.Black;
        }

        private void txtNombreMesa_TextChanged(object sender, TextChangedEventArgs e)
        {
            visualizarNumChar();
            if (!txtNombreMesa.Tag.ToString().Equals(txtNombreMesa.Text))
            {
                imgNombreMesa.Visibility = Visibility.Visible;
                imgNombreMesa.Source = validNombreMesa() ? (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png") : (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                imgNombreMesa.ToolTip = validNombreMesa() ? null : "Debe introducir un nombre";
                btnEliminarCambios.Visibility = Visibility.Visible;
                btnGuardarCambios.Visibility = Visibility.Visible;
                txtNombreMesa.Tag = txtNombreMesa.Text;
            }
        }

        private void btnEliminarCambios_Click(object sender, RoutedEventArgs e)
        {
            cargarMesa();
            btnEliminarCambios.Visibility = Visibility.Hidden;
            btnGuardarCambios.Visibility = Visibility.Hidden;
            imgNombreMesa.Visibility = Visibility.Hidden;
            imgNumComensales.Visibility = Visibility.Hidden;
        }

        private void cargarMesa()
        {
            txtNombreMesa.Tag = mesa.name;
            comboBoxEstado.Tag = mesa.status ? comboBoxEstado.Items[0] : comboBoxEstado.Items[1];
            txtNombreMesa.Text = mesa.name;
            txtNumComensales.Text = mesa.comensalesMax.ToString();
            txtNumComensales.Tag = txtNumComensales.Text;
            comboBoxEstado.SelectedItem = mesa.status ? comboBoxEstado.Items[0] : comboBoxEstado.Items[1];
            imgNombreMesa.Visibility = Visibility.Hidden;
            imgNumComensales.Visibility = Visibility.Hidden;
            imgEstado.Visibility = Visibility.Hidden;
        }

        private bool validNombreMesa() => !String.IsNullOrEmpty(txtNombreMesa.Text) && !String.IsNullOrWhiteSpace(txtNombreMesa.Text);

        private void txtNumComensales_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[^0-9\\s]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool validNumComensales() => !String.IsNullOrEmpty(txtNumComensales.Text) && !String.IsNullOrWhiteSpace(txtNumComensales.Text);


        private void txtNumComensales_TextChanged(object sender, TextChangedEventArgs e)
        {
            if( txtNumComensales.Tag != null && !txtNumComensales.Tag.ToString().Equals(txtNumComensales.Text))
            {
                if(String.IsNullOrEmpty(txtNumComensales.Text) || String.IsNullOrWhiteSpace(txtNumComensales.Text))
                {
                    imgNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgNumComensales.ToolTip = "Debe introducir un número de comensales";
                }else if(txtNumComensales.Text.Contains(" "))
                {
                    imgNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\error.png");
                    imgNumComensales.ToolTip = "El campo no puede incluir espacios";
                }
                else
                {
                    imgNumComensales.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                    imgNumComensales.ToolTip = null;
                }
                imgNumComensales.Visibility = Visibility.Visible;
                btnEliminarCambios.Visibility = Visibility.Visible;
                btnGuardarCambios.Visibility = Visibility.Visible;
            }
        }

        private void comboBoxEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!comboBoxEstado.Tag.ToString().Equals(comboBoxEstado.SelectedItem.ToString()))
            {
                imgEstado.Visibility = Visibility.Visible;
                imgEstado.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\verify.png");
                comboBoxEstado.Tag = comboBoxEstado.SelectedItem;
                btnEliminarCambios.Visibility = Visibility.Visible;
                btnGuardarCambios.Visibility = Visibility.Visible;
            }
        }

        private void btn_cerrar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        private void btn_cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = Brushes.White;
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar.png");
        }

        private void btn_cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_cerrar.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            imgCerrar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cerrar_blanco.png");
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            if(validNombreMesa() && validNumComensales() && comboBoxEstado.SelectedIndex != -1)
            {
                mesa.name = txtNombreMesa.Text;
                mesa.status = comboBoxEstado.Items.IndexOf(comboBoxEstado.SelectedItem) == 0;
                mesa.comensalesMax = int.Parse(txtNumComensales.Text);
                mesaUpdated = await Mesa.updateTable(mesa._id, mesa);
                this.Close();
            }
            else
            {
                //Mostrar Error
                MessageBox.Show("Error al actualizar, hay campos incorrectos.", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void btnGuardarCambios_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGuardarCambios.Background = (Brush)(new BrushConverter().ConvertFrom("#63c554"));
            btnGuardarCambios.Foreground = Brushes.White;
            imgGuardarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\save_blanco.png");
        }

        private void btnEliminarCambios_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminarCambios.Background = (Brush)(new BrushConverter().ConvertFrom("#ff3232"));
            btnEliminarCambios.Foreground = Brushes.White;
            imgEliminarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel_blanco.png");
        }

        private void btnEliminarCambios_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEliminarCambios.Background = Brushes.White;
            btnEliminarCambios.Foreground = Brushes.Black;
            imgEliminarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\cancel.png");
        }

        private void btnGuardarCambios_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGuardarCambios.Background = Brushes.White;
            btnGuardarCambios.Foreground = Brushes.Black;
            imgGuardarCambios.Source = (ImageSource)new ImageSourceConverter().ConvertFrom("..\\..\\images\\save.png");
        }
    }
}
