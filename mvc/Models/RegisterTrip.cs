using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class RegisterTrip
    {
        public int userid { get; set; }

        public string c_fname { get; set; }

        public string c_lname { get; set; }

        public string c_gender { get; set; }

        public string c_address { get; set; }

        public string c_triptype { get; set; }

        public int c_tripid { get; set; }

        public string c_tripdate { get; set; }

        public int c_totalpeople    { get; set; }

        public float c_tripPrice { get; set; }

        public string c_cardnumber      { get; set; }

        public string c_csvnumber { get; set; } 

        public string c_holdername { get; set; }
    }
}