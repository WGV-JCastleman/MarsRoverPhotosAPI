using MarsRoverPhotosAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverPhotosAPI.Services
{
    public interface IPhotosService
    {
        List<PhotoRequest> ReadPhotoRequestsfromFile();
        List<PhotoResult> DownLoadPhotos(List<PhotoRequest> dates);     
    }
}
