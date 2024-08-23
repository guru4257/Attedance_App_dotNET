using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DemoWebAPI.Models
{
    public class Faculty
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id {get; set;}

        [Required(ErrorMessage = "Faculty name is required")]
        public string FacultyName {get; set;}

        [Required(ErrorMessage = "Faculty Id is required")]
        public string FacultyId {get; set;}

        [Required(ErrorMessage = "Department is required")]
        public string Department {get; set;}

        [JsonIgnore]
        public string? Password {get; set;}
       
        [JsonIgnore]
        public List<string> Students {get; set;} = new List<string>();


    }
}