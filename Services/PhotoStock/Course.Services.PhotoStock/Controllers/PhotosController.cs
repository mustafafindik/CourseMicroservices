using Course.Services.PhotoStock.Dtos;
using Course.Services.PhotoStock.Helpers;
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
        [HttpPost]
        public async Task<IActionResult> PhotoSaveAsync(IFormFile photo, CancellationToken cancellationToken)
        {

            if (photo != null && photo.Length > 0)
            {             
                var filepath =  await FileHelper.Task<string>(photo, cancellationToken);
                PhotoDto photoDto = new() { Url = filepath };

                return Ok(photoDto);
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            
            if (!FileHelper.FileExist(photoUrl))
            {
                return BadRequest("Resim yok");
            }

            FileHelper.Delete(photoUrl);

            return Ok();
        }
    }
}
