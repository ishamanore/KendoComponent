using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class RegLoginModel
    {
        public int? c_id { get; set; }
        public string? c_username { get; set; }
        public string c_email { get; set; }
        public string c_password { get; set; }

    }
}