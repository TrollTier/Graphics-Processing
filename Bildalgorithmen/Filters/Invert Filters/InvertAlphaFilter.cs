using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class InvertAlphaFilter
    {
        public static byte[] Convert(byte[] pixels)
        {
            byte[] newPixels = new byte[pixels.Length];

            for (int i = 0; i < pixels.Length - 4; i += 4)
            {
                newPixels[i] = pixels[i];
                newPixels[i + 1] = pixels[i + 1];
                newPixels[i + 2] = pixels[i + 2];
                newPixels[i + 3] = (byte)(255 - pixels[i + 3]); 
            }

            return newPixels; 
        }
    }
}
