using ImageBlur.Services;
using System.Drawing.Imaging;
using System.Drawing;

namespace ImageBlur.Facades
{
    public class ImageProcessingFacade : IImageProcessingFacade
    {
        private readonly IImageService _imageService;

        public ImageProcessingFacade(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<Image> LoadImageAsync(string filePath)
        {
            return await _imageService.LoadImageAsync(filePath);
        }

        public async Task<Image> BlurImageAsync(Image image)
        {
            return await _imageService.BlurImageAsync(image);
        }

        public async Task<Image> SharpenImageAsync(Image image)
        {
            return await _imageService.SharpenImageAsync(image);
        }

        public async Task<Image> ResizeImageAsync(Image image, Size size)
        {
            return await _imageService.ResizeImageAsync(image, size);
        }

        public async Task SaveImageAsync(Image image, string filePath, ImageFormat format)
        {
            await _imageService.SaveImageAsync(image, filePath, format);
        }
    }
}