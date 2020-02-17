using MarsRoverPhotosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverPhotosAPI.DomainServices
{
    public class NASAMarsRoverApi
    {
        private readonly IConfiguration _config;
        private const string BASEURL = "MarsRoverPhotosAPIBaseUrl";
        private const string APIKEY = "MarsRoverPhotosAPIKey";

        public NASAMarsRoverApi(IConfiguration config)
        {
            _config = config;
        }
        public List<Photo> InvokePhotoApi(PhotoResult request)
        {
            var formats = CultureInfo.CurrentCulture.DateTimeFormat.GetAllDateTimePatterns().ToList();
            formats.Add("MMMM dd, yyyy");          
            formats.Add("MMM-dd-yyyy");          
   
            if (DateTime.TryParseExact(request.RequestDate, formats.ToArray(), CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out DateTime dateVal))
            {
                request.ValidatedDate = dateVal;
                // Successfully parse
                var apiUrl = $"{_config[BASEURL].ToString()}?api_key={_config[APIKEY].ToString()}&earth_date={dateVal.Year}-{dateVal.Month}-{dateVal.Day}";

                using (var response = new HttpClient { }.GetAsync(apiUrl).Result)
                {

                        response.EnsureSuccessStatusCode();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var rootPhotoObj = JsonConvert.DeserializeObject<RootObject>(content);

                        return rootPhotoObj.Photos;
                    }
                    else
                    {
                        request.Message = $"Mars Rover Photos Api failed to retrieve data for requested Date {request.RequestDate}, Status message: {response.StatusCode}";
                        return null;
                       
                    }
                    
                }
            }
            else
            {
                request.Message = $"Invalid Date";
                return null;
            }
         
        }
        private static HttpClient GetApi(string downloadBaseAddress)
        {
            var baseAddress = new Uri(downloadBaseAddress);
            return new HttpClient { BaseAddress = baseAddress };
        }
    }
}
