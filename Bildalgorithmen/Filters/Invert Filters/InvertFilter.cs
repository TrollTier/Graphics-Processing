using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public static class InvertFilter
    {
        public static byte[] Convert(byte[] pixels, int bytesPerPixel)
        {
            if (pixels != null)
            {
                for (int i = 0; i < pixels.Length; i += bytesPerPixel)
                {
                    pixels[i] = (Byte)(255 - pixels[i]);
                    pixels[i + 1] = (Byte)(255 - pixels[i + 1]);
                    pixels[i + 2] = (Byte)(255 - pixels[i + 2]);
                }

                return pixels; 
            }

            return null; 
        }
    }
}
