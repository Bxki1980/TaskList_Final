// Data/LoginContext.cs

using Microsoft.EntityFrameworkCore;
using TaskList_Final_.Models;

namespace TaskList_Final_.Data
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
