using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using MarsRoverPhotosAPI.Models;
using MarsRoverPhotosAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarsRoverPhotosAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotosService _photosService;
        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;

        }

        [HttpGet]
        [Route("user/{user}/Greetings")]
        public string GetMyGreetings(string user)
        {
            var client = new HttpClient();

            return $"Hello {user}, Welcome to Mars Rover photos Api";

        }
        [HttpGet]
        [Route("requestDates")]
        public List<PhotoRequest> GetRequestDates()
        {
            return _photosService.ReadPhotoRequestsfromFile();

        }
        [HttpPost]
        [Route("DownloadPhotosByDate")]
        public List<PhotoResult> DownloadPhotosByDate([FromBody] List<PhotoRequest> requests)
        {
            return _photosService.DownLoadPhotos(requests);
        }

        [HttpGet]
        [Route("DownloadPhotosByFile")]
        public List<PhotoResult> DownloadPhotosByFile()
        {
            var requests = _photosService.ReadPhotoRequestsfromFile();
            return _photosService.DownLoadPhotos(requests);
        }
    }
}