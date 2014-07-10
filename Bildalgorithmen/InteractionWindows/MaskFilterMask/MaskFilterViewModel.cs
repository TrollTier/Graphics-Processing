using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using De.DarkSunProgramming.Filters;
using System.ComponentModel;
using System.Windows.Input;
using De.Darksunprogramming.Commands;

namespace Bildalgorithmen.InteractionWindows.MaskFilterMask
{
    public class MaskFilterViewModel : INotifyPropertyChanged
    {
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

        #region commands

        /// <summary>
        /// Gets the command to close the window without using the filter.
        /// </summary>
        public ICommand CancelCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to close the mask window and use the filter.
        /// </summary>
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion

        #region fields

        /// <summary>
        /// The alpha channel value to use for the filter.
        /// </summary>
        private byte alpha = 255; 

        /// <summary>
        /// The blue channel value to use for the filter.
        /// </summary>
        private byte blue = 255;

        /// <summary>
        /// The green channel value to use for the filter.
        /// </summary>
        private byte green = 255;

        /// <summary>
        /// The red channel value to use for the filter.
        /// </summary>
        private byte red = 255;

        /// <summary>
        /// The image after using the filter.
        /// </summary>
        private BitmapSource preview; 

        /// <summary>
        /// The source image.
        /// </summary>
        private BitmapSource source;

        /// <summary>
        /// Inidicates wether to save or not.
        /// </summary>
        private bool save = false;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the alpha channel value to use in the filter.
        /// </summary>
        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; UseMask(); OnPropertyChanged("Alpha"); }
        }

        /// <summary>
        /// Gets or sets the blue channel value to use in the filter.
        /// </summary>
        public byte Blue
        {
            get { return blue; }
            set { blue = value; UseMask(); OnPropertyChanged("Blue"); }
        }

        /// <summary>
        /// Gets or sets the green channel value to use in the filter.
        /// </summary>
        public byte Green
        {
            get { return green; }
            set { green = value; UseMask(); OnPropertyChanged("Green"); }
        }

        /// <summary>
        /// Gets or sets the red channel value to use in the filter.
        /// </summary>
        public Byte Red
        {
            get { return red; }
            set { red = value; UseMask(); OnPropertyChanged("Red"); }
        }

        /// <summary>
        /// Gets the image after using the filter with the current information.
        /// </summary>
        public BitmapSource PreviewImage
        {
            get { return preview; }
        }

        /// <summary>
        /// Gets a boolean value, indicating wether the data shall 
        /// be used or not.
        /// </summary>
        public bool Save
        {
            get { return save; }
        }

        #endregion

        #region ctors

        public MaskFilterViewModel(BitmapSource source)
        {
            this.source = source;
            this.preview = source;

            CancelCommand = new DelegatedCommand(new Action(CancelAndClose), delegate() { return true; });
            SaveCommand = new DelegatedCommand(new Action(SaveAndClose), delegate() { return true; });
        }

        #endregion

        #region methods

        public void UseMask()
        {
            if (source != null)
            {
                int stride = (int)(source.Width * (source.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * source.Height)];
                source.CopyPixels(pixels, stride, 0);

                try
                {
                    byte[] newPixels = MaskFilter.Convert(pixels, red, green, blue, alpha);
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

        private void CancelAndClose()
        {
            save = false;
            OnCloseRequested(); 
        }

        private void SaveAndClose()
        {
            save = true;
            OnCloseRequested(); 
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
