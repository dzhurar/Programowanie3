using System.Windows;
using System.Windows.Controls;

namespace Programowanie3
{
    /// <summary>
    /// Interaction logic for Calc.xaml
    /// </summary>
    public partial class Calc : Window
    {
        private double _firstNumber = 0;
        private string _operation = "";
        private bool _isNewNumber = true;
        public Calc()
        {
            InitializeComponent();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 0) return;
            if (double.TryParse(Display.Text, out double parsedValue))
            {
                _firstNumber = parsedValue;
                _operation = ((Button)sender).Content.ToString();
                _isNewNumber = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
            Result.Text = "";
            _firstNumber = 0;
            _operation = "";
            _isNewNumber = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 0 || string.IsNullOrEmpty(_operation))
            {
                return;
            }
            double result = 0;
            if (double.TryParse(Display.Text, out double secondNumber))
            {
                switch (_operation)
                {
                    case "+":
                        result = _firstNumber + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - secondNumber;
                        break;
                    case "x":
                        result = _firstNumber * secondNumber;
                        break;
                    case "÷":
                        if (secondNumber == 0)
                        {
                            Result.Text = "Cannot divide by zero";
                            return;
                        }
                        result = _firstNumber / secondNumber;
                        break;
                }
                Result.Text = result.ToString();
                Display.Text = result.ToString();
                _operation = "";
                _isNewNumber = true;
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string btnContent = ((Button)sender).Content.ToString();
            if(btnContent == "," && Display.Text.Contains(",")) return;
            if (btnContent == "0" && Display.Text == "0") return;

            if (btnContent == "," && (_isNewNumber || Display.Text == ""))
            {
                Display.Text = "0,";
                _isNewNumber = false;
                return;
            }

            if (_isNewNumber)
            {
                Display.Text = btnContent;
                _isNewNumber = false;
            }
            else
            {
                Display.Text += btnContent;
            }

        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if(Display.Text.Length > 0)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text))
                return;

            if (double.TryParse(Display.Text, out double currentVal))
            {
                double result = currentVal / 100.0;
                Display.Text = result.ToString();
                _isNewNumber = true;
            }
        }
    }
}