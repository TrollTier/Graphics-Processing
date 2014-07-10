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
    /// Interaktionslogik für GainFilterMask.xaml
    /// </summary>
    public partial class GainFilterMask : Window
    {
        private GainFilterViewModel context;

        /// <summary>
        /// Gets the chosen bias.
        /// </summary>
        public int Bias
        {
            get { return context.Bias; }
        }

        /// <summary>
        /// Gets the chosen gain.
        /// </summary>
        public float Gain
        {
            get { return context.Gain; }
        }

        /// <summary>
        /// Gets the chosen result.
        /// </summary>
        public MaskResult Result
        {
            get { return context.Result; }
        }

        /// <summary>
        /// Initializes a new instance of the GainFilterMask class.
        /// </summary>
        /// <param name="source">The image to use the filter on.</param>
        public GainFilterMask(BitmapSource source)
        {
            InitializeComponent();
            DataContext = context = new GainFilterViewModel(source);

            context.CloseRequested += delegate(object sender, EventArgs e)
            {
                this.Close();
            };
        }
    }
}
