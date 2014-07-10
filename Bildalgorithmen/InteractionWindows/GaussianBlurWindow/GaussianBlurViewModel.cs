using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using De.DarkSunProgramming.Filters;
using System.Windows;
using System.ComponentModel;
using De.Darksunprogramming.Commands;

namespace Bildalgorithmen.InteractionWindows
{
    public class GaussianBlurViewModel : INotifyPropertyChanged
    {
        #region consts

        private const int RADIUS_MIN = 1;
        private const int ROUNDS_MIN = 1; 

        #endregion

        #region events

        /// <summary>
        /// Raised, if the parent window shall be closed.
        /// </summary>
        public event EventHandler WindowCloseRequested;

        private void OnWindowCloseRequested()
        {
            if (WindowCloseRequested != null)
                WindowCloseRequested(this, new EventArgs()); 
        }

        #endregion

        #region commands

        /// <summary>
        /// Gets the command to decrease the radius.
        /// </summary>
        public DelegatedCommand DecreaseRadiusCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the command of the rounds used for the filter.
        /// </summary>
        public DelegatedCommand DecreaseRoundsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to decrease the sigma value.
        /// </summary>
        public DelegatedCommand DecreaseSigmaCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the radius.
        /// </summary>
        public DelegatedCommand IncreaseRadiusCommand
        {
            get; private set;
        }

        /// <summary>
        /// Gets the command to increase the rounds, used for the filter.
        /// </summary>
        public DelegatedCommand IncreaseRoundsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to increase the sigma value.
        /// </summary>
        public DelegatedCommand IncreaseSigmaCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to close and cancel.
        /// </summary>
        public DelegatedCommand CancelCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to close the window and save the results.
        /// </summary>
        public DelegatedCommand SaveCommand
        {
            get;
            private set; 
        }

        #endregion

        #region fields

        /// <summary>
        /// The image after running the filter.
        /// </summary>
        private BitmapSource blurredImage; 

        /// <summary>
        /// The originalImage to use the filter on.
        /// </summary>
        private BitmapSource originalImage;

        /// <summary>
        /// The radius to get neighbour pixels in.
        /// </summary>
        private int radius = 1; 

        /// <summary>
        /// The number of times to run the gaussian blur filter.
        /// </summary>
        private int rounds = 1;

        /// <summary>
        /// A threshold. The higher sigma, the 'more blurry' the originalImage gets.
        /// </summary>
        private float sigma = 1.5f;

        #endregion

        #region properties

        /// <summary>
        /// Gets the blurred image.
        /// </summary>
        public BitmapSource BlurredImage
        {
            get
            {
                return blurredImage; 
            }
        }

        /// <summary>
        /// Gets or sets the radius of the gaussian blur filter.
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set 
            { 
                radius = value; 
                OnPropertyChanged("Radius");
                GaussianBlur(); 
            }
        }

        /// <summary>
        /// Gets or sets the number of times to run the filter.
        /// </summary>
        public int Rounds
        {
            get { return rounds; }
            set 
            { 
                rounds = value;
                OnPropertyChanged("Rounds");
                GaussianBlur(); 
            }
        }

        /// <summary>
        /// Gets a boolean value, indicating wether the 
        /// data shall be used or not.
        /// </summary>
        public bool Save
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the sigma threshold.
        /// </summary>
        public float Sigma
        {
            get { return sigma; }
            set 
            { 
                sigma = value;
                OnPropertyChanged("Sigma");
                GaussianBlur(); 
            }
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the GaussianBlurViewModel class.
        /// </summary>
        /// <param name="origin">The image to use the filter on.</param>
        public GaussianBlurViewModel(BitmapSource origin)
        {
            this.originalImage = origin;
            this.Save = false;

            InitializeCommands();
            GaussianBlur(); 
        }

        private void InitializeCommands()
        {
            DecreaseRadiusCommand = new DelegatedCommand(new Action(DecreaseRadius), delegate() { return true; });
            DecreaseRoundsCommand = new DelegatedCommand(new Action(DecreaseRounds), delegate() { return true; });
            DecreaseSigmaCommand = new DelegatedCommand(new Action(DecreaseSigma), delegate() { return true; });
            IncreaseRadiusCommand = new DelegatedCommand(new Action(IncreaseRadius), delegate() { return true; });
            IncreaseRoundsCommand = new DelegatedCommand(new Action(IncreaseRounds), delegate() { return true; });
            IncreaseSigmaCommand = new DelegatedCommand(new Action(IncreaseSigma), delegate() { return true; }); 

            SaveCommand = new DelegatedCommand(new Action(SaveAndClose), delegate() { return true; });
            CancelCommand = new DelegatedCommand(new Action(CancelAndClose), delegate() { return true; });
        }

        #endregion

        #region methods

        public void GaussianBlur()
        {
            if (originalImage != null)
            {
                int stride = (int)(originalImage.Width * (originalImage.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * originalImage.Height)];
                originalImage.CopyPixels(pixels, stride, 0);

                try
                {
                    byte[] newPixels = GaussianBlurFilter.Convert(pixels, originalImage.Format.BitsPerPixel / 8, radius, sigma, stride, rounds);
                    blurredImage = ImageHelpers.CreateImage(newPixels, originalImage);
                }
                catch
                {
                    blurredImage = null;
                }
            } else { blurredImage = null; }

            OnPropertyChanged("BlurredImage"); 
        }

        #endregion

        #region command handlers

        private void DecreaseRadius()
        {
            Radius = Math.Max(RADIUS_MIN, radius - 1); 
        }

        private void DecreaseRounds()
        {
            Rounds = Math.Max(1, rounds - 1); 
        }

        private void DecreaseSigma()
        {
            Sigma = Math.Max(0f, sigma - 0.1f);
        }

        private void IncreaseRadius()
        {
            Radius++;
        }

        private void IncreaseRounds()
        {
            Rounds += 1; 
        }

        private void IncreaseSigma()
        {
            Sigma += 0.1f; 
        }

        private void SaveAndClose()
        {
            Save = true;
            OnWindowCloseRequested();
        }

        private void CancelAndClose()
        {
            Save = false;
            OnWindowCloseRequested();
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
