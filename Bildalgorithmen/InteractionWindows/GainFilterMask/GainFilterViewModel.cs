using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using De.DarkSunProgramming.Filters;
using De.Darksunprogramming.Commands;

namespace Bildalgorithmen.InteractionWindows
{
    public class GainFilterViewModel : MaskViewModel
    {
        #region commands

        /// <summary>
        /// Gets the command to decrease the bias, used by the filter.
        /// </summary>
        public ICommand DecreaseBiasCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to decrease the gain, used by the filter.
        /// </summary>
        public ICommand DecreaseGainCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the bias, used by the filter.
        /// </summary>
        public ICommand IncreaseBiasCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the gain, used by the filter.
        /// </summary>
        public ICommand IncreaseGainCommand
        {
            get;
            private set; 
        }

        #endregion

        #region fields

        /// <summary>
        /// The bias to use in the filter.
        /// </summary>
        private int bias = 0; 

        /// <summary>
        /// The gain to use in the filter.
        /// </summary>
        private float gain = 1.0f; 

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the bias, used by the filter.
        /// </summary>
        public int Bias
        {
            get { return bias; }
            set
            {
                if (bias != value)
                {
                    bias = value;
                    OnPropertyChanged("Bias");
                    UseGainFilter(); 
                }
            }
        }

        /// <summary>
        /// Gets or sets the gain, used by the filter.
        /// </summary>
        public float Gain
        {
            get { return gain; }
            set
            {
                if (gain != value)
                {
                    gain = value;
                    OnPropertyChanged("Gain");
                    UseGainFilter();
                }
            }
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the GainFilterViewModel class.
        /// </summary>
        /// <param name="source">The image to use the filter on.</param>
        public GainFilterViewModel(BitmapSource source)
            : base(source)
        {
            DecreaseBiasCommand = new DelegatedCommand(new Action(DecreaseBias), delegate() { return SourceImage != null; });
            DecreaseGainCommand = new DelegatedCommand(new Action(DecreaseGain), delegate() { return SourceImage != null; }); 
            IncreaseBiasCommand = new DelegatedCommand(new Action(IncreaseBias), delegate() { return SourceImage != null; });
            IncreaseGainCommand = new DelegatedCommand(new Action(IncreaseGain), delegate() { return SourceImage != null; });

            UseGainFilter(); 
        }

        #endregion

        #region methods

        public void UseGainFilter()
        {
            BitmapSource source = SourceImage;

            if (source != null)
            {
                int stride = (int)(source.Width * (source.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * source.Height)];
                source.CopyPixels(pixels, stride, 0);

                CreateImage(GainFilter.Convert(pixels, gain, bias));
            }
        }

        #endregion

        #region command handlers

        public void DecreaseBias()
        {
            Bias -= 1; 
        }

        public void DecreaseGain()
        {
            Gain = Math.Max(gain - 0.1f, 0); 
        }

        public void IncreaseBias()
        {
            Bias += 1;
        }

        public void IncreaseGain()
        {
            Gain += 0.1f; 
        }

        #endregion
    }
}
