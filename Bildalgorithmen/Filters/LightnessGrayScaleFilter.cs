using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    /// <summary>
    /// This class provides a method to convert an image to a gray scale image
    /// using the lightness method.
    /// The lightness method adds the highest and the lowest of the r/g/b values 
    /// and divides the sum by two.
    /// </summary>
    public static class LightnessGrayScaleFilter
    {
        public static byte[] Convert(Byte[] pixels, int bytesPerPixel)
        {
            if (pixels != null)
            {
                byte lightness;

                for (int i = 0; i < pixels.Length; i += bytesPerPixel)
                {
                    lightness = (byte)((Math.Max(Math.Max(pixels[i], pixels[i + 1]), pixels[i + 2]) 
                        + Math.Min(Math.Min(pixels[i], pixels[i + 1]), pixels[i + 2])) / 2); 
                    
                    pixels[i] = pixels[i + 1] = pixels[i + 2] = lightness;
                }

                return pixels;
            }

            return null; 
        }
    }
}
