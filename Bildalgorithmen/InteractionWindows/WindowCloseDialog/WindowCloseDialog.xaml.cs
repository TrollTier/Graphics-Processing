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
using System.Windows.Shapes;

namespace Bildalgorithmen.InteractionWindows
{
    /// <summary>
    /// Interaktionslogik für WindowCloseDialog.xaml
    /// </summary>
    public partial class WindowCloseDialog : Window
    {
        /// <summary>
        /// The result to return.
        /// </summary>
        public MessageBoxResult Result
        {
            get;
            private set; 
        }

        public WindowCloseDialog()
        {
            InitializeComponent();
        }

        private void cmdYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Close(); 
        }

        private void cmdNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close(); 
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Result = MessageBoxResult.No;
                this.Close(); 
            }
            else if (e.Key == Key.Enter)
            {
                Result = MessageBoxResult.Yes;
                this.Close(); 
            }
        }
    }
}
