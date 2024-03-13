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
        public DbSet<LoginModel> Login { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\Local;Initial Catalog=TaskListdb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False", Option =>
            {
                Option.EnableRetryOnFailure();
            });
        }
    }
}
