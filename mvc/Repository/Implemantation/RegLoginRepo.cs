using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using mvc.Repository.Interface;

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

        }
    }
}