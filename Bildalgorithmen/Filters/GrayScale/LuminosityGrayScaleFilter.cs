using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public static class LuminosityGrayScaleFilter
    {
        public static byte[] Convert(byte[] pixels, int bytesPerPixel)
        {
            byte value; 

            if (pixels != null)
            {
                for (int i = 0; i < pixels.Length; i += bytesPerPixel)
                {
                    value = (byte)((pixels[i] * 0.07) 
                        + (pixels[i + 1] * 0.71)
                        + (pixels[i + 2] * 0.21));

                    pixels[i] = pixels[i + 1] = pixels[i + 2] = value;
                }

                return pixels;
            }

            return null; 
        }
    }
}
