using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskList_Final_.Models;

namespace TaskList_Final_.Data
{
    public class LoginContex : DbContext
    {
        public LoginContex(DbContextOptions<LoginContex> options) : base(options)
        {

        }
        public DbSet<AuthenticateModel> AuthenticateModel { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        

    }
}
