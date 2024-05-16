using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Data; 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    // DbSet properties to represent tables in the database
    
    public DbSet<PostsEntity> Posts { get; set; }

    // You can add more DbSets for other entity models
}
