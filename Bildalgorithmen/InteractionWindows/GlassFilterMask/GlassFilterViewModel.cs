using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using De.DarkSunProgramming;
using De.DarkSunProgramming.Filters;
using System.Windows.Input;
using De.Darksunprogramming.Commands;

namespace Bildalgorithmen.InteractionWindows
{
    public class GlassFilterViewModel : INotifyPropertyChanged
    {
        #region commands

        /// <summary>
        /// Gets the command to decrease the used radius.
        /// </summary>
        public ICommand DecreaseRadiusCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the used radius.
        /// </summary>
        public ICommand IncreaseRadiusCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to cancel and close the mask window.
        /// </summary>
        public ICommand CancelCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to save and close the mask window.
        /// </summary>
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion

        #region events

        /// <summary>
        /// Raised, if the mask window shall be closed.
        /// </summary>
        public event EventHandler CloseRequested;

        private void OnCloseRequested()
        {
            if (CloseRequested != null)
                CloseRequested(this, null);
        }

        #endregion

        #region fields

        /// <summary>
        /// The radius of the filter.
        /// </summary>
        private int radius;

        /// <summary>
        /// Defines, wether the filter shall be used or not.
        /// </summary>
        private MaskResult result;

        /// <summary>
        /// The image after using the filter.
        /// </summary>
        private BitmapSource preview; 

        /// <summary>
        /// The image to use the filter on.
        /// </summary>
        private BitmapSource source;

        #endregion

        #region properties
         
        /// <summary>
        /// Gets or sets the radius to use in the filter.
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set { radius = value; OnPropertyChanged("Radius"); UseGlassFilter(); }
        }

        /// <summary>
        /// Gets the result, chosen by the user.
        /// </summary>
        public MaskResult Result
        {
            get { return result; }
        }

        /// <summary>
        /// Gets the image after using the filter.
        /// </summary>
        public BitmapSource PreviewImage
        {
            get { return preview; }
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the GlassFilterViewModel class.
        /// </summary>
        /// <param name="source">The image to use the filter on.</param>
        public GlassFilterViewModel(BitmapSource source)
        {
            this.source = source;
            this.radius = 1;

            DecreaseRadiusCommand = new DelegatedCommand(new Action(DecreaseRadius), delegate() { return true; });
            IncreaseRadiusCommand = new DelegatedCommand(new Action(IncreaseRadius), delegate() { return true; });

            CancelCommand = new DelegatedCommand(new Action(Cancel), delegate() { return true; });
            SaveCommand = new DelegatedCommand(new Action(Save), delegate() { return true; });

            UseGlassFilter(); 
        }

        #endregion

        #region methods

        private void Cancel()
        {
            result = MaskResult.Cancel;
            OnCloseRequested(); 
        }
        private void Save()
        {
            result = MaskResult.Save;
            OnCloseRequested(); 
        }

        private void DecreaseRadius() { DecreaseRadius(1); }
        private void IncreaseRadius() { IncreaseRadius(1); }

        public void DecreaseRadius(int amount = 1)
        {
            Radius = (radius - amount < 0) ? 0 : radius - amount;
        }

        public void IncreaseRadius(int amount = 1)
        {
            Radius = (radius + amount < 0) ? 0 : radius + amount;
        }

        public void UseGlassFilter()
        {
            if (source != null)
            {
                int stride = (int)(source.Width * (source.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * source.Height)];
                source.CopyPixels(pixels, stride, 0);

                try
                {
                    byte[] newPixels = GlassFilter.Convert(pixels, radius, stride);
                    preview = ImageHelpers.CreateImage(newPixels, source);
                }
                catch
                {
                    preview = null;
                }
            }
            else { source = null; }

            OnPropertyChanged("PreviewImage"); 
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

    }
}
