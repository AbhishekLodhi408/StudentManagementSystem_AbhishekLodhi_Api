using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Domain.Entities
{
    public class Class
    {
        public int ClassId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
