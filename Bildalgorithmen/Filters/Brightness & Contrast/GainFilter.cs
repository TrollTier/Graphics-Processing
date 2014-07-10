using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De.DarkSunProgramming.ImageProcessing;

namespace De.DarkSunProgramming.Filters
{
    public class GainFilter
    {
        public static byte[] Convert(byte[] pixels, float gain, int bias)
        {
            if (gain > 0)
            {
                byte[] newPixels = new byte[pixels.Length];

                for (int i = 0; i < pixels.Length - 4; i += 4)
                {
                    newPixels[i] = ImageHelpers.GetByteForDouble((pixels[i] * gain) + bias);
                    newPixels[i + 1] = ImageHelpers.GetByteForDouble((pixels[i + 1] * gain) + bias);
                    newPixels[i + 2] = ImageHelpers.GetByteForDouble((pixels[i + 2] * gain) + bias);
                }

                return newPixels;
            }

            return pixels;
        }
    }
}
