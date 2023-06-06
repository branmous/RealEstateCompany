using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces.Services;
using System.Net;

namespace RealEstate.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyImageService _propertyImageService;

        public PropertyImagesController(IPropertyService propertyService,
            IPropertyImageService propertyImageService)
        {
            this._propertyService = propertyService;
            _propertyImageService = propertyImageService;
        }

        [HttpPost("{id:int}/SetImages")]
        public async Task<IActionResult> PostSetImagesAsync(int id, List<IFormFile> propertyImages)
        {
            try
            {
                if (propertyImages.Count == 0)
                {
                    return BadRequest("You must upload at least one image of the property.");
                }

                var property = await _propertyService.FindByIdAsync(id);
                List<byte[]> images = new();

                foreach (var image in propertyImages)
                {
                    if (image.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await image.CopyToAsync(stream);
                            images.Add(stream.ToArray());
                        }
                    }
                }

                await _propertyImageService.SavePhotos(property, images);
                return Ok(new { Message = "Images saved successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
