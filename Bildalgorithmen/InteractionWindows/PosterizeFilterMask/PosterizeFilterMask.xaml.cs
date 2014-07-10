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
    /// Interaktionslogik für PosterizeFilterMask.xaml
    /// </summary>
    public partial class PosterizeFilterMask : Window
    {
        private PosterizeFilterViewModel context;

        /// <summary>
        /// Gets the levels to use.
        /// </summary>
        public int Levels
        {
            get { return context.Levels; }
        }

        /// <summary>
        /// Gets the result, chosen by the user.
        /// </summary>
        public MaskResult Result
        {
            get { return context.Result; }
        }

        /// <summary>
        /// Initializes a new instance of the PosterizeFilterMask class.
        /// </summary>
        /// <param name="source">The BitmapSource to use the filter on.</param>
        public PosterizeFilterMask(BitmapSource source)
        {
            InitializeComponent();
            DataContext = context = new PosterizeFilterViewModel(source);

            context.CloseRequested += delegate(object sender, EventArgs e)
            {
                this.Close();
            };
        }
    }
}
