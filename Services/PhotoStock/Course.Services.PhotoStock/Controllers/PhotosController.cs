using Course.Services.PhotoStock.Dtos;
using Course.Services.PhotoStock.Helpers;
using Course.Services.PhotoStock.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        [HttpPost]
        public async Task<IActionResult> PhotoSaveAsync(IFormFile photo, CancellationToken cancellationToken)
        {

            var response = await _photoService.AddPhoto(photo, cancellationToken);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {

            var response = await _photoService.DeletePhoto(photoUrl);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
