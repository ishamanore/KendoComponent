using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Trip
    {
        public int c_id{ get; set; }
        public string c_triptype{ get; set; }
        public int c_tripid{ get; set; }
        public string c_date{   get; set; }
        public string c_time{ get; set; }
        public int c_days{ get; set; }
        public string c_image{ get; set; }
        public string c_price{ get; set; }
        public int c_availableseat{ get; set; }
        public int c_initialseat{ get; set; }
        public string c_description{ get; set; }


        public string c_tripname { get; set; }
    }
}