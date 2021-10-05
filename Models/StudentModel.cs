using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nome.Models
{
    public class StudentModel
    {

        public List<Student> StudentsList { get; set; }
        public StudentToInsert StudentToInsert { get; set; }

        public StudentModel()
        {
            StudentsList = new List<Student>();
        }             
    }

    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
    public class StudentToInsert
    {
        [MinLength(1, ErrorMessage = "Name field can't be empty!")]
        public string FirstName { get; set; }
        [MinLength(1, ErrorMessage = "Surname field can't be empty!")]
        public string LastName { get; set; }
    }
}