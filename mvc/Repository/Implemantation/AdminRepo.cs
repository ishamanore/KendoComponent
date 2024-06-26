using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using mvc.Repository.Interface;
using Npgsql;

namespace mvc.Repository.Implemantation
{
    public class AdminRepo : CommonRepo, IAdminRepo
    {

        public List<Trip> FetchAllTrip(int pagenumber, int pageSize)
        {
            var triplist = new List<Trip>();
            try
            {
                conn2.Open();
                int offset = (pagenumber - 1) * pageSize;
                using var cmd = new NpgsqlCommand("SELECT t.c_id, t.c_triptype, t.c_tripid, t.c_date, t.c_time, t.c_days, t.c_image, t.c_price, t.c_availableseat, t.c_initialseat, t.c_description , n.c_tripname FROM public.t_trip t inner join t_tripnames n on t.c_tripid = n.c_tripid ORDER BY t.c_id LIMIT @PageSize OFFSET @Offset", conn2);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@Offset", offset);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trip trip = new Trip()
                    {
                        c_id = Convert.ToInt32(reader["c_id"]),
                        c_triptype = reader["c_triptype"].ToString(),
                        c_tripid = Convert.ToInt32(reader["c_tripid"]),
                        c_date = reader["c_date"].ToString(),
                        c_time = reader["c_time"].ToString(),
                        c_days = Convert.ToInt32(reader["c_days"]),
                        c_image = reader["c_image"].ToString(),
                        c_price = reader["c_price"].ToString(),
                        c_availableseat = Convert.ToInt32(reader["c_availableseat"]),
                        c_initialseat = Convert.ToInt32(reader["c_initialseat"]),
                        c_description = reader["c_description"].ToString(),
                        c_tripname = reader["c_tripname"].ToString(),
                    };
                    triplist.Add(trip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn2.Close();
            }
            return triplist;
        }

        List<Trip> IAdminRepo.searchTrip(string search)
        {
            var triplist = new List<Trip>();
            try
            {
                conn3.Open();
                // Define the SQL query with parameters for dynamic searching
                string queryString = @"
                    SELECT t.c_id, t.c_triptype, t.c_tripid, t.c_date, t.c_time, t.c_days, t.c_image, t.c_price, t.c_availableseat, t.c_initialseat, t.c_description, n.c_tripname 
                    FROM public.t_trip t 
                    INNER JOIN t_tripnames n ON t.c_tripid = n.c_tripid 
                    WHERE t.c_triptype LIKE @Search 
                        OR t.c_date LIKE @Search 
                        OR t.c_time LIKE @Search 
                        OR CAST(t.c_days AS TEXT) LIKE @Search 
                        OR t.c_price LIKE @Search 
                        OR CAST(t.c_availableseat AS TEXT) LIKE @Search 
                        OR CAST(t.c_initialseat AS TEXT) LIKE @Search 
                        OR t.c_description LIKE @Search 
                        OR n.c_tripname LIKE @Search
                    ORDER BY t.c_id;";
                using var cmd = new NpgsqlCommand(queryString, conn3);
                // Set the parameter value
                cmd.Parameters.AddWithValue("@Search", $"%{search}%");
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trip trip = new Trip()
                    {
                        c_id = Convert.ToInt32(reader["c_id"]),
                        c_triptype = reader["c_triptype"].ToString(),
                        c_tripid = Convert.ToInt32(reader["c_tripid"]),
                        c_date = reader["c_date"].ToString(),
                        c_time = reader["c_time"].ToString(),
                        c_days = Convert.ToInt32(reader["c_days"]),
                        c_image = reader["c_image"].ToString(),
                        c_price = reader["c_price"].ToString(),
                        c_availableseat = Convert.ToInt32(reader["c_availableseat"]),
                        c_initialseat = Convert.ToInt32(reader["c_initialseat"]),
                        c_description = reader["c_description"].ToString(),
                        c_tripname = reader["c_tripname"].ToString(),
                    };
                    triplist.Add(trip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn3.Close();
            }
            return triplist;
        }


        public Trip GetOneTrip(int id)
        {
            Trip trip = new Trip();
            try
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT t.c_id, t.c_triptype, t.c_tripid, t.c_date, t.c_time, t.c_days, t.c_image, t.c_price, t.c_availableseat, t.c_initialseat, t.c_description , n.c_tripname FROM public.t_trip t inner join t_tripnames n on t.c_tripid = n.c_tripid where t.c_id = @c_id", conn);
                cmd.Parameters.AddWithValue("@c_id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    trip.c_id = Convert.ToInt32(reader["c_id"]);
                    trip.c_triptype = reader["c_triptype"].ToString();
                    trip.c_tripid = Convert.ToInt32(reader["c_tripid"]);
                    trip.c_date = reader["c_date"].ToString();
                    trip.c_time = reader["c_time"].ToString();
                    trip.c_days = Convert.ToInt32(reader["c_days"]);
                    trip.c_image = reader["c_image"].ToString();
                    trip.c_price = reader["c_price"].ToString();
                    trip.c_availableseat = Convert.ToInt32(reader["c_availableseat"]);
                    trip.c_initialseat = Convert.ToInt32(reader["c_initialseat"]);
                    trip.c_description = reader["c_description"].ToString();
                    trip.c_tripname = reader["c_tripname"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return trip;
        }

        public void AddTrip(Trip trip)
        {
            try
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("INSERT INTO public.t_trip( c_triptype, c_tripid, c_date, c_time, c_days, c_image, c_price, c_availableseat, c_initialseat, c_description) VALUES (@c_triptype, @c_tripid, @c_date, @c_time, @c_days, @c_image, @c_price, @c_availableseat, @c_initialseat, @c_description)", conn);
                cmd.Parameters.AddWithValue("@c_triptype", trip.c_triptype);
                cmd.Parameters.AddWithValue("@c_tripid", trip.c_tripid);
                cmd.Parameters.AddWithValue("@c_date", trip.c_date);
                cmd.Parameters.AddWithValue("@c_time", trip.c_time);
                cmd.Parameters.AddWithValue("@c_days", trip.c_days);
                cmd.Parameters.AddWithValue("@c_image", trip.c_image);
                cmd.Parameters.AddWithValue("@c_price", trip.c_price);
                cmd.Parameters.AddWithValue("@c_availableseat", trip.c_availableseat);
                cmd.Parameters.AddWithValue("@c_initialseat", trip.c_initialseat);
                cmd.Parameters.AddWithValue("@c_description", trip.c_description);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateTrip(Trip trip)
        {
            try
            {
                conn2.Open();
                using var cmd = new NpgsqlCommand("UPDATE public.t_trip SET c_triptype=@c_triptype, c_tripid=@c_tripid, c_date=@c_date, c_time=@c_time, c_days=@c_days, c_image=@c_image, c_price=@c_price, c_availableseat=@c_availableseat, c_initialseat=@c_initialseat, c_description=@c_description WHERE c_id = @c_id", conn2);
                cmd.Parameters.AddWithValue("@c_id", trip.c_id);
                cmd.Parameters.AddWithValue("@c_triptype", trip.c_triptype);
                cmd.Parameters.AddWithValue("@c_tripid", trip.c_tripid);
                cmd.Parameters.AddWithValue("@c_date", trip.c_date);
                cmd.Parameters.AddWithValue("@c_time", trip.c_time);
                cmd.Parameters.AddWithValue("@c_days", trip.c_days);
                cmd.Parameters.AddWithValue("@c_image", trip.c_image);
                cmd.Parameters.AddWithValue("@c_price", trip.c_price);
                cmd.Parameters.AddWithValue("@c_availableseat", trip.c_availableseat);
                cmd.Parameters.AddWithValue("@c_initialseat", trip.c_initialseat);
                cmd.Parameters.AddWithValue("@c_description", trip.c_description);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn2.Close();
            }
        }

        public void DeleteTrip(int id)
        {
            try
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("DELETE FROM public.t_trip WHERE c_id = @c_id", conn);
                cmd.Parameters.AddWithValue("@c_id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<TripNames> FetchAllTripNames()
        {
            var triplist = new List<TripNames>();
            try
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT c_tripid, c_tripname FROM t_tripnames", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TripNames trip = new TripNames()
                    {
                        c_tripid = Convert.ToInt32(reader["c_tripid"]),
                        c_tripname = reader["c_tripname"].ToString(),
                    };
                    triplist.Add(trip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return triplist;
        }



        public List<ShowAllBookTcket> GetAllBookTicket()
        {
            var tickets = new List<ShowAllBookTcket>();
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand("select u.c_username , t.c_userid, t.c_tripid , t.c_registerid, t.c_tridate , t.c_totalpeople , t.c_totalprice , n.c_tripname from t_reglogin u inner join t_register_trip t on u.c_id = t.c_userid  inner join t_tripnames n on t.c_tripid = n.c_tripid", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ticket = new ShowAllBookTcket
                    {
                        c_username = reader.GetString(0),
                        c_userid = reader.GetInt32(1),
                        c_tripid = reader.GetInt32(2),
                        c_registerid = reader.GetInt32(3),
                        c_tripdate = reader.GetString(4),
                        c_totalpeople = reader.GetInt32(5),
                        c_totalprice = reader.GetDouble(6),
                        c_tripname = reader.GetString(7),
                    };
                    tickets.Add(ticket);
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
            return tickets;
        }

        int IAdminRepo.TotalInternational()
        {
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select count(c_triptype) from t_trip where c_triptype='international';", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int international = Convert.ToInt32(dr["count"]);
                return international;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error while getting international : " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        int IAdminRepo.TotalDomestic()
        {
            try
            {
                conn2.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select count(c_triptype) from t_trip where c_triptype='domestic';", conn2);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int domestic = Convert.ToInt32(dr["count"]);
                return domestic;
            }

            catch (Exception ex)
            {
                Console.WriteLine("error while getting domestic : " + ex.Message);
                return 0;
            }
            finally
            {
                conn2.Close();
            }
        }
    }



}