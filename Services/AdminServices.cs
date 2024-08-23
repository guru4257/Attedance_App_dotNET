using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebAPI.Context;
using DemoWebAPI.Models;
using MongoDB.Driver;

namespace DemoWebAPI.Services
{
    public class AdminServices
    {
        private readonly IMongoCollection<Student> _student;
        private readonly IMongoCollection<Faculty> _faculty;

        private readonly IMongoCollection<Admin> _admin;

        public AdminServices(MongoDbContext context){

            _student = context.StudentCollection;
            _faculty = context.FacultyCollection;
            _admin = context.AdminCollection;
        }

         // function for getting all students from the DB

          public async Task<ServiceResponse<List<Student>>> GetAllStudent(){
          
           var students = await _student.Find(student => true).ToListAsync();
           
           if(students.Count == 0){

                return new ServiceResponse<List<Student>>{
                    Success = true,
                    Message = "No Data Found"
                };
           }
           return new ServiceResponse<List<Student>>{
              Success = true,
              Message = "Data Fetched Successfully",
              Data = students
           };
       }


       //funtion add the Faculty

       public async Task<ServiceResponse<Faculty>> AddFaculty(Faculty faculty){

            
             try{
                
                var filter = Builders<Faculty>.Filter.Eq(fac => fac.FacultyId,faculty.FacultyId);
                var facultyExist = await _faculty.Find(filter).FirstOrDefaultAsync();

                if(facultyExist != null){

                    return new ServiceResponse<Faculty>{
                        Success = true,
                        Message = "Hello Admin, You Already Added this Faculty..!"
                    };
                } 
             }catch(Exception error){

                Console.WriteLine("There is an Error in searching the faulty from DB",error);

                return new ServiceResponse<Faculty>{
                    Success = false,
                    Message = "There is an Error in searching the faulty from DB"
                };
             }


             try{

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(faculty.FacultyId);

                faculty.Password = hashPassword;

                await _faculty.InsertOneAsync(faculty);
             }catch(Exception error){

                Console.WriteLine("There is an error in the Adding the faculty",error);

                return new ServiceResponse<Faculty>{

                    Success = false,
                    Message = "There is an error in the Adding the faculty"
                };
             }

             return new ServiceResponse<Faculty>{
                Success = true,
                Message = "faculty Added Sucessfully",
                Data = faculty
             };
       }

       public async Task<ServiceResponse<List<Faculty>>> GetAllFaculties(){
          
          var faculties = await _faculty.Find(faculty => true).ToListAsync();
           
           if(faculties.Count == 0){

                return new ServiceResponse<List<Faculty>>{
                    Success = true,
                    Message = "No Data Found"
                };
           }
           return new ServiceResponse<List<Faculty>>{
              Success = true,
              Message = "Data Fetched Successfully",
              Data = faculties
           };
       }

       public async Task<ServiceResponse<Faculty>> GetFacultyByDepartment(string department){

            var faculty = await _faculty.Find<Faculty>(faculty => faculty.Department == department).FirstOrDefaultAsync();

            if(faculty == null){

                return new ServiceResponse<Faculty>{
                    Success = false,
                    Message = "Faculty Not Found"
                };
            }

            return new ServiceResponse<Faculty>{
                Success = true,
                Message = "Faculty detail found",
                Data = faculty
            };
       }
    }
}