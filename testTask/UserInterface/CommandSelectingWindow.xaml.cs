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

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для CommandSelectingWindow.xaml
    /// </summary>
    public partial class CommandSelectingWindow : Window
    {
        public string SelectedCommand { get; set; } = "";
        public CommandSelectingWindow(List<string> existingCommands)
        {
            InitializeComponent();
            commandsBox.ItemsSource = existingCommands;
        }

        private void selectClick(object sender, RoutedEventArgs e)
        {
            SelectedCommand = (string)commandsBox.SelectedItem;
            this.Close();
        }
    }
}
