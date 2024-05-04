using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using mvc.Repository.Interface;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class RegLoginController : Controller
    {
        private readonly IRegLoginRepo _regLoginRepo;
        protected IHttpContextAccessor _httpContextAccessor;

        public RegLoginController(IRegLoginRepo regLoginRepo, IHttpContextAccessor httpContextAccessor)
        {
            _regLoginRepo = regLoginRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegLoginModel register)
        {
            _regLoginRepo.Register(register);
            return RedirectToAction("Login", "RegLogin");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string c_email , string c_password)
        {
            if (_regLoginRepo.Login(c_email , c_password))
            {
                string role = _httpContextAccessor.HttpContext.Session.GetString("role");
                if (role.Equals("admin"))
                {
                    return Ok(new { Response = "admin" });
                }
                else
                {
                    return Ok(new { Response = "user" });
                }
            }
            else
            {
                TempData["loginfail"] = "Invalid credential";
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}