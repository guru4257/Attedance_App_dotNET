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
    public class FacultyController : ControllerBase
    { 


        private readonly FacultyServices _facultyService;

        public FacultyController(FacultyServices facultyservices){

             _facultyService = facultyservices;
        } 


        [HttpGet("getAllStudents")]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> GetAllStudent(){

             var response = await _facultyService.GetAllStudent();

             if(!response.Success){

                return NotFound(response);
             }

             return Ok(response);
        } 


        [HttpPost("addStudent")]

        public async Task<ActionResult<ServiceResponse<Student>>> AddStudent(Student student){

            var response = await _facultyService.CreateStudent(student);

            return Ok(response);

        }

        [HttpGet("getStudent/{rollNumber:length(7)}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudentById(string rollNumber){

            var response = await _facultyService.GetStudentByID(rollNumber);

            if(!response.Success){

                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("removeStudent/{rollNumber:length(7)}")]

        public async Task<ActionResult<ServiceResponse<string>>> removeStudent(string rollNumber){

            var response = await _facultyService.DeleteStudent(rollNumber);
            
            if(!response.Success){

                return NotFound(response);
            }

            return Ok(response);
        }
    }
}