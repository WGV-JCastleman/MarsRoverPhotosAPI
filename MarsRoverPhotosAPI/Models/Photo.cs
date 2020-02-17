using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverPhotosAPI.Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rover_id { get; set; }
        public string Full_name { get; set; }
    }

    public class Camera2
    {
        public string Name { get; set; }
        public string Full_name { get; set; }
    }

    public class Rover
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Landing_date { get; set; }
        public string launch_date { get; set; }
        public string Status { get; set; }
        public int Max_sol { get; set; }
        public string Max_date { get; set; }
        public int Total_photos { get; set; }
        public List<Camera2> Cameras { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public int Sol { get; set; }
        public Camera Camera { get; set; }
        public string Img_src { get; set; }
        public string Earth_date { get; set; }
        public Rover Rover { get; set; }
    }

    public class RootObject
    {
        public List<Photo> Photos { get; set; }
    }
}
