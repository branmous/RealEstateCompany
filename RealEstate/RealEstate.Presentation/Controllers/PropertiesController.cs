﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.DTOs;
using System.Net;

namespace RealEstate.Presentation.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IAccountService _accountService;

        public PropertiesController(IPropertyService propertyService,
            IPropertyImageService propertyImageService,
            IAccountService accountService)
        {
            _propertyService = propertyService;
            _propertyImageService = propertyImageService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _propertyService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                return Ok(await _propertyService.FindByIdAsync(id));
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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PropertyDTO property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _accountService.GetUserAsyc(User.Identity!.Name!);
                var response = await _propertyService.SavePropertyAsync(new Property
                {
                    Address = property.Address,
                    CodeInternal = property.CodeInternal,
                    Name = property.Name,
                    Price = property.Price,
                    Year = property.Year,
                    OwnerId = user.Id,
                });
                return StatusCode((int)HttpStatusCode.Created, response);
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

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PropertyDTO property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _accountService.GetUserAsyc(User.Identity!.Name!);
                var response = await _propertyService.UpdateAsync(new Property
                {
                    Id = property.Id,
                    Address = property.Address,
                    CodeInternal = property.CodeInternal,
                    Name = property.Name,
                    Price = property.Price,
                    Year = property.Year,
                    OwnerId = user.Id,
                });

                return Ok(response);
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
                return Ok(new { Message = "images saved successfully" });
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
