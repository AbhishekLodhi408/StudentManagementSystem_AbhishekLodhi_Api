using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Domain.Interface;
using StudentManagementSystem.Infrastructure.AppDBContextName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Infrastructure.Repository
{
    public  class SMSystemRepository :ISMSystemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;

        public SMSystemRepository(IConfiguration configuration, AppDBContext appDBContext)
        {
            _configuration = configuration;
            _dbContext = appDBContext;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }
    }
}
