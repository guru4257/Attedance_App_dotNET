using System;

namespace DemoWebAPI.Models
{
    public class DatabaseSettings
    {
        public string connectionURI {get; set;}
        public string Databasename {get; set;}
        public string StudentCollection {get; set;}

        public string FacultyCollection {get; set;}
        public string AdminCollection {get; set;} 

    }
}