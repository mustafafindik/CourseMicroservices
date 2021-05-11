using Course.Services.PhotoStock.Dtos;
using Course.Services.PhotoStock.Helpers;
using Course.Services.PhotoStock.Services.Abstract;
using Course.Shared.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.PhotoStock.Services.Concrete
{
    public class PhotoService : IPhotoService
    {
        public async Task<IDataResult<PhotoDto>> AddPhoto(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var filepath = await FileHelper.Task<string>(photo, cancellationToken);
                PhotoDto photoDto = new() { Url = filepath };

                return new SuccessDataResult<PhotoDto>(photoDto);
            }

            return new ErrorDataResult<PhotoDto>("Resim Kaydedilemedi");
        }

        public async Task<IResult> DeletePhoto(string photoUrl)
        {
            if (!FileHelper.FileExist(photoUrl))
            {
                return new ErrorResult("Resim Bulunamadı");
            }

            var result = FileHelper.Delete(photoUrl);
            if (result)
            {
                return new SuccessResult("Resim Silindi");
            }
            return new ErrorResult("Resim Silinemedi");
        }
    }
}
