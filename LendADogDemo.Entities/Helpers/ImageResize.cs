using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace LendADogDemo.Entities.Helpers
{
    public static class ImageResize
    {
        public static byte[] ResizeImageConvertInBytes(this Image image, int width, int height)
        {
            byte[] desiredArrey = null;

            var newSize = CalculateResizedDimensions(image, 360, 270);

            Bitmap fullSizeBitmap = new Bitmap(image, newSize);

            using (var graphics = Graphics.FromImage(fullSizeBitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var attribute = new ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(fullSizeBitmap, new Rectangle(new Point(0, 0), newSize), 0, 0, fullSizeBitmap.Width, fullSizeBitmap.Height, GraphicsUnit.Pixel, attribute);
                }
            }

            using (MemoryStream resultStream = new MemoryStream())
            {
                fullSizeBitmap.Save(resultStream, ImageFormat.Jpeg);
                desiredArrey = resultStream.ToArray();
                resultStream.Close();
            }
            fullSizeBitmap.Dispose();
            return desiredArrey;
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

    }
}
