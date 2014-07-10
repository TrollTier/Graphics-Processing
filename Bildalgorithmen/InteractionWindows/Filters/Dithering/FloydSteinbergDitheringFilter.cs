using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.DarkSunProgramming.Filters
{
    public static class FloydSteinbergDitheringFilter
    {
        private static byte THRESHOLD_MONOCHROME = 127;
        private static byte MAX_BYTE = 255;
        private static byte MIN_BYTE = 0; 


        public static byte[] Convert(byte[] pixels, int bytesPerPixel, int stride)
        {
            byte[] newPixels = new byte[pixels.Length];

            byte r;  
            byte g;
            byte b;
            
            byte rN;
            byte gN;
            byte bN; 

            int rDiff;
            int gDiff;
            int bDiff;
            
            double fR = 7 / 16; // Factor for pixel to the right
            double fBL = 3 / 16; // Factor for pixel bottom left
            double fB = 5 / 16; // Factor for bottom pixel
            double fBR = 1 / 16; // Factor for pixel bottom right

            int pIndexB;   // Pixel index for neighbour bottom left

            int x;
            int y;
            int width = stride / bytesPerPixel;
            int height = (pixels.Length / bytesPerPixel / width);

            byte newVal;
             

            for (int pixel = 0; pixel < pixels.Length; pixel += bytesPerPixel)
            {
                y = pixel / stride;
                x = (pixel % stride) / bytesPerPixel; 

                r = pixels[pixel + 2];
                g = pixels[pixel + 1];
                b = pixels[pixel];

                newVal = GetMonochromeValueForColor(GetByteForDouble((r + g + b) / 3));
                r = newVal;
                g = newVal;
                b = newVal;

                newPixels[pixel + 2] = r;
                newPixels[pixel + 1] = g;
                newPixels[pixel] = b;
                newPixels[pixel + 3] = pixels[pixel + 3]; 

                if (x < width - 1) //Has right neighbour
                {
                    rN = pixels[pixel + 6];
                    gN = pixels[pixel + 5];
                    bN = pixels[pixel + 4];

                    rDiff = r - rN;
                    gDiff = g - gN;
                    bDiff = b - bN;

                    newPixels[pixel + 6] = GetByteForDouble(rN + (rDiff * fR));
                    newPixels[pixel + 5] = GetByteForDouble(gN + (gDiff * fR));
                    newPixels[pixel + 4] = GetByteForDouble(bN + (gDiff * fR));
                    newPixels[pixel + 7] = pixels[pixel + 3]; 
                }

                if (y < height - 1) //Has bottom neighbours
                {
                    pIndexB = pixel + stride - bytesPerPixel;   //Bottom left neighbour

                    if (x > 0) //Has bottom left neighbour
                    {
                        rN = pixels[pIndexB + 2];
                        gN = pixels[pIndexB + 1];
                        bN = pixels[pIndexB];

                        rDiff = r - rN;
                        gDiff = g - gN;
                        bDiff = b - bN;

                        newPixels[pIndexB + 2] = GetByteForDouble(rN + (rDiff * fBL));
                        newPixels[pIndexB + 1] = GetByteForDouble(gN + (gDiff * fBL));
                        newPixels[pIndexB] = GetByteForDouble(bN + (bDiff * fBL));
                        newPixels[pIndexB + 3] = pixels[pixel + 3]; 
                    }

                    pIndexB += bytesPerPixel;
                    rN = pixels[pIndexB + 2];
                    gN = pixels[pIndexB + 1];
                    bN = pixels[pIndexB];

                    rDiff = r - rN;
                    gDiff = g - gN;
                    bDiff = b - bN;

                    newPixels[pIndexB + 2] = GetByteForDouble(rN + (rDiff * fB));
                    newPixels[pIndexB + 1] = GetByteForDouble(gN + (gDiff * fB));
                    newPixels[pIndexB] = GetByteForDouble(bN + (bDiff * fB));
                    newPixels[pIndexB + 3] = pixels[pixel + 3]; 

                    pIndexB += bytesPerPixel;
                    if (x < width - 1) //Has bottom right neighbour
                    {
                        rN = pixels[pIndexB + 2];
                        gN = pixels[pIndexB + 1];
                        bN = pixels[pIndexB];

                        rDiff = r - rN;
                        gDiff = g - gN;
                        bDiff = b - bN;

                        newPixels[pIndexB + 2] = GetByteForDouble(rN + (rDiff * fBR));
                        newPixels[pIndexB + 1] = GetByteForDouble(gN + (gDiff * fBR));
                        newPixels[pIndexB] = GetByteForDouble(bN + (bDiff * fBR));
                        newPixels[pIndexB + 3] = pixels[pixel + 3]; 
                    }
                }
            }

            return newPixels; 
        }

        private static byte GetMonochromeValueForColor(byte color)
        {
            return (color <= THRESHOLD_MONOCHROME) ? MIN_BYTE : MAX_BYTE; 
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
    }
}
