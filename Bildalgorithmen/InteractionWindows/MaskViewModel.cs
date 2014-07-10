using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using De.DarkSunProgramming;
using System.Windows.Input;
using De.Darksunprogramming.Commands;
using System.Windows;

namespace Bildalgorithmen.InteractionWindows
{
    public class MaskViewModel : INotifyPropertyChanged
    {
        #region commands

        /// <summary>
        /// Gets the command to cancel and close the mask window.
        /// </summary>
        public ICommand CancelCommand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the command to save and close the mask window.
        /// </summary>
        public ICommand SaveCommand
        {
            get;
            protected set;
        }

        #endregion

        #region events

        /// <summary>
        /// Raised, if the mask window shall be closed.
        /// </summary>
        public event EventHandler CloseRequested;

        protected virtual void OnCloseRequested()
        {
            if (CloseRequested != null)
                CloseRequested(this, null); 
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region fields

        /// <summary>
        /// The result, chosen by the user.
        /// </summary>
        private MaskResult result = MaskResult.Cancel; 

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
            protected set { preview = value; OnPropertyChanged("Preview"); }
        }

        /// <summary>
        /// Gets the source image to use the filter on.
        /// </summary>
        public BitmapSource SourceImage
        {
            get { return source; }
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the MaskViewModel class.
        /// </summary>
        /// <param name="source">The <code>BitmapSource</code> to use the filter on.</param>
        public MaskViewModel(BitmapSource source)
        {
            this.source = source;

            CancelCommand = new DelegatedCommand(new Action(Cancel), delegate() { return true; });
            SaveCommand = new DelegatedCommand(new Action(Save), delegate() { return true; });
        }

        #endregion

        #region methods

        protected virtual void CreateImage(byte[] pixels)
        {
            if (pixels != null)
            {
                try
                {
                    preview = ImageHelpers.CreateImage(pixels, source);
                    OnPropertyChanged("PreviewImage");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        protected virtual void Cancel()
        {
            result = MaskResult.Cancel;
            OnCloseRequested(); 
        }

        protected virtual void Save()
        {
            result = MaskResult.Save;
            OnCloseRequested();
        }

        #endregion
    }
}
