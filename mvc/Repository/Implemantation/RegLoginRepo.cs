using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using mvc.Repository.Interface;
using Npgsql;

namespace mvc.Repository.Implemantation
{
    public class RegLoginRepo : CommonRepo, IRegLoginRepo
    {
        protected IHttpContextAccessor _httpContextAccessor;
        public RegLoginRepo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Register(RegLoginModel reg)
        {
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand("INSERT INTO t_reglogin(c_username, c_email, c_password, c_role) VALUES (@c_username, @c_email, @c_password, 'user')", conn);
                cmd.Parameters.AddWithValue("@c_username", reg.c_username);
                cmd.Parameters.AddWithValue("@c_email", reg.c_email);
                cmd.Parameters.AddWithValue("@c_password", reg.c_password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Login(RegLoginModel login)
        {
            bool isUserValid = false;
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand("SELECT c_id, c_username, c_email, c_password, c_role FROM t_reglogin WHERE c_email=@c_email AND c_password=@c_password", conn);
                cmd.Parameters.AddWithValue("@c_email", login.c_email);
                cmd.Parameters.AddWithValue("@c_password", login.c_password);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _httpContextAccessor.HttpContext.Session.SetString("email", reader["c_email"].ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("role", reader["c_role"].ToString());
                    isUserValid = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return isUserValid;
        }
    }
}