using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Bildalgorithmen
{
    public static class ImageHelpers
    {
        /// <summary>
        /// Creates a new BitmapImage with the specified pixels and the information
        /// of an original image.
        /// <para>
        /// This function does not check for errors.
        /// </para>
        /// </summary>
        /// <param name="pixels">The changed pixels, for example after using a filter.</param>
        /// <param name="image">The original image that has information about the format, width, height etc.</param>
        public static BitmapSource CreateImage(byte[] pixels, BitmapSource image)
        {
            BitmapSource newImage = BitmapSource.Create(image.PixelWidth, image.PixelHeight,
                image.DpiX, image.DpiY,
                image.Format, image.Palette, pixels,
                (int)(image.Width * (image.Format.BitsPerPixel / 8)));

            return newImage;
        }


        public static byte GetByteForDouble(double subPixel)
        {
            if (subPixel < 0)
                return 0;

            if (subPixel > 255)
                return 255;

            else
                return (byte)subPixel;
        }
    }
}
