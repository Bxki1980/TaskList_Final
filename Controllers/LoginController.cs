using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList_Final_.Data;
using TaskList_Final_.Models;
using TaskList_Final_.Repositories;

namespace TaskList_Final_.Controllers
{
    [Route("[controller]")]
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


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!_LoginRepository.LoginValidation(model.UserName, model.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            // Add your logic for successful login here

            return Ok("Login successful!");
        }



    }
}
