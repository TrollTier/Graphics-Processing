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
    /// Interaktionslogik für GlassFilterMask.xaml
    /// </summary>
    public partial class GlassFilterMask : Window
    {
        private GlassFilterViewModel context;

        /// <summary>
        /// Gets the chosen radius.
        /// </summary>
        public int Radius
        {
            get { return context.Radius; }
        }

        /// <summary>
        /// Gets the chosen mask result.
        /// </summary>
        public MaskResult Result
        {
            get { return context.Result; }
        }


        public GlassFilterMask(BitmapSource source)
        {
            InitializeComponent();
            context = new GlassFilterViewModel(source);
            DataContext = context;

            context.CloseRequested += delegate(object sender, EventArgs e)
            {
                this.Close();
            };
        }
    }
}
