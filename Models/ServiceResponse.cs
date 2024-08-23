using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace DemoWebAPI.Models
{
    public class ServiceResponse<T>
    {
        public bool Success {get; set;}
        public string Message {get; set;}
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data {get; set;}
    }
}