using DisTrack.Constants;
using DisTrack.Helper;
using DisTrack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DisTrack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public LoginController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }

        [HttpPost]
        public IActionResult SignInUser([FromBody]LoginViewModel loginDetails)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginDetails.Email) && !string.IsNullOrEmpty(loginDetails.Password))
                {
                    var hashedPassword = PasswordEncryption.SHA512ComputeHash(loginDetails.Password);
                    var db = new FakeDatabase();

                    if (db.FakeUsers.ContainsKey(loginDetails.Email) && db.FakeUsers.Values.Contains(hashedPassword))
                    {
                        return Ok("Login successful");
                    }
                }
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }

            return BadRequest("Incorrect login details");
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult CreateUser([FromBody]LoginViewModel loginDetails)
        {
            var hashedPassword = PasswordEncryption.SHA512ComputeHash(loginDetails.Password);
            var db = new FakeDatabase();

            try
            {
                if (!string.IsNullOrEmpty(loginDetails.Email) && !string.IsNullOrEmpty(loginDetails.Password))
                {
                    if (!db.FakeUsers.ContainsKey(loginDetails.Email))
                    {
                        db.FakeUsers.Add(loginDetails.Email, hashedPassword);
                    }

                    return Ok("New account created");
                }
                    
            }

            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }

            return BadRequest("Unable to sign up");

        }

    }
}
