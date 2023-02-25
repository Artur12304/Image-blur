using AForge.Imaging.Filters;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageBlur.Services
{
    public class ImageService : IImageService
    {
        public async Task<Image> LoadImageAsync(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return await Task.Run(() => Image.FromStream(stream));
        }

        public async Task<Image> BlurImageAsync(Image image)
        {
            var filter = new GaussianBlur(100, 100);
            return await Task.Run(() => filter.Apply((Bitmap)image));
        }

        public async Task<Image> SharpenImageAsync(Image image)
        {
            var filter = new Sharpen();
            return await Task.Run(() => filter.Apply((Bitmap)image));
        }

        public async Task<Image> ResizeImageAsync(Image image, Size size)
        {
            var resizedImage = new Bitmap(size.Width, size.Height);
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, size.Width, size.Height);
            }
            return await Task.FromResult((Image)resizedImage);
        }

        public async Task SaveImageAsync(Image image, string filePath, ImageFormat format)
        {
            // Generate new file name with prefix
            string newFileName = "New_" + Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath);
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);
            // Save modified image with new file name
            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                Bitmap bmp = new Bitmap(image);
                bmp.Save(stream, format);
                //image.Save(stream, format);
                await stream.FlushAsync();
            }
        }
    }
}