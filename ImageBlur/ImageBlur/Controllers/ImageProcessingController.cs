using ImageBlur.Facades;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageBlur.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageProcessingController : ControllerBase
    {
        private readonly IImageProcessingFacade _imageProcessingFacade;

        public ImageProcessingController(IImageProcessingFacade imageProcessingFacade)
        {
            _imageProcessingFacade = imageProcessingFacade;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(string filePath)
        {
            try
            {
                var image = await _imageProcessingFacade.LoadImageAsync(filePath);
                await _imageProcessingFacade.SaveImageAsync(image, filePath, ImageFormat.Jpeg);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("blur")]
        public async Task<IActionResult> GetBlurImage(string filePath)
        {
            try
            {
                var image = await _imageProcessingFacade.LoadImageAsync(filePath);
                var result = await _imageProcessingFacade.BlurImageAsync(image);
                return File(ImageToByte(result), "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("sharpen")]
        public async Task<IActionResult> GetSharpenedImage(string filePath)
        {
            try
            {
                var image = await _imageProcessingFacade.LoadImageAsync(filePath);
                var result = await _imageProcessingFacade.SharpenImageAsync(image);
                return File(ImageToByte(result), "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("resize")]
        public async Task<IActionResult> ResizeImage(string filePath, int width, int height)
        {
            try
            {
                var image = await _imageProcessingFacade.LoadImageAsync(filePath);
                var resizedImage = await _imageProcessingFacade.ResizeImageAsync(image, new Size(width, height));
                await _imageProcessingFacade.SaveImageAsync(resizedImage, filePath, ImageFormat.Jpeg);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [NonAction]
        public byte[] ImageToByte(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}