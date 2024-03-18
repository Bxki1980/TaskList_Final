using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskList_Final_.Data;
using TaskList_Final_.Models;
using TaskList_Final_.Repositories;

namespace TaskList_Final_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class LoginController : ControllerBase
    {
        private readonly LoginContex _LoginContext;
        private readonly ILoginRepository _LoginRepository;

        public LoginController(LoginContex context, ILoginRepository loginRepository)
        {
            _LoginContext = context;
            _LoginRepository = loginRepository;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Login(AuthenticateModel model)
        {
            if (_LoginRepository.Authenticate(model.UserName, model.Password) == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Add your logic for successful login here

            return Ok("Login successful!");
        }

        [HttpPost("CreatNewAccount")]
        public IActionResult CreateAcc(UserModel User)
        {
            if (_LoginRepository.CreateAcc == null)
            {
                return BadRequest("Error !!!");
            }

            // Add your logic for successful login here

            return Ok(User);
        }

    }
}
