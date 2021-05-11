using Course.Services.PhotoStock.Dtos;
using Course.Shared.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.PhotoStock.Services.Abstract
{
    public interface IPhotoService
    {
        Task<IDataResult<PhotoDto>> AddPhoto(IFormFile photo, CancellationToken cancellationToken);
        Task<IResult> DeletePhoto(string photoUrl);
    }
}
