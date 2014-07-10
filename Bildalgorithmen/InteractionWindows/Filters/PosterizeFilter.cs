using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class PosterizeFilter
    {
        public static byte[] Convert(byte[] pixels, int levels)
        {
            byte[] newPixels = new byte[pixels.Length];
            int stepping = 256 / levels;

            for (int i = 0; i < pixels.Length - 4; i += 4)
            {
                newPixels[i] = (byte)(pixels[i] / stepping * stepping);
                newPixels[i + 1] = (byte)(pixels[i + 1] / stepping * stepping);
                newPixels[i + 2] = (byte)(pixels[i + 2] / stepping * stepping);
            }

            return newPixels;
        }
    }
}
