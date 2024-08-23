using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Auth.AccessControlPolicy;
using DemoWebAPI.Context;
using DemoWebAPI.Models;
using MongoDB.Driver;

namespace DemoWebAPI.Services
{
    public class FacultyServices
    { 

        private  IMongoCollection<Faculty> _faculty;

        private  IMongoCollection<Student> _student;
        
        // enabling the faculty service by getting collection from the context
        public FacultyServices(MongoDbContext context){

            _faculty = context.FacultyCollection;
            _student = context.StudentCollection;
        }

         // function for getting all students who resposible for the teacher basis from the db

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

       public async Task<ServiceResponse<Student>> GetStudentByID(string RollNumber){
           
           var student = await _student.Find<Student>(student => student.RollNumber == RollNumber).FirstOrDefaultAsync();

           if(student == null){

             return new ServiceResponse<Student>{

                 Success = false,
                 Message = " Student not found..!"
             };
           }

           return new ServiceResponse<Student>{
                
                 Success = true,
                 Message = "Student Details Fetched..!",
                 Data = student
           };
       }

       
    //    funciton for creating a student or add new student
       public async Task<ServiceResponse<Student>> CreateStudent(Student student_data){


            try{

                var studentExist = await _student.Find<Student>(student => student.RollNumber == student_data.RollNumber).FirstOrDefaultAsync();

                if(studentExist != null){

                    return new ServiceResponse<Student>{
                        Success = true,
                        Message = "You already added this Student "+ student_data.StudentName
                    };
                }

            }catch(Exception error){

                Console.WriteLine("Exception Ocuured at chehcking the Existing student logic",error);
                return new ServiceResponse<Student>{
                    Success = false,
                    Message = "There is an Error in the Searching the Student in the DB. "
                };
            }

            try{
                   
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(student_data.RollNumber);
                student_data.Password = hashPassword;

                await _student.InsertOneAsync(student_data);

            }catch(Exception error){
                
                Console.WriteLine("Exception Ocuured at Inserting the Student Logic",error);
                return new ServiceResponse<Student>{
                    Success = false,
                    Message = "There is an Error in the Adding the Studuent in the DB. "
                };
            }
          


            // To update the Students id in faculty collection 

            try{

                var filter = Builders<Faculty>.Filter.Eq(faculty => faculty.Department,student_data.Department);
                var update = Builders<Faculty>.Update.Push(faculty => faculty.Students,student_data.Id.ToString());

                var faculty_updation = await _faculty.UpdateOneAsync(filter,update);

                Console.WriteLine(faculty_updation);

                if(faculty_updation.ModifiedCount == 0){

                    return new ServiceResponse<Student>{
                        Success = false,
                        Message = $"The Faculty is Not Assigned for {student_data.Department} Department"

                    };
                }

            }catch(Exception error){

                Console.WriteLine("Exception Ocuured at Inserting the Student to faculty Logic",error);
                return new ServiceResponse<Student>{
                    Success = false,
                    Message = "There is an Error in the Mapping the student to the faculty"
                };
            }

            return new ServiceResponse<Student>{
                Success = true,
                Message = "Student Added Successfully",
                Data = student_data
            };
       }

       public async Task<ServiceResponse<string>> DeleteStudent(string RollNumber){
          
            
            try{
              
               var result = await _student.DeleteOneAsync(student => student.RollNumber == RollNumber);

               return new ServiceResponse<string>{
                Success = result.DeletedCount > 0,
                Message = result.DeletedCount > 0 ? "Student removed Successfully" : "The Student is Already not in the DB",
                Data = RollNumber
              };

            }catch(Exception error){

                Console.WriteLine($"Exception Ocuured at Removing the Student Logic{error}");
                return new ServiceResponse<string>{
                    Success = false,
                    Message = "There is an Error in the Removing the Student from the DB"
                };
            }

            
       }

        
    }
}