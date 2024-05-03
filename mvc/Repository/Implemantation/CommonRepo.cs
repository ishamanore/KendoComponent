using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace mvc.Repository.Implemantation
{
    public class CommonRepo
    {
        protected NpgsqlConnection conn;

        public CommonRepo()
        {
            IConfiguration configuration = new ConfigurationBuilder().
            SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            conn = new NpgsqlConnection(configuration.GetConnectionString("connection"));
        }

        
    }
}