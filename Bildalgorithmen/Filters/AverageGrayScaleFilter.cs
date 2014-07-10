using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public static class AverageGrayScaleFilter
    {
        public static byte[] Convert(byte[] pixels, int bytesPerPixel)
        {
            if (pixels != null)
            {
                byte meridian;

                for (int i = 0; i < pixels.Length; i += bytesPerPixel)
                {
                    meridian = (byte)((pixels[i] + pixels[i + 1] + pixels[i + 2]) / 3);
                    pixels[i] = pixels[i + 1] = pixels[i + 2] = meridian;
                }

                return pixels;
            }

            return null; 
        }
    }
}
