using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public class GlassFilter
    {
        private static Random random = new Random();

        public static byte[] Convert(byte[] pixels, int radius, int stride)
        {
            int[] coords = new int[2];
            byte[] newPixels = new byte[pixels.Length];

            int pN;

            for (int i = 0; i <= pixels.Length - 4; i += 4)
            {
                GetCoordinates(coords, radius);

                pN = Math.Min(Math.Max(0, i + (4 * coords[0]) + (coords[1] * stride)), pixels.Length - 4);

                newPixels[i] = pixels[pN];
                newPixels[i + 1] = pixels[pN + 1];
                newPixels[i + 2] = pixels[pN + 2];
                newPixels[i + 3] = pixels[pN + 3];
            }

            return newPixels;
        }

        private static void GetCoordinates(int[] coords, int radius)
        {
            coords[0] = random.Next(-(radius + 1), radius + 1);
            coords[1] = random.Next(-(radius + 1), radius + 1);

            //No return needed for arrays are reference values.
        }
    }
}
