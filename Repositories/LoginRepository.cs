﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskList_Final_.Data;
using TaskList_Final_.Models;

namespace TaskList_Final_.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly LoginContext _loginContext;
        private readonly string _jwtSecret;

        public LoginRepository(LoginContext context, IConfiguration configuration)
        {
            _loginContext = context;
            _jwtSecret = configuration["Jwt:Key"]; // Retrieve the secret key from appsettings.json
        }

        // Authenticate
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // Retrieve user
            var user = await _loginContext.Users.SingleOrDefaultAsync(u => u.UserName == username && u.Password == password);

            // If user doesn't exist, return null
            if (user == null)
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Create a new account
        public async Task CreateAccountAsync(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Password is required");

            if (await _loginContext.Users.AnyAsync(x => x.UserName == user.UserName))
                throw new ArgumentException($"Username '{user.UserName}' is already taken");

            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new ArgumentException("First name is mandatory");

            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new ArgumentException("Last name is mandatory");

            _loginContext.Users.Add(user);
            await _loginContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var user = await _loginContext.Users.FindAsync(id);
            if (user != null)
            {
                _loginContext.Users.Remove(user);
                await _loginContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _loginContext.Users.OrderBy(u => u.ID).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(int id, UserModel updatedUser)
        {
            var user = await _loginContext.Users.FindAsync(id);
            if (user != null)
            {
                // Update fields
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.UserName = updatedUser.UserName;

                // If password is provided, update it
                if (!string.IsNullOrWhiteSpace(updatedUser.Password))
                {
                    user.Password = updatedUser.Password;
                }

                _loginContext.Users.Update(user);
                await _loginContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
