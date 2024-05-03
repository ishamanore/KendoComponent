using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using mvc.Repository.Interface;
using Npgsql;

namespace mvc.Repository.Implemantation
{
    public class TripNamesRepo : CommonRepo , ITripNamesRepo
    {
        public List<TripNames> FetchAllTripNames()
        {
            var tripnamelist = new List<TripNames>();
            try
            {
                conn.Open();
                using var cmd = new NpgsqlCommand("",conn);
                using var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    TripNames tripname = new TripNames()
                    {
                        c_tripid = Convert.ToInt32(reader["c_tripid"]),
                        c_tripname = reader["c_tripname"].ToString(),
                    };
                    tripnamelist.Add(tripname);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return tripnamelist;

        }
        
    }
}