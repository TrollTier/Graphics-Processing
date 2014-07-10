using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using De.DarkSunProgramming;
using De.Darksunprogramming.Commands;
using De.DarkSunProgramming.Filters;

namespace Bildalgorithmen.InteractionWindows
{
    public class GammaFilterViewModel : INotifyPropertyChanged
    {
        #region events

        /// <summary>
        /// Raised, if the mask window shall be closed.
        /// </summary>
        public event EventHandler WindowCloseRequested;

        private void OnCloseRequested()
        {
            if (WindowCloseRequested != null)
                WindowCloseRequested(this, null); 
        }

        #endregion

        #region commands

        /// <summary>
        /// Gets the command to cancel the process and close the window.
        /// </summary>
        public ICommand CancelCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to save the process and close the window.
        /// </summary>
        public ICommand SaveCommand
        {
            get;
            private set; 
        }

        #endregion

        #region fields

        /// <summary>
        /// The currently used gamma value.
        /// </summary>
        private float gamma = 1.0f;

        /// <summary>
        /// The least usable gamma value.
        /// </summary>
        private float min;

        /// <summary>
        /// The highest usable gamma value.
        /// </summary>
        private float max;

        /// <summary>
        /// The image after gamma correcture.
        /// </summary>
        private BitmapSource changedImage; 

        /// <summary>
        /// The image before any gamma filter has been used.
        /// </summary>
        private BitmapSource oImage; 

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the currently used gamma value.
        /// </summary>
        public float Gamma
        {
            get { return gamma; }
            set
            {
                if (value >= min && value <= max)
                {
                    gamma = value;
                    OnPropertyChanged("Gamma");
                    OnPropertyChanged("GammaPercent"); 
                    CreateGammaImage(); 
                }
            }
        }

        /// <summary>
        /// Gets the string that displays the current gamma correcture in percent.
        /// </summary>
        public string GammaPercent
        {
            get { return (int)((Math.Abs(gamma) - 1) * 100) + " %"; } 
        }

        /// <summary>
        /// Gets or sets the least usable gamma value.
        /// </summary>
        public float Minimum
        {
            get { return min; }
            set { min = value; OnPropertyChanged("Minimum"); }
        }

        /// <summary>
        /// Gets or sets the highest usable gamma value.
        /// </summary>
        public float Maximum
        {
            get { return max; }
            set { max = value; OnPropertyChanged("Maximum"); }
        }

        /// <summary>
        /// Gets the image after using the gamma filter.
        /// </summary>
        public BitmapSource ImageAfterGamma
        {
            get { return changedImage; }
        }

        /// <summary>
        /// Gets the result of the interaction.
        /// </summary>
        public MaskResult Result
        {
            get;
            private set; 
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the GammaFilterViewModel class.
        /// </summary>
        /// <param name="image">The image to use.</param>
        /// <param name="min">The least usable gamma value.</param>
        /// <param name="max">The highest usable gamma value.</param>
        public GammaFilterViewModel(BitmapSource image, float min, float max)
        {
            oImage = image;
            this.min = min;
            this.max = max;

            CancelCommand = new DelegatedCommand(new Action(Cancel), delegate() { return true; }); 
            SaveCommand = new DelegatedCommand(new Action(Save), delegate() { return true; });

            CreateGammaImage();
        }

        #endregion

        #region commandHandlers

        private void Cancel()
        {
            Result = MaskResult.Cancel;
            OnCloseRequested(); 
        }

        private void Save()
        {
            Result = MaskResult.Save;
            OnCloseRequested(); 
        }

        #endregion

        #region methods

        public void CreateGammaImage()
        {
            if (oImage != null)
            {
                try
                {
                    int stride = (int)(oImage.Width * (oImage.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * oImage.Height)];
                    oImage.CopyPixels(pixels, stride, 0);
                    int bytesPP = oImage.Format.BitsPerPixel / 8;

                    changedImage = ImageHelpers.CreateImage(GammaFilter.Convert(pixels, gamma, bytesPP), oImage); 
                }
                catch { changedImage = null; }

                OnPropertyChanged("ImageAfterGamma");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
