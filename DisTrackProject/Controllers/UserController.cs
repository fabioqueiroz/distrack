using DisTrack.Data;
using DisTrack.Domain.Interfaces;
using DisTrack.Helper;
using DisTrackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisTrackProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var checkEmailInDb = await _userService.GetUserByEmail(userViewModel.Email);

                if (checkEmailInDb == null)
                {
                    var newUser = new User
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        Password = PasswordEncryption.SHA512ComputeHash(userViewModel.Password)
                    };

                    await _userService.AddUser(newUser);

                    return Ok();  
                }
                else
                {
                    return BadRequest("This email is already in use");
                }
            }

            return BadRequest();

        }
    }
}
