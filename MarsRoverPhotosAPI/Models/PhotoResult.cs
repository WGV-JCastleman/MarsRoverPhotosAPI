using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverPhotosAPI.Models
{
    public class PhotoResult
    {
        public string RequestDate { get; set; }
        public DateTime ValidatedDate { get; set; }
        public List<string> PhotoName{ get; set; }
        public string Message;
    }
}
