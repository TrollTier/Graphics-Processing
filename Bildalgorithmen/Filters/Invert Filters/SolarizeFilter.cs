using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class SolarizeFilter
    {
        public static byte[] Convert(byte[] pixels, byte threshold)
        {
            byte[] newPixels = new byte[pixels.Length];
            int median;
            byte invertBase = 255;

            for (int i = 0; i < pixels.Length - 4; i += 4)
            {
                median = (pixels[i] + pixels[i + 1] + pixels[i + 2]) / 3;

                if (median <= threshold)
                {
                    newPixels[i] = (byte)(invertBase - pixels[i]);
                    newPixels[i + 1] = (byte)(invertBase - pixels[i + 1]);
                    newPixels[i + 2] = (byte)(invertBase - pixels[i + 2]);
                }
                else
                {
                    newPixels[i] = pixels[i];
                    newPixels[i + 1] = pixels[i + 1];
                    newPixels[i + 2] = pixels[i + 2]; 
                }

                newPixels[i + 3] = pixels[i + 3]; 
            }

            return newPixels;
        }
    }
}
