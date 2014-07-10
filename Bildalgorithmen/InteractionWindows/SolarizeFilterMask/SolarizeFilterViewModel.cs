using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using De.DarkSunProgramming.Filters;
using System.Windows.Input;
using De.Darksunprogramming.Commands;

namespace Bildalgorithmen.InteractionWindows
{
    public class SolarizeFilterViewModel : MaskViewModel
    {
        #region commands

        /// <summary>
        /// Gets the command to decrease the levels to use by one.
        /// </summary>
        public ICommand DecreaseThresholdCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the levels to use by one.
        /// </summary>
        public ICommand IncreaseThresholdCommand
        {
            get;
            private set;
        }

        #endregion

        #region fields

        /// <summary>
        /// The levels of the posterizer filter.
        /// </summary>
        private byte threshold = 128;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the levels to use in the posterizer filter.
        /// </summary>
        public byte Threshold
        {
            get { return threshold; }
            set 
            {
                if (threshold != value)
                {
                    threshold = value;
                    OnPropertyChanged("Threshold"); 
                    Solarize();
                }
            }
        }

        #endregion

        #region ctors

        public SolarizeFilterViewModel(BitmapSource source)
            : base(source)
        {
            DecreaseThresholdCommand = new DelegatedCommand(new Action(DecreaseThreshold), delegate() { return SourceImage != null; });
            IncreaseThresholdCommand = new DelegatedCommand(new Action(IncreaseThreshold), delegate() { return SourceImage != null; });

            Solarize();
        }

        #endregion

        #region methods

        private void DecreaseThreshold()
        {
            Threshold = (byte)(Math.Min(Math.Max(0, threshold - 1), 255));
        }

        private void IncreaseThreshold()
        {
            Threshold = (byte)(Math.Min(Math.Max(0, threshold + 1), 255));
        }

        public void Solarize()
        {
            BitmapSource source = SourceImage;

            if (source != null)
            {
                int stride = (int)(source.Width * (source.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * source.Height)];
                source.CopyPixels(pixels, stride, 0);

                CreateImage(SolarizeFilter.Convert(pixels, threshold)); 
            }
        }

        #endregion
    }
}
