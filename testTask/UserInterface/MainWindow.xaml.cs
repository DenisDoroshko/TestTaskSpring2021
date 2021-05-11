using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Calculating;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Current command
        /// </summary>
        private string currentCommand = null;

        /// <summary>
        /// Is initialized first value
        /// </summary>
        private bool isIntialized;

        /// <summary>
        /// Calculator
        /// </summary>
        private Calculator calculator;
        public MainWindow()
        {
            InitializeComponent();
            calculator = new Calculator(new MathUnit());
        }

        /// <summary>
        /// Processing number button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberClick(object sender, RoutedEventArgs e)
        {
            string text = (string)((Button)sender).Content;
            if (currentCommand == null || calculator.MathUnit.ExistingCommands[currentCommand] == CommandTypes.Binary)
                inputBox.Text += text;
            else
                MessageBox.Show("Number can't be added after unary operation.");
        }

        /// <summary>
        /// Processing clear button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetClick(object sender, RoutedEventArgs e)
        {
            calculator.Reset();
            isIntialized = false;
            inputBox.Text = "";
            resultBox.Text = "";
        }

        /// <summary>
        /// Processing undo button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoClick(object sender, RoutedEventArgs e)
        {
            resultBox.Text =  calculator.Undo().ToString();
            inputBox.Text = calculator.MathUnit.Initial.ToString();
            currentCommand = null;
            var history = calculator.History.ToList();
            history.Reverse();
            foreach (var command in history) 
            {
                if (command.CommandType == CommandTypes.Binary)
                    inputBox.Text += $" {command.ToString()} {command.ParameterValue}";
                else
                    inputBox.Text += $" {command.ToString()} ";
            }
            if (calculator.History.Count == 0)
                isIntialized = false;


        }

        /// <summary>
        /// Processing equality button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equalityClick(object sender, RoutedEventArgs e)
        {
            if (currentCommand != null)
            {
                var commandType = calculator.MathUnit.ExistingCommands[currentCommand];
                if (commandType == CommandTypes.Unary)
                {
                    resultBox.Text = calculator.Run(currentCommand).ToString();
                    currentCommand = null;
                    calculator.Reset();
                    isIntialized = false;
                    inputBox.Text = "";
                }
                else
                {
                    var items = inputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (items[items.Length - 1].Any(t => Char.IsDigit(t)))
                    {
                        double number;
                        double.TryParse(items[items.Length - 1], out number);
                        resultBox.Text = calculator.Run(currentCommand, number).ToString();
                        currentCommand = null;
                        calculator.Reset();
                        isIntialized = false;
                        inputBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Input number.");
                    }
                }
            }
        }

        /// <summary>
        /// Processing comma button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commaClick(object sender, RoutedEventArgs e)
        {
            var items = inputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length != 0 && items[items.Length - 1].All(t => t != ',') && items[items.Length - 1].Any(t => Char.IsDigit(t)))
                inputBox.Text += ',';
            else
                MessageBox.Show("Comma can't be added.");
        }

        /// <summary>
        /// Processing command button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandClick(object sender, RoutedEventArgs e)
        {
            string text = (string)((Button)sender).Content;
            if (currentCommand == null && inputBox.Text.Length != 0)
            {
                inputBox.Text += $" {text} ";
                currentCommand = text;
                if (isIntialized == false)
                {
                    var items = inputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    double initialNumber;
                    double.TryParse(items[0], out initialNumber);
                    calculator.MathUnit.Initial = initialNumber;
                    isIntialized = true;
                }
            }
            else
            {
                if(currentCommand != null)
                {
                    var commandType = calculator.MathUnit.ExistingCommands[currentCommand];
                    if(commandType == CommandTypes.Unary)
                    {
                        resultBox.Text = calculator.Run(currentCommand).ToString();
                        inputBox.Text += $" {text} ";
                        currentCommand = text;
                    }
                    else
                    {
                        var items = inputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (items[items.Length - 1].Any(t => Char.IsDigit(t)))
                        {
                            double number;
                            double.TryParse(items[items.Length - 1], out number);
                            resultBox.Text = calculator.Run(currentCommand, number).ToString();
                            inputBox.Text += $" {text} ";
                            currentCommand = text;
                        }
                        else
                        {
                            MessageBox.Show("Input number.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Input number.");
                }
            }
        }

        /// <summary>
        /// Processing set command button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCommandClick(object sender, RoutedEventArgs e)
        {
            var window = new CommandSelectingWindow(calculator.MathUnit.ExistingCommands.Keys.ToList()) { Owner = this };
            window.ShowDialog();
            string operation = window.SelectedCommand;
            if (calculator.AddCommand(operation))
            {
                var button = (Button)sender;
                button.Click -= setCommandClick;
                button.Click += commandClick;
                button.Content = operation;
            }
            else
            {
                MessageBox.Show("Command can't be added.");
            }
        }

        /// <summary>
        /// Processing remove command button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeCommandClick(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name) 
            {
                case "remove1":
                    removeCommand(addedCommand1);
                    break;
                case "remove2":
                    removeCommand(addedCommand2);
                    break;
                case "remove3":
                    removeCommand(addedCommand3);
                    break;
                case "remove4":
                    removeCommand(addedCommand4);
                    break;
            }
        }

        /// <summary>
        /// Removes command by given button
        /// </summary>
        /// <param name="button">Given button</param>
        private void removeCommand(Button button)
        {
            if((string)button.Content != "Set")
            {
                calculator.RemoveCommand((string)button.Content);
                button.Content = "Set";
                button.Click -= commandClick;
                button.Click += setCommandClick;
            }
        }
    }
}
