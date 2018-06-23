using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using org.mariuszgromada.math.mxparser;

namespace calculette
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private org.mariuszgromada.math.mxparser.Expression _expression;
        private String _resultString;
        private String _calcString;
        private ObservableCollection<HistoElement> _historique = new ObservableCollection<HistoElement>();


        public MainWindow()
        {

            InitializeComponent();
            _resultString = "0";
            _calcString = "0";

            this.DataContext = this;
        }

        public string CalcString
        {
            get { return _calcString; }
            set
            {
                if (_calcString != value)
                {
                    _calcString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CalcString)));

                }
            }
        }


        public string ResultString
        {
            get { return _resultString; }
            set
            {
                if (_resultString != value)
                {
                    _resultString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultString)));
                }
            }
        }

        public org.mariuszgromada.math.mxparser.Expression Expression { get => _expression; set => _expression = value; }
        public ObservableCollection<HistoElement> Historique { get => _historique; set => _historique = value; }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            CalcString = AddToOperation(btn.Content.ToString());
        }

        private string AddToOperation(string symbol)
        {
            if (symbol == "Modulo")
                symbol = "#";
            if (symbol == "ln")
                symbol = "ln(";
            if (symbol == "log10")
                symbol = "log10(";
            if (symbol == "Sin")
                symbol = "sin(";
            if (symbol == "Cos")
                symbol = "cos(";
            if (symbol == "Tan")
                symbol = "tan(";
            if (symbol == "ArcSin")
                symbol = "arcsin(";
            if (symbol == "ArcCos")
                symbol = "arcos(";
            if (symbol == "ArcTan")
                symbol = "arctan(";
            if (symbol == "Exp")
                symbol = "exp(";

            return (CalcString == "0") ? symbol : String.Concat(CalcString, symbol);

        }

        private void Button_valid(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Button_remove(object sender, RoutedEventArgs e)
        {
            Remove();
        }
        private void Button_delete(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Calculate()
        {
            _expression = new org.mariuszgromada.math.mxparser.Expression(CalcString);

            if (_expression.checkSyntax())
            {
                Double result = _expression.calculate();
                ResultString = result.ToString();
                Console.WriteLine(_expression.calculate());
                HistoElement histo = new HistoElement(CalcString, ResultString);
                Historique.Add(histo);

            }
            else
                ResultString = "Erreur";
        }
        private void Remove()
        {
            if (CalcString.Length > 0)
                CalcString = CalcString.Remove(CalcString.Length - 1);
        }

        private void Delete()
        {
            CalcString = "0";
            ResultString = "0";
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {

                case Key.Delete:
                    Remove();
                    break;
                case Key.Back:
                    Remove();
                    break;
                case Key.Enter:
                    Calculate();
                    break;
                case Key.Add:
                    AddToOperation("+");
                    break;
                case Key.Subtract:
                    AddToOperation("-");
                    break;
                case Key.Divide:
                    AddToOperation("/");
                    break;
                case Key.Multiply:
                    AddToOperation("*");
                    break;
                case Key.OemComma:
                    AddToOperation(",");
                    break;
                case Key.Oem4:
                    AddToOperation(")");
                    break;
                case Key.Oem3:
                    AddToOperation("%");
                    break;
                case Key.NumPad0:
                    AddToOperation("0");
                    break;
                case Key.NumPad1:
                    AddToOperation("1");
                    break;
                case Key.NumPad2:
                    AddToOperation("2");
                    break;
                case Key.NumPad3:
                    AddToOperation("3");
                    break;
                case Key.NumPad4:
                    AddToOperation("4");
                    break;
                case Key.NumPad5:
                    AddToOperation("5");
                    break;
                case Key.NumPad6:
                    AddToOperation("6");
                    break;
                case Key.NumPad7:
                    AddToOperation("7");
                    break;
                case Key.NumPad8:
                    AddToOperation("8");
                    break;
                case Key.NumPad9:
                    AddToOperation("9");
                    break;
                case Key.D1:
                    AddToOperation("1");
                    break;
                case Key.D2:
                    AddToOperation("2");
                    break;
                case Key.D3:
                    AddToOperation("3");
                    break;
                case Key.D4:
                    AddToOperation("4");
                    break;
                case Key.D5:
                    String symbol = Keyboard.Modifiers == ModifierKeys.Shift ? symbol = "5" : symbol = "(";
                    AddToOperation(symbol);
                    break;
                case Key.D6:
                    symbol = Keyboard.Modifiers == ModifierKeys.Shift ? symbol = "6" : symbol = "-";
                    AddToOperation(symbol);
                    break;
                case Key.D7:
                    AddToOperation("7");
                    break;
                case Key.D8:
                    AddToOperation("8");
                    break;
                case Key.D9:
                    AddToOperation("9");
                    break;
                default:
                    break;
            }
        }

        private void CopyPasteOperation(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            StackPanel stk = btn.Content as StackPanel;

            HistoElement histo = stk.DataContext as HistoElement;

            CalcString = histo.Operation;
            ResultString = "0";
        }

        private void CopyPasteResult(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            StackPanel stk = btn.Content as StackPanel;

            HistoElement histo = stk.DataContext as HistoElement;

            CalcString = histo.Result;
            ResultString = "0";
        }






    }

}
