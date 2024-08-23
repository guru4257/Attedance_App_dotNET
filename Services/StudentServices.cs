using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Context;


using DemoWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DemoWebAPI.Services
{
    
    public class StudentServices
    {
       private readonly IMongoCollection<Student> _student;

       public StudentServices(MongoDbContext context){
            
             _student = context.StudentCollection;
       }


       
        
    }


}