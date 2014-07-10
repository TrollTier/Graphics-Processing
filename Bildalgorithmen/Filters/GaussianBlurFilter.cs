using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace De.DarkSunProgramming.Filters
{
    public static class GaussianBlurFilter
    {
        /// <summary>
        /// Uses the gaussian blur filter to blur an image.
        /// See http://www.swageroo.com/wordpress/how-to-program-a-gaussian-blur-without-using-3rd-party-libraries/
        /// for a nice description of the algo.
        /// </summary>
        /// <param name="pixels">The pixels of the image.</param>
        /// <param name="bytesPerPixel">The bytes per pixel of the image.</param>
        /// <param name="radius">The radius of the used neighbour pixels.</param>
        /// <param name="sigma">The sigma value.</param>
        /// <param name="stride">The stride of the used image.</param>
        /// <param name="rounds">The number of times the algorithm shall run.</param>
        public static byte[] Convert(byte[] pixels, int bytesPerPixel, int radius, float sigma, int stride, int rounds)
        {
            double[,] kernel = CreateKernel(radius, sigma);

            if (pixels == null || kernel == null)
                return null;

            double r;
            double g;
            double b;

            byte rOri;
            byte gOri;
            byte bOri;

            int neighbourIndex;

            for (int i = 0; i < rounds; i++)
            {
                for (int pIndex = 0; pIndex < pixels.Length - bytesPerPixel; pIndex += bytesPerPixel)
                {
                    rOri = pixels[pIndex + 2];
                    gOri = pixels[pIndex + 1];
                    bOri = pixels[pIndex];

                    r = 0;
                    g = 0;
                    b = 0;

                    for (int x = -radius; x <= radius; x++)
                    {
                        for (int y = -radius; y <= radius; y++)
                        {
                            neighbourIndex = pIndex + (bytesPerPixel * x) + (y * stride);
                            SetPixelValues(pIndex, neighbourIndex, kernel[x + radius, y + radius], ref r, ref g, ref b, rOri, gOri, bOri, pixels);
                        }
                    }

                    pixels[pIndex] = GetByteForDouble(b);
                    pixels[pIndex + 1] = GetByteForDouble(g);
                    pixels[pIndex + 2] = GetByteForDouble(r);
                }
            }

            return pixels;
        }

        private static void SetPixelValues(int pIndex, int neighbourIndex, double factor,
            ref double r, ref double g, ref double b, byte rOri, byte gOri, byte bOri, 
             byte[] pixels)
        {
            if (neighbourIndex >= 0 && neighbourIndex < pixels.Length - 3)
            {
                r += (pixels[neighbourIndex + 2] * factor);
                g += (pixels[neighbourIndex + 1] * factor);
                b += (pixels[neighbourIndex] * factor);
            }
            else
            {
                r += rOri * factor;
                g += gOri * factor;
                b += bOri * factor;
            }
        }

        private static byte GetByteForDouble(double subPixel)
        {
            if (subPixel < 0)
                return 0;

            if (subPixel > 255)
                return 255;

            else
                return (byte)subPixel;
        }

        /// <summary>
        /// Creates the kernel matrix for the gaussian blur algorithm.
        /// See http://www.swageroo.com/wordpress/how-to-program-a-gaussian-blur-without-using-3rd-party-libraries/
        /// for a nice description of the formulas.
        /// </summary>
        /// <param name="radius">The radius of the blur effect.</param>
        /// <param name="sigma">The sigma value.</param>
        private static double[,] CreateKernel(int radius, float sigma)
        {
            if (radius > 0)
            {
                double sigmaPow = sigma * sigma;
                double sigmaPow2 = 2 * sigmaPow; 
                double firstProduct = 1 / (2 * Math.PI * sigmaPow);
                double[,] matrix = new double[2 * radius + 1, 2 * radius + 1];
                double sum = 0;
                double val;
                int xRel;
                int yRel; 

                for (int x = 0; x <= radius * 2; x++)
                {
                    xRel = x - radius; 
                    for (int y = 0; y <= radius * 2; y++)
                    {
                        yRel = y - radius; 
                        val = (firstProduct * Math.Pow(Math.E, -(((xRel * xRel) + (yRel * yRel)) / sigmaPow2)));
                        matrix[x, y] = val; 

                        sum += val;
                    }
                }

                double factor = 1 / sum;

                for (int x = 0; x <= radius * 2; x++)
                    for (int y = 0; y <= radius * 2; y++)
                        matrix[x, y] *= factor;

                return matrix; 
            }

            return null; 
        }
    }
}
