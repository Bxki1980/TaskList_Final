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


        // Create new account for the user
        [HttpPost("CreatNewAccount")]
        public IActionResult CreateAcc(UserModel userModel)
        {
            try
            {
                
                _LoginRepository.CreateAcc(userModel.FirstName, userModel.LastName, userModel.UserName, userModel.Password);
                 _LoginContext.Add(userModel);

                return Ok(userModel);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception
                return BadRequest("Failed to create account. Please try again later.");
            }
        }

        //Read
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allUserModels = await _LoginRepository.getAll();
                return Ok(allUserModels);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

