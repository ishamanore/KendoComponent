using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class ShowAllBookTcket
    {
        public int c_registerid { get; set; }

        public int c_userid { get; set; }
        public int c_id { get; set; }
        public string c_username { get; set; }
        public string c_tripname { get; set; }
        public int c_tripid { get; set; }

        public string c_tripdate { get; set; }

        public int c_totalpeople { get; set; }

        public double c_totalprice { get; set; }
        public string c_cardnumber { get; set; }

        public string c_csvnumber { get; set; }

        public string c_holdername { get; set; }

    }
}