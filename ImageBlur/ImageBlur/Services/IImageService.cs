using System.Drawing;
using System.Drawing.Imaging;

namespace ImageBlur.Services
{
    public interface IImageService
    {
        Task<Image> LoadImageAsync(string filePath);
        Task<Image> BlurImageAsync(Image image);
        Task<Image> SharpenImageAsync(Image image);
        Task<Image> ResizeImageAsync(Image image, Size size);
        Task SaveImageAsync(Image image, string filePath, ImageFormat format);
    }
}