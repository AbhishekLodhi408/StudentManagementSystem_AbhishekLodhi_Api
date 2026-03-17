using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }

        // Navigation Property (Many-to-Many)
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
