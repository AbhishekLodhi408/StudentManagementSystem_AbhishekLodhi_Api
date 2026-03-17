using StudentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Domain.Interface
{
    public interface ISMSystemRepository
    {
        Task<List<Student>> GetAllStudents();
    }
}
