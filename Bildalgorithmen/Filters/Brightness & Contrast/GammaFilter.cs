using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class GammaFilter
    {

        public static byte[] Convert(byte[] pixels, float gamma, int bytesPerPixel)
        {
            if (pixels != null)
            {
                byte[] pixelsNew = new byte[pixels.Length];
                byte max = 255;
                byte min = 0;

                float r;
                float g;
                float b;

                for (int i = 0; i < pixels.Length; i += bytesPerPixel)
                {
                    r = pixels[i + 2];
                    g = pixels[i + 1];
                    b = pixels[i];

                    r *= gamma;
                    g *= gamma;
                    b *= gamma;

                    pixelsNew[i] = (b > 255) ? max : (b < 0) ? min : (byte)b;
                    pixelsNew[i + 1] = (g > 255) ? max : (g < 0) ? min : (byte)g;
                    pixelsNew[i + 2] = (r > 255) ? max : (r < 0) ? min : (byte)r;
                    pixelsNew[i + 3] = pixels[i + 3]; 
                }

                return pixelsNew;
            } else { return null; }
        }
    }
}
