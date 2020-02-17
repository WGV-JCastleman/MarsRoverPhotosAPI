using MarsRoverPhotosAPI.DomainServices;
using MarsRoverPhotosAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MarsRoverPhotosAPI.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IConfiguration _config;
        private IHostingEnvironment _env;
        private const string DirectoryPath = "SaveToDirectoryPath";
        private const string RequestPath = "RequestPath";
        public PhotosService(IConfiguration config, IHostingEnvironment env)
        {
            _env = env;
            _config = config;
        }

        public List<PhotoRequest> ReadPhotoRequestsfromFile()
        {
            int counter = 0;
            string line;
            var requestList = new List<PhotoRequest>();

            var filePath = $"{Directory.GetCurrentDirectory()}\\{_config[RequestPath].ToString()}";

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);

            while ((line = file.ReadLine()) != null)
            {
                requestList.Add(new PhotoRequest() { Date = line });
                counter++;
            }

            file.Close();
            return requestList;
        }
        public List<PhotoResult> DownLoadPhotos(List<PhotoRequest> dates)
        {
            var responseList = new List<PhotoResult>();

            var api = new NASAMarsRoverApi(_config);

            Parallel.ForEach(dates, date =>
             {
                 var photoResult = new PhotoResult() { RequestDate = date.Date };
                 var photos = api.InvokePhotoApi(photoResult);
                
                 if (photos != null)
                 {
                     var imgsrcs = photos.Select(x => x.Img_src).ToList();
                     var dirPath = $"{_config[DirectoryPath].ToString()}_RequestDate{photoResult.ValidatedDate.ToString("yyyyMMdd")}";
                     DirectoryInfo di = Directory.CreateDirectory(dirPath);

                     Parallel.ForEach(imgsrcs, imgsrc =>
                     {
                         using (WebClient client = new WebClient())
                         {
                             var index = imgsrc.LastIndexOf("/") + 1;
                             var length = imgsrc.Length - index;
                             var fileName = imgsrc.Substring(index, length);

                             client.DownloadFile(new Uri(imgsrc), Path.Combine(dirPath, Path.GetFileName(fileName)));
                         }
                     });

                     photoResult.PhotoName = imgsrcs;
                     photoResult.Message = $"SuccessFully Downloaded {imgsrcs.Count} photos.";


                 }
                 else
                 {                  
                     photoResult.PhotoName = null;
                     photoResult.Message += $" : Failed to Download photos";
                 }

                 responseList.Add(photoResult);
             });

            return responseList;

        }

    }
}
