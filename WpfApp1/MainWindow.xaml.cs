using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Text;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string expression = "";
        private bool isNewInput = true;
        private bool isResultDisplayed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (isResultDisplayed)
            {
                currentInput = "0";
                expression = "";
                isResultDisplayed = false;
            }

            var button = (Button)sender;
            string digit = button.Content.ToString();

            if (currentInput == "0" || isNewInput)
            {
                currentInput = digit;
                isNewInput = false;
            }
            else
            {
                currentInput += digit;
            }

            Display.Text = currentInput;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (isResultDisplayed)
            {
                currentInput = "0";
                expression = "";
                isResultDisplayed = false;
            }

            if (!currentInput.Contains(","))
            {
                if (isNewInput || currentInput == "0")
                {
                    currentInput = "0,";
                    isNewInput = false;
                }
                else
                {
                    currentInput += ",";
                }
            }

            Display.Text = currentInput;
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput == "0") return;

            if (currentInput.StartsWith("-"))
            {
                currentInput = currentInput.Substring(1);
            }
            else
            {
                currentInput = "-" + currentInput;
            }

            Display.Text = currentInput;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string operation = button.Content.ToString();

            if (isResultDisplayed)
            {
                expression = currentInput;
                isResultDisplayed = false;
            }

            if (!isNewInput)
            {
                expression += currentInput;
                currentInput = "0";
            }

            expression += operation;
            isNewInput = true;
        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string function = button.Content.ToString();
            double value = double.Parse(currentInput);

            try
            {
                switch (function)
                {
                    case "sin":
                        currentInput = Math.Sin(value * Math.PI / 180).ToString();
                        break;
                    case "cos":
                        currentInput = Math.Cos(value * Math.PI / 180).ToString();
                        break;
                    case "tan":
                        currentInput = Math.Tan(value * Math.PI / 180).ToString();
                        break;
                    case "x²":
                        currentInput = Math.Pow(value, 2).ToString();
                        break;
                    case "√x":
                        currentInput = Math.Sqrt(value).ToString();
                        break;
                    case "³√x":
                        currentInput = Math.Pow(value, 1.0 / 3).ToString();
                        break;
                    case "10^x":
                        currentInput = Math.Pow(10, value).ToString();
                        break;
                    case "log":
                        currentInput = Math.Log10(value).ToString();
                        break;
                    case "ln":
                        currentInput = Math.Log(value).ToString();
                        break;
                    case "n!":
                        currentInput = Factorial(value).ToString();
                        break;
                    case "|x|":
                        currentInput = Math.Abs(value).ToString();
                        break;
                    case "1/x":
                        currentInput = (1 / value).ToString();
                        break;
                }

                Display.Text = currentInput;
                isNewInput = true;
            }
            catch (Exception ex)
            {
                Display.Text = "Ошибка";
                currentInput = "0";
                expression = "";
                isNewInput = true;
            }
        }

        private double Factorial(double n)
        {
            if (n < 0) return double.NaN;
            if (n == 0) return 1;

            double result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private void ConstantButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string constant = button.Content.ToString();

            switch (constant)
            {
                case "π":
                    currentInput = Math.PI.ToString();
                    break;
                case "e":
                    currentInput = Math.E.ToString();
                    break;
            }

            Display.Text = currentInput;
            isNewInput = true;
        }

        private void ParenthesisButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string parenthesis = button.Content.ToString();

            if (isNewInput || currentInput == "0")
            {
                currentInput = parenthesis;
                isNewInput = false;
            }
            else
            {
                currentInput += parenthesis;
            }

            Display.Text = currentInput;
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            expression = "";
            Display.Text = currentInput;
            isNewInput = true;
            isResultDisplayed = false;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isNewInput)
                {
                    expression += currentInput;
                }

                string expr = expression.Replace("×", "*").Replace("÷", "/").Replace(",", ".");

                var result = new DataTable().Compute(expr, null);
                currentInput = result.ToString();
                Display.Text = currentInput;

                expression = "";
                isNewInput = true;
                isResultDisplayed = true;
            }
            catch (Exception ex)
            {
                Display.Text = "Ошибка";
                currentInput = "0";
                expression = "";
                isNewInput = true;
            }
        }
    }
}