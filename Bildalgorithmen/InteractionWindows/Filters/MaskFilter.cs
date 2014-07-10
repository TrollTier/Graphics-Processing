using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class MaskFilter
    {
        public static byte[] Convert(byte[] pixels, byte r, byte g, byte b, byte a)
        {
            byte[] newPixels = new byte[pixels.Length];

            for (int i = 0; i <= pixels.Length - 4; i += 4)
            {
                newPixels[i] = (byte)(pixels[i] & b);
                newPixels[i + 1] = (byte)(pixels[i + 1] & g);
                newPixels[i + 2] = (byte)(pixels[i + 2] & r);
                newPixels[i + 3] = (byte)(pixels[i + 3] & a);
            }

            return newPixels; 
        }
    }
}
