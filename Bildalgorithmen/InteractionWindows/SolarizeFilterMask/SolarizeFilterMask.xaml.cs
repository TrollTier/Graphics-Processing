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
    /// Interaktionslogik für SolarizeFilterMask.xaml
    /// </summary>
    public partial class SolarizeFilterMask : Window
    {
        private SolarizeFilterViewModel context;

        /// <summary>
        /// Gets the result, chosen by the user.
        /// </summary>
        public MaskResult Result
        {
            get { return context.Result; }
        }

        /// <summary>
        /// Gets the threshold, chosen by the user.
        /// </summary>
        public byte Threshold
        {
            get { return context.Threshold; }
        }

        /// <summary>
        /// Initializes a new instance of the SolarizeFilterMask class.
        /// </summary>
        /// <param name="source">The image to use the filter on.</param>
        public SolarizeFilterMask(BitmapSource source)
        {
            InitializeComponent();
            DataContext = context = new SolarizeFilterViewModel(source);

            context.CloseRequested += delegate(object sender, EventArgs e)
            {
                this.Close();
            };
        }
    }
}
