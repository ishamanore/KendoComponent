using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using mvc.Repository.Interface;
using Npgsql;
using Npgsql.Internal;

namespace mvc.Repository.Implemantation
{
    public class UserRepo : CommonRepo, IUserRepo
    {

        public List<TripNames> GetTripNames(string type)
        {
            var tripNames = new List<TripNames>();
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("SELECT c_tripid, c_tripname, c_triptype FROM public.t_tripnames WHERE c_triptype = @type;", conn);
                command.Parameters.AddWithValue("@type", type);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var name = new TripNames()
                    {
                        c_tripid = Convert.ToInt32(reader["c_tripid"]),
                        c_tripname = Convert.ToString(reader["c_tripname"])
                    };
                    tripNames.Add(name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting trip names using type: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return tripNames;

        }

        List<string> IUserRepo.GetTripDate(int  name)
        {
            var dates = new List<string>();
            try{
                conn.Open();
                var command = new NpgsqlCommand("select c_date FROM public.t_trip where c_tripid = @id;" , conn);
                command.Parameters.AddWithValue("@id" , name);
                var reader = command.ExecuteReader();
                while(reader.Read()){
                    dates.Add(Convert.ToString(reader["c_date"]));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting trip dates using name: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dates;
        }

        double IUserRepo.GetTripPrice(int name , string date)
        {
            double price = 0.0;
            try{
                conn2.Open();
                var command = new NpgsqlCommand("select c_price from t_trip where c_tripid =@id and c_date = @date" , conn2);
                command.Parameters.AddWithValue("@id" , name);
                command.Parameters.AddWithValue("@date", date);
                var reader = command.ExecuteReader();
                while(reader.Read()){
                    price = Convert.ToDouble(reader["c_price"]);
                }
            }
            catch(Exception ex){
                Console.WriteLine("Error while getting price of the trip using name :" + ex.Message);
                return 0.0;
            }
            finally{
                conn2.Close();
            }
            return price;
        }
    }
}