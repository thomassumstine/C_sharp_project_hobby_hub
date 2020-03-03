using Microsoft.EntityFrameworkCore;

namespace HobbyHub.Models
{
    public class HobbyHubContext : DbContext
    {
        public HobbyHubContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> Hobby {get;set;}

        public DbSet<AddedToPersonalHobbies> AddedToPersonalHobbies {get;set;}
    }
}