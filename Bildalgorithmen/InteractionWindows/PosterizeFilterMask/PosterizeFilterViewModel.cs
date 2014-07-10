using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using De.Darksunprogramming.Commands;
using De.DarkSunProgramming.Filters;

namespace Bildalgorithmen.InteractionWindows
{
    public class PosterizeFilterViewModel : MaskViewModel
    {
        #region commands

        /// <summary>
        /// Gets the command to decrease the levels to use by one.
        /// </summary>
        public ICommand DecreaseLevelsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the levels to use by one.
        /// </summary>
        public ICommand IncreaseLevelsCommand
        {
            get;
            private set;
        }

        #endregion

        #region fields

        /// <summary>
        /// The levels of the posterizer filter.
        /// </summary>
        private int levels = 2;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the levels to use in the posterizer filter.
        /// </summary>
        public int Levels
        {
            get { return levels; }
            set 
            {
                if (levels != value)
                {
                    levels = value;
                    OnPropertyChanged("Levels"); 
                    Posterize();
                }
            }
        }

        #endregion

        #region ctors

        public PosterizeFilterViewModel(BitmapSource source)
            : base(source)
        {
            DecreaseLevelsCommand = new DelegatedCommand(new Action(DecreaseLevels), delegate() { return SourceImage != null; });
            IncreaseLevelsCommand = new DelegatedCommand(new Action(IncreaseLevels), delegate() { return SourceImage != null; });

            Posterize();
        }

        #endregion

        #region methods

        private void DecreaseLevels()
        {
            Levels = (levels - 1 < 2) ? 1 : levels - 1;
        }

        private void IncreaseLevels()
        {
            Levels = (levels + 1 < 2) ? 1 : levels + 1;
        }

        public void Posterize()
        {
            BitmapSource source = SourceImage;

            if (source != null)
            {
                int stride = (int)(source.Width * (source.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * source.Height)];
                source.CopyPixels(pixels, stride, 0);

                CreateImage(PosterizeFilter.Convert(pixels, levels)); 
            }
        }

        #endregion
    }
}
