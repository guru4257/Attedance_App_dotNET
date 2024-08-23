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
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id {get; set;}

        [Required(ErrorMessage ="Name is Required")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Password is Required")]
        public string Password {get; set;}
        
    }
}