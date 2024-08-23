using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoWebAPI.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminServices _adminServices;

        public AdminController(AdminServices adminServices){

            _adminServices = adminServices;
        }


        [HttpGet("getAllStudents")]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> GetAllStudent(){

             var response = await _adminServices.GetAllStudent();

             if(!response.Success){

                return NotFound(response);
             }

             return Ok(response);
        }

        [HttpGet("getAllFaculties")]
        public async Task<ActionResult<ServiceResponse<List<Faculty>>>> GetAllFaculties(){

             var response = await _adminServices.GetAllFaculties();

             if(!response.Success){

                return NotFound(response);
             }

             return Ok(response);
        }

        [HttpPost("addFaculty")]

        public async Task<ActionResult<ServiceResponse<Faculty>>> AddFaculty(Faculty faculty){

            var response = await _adminServices.AddFaculty(faculty);

            if(!response.Success){
                return StatusCode(500,response);
            }

            return Ok(response);
        }

        [HttpGet("/getFaculty/{Department:}")]

        public async Task<ActionResult<ServiceResponse<Faculty>>> GetFacltyByDeparment(string Department){
             
             var response = await _adminServices.GetFacultyByDepartment(Department);

             if(!response.Success){

                return NotFound(response);
             }


             return Ok(response);
        }

    }
}