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

namespace Bildalgorithmen.InteractionWindows.MaskFilterMask
{
    /// <summary>
    /// Interaktionslogik für MaskFilterMask.xaml
    /// </summary>
    public partial class MaskFilterMask : Window
    {
        private MaskFilterViewModel context;

        /// <summary>
        /// Gets the alpha channel value, chosen by the user.
        /// </summary>
        public byte Alpha
        {
            get { return context.Alpha; }
        }

        /// <summary>
        /// Gets the blue channel value, chosen by the user.
        /// </summary>
        public byte Blue
        {
            get { return context.Blue; }
        }

        /// <summary>
        /// Gets the green channel value, chosen by the user.
        /// </summary>
        public byte Green
        {
            get { return context.Green; }
        }

        /// <summary>
        /// Gets the red channel value, chosen by the user.
        /// </summary>
        public byte Red
        {
            get { return context.Red; }
        }

        /// <summary>
        /// Gets a boolean value, indicating wether or not the 
        /// filter shall be executed on the image.
        /// </summary>
        public bool Save
        {
            get { return context.Save; }
        }

        public MaskFilterMask(BitmapSource source)
        {
            InitializeComponent();
            context = new MaskFilterViewModel(source);
            DataContext = context;

            context.CloseRequested += delegate(object sende, EventArgs e)
            {
                this.Close();
            };
        }
    }
}
