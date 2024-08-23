using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DemoWebAPI.Models;
using DemoWebAPI.Context;
using DemoWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoWebAPI.Controller
{   
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    { 


        private readonly StudentServices _studentService;

        public StudentController(StudentServices studentServices){

             _studentService = studentServices;
        } 


        
    }
}