using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Domain.Domain
{
    public class SMSystemDomain
    {
        private readonly ISMSystemRepository _sMSystemRepository;
        public SMSystemDomain(ISMSystemRepository sMSystemRepository)
        {
            _sMSystemRepository = sMSystemRepository;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _sMSystemRepository.GetAllStudents();
        }
    }
}
