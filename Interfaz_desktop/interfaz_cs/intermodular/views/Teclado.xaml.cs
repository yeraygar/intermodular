using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para LoginCliente.xaml
    /// </summary>
    public partial class Teclado : Window, INotifyPropertyChanged
    {
        #region Public Properties

        private bool _showNumericKeyboard;
        public bool ShowNumericKeyboard
        {
            get { return _showNumericKeyboard; }
            set { _showNumericKeyboard = value; this.OnPropertyChanged("ShowNumericKeyboard"); }
        }

        private string _result;
        private TextBox textbox;
        private LoginCliente loginCliente;

        public string Result
        {
            get { return _result; }
            private set { _result = value; this.OnPropertyChanged("Result"); }
        }

        #endregion

        #region Constructor

        public Teclado(PasswordBox owner, Window wndOwner)
        {
            InitializeComponent();
            this.Owner = wndOwner;
            this.DataContext = this;
            Result = "";
        }

        public Teclado(TextBox textbox, LoginCliente loginCliente)
        {
            InitializeComponent();
            this.textbox = textbox;
            this.loginCliente = loginCliente;
            Result = "";
        }

        #endregion

        #region Callbacks

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.CommandParameter.ToString())
                {
                    case "LSHIFT":
                        Regex upperCaseRegex = new Regex("[A-Z]");
                        Regex lowerCaseRegex = new Regex("[a-z]");
                        Button btn;
                        foreach (UIElement elem in AlfaKeyboard.Children) //iterate the main grid
                        {
                            Grid grid = elem as Grid;
                            if (grid != null)
                            {
                                foreach (UIElement uiElement in grid.Children)  //iterate the single rows
                                {
                                    btn = uiElement as Button;
                                    if (btn != null) // if button contains only 1 character
                                    {
                                        if (btn.Content.ToString().Length == 1)
                                        {
                                            if (upperCaseRegex.Match(btn.Content.ToString()).Success) // if the char is a letter and uppercase
                                                btn.Content = btn.Content.ToString().ToLower();
                                            else if (lowerCaseRegex.Match(button.Content.ToString()).Success) // if the char is a letter and lower case
                                                btn.Content = btn.Content.ToString().ToUpper();
                                        }

                                    }
                                }
                            }
                        }
                        break;

                    case "ALT":
                    case "CTRL":
                        break;

                    case "RETURN":
                        this.DialogResult = true;
                        break;

                    case "BACK":
                        if (Result.Length > 0)
                            Result = Result.Remove(Result.Length - 1);
                        break;

                    default:
                        Result += button.Content.ToString();
                        break;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}

