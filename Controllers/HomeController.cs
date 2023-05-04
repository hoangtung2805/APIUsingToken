using APIUsingToken;
using APIUsingToken.Models;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace APIUsingToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController
    {
        [Authorize]
        [HttpGet("GetAllStudent")]
        public async  Task<List<Student>> GetAllStudent()
        {
            var _context = new StudentContext();
            return _context.Students.ToList();
        }
        [HttpGet("GetStudentByID"),Authorize]
        public async Task<Student> GetStudentByID(int id)
        {
            var _context = new StudentContext();
            var _student = _context.Students.Where(x => x.StudentId == id).FirstOrDefault();
            return _student;
        }
        [HttpPost("CreateStudent"),Authorize]
        public async Task<Student> CreateStudent(Student student)
        {
            var _context = new StudentContext();
            var existingStudent =  _context.Students.Where(x => x.StudentId == student.StudentId);
            if(existingStudent == null)
            {
                throw new Exception("ko the trung ID");
            }
            var _student = _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return _student.Entity;

        
        }
        [HttpPost("UpdateStudentById"),Authorize]

        public async Task<Student> UpdateStudent(Student student ,int id)
        {
            var _context = new StudentContext();
            var existingStudent = _context.Students.Where(x=>x.StudentId == id).FirstOrDefault();
            if(existingStudent != null)
            {
                throw new Exception("Student not fount");

            }
            existingStudent.Name = student.Name;
            existingStudent.Gender = student.Gender;
            existingStudent.Dob = student.Dob;
            existingStudent.Username    = student.Username;
            existingStudent.Password = student.Password;    
            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
            return existingStudent;

        }
        [HttpDelete("DeleteStudent"),Authorize]
        public async Task  DeleteStudent(int id , Student student)
        {
            var _context = new StudentContext();
            var existingStudent = _context.Students.Where(x => x.StudentId == id).FirstOrDefault();
            if(existingStudent == null) {
                throw new Exception("Student not found");
            }
            _context.Students.Remove(existingStudent);
            await _context.SaveChangesAsync();

        }
    }
}
