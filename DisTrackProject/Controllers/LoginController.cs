using DisTrack.Commons.Models;
using DisTrack.Constants;
using DisTrack.Domain.Interfaces;
using DisTrack.Helper;
using DisTrack.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DisTrack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }

        /// <summary>
        /// Allow the user to sign in with email and password
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns>Returns a confirmation</returns>
        /// <response code="200">Client signed in</response>
        /// <response code="400">Username not found</response>
        [EnableCors(SystemConstants.Cors.AllowAllPolicy)]
        [HttpPost]
        public IActionResult SignInUser([FromBody]LoginViewModel loginDetails)
        {          
            try
            {
                if (!ModelState.IsValid || loginDetails == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(loginDetails.Email) && !string.IsNullOrEmpty(loginDetails.Password))
                {
                    var hashedPassword = PasswordEncryption.SHA512ComputeHash(loginDetails.Password);

                    var userInDb = _userService.GetUserByEmail(loginDetails.Email);

                    if (userInDb.Password.Equals(hashedPassword))
                    {
                        return Ok("Login successful");
                    }

                }
            }
            catch (WebException ex)
            {
                Trace.TraceError(ex.Message);
                throw new WebException();
            }

            return BadRequest("Incorrect login details");
        }

        /// <summary>
        /// Allow the user to change password
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns>Returns a confirmation</returns>
        /// <response code="200">Password modified</response>
        /// <response code="400">Update failed</response>
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult UpdatePassword([FromBody]LoginViewModel loginDetails)
        {
            var hashedPassword = PasswordEncryption.SHA512ComputeHash(loginDetails.Password);

            try
            {
                if (!ModelState.IsValid || loginDetails == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(loginDetails.Email) && !string.IsNullOrEmpty(loginDetails.Password))
                {
                    var userInDb = _userService.GetUserByEmail(loginDetails.Email);

                    if (userInDb.Password.Equals(hashedPassword))
                    {
                        var userModel = new UserModel
                        {
                            Id = userInDb.Id,
                            UserName = userInDb.UserName,
                            Email = loginDetails.Email,
                            Password = loginDetails.Password
                        };

                        _userService.UpdateUserDetails(userModel);

                        return Ok("Password changed");
                    }

                }
                    
            }
            catch (WebException ex)
            {
                Trace.TraceError(ex.Message);
                throw new WebException();
            }

            return BadRequest("Unable to sign in");

        }

    }
}
