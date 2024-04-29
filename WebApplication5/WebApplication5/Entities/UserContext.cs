using Microsoft.EntityFrameworkCore;
using WebApplication5.Entities;
using WebApplication5.Models;
namespace WebApplication5.Entities

{
    public class UserContext : DbContext
    {
        

        public UserContext(DbContextOptions<UserContext> options)  : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

       

    }
}
