using System.Drawing.Imaging;
using System.Drawing;

namespace ImageBlur.Facades
{
    public interface IImageProcessingFacade
    {
        Task<Image> LoadImageAsync(string filePath);
        Task<Image> BlurImageAsync(Image image);
        Task<Image> SharpenImageAsync(Image image);
        Task<Image> ResizeImageAsync(Image image, Size size);
        Task SaveImageAsync(Image image, string filePath, ImageFormat format);
    }
}