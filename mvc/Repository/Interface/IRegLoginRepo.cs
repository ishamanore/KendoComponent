using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Repository.Interface
{
    public interface IRegLoginRepo
    {
        void Register(RegLoginModel reg);
        bool Login(RegLoginModel login);
    }
}