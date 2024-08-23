using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System;
using System.Text.Json.Serialization;

namespace DemoWebAPI.Models;

public class Student{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public string? Id {get; set;}

    [Required(ErrorMessage ="Roll Number is required")]
    public string RollNumber {get; set;}

    [Required(ErrorMessage ="Register Number is required")]
    public Int64 RegisterNumber {get; set;}

    [JsonIgnore]
    public string? Password {get; set;}

    [Required(ErrorMessage ="Batch is requires")]
    public string Batch {get; set;}
    
    [Required(ErrorMessage ="Department is requires")]
    public string Department {get; set;}

    [Required(ErrorMessage ="Student name is requires")]
    public string StudentName {get; set;}

    [JsonIgnore]
    public List<Attendance> Attendence {get; set;} = new List<Attendance>();

                                        
}