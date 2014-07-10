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
    /// Interaktionslogik für GaussianBlurFilterWindow.xaml
    /// </summary>
    public partial class GaussianBlurFilterWindow : Window
    {
        private GaussianBlurViewModel context;

        /// <summary>
        /// Gets the radius of the gaussian blur filter.
        /// </summary>
        public int Radius
        {
            get { return context.Radius; }
        }

        /// <summary>
        /// Gets the number of times to run the filter.
        /// </summary>
        public int Rounds
        {
            get { return context.Rounds; }
        }

        /// <summary>
        /// Gets the sigma threshold.
        /// </summary>
        public float Sigma
        {
            get { return context.Sigma; }
        }

        /// <summary>
        /// Gets a boolean value, indicating wether the 
        /// data shall be used or not.
        /// </summary>
        public bool Save
        {
            get { return context.Save; }
        }

        /// <summary>
        /// Initializes a new instance of the GaussianBlurFilterWindow class.
        /// </summary>
        /// <param name="original">The image to run the gaussian blur filter on.</param>
        public GaussianBlurFilterWindow(BitmapSource original)
        {
            InitializeComponent();
            context = new GaussianBlurViewModel(original);
            DataContext = context;

            context.WindowCloseRequested += Context_CloseRequested;
        }

        private void Context_CloseRequested(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
