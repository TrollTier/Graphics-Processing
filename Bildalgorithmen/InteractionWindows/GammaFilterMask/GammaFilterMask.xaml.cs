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
using De.DarkSunProgramming;

namespace Bildalgorithmen.InteractionWindows
{
    /// <summary>
    /// The GammaFilterMask window displays a mask for a gamma filter.
    /// It gets a <code>BitmapSource</code> as the original image and 
    /// provides a wysiwyg editor for the gamma value on this image.
    /// </summary>
    public partial class GammaFilterMask : Window
    {
        private GammaFilterViewModel context;

        /// <summary>
        /// Gets the selected gamma value.
        /// </summary>
        public float Gamma
        {
            get { return context.Gamma; }
        }

        /// <summary>
        /// Gets the operation to do after closing the mask.
        /// </summary>
        public MaskResult Result
        {
            get { return context.Result; }
        }

        /// <summary>
        /// Initializes a new instance of the GammaFilterMask class.
        /// </summary>
        /// <param name="image">The image to use for the filter.</param>
        /// <param name="min">The least usable gamma value.</param>
        /// <param name="max">The highest usable gamma value.</param>
        public GammaFilterMask(BitmapSource image, float min, float max)
        {
            InitializeComponent();
            context = new GammaFilterViewModel(image, min, max);
            DataContext = context;

            context.WindowCloseRequested += Context_CloseRequested;
        }

        private void Context_CloseRequested(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
