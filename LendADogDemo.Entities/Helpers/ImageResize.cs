using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace LendADogDemo.Entities.Helpers
{
    public static class ImageResize
    {
        public static Image Resize(this Image image, int width, int height)
        {
            var newSize = CalculateResizedDimensions(image, width, height);

            var destImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);

            //destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            destImage.SetResolution(72, 72);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var attribute = new ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(image, new Rectangle(new Point(0, 0), newSize), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attribute);
                }
            }
            return destImage;
        }

        private static System.Drawing.Size CalculateResizedDimensions(Image image, int desiredWidth, int desiredHeight)
        {
            var widthScale = (double)desiredWidth / image.Width;
            var heightScale = (double)desiredHeight / image.Height;

            var scale = widthScale < heightScale ? widthScale : heightScale;

            return new System.Drawing.Size
            {
                Width = (int)(scale * image.Width),
                Height = (int)(scale * image.Height)
            };
        }

        public static byte[] ImageToByteArray(this Image imageIn)
        {
            byte[] newPhoto = null;
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                newPhoto = ms.ToArray();
                ms.Close();
            }

            return newPhoto;
        }
    }
}
