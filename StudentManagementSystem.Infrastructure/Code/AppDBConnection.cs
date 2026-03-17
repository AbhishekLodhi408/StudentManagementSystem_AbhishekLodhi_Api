using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace StudentManagementSystem.Infrastructure.Code
{
    public class AppDBConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;
        public AppDBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connString = configuration.GetConnectionString("DefaultConnString");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
