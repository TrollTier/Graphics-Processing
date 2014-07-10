using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bildalgorithmen.InteractionWindows;

namespace Bildalgorithmen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel context; 

        public MainWindow()
        {
            InitializeComponent();
            DataContext = context = new MainWindowViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowCloseDialog dialog = new WindowCloseDialog();
            dialog.ShowDialog();

            if (dialog.Result == MessageBoxResult.No)
                e.Cancel = true; 
        }
    }
}
