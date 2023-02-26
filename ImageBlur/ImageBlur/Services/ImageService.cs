using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageBlur.Services
{
    public class ImageService : IImageService
    {
        public async Task<Image> LoadImageAsync(string filePath)
        {
            return await Task.Run(() => Image.Load(filePath));
        }

        public async Task<Image> BlurImageAsync(Image image)
        {
            await Task.Run(() => image.Mutate(x => x.GaussianBlur(5)));
            return image;
        }

        public async Task<Image> SharpenImageAsync(Image image)
        {
            await Task.Run(() => image.Mutate(x => x.GaussianSharpen(3)));
            return image;
        }

        public async Task<Image> ResizeImageAsync(Image image, Size size)
        {
            await Task.Run(() => image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Stretch
            })));
            return image;
        }

        public async Task SaveImageAsync(Image image, string filePath)
        {
            // Generate new file name with prefix
            string newFileName = "New_" + Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath);
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);
            // Save modified image with new file name
            await image.SaveAsync(newFilePath);
        }
    }
}