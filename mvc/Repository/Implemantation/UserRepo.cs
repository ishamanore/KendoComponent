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

        bool IUserRepo.registerTrip(RegisterTrip registerTrip)
        {
            try{
                conn2.Open();
                var command = new NpgsqlCommand("update t_reglogin set c_fname=@fname , c_lname=@lname , c_gender=@gender , c_address = @add where c_id = @userid;" , conn2);
                command.Parameters.AddWithValue("@fname" , registerTrip.c_fname);
                command.Parameters.AddWithValue("@lname" , registerTrip.c_lname);
                command.Parameters.AddWithValue("@gender" , registerTrip.c_gender);
                command.Parameters.AddWithValue("@add" , registerTrip.c_address);
                command.Parameters.AddWithValue("@userid" , registerTrip.userid);
                var result  = command.ExecuteNonQuery();
                if (result> 0){
                    conn.Open();
                    var comm = new NpgsqlCommand("insert into t_register_trip(c_userid , c_tripid , c_tridate , c_totalpeople , c_totalprice , c_cardnumber , c_csvnumber , c_holdername) values (@userid, @tripid , @date, @people , @price , @card  ,@csv , @holder);" , conn);
                    comm.Parameters.AddWithValue("@userid" , registerTrip.userid);
                    comm.Parameters.AddWithValue("@tripid" , registerTrip.c_tripid);
                    comm.Parameters.AddWithValue("@date" , registerTrip.c_tripdate);
                    comm.Parameters.AddWithValue("@people",registerTrip.c_totalpeople);
                    comm.Parameters.AddWithValue("@price" , registerTrip.c_tripPrice * registerTrip.c_totalpeople);
                    comm.Parameters.AddWithValue("@card" , registerTrip.c_cardnumber);
                    comm.Parameters.AddWithValue("@csv" , registerTrip.c_csvnumber);
                    comm.Parameters.AddWithValue("@holder" , registerTrip.c_holdername);
                    var result2 = comm.ExecuteNonQuery();
                    if(result2  >0){
                        conn3.Open();
                        var command3 = new NpgsqlCommand("update t_trip set c_availableseat = c_availableseat - @seat where c_tripid=@tripid and c_date= @date;" , conn3);
                        command3.Parameters.AddWithValue("@seat" , registerTrip.c_totalpeople);
                        command3.Parameters.AddWithValue("@tripid" , registerTrip.c_tripid);
                        command3.Parameters.AddWithValue("@date" , registerTrip.c_tripdate);
                        var re = command3.ExecuteNonQuery();
                        return re > 0;
                    }
                    return false;
                }
                return false;
            }
            catch(Exception ex){
                Console.WriteLine("error while regitering trip", ex.Message);
                return false;
            }
            finally{
                conn2.Close();
                conn.Close();
                conn3.Close();
            }
        }

        bool IUserRepo.seatValidation(RegisterTrip registerTrip)
        {
            try{
                conn.Open();
                var command = new NpgsqlCommand ("select c_availableseat from t_trip where c_tripid = @id and c_date = @date;" , conn);
                command.Parameters.AddWithValue("@id",registerTrip.c_tripid);
                command.Parameters.AddWithValue("@date" , registerTrip.c_tripdate);
                var reader = command.ExecuteReader();
                while(reader.Read()){
                    return Convert.ToInt32(reader["c_availableseat"]) > registerTrip.c_totalpeople;
                }
                return false;
            }
            catch(Exception ex){
                Console.WriteLine("errro while validate seat " + ex.Message);
                return false;
            }
            finally{
                conn.Close();
            }
        }
    }
}