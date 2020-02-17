# MarsRoverPhotosAPI
 
 Phase 1: 
 1) Build a project in C#/.netCore
 2) Available Enpoint (to meet AC) /api/Photos/DownloadPhotosByFile
 3) run project on MarsRoverPhotosAPI mode. you can test Functionality using swagger
 4) run project on IIS Express use "/api/Photos/DownloadPhotosByFile" in postman
 5) dates.txt is added as part of the Project.
 6) the photos will be downloded in the C:\\ locally in a folder of the format MarsRoverPhotos_RequestDate20160713
 7) Result will show in the format 
  {
        "message": "SuccessFully Downloaded 36 photos.",
        "requestDate": "02/27/17",
        "validatedDate": "2017-02-27T00:00:00",
        "photoName": [
            http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01622/opgs/edr/fcam/FLB_541484941EDR_F0611140FHAZ00341M_.JPG",
            ....
            ...
            ...
        ]
    }
 
