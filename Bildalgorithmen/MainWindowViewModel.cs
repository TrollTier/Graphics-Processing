using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De.Darksunprogramming.Commands;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using De.DarkSunProgramming.Filters;
using Bildalgorithmen.InteractionWindows;
using System.Windows.Input;
using Bildalgorithmen.InteractionWindows.MaskFilterMask;

namespace Bildalgorithmen
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region commands

        /// <summary>
        /// Gets the command to use the average gray scale filter.
        /// </summary>
        public ICommand AverageGrayScaleFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to use the floyd-steinberg dithering filter 
        /// on the current image.
        /// </summary>
        public ICommand FloydSteinbergDitheringFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the gain filter on the current image.
        /// </summary>
        public ICommand GainFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the gamme filter on the current image.
        /// </summary>
        public ICommand GammaFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to use the gaussian blur filter.
        /// </summary>
        public ICommand GaussianBlurFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to use the glass filter on the current image.
        /// </summary>
        public ICommand GlassFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the lightness gray scale filter.
        /// </summary>
        public ICommand LightnessGrayScaleFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to use the luminosity gray scale filter.
        /// </summary>
        public ICommand LuminosityGrayScaleFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to run the invert filter.
        /// </summary>
        public ICommand InvertFilterCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to invert the alpha channel.
        /// </summary>
        public ICommand InvertAlphaFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the MaskFilter.
        /// </summary>
        public ICommand MaskFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the posterize command.
        /// </summary>
        public ICommand PosterizeFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to use the solarize filter on the current image.
        /// </summary>
        public ICommand SolarizeFilterCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to open a new file.
        /// </summary>
        public ICommand OpenFileCommand
        {
            get;
            private set; 
        }

        /// <summary>
        /// Gets the command to scale the image.
        /// </summary>
        public ICommand ScaleCommand
        {
            get;
            private set; 
        }

        #endregion

        #region fields

        /// <summary>
        /// The used image.
        /// </summary>
        private BitmapSource image;

        #endregion

        #region properties

        /// <summary>
        /// Gets the used image.
        /// </summary>
        public BitmapSource Image
        {
            get { return image; }
        }

        #endregion

        #region ctors

        public MainWindowViewModel()
        {
            InitializeCommands(); 
        }

        #endregion

        #region APIs

        /// <summary>
        /// Shows an OpenFileDialog and loads the selected image.
        /// </summary>
        public void OpenNewFile()
        {
            string file = Drakandor.InteractionHelpers.InteractionDialogs.GetFilePath();
            CreateImage(file); 
        }

        public void Scale()
        {
            Scale(1.5f);
        }

        public void Scale(float scaleFactor)
        {
            if (image != null && scaleFactor >= 0)
            {
                int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * image.Height)];
                image.CopyPixels(pixels, stride, 0);

                BitmapImage newImage = new BitmapImage();
                

            }
        }

        #endregion

        #region methods

        private void InitializeCommands()
        {
            AverageGrayScaleFilterCommand = new DelegatedCommand(new Action(AverageGrayScale), delegate() { return image != null; }); 
            LightnessGrayScaleFilterCommand = new DelegatedCommand(new Action(LightnessGrayScale), delegate() { return image != null; }); 
            LuminosityGrayScaleFilterCommand = new DelegatedCommand(new Action(LuminosityGrayScale), delegate() { return image != null; });

            FloydSteinbergDitheringFilterCommand = new DelegatedCommand(new Action(FloydSteinbergDithering), delegate() { return image != null; });
            GainFilterCommand = new DelegatedCommand(new Action(GainFilter), delegate() { return image != null; });
            GammaFilterCommand = new DelegatedCommand(new Action(Gamma), delegate() { return image != null; }); 
            GaussianBlurFilterCommand = new DelegatedCommand(new Action(GaussianBlur), delegate() { return image != null; });
            GlassFilterCommand = new DelegatedCommand(new Action(GlassFilter), delegate() { return image != null; });
            InvertFilterCommand = new DelegatedCommand(new Action(Invert), delegate() { return image != null; });
            InvertAlphaFilterCommand = new DelegatedCommand(new Action(InvertAlpha), delegate() { return image != null; });
            MaskFilterCommand = new DelegatedCommand(new Action(Mask), delegate() { return image != null; });
            PosterizeFilterCommand = new DelegatedCommand(new Action(Posterize), delegate() { return image != null; });
            SolarizeFilterCommand = new DelegatedCommand(new Action(Solarize), delegate() { return image != null; });

            ScaleCommand = new DelegatedCommand(new Action(Scale), delegate() { return image != null; });

            OpenFileCommand = new DelegatedCommand(new Action(OpenNewFile), delegate() { return true; });
        }

        private void CreateImage(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    image = new BitmapImage(new Uri(path));
                    OnPropertyChanged("Image");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            }
        }

        private void CreateImage(byte[] pixels)
        {
            if (pixels != null)
            {
                try
                {
                    BitmapSource newImage = BitmapSource.Create(image.PixelWidth, image.PixelHeight,
                        image.DpiX, image.DpiY,
                        image.Format, image.Palette, pixels,
                        (int)(image.Width * (image.Format.BitsPerPixel / 8)));

                    image = newImage;
                    OnPropertyChanged("Image");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private byte[] GetPixelsForImage()
        {
            try
            {
                int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                byte[] pixels = new Byte[(int)(stride * image.Height)];
                image.CopyPixels(pixels, stride, 0);

                return pixels;
            }
            catch { return null; }
        }

        #endregion

        #region filters

        /// <summary>
        /// Uses the average gray scale filter for the current image.
        /// For further information see De.DarkSunProgramming.AverageGrayScaleFilter class.
        /// </summary>
        public void AverageGrayScale()
        {
            int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
            Byte[] pixels = new Byte[(int)(stride * image.Height)];
            image.CopyPixels(pixels, stride, 0);

            CreateImage(AverageGrayScaleFilter.Convert(pixels, image.Format.BitsPerPixel / 8)); 
        }

        /// <summary>
        /// Uses the lightness gray scale filter for the current image.
        /// For further information see De.DarkSunProgramming.LightnessGrayScaleFilter class.
        /// </summary>
        public void LightnessGrayScale()
        {
            int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
            Byte[] pixels = new Byte[(int)(stride * image.Height)];
            image.CopyPixels(pixels, stride, 0);

            CreateImage(LightnessGrayScaleFilter.Convert(pixels, image.Format.BitsPerPixel / 8)); 
        }

        /// <summary>
        /// Uses the luminosity gray scale filter for the current image.
        /// For further information see De.DarkSunProgramming.LuminosityGrayScaleFilter class.
        /// </summary>
        public void LuminosityGrayScale()
        {
            int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
            Byte[] pixels = new Byte[(int)(stride * image.Height)];
            image.CopyPixels(pixels, stride, 0);

            CreateImage(LuminosityGrayScaleFilter.Convert(pixels, image.Format.BitsPerPixel / 8)); 
        }

        /// <summary>
        /// Uses the floyd-steinberg dithering algorithm on the current image.
        /// </summary>
        public void FloydSteinbergDithering()
        {
            int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
            Byte[] pixels = new Byte[(int)(stride * image.Height)];
            image.CopyPixels(pixels, stride, 0);

            CreateImage(FloydSteinbergDitheringFilter.Convert(pixels, image.Format.BitsPerPixel / 8, stride)); 
        }

        /// <summary>
        /// Shows a mask for the gain filter and uses the gain filter on the current 
        /// image, if desired.
        /// </summary>
        public void GainFilter()
        {
            if (image != null)
            {
                GainFilterMask mask = new GainFilterMask(image);
                mask.ShowDialog(); 

                if (mask.Result == De.DarkSunProgramming.MaskResult.Save)
                {
                    byte[] pixels = GetPixelsForImage();

                    if (pixels != null)
                    {
                        CreateImage(De.DarkSunProgramming.Filters.GainFilter.Convert(pixels, mask.Gain, mask.Bias));
                    }
                }
            }
        }
        
        /// <summary>
        /// Uses the gamma filter on the current image.
        /// </summary>
        public void Gamma()
        {
            if (image != null)
            {
                GammaFilterMask mask = new GammaFilterMask(image, -0.0f, 2.0f);
                mask.ShowDialog();

                if (mask.Result == De.DarkSunProgramming.MaskResult.Save)
                {
                    byte[] pixels = GetPixelsForImage();

                    if (pixels != null)
                    {
                        CreateImage(GammaFilter.Convert(pixels, mask.Gamma, image.Format.BitsPerPixel / 8));
                    }
                }
            }
        }

        /// <summary>
        /// Uses the gaussian blur filter for the current image.
        /// </summary>
        public void GaussianBlur()
        {
            if (image != null)
            {
                GaussianBlurFilterWindow win = new GaussianBlurFilterWindow(image);
                win.ShowDialog();

                if (win.Save == true)
                {
                    int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * image.Height)];
                    image.CopyPixels(pixels, stride, 0);

                    CreateImage(GaussianBlurFilter.Convert(pixels, image.Format.BitsPerPixel / 8, 
                        win.Radius, win.Sigma, stride, win.Rounds)); 
                }
            }
        }

        /// <summary>
        /// Uses the glass filter on the current image.
        /// </summary>
        public void GlassFilter()
        {
            if (image != null)
            {
                GlassFilterMask mask = new GlassFilterMask(image);
                mask.ShowDialog();

                if (mask.Result == De.DarkSunProgramming.MaskResult.Save)
                {
                    int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * image.Height)];
                    image.CopyPixels(pixels, stride, 0);

                    CreateImage(De.DarkSunProgramming.Filters.GlassFilter.Convert(pixels, mask.Radius, stride));
                }
            }
        }

        /// <summary>
        /// Runs the invert filter if an image is loaded.
        /// </summary>
        public void Invert()
        {
            if (image != null)
            {
                int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * image.Height)];
                image.CopyPixels(pixels, stride, 0);

                CreateImage(InvertFilter.Convert(pixels, image.Format.BitsPerPixel / 8));
            }
        }

        /// <summary>
        /// Runs the invert alpha filter on the current image.
        /// </summary>
        public void InvertAlpha()
        {
            if (image != null)
            {
                int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                Byte[] pixels = new Byte[(int)(stride * image.Height)];
                image.CopyPixels(pixels, stride, 0);

                CreateImage(InvertAlphaFilter.Convert(pixels)); 
            }
        }

        /// <summary>
        /// Used a mask filter on the current image.
        /// </summary>
        public void Mask()
        {
            if (image != null)
            {
                MaskFilterMask mask = new MaskFilterMask(image);
                mask.ShowDialog();

                if (mask.Save)
                {
                    int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * image.Height)];
                    image.CopyPixels(pixels, stride, 0);

                    CreateImage(MaskFilter.Convert(pixels, mask.Red, mask.Green, mask.Blue, mask.Alpha));
                }
            }
        }

        /// <summary>
        /// Uses the posterize filter on the current image.
        /// </summary>
        public void Posterize()
        {
            if (image != null)
            {
                PosterizeFilterMask mask = new PosterizeFilterMask(image);
                mask.ShowDialog();

                if (mask.Result == De.DarkSunProgramming.MaskResult.Save)
                {
                    int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * image.Height)];
                    image.CopyPixels(pixels, stride, 0);

                    CreateImage(PosterizeFilter.Convert(pixels, mask.Levels));
                }
            }
        }

        /// <summary>
        /// Uses the solarize filter on the current image.
        /// </summary>
        public void Solarize()
        {
            if (image != null)
            {
                SolarizeFilterMask mask = new SolarizeFilterMask(image);
                mask.ShowDialog();

                if (mask.Result == De.DarkSunProgramming.MaskResult.Save)
                {
                    int stride = (int)(image.Width * (image.Format.BitsPerPixel / 8));
                    Byte[] pixels = new Byte[(int)(stride * image.Height)];
                    image.CopyPixels(pixels, stride, 0);

                    CreateImage(SolarizeFilter.Convert(pixels, mask.Threshold));
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property)); 
        }

        #endregion
    }
}
